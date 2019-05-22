using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkShop : MonoBehaviour {

    //이밴트와 상점창
    public GameObject eventPaint_obj, eventPet_obj, foodShop_obj, inShop_obj, basicShop_obj;
    public GameObject blackClose_obj;
    public int shopNum;
    public string str;
    public Text coldRain_txt, hotRain_txt;
    //거리의화가
    public int[] eventPaint_i;
    public GameObject[] eventPaintImg_obj;
    public GameObject buyPaintYN_obj;
    public Sprite[] moviePaint_spr, specialPaint_spr, storyPaint_spr;
    public int special_i,movie_i,story_i,paint_i;
    public string paint_str;
    //야시장
    public GameObject foodBuy_obj;
    public int point_i;
    public Color colorP;
    public GameObject needToast_obj;
    public Text clover_txt;
    //공원상점
    public GameObject shopReform_obj, shopIng_obj;
    public GameObject shopNY_obj,met_obj,met2_obj,icebox_obj,shelf_obj;
    int c_i;
    int p_i;
    //소리
    public GameObject audio_obj;
    // Use this for initialization
    void Start () {
        colorP = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
        //paintImg();
    }

    public void allClose()
    {
        eventPaint_obj.SetActive(false);
        eventPet_obj.SetActive(false);
        foodShop_obj.SetActive(false);
        inShop_obj.SetActive(false);
        basicShop_obj.SetActive(false);
        blackClose_obj.SetActive(false);
    }

    void OpenClose()
    {
        blackClose_obj.SetActive(true);
    }

    /// <summary>
    /// 거리의화가
    /// </summary>
    public void OpenPaintShop()
    {
        //paintImg();
        OpenClose();
        eventPaint_obj.SetActive(true);
    }
    //그림 랜덤 바꿔주기
    void paintImg()
    {
        //기본값이9이고 9일경우 다시랜덤을돌린다
        //아직수집못한것만 값을 넣어준다

        //명화
        special_i = 9;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("paintp"+i, 0) == 0)
            {
                eventPaint_i[i] = i;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (special_i == 9)
            {
                special_i = eventPaint_i[Random.Range(i, 4)];
            }
        }
        if (special_i != 9)
        {
            eventPaintImg_obj[0].GetComponent<Image>().sprite = specialPaint_spr[special_i];
        }

        //영화
        for (int i = 0; i < 4; i++)
        {
            eventPaint_i[i] = 9;
            if (PlayerPrefs.GetInt("paintm" + i, 0) == 0)
            {
                eventPaint_i[i] = i;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (movie_i == 9)
            {
                movie_i = eventPaint_i[Random.Range(i, 4)];
            }
        }
        if (movie_i != 9)
        {
            eventPaintImg_obj[0].GetComponent<Image>().sprite = specialPaint_spr[movie_i];
        }

        //스토리
        //eventPaint_i[2] = Random.Range(0, 4);
        //eventPaintImg_obj[2].GetComponent<Image>().sprite = storyPaint_spr[eventPaint_i[2]];
    }
    public void buyMovie()
    {
        paint_str = "m";
        paint_i = movie_i;
        if (paint_i != 9)
        {
            buyPaintYN_obj.SetActive(true);
        }
    }
    public void buySpecial()
    {
        paint_str = "p";
        paint_i = special_i;
        if (paint_i != 9)
        {
            buyPaintYN_obj.SetActive(true);
        }
    }
    public void buyStory()
    {
        paint_str = "s";
        paint_i = story_i;
        if (paint_i != 9)
        {
            buyPaintYN_obj.SetActive(true);
        }
    }

    public void buyPaintY()
    {
        p_i = PlayerPrefs.GetInt(str + "h", 0);
        c_i = PlayerPrefs.GetInt(str + "c", 0);
        if (p_i >= 6 && c_i >= 10)
        {
            p_i = p_i - 6;
            c_i = c_i - 6;
            PlayerPrefs.SetInt(str + "h", p_i);
            PlayerPrefs.SetInt(str + "c", c_i);
            PlayerPrefs.SetInt("paint" + paint_str + paint_i, 1);
            PlayerPrefs.Save();
            buyPaintYN_obj.SetActive(false);
        }
        else
        {
            needMoney();
        }
    }
    public void buyPaintN()
    {
        buyPaintYN_obj.SetActive(false);
    }


    public void OpenPetShop()
    {
        OpenClose();
        eventPet_obj.SetActive(true);
    }
    public void OpenFoodShop()
    {
        clover_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
        OpenClose();
        foodShop_obj.SetActive(true);
    }
    public void OpenInShop()
    {
        inShop_obj.SetActive(true);
    }
    public void OpenBasicShop()
    {
        shopLoad();
        OpenClose();
        basicShop_obj.SetActive(true);
    }

    void shopLoad()
    {
        coldRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
        hotRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
        //아직못사는 물건에 테이프
        if (PlayerPrefs.GetInt("mat1lv", 0) < 4)
        {
            met_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("mat2lv", 0) < 4)
        {
            met2_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("shelflv", 0) < 3)
        {
            shelf_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("iceboxlv", 0) < 1)
        {
            icebox_obj.SetActive(true);
        }
    }


    //상점
    public void BuyShop()
    {
        shopNY_obj.SetActive(true);
    }
    public void BuyShopY()
    {
        p_i = PlayerPrefs.GetInt(str + "h", 0);
        c_i = PlayerPrefs.GetInt(str + "c", 0);
        //도어,부엌,선반,아이스박스0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 6&& c_i>=10)
                {
                    p_i = p_i - 6;
                    c_i = c_i - 6;
                    PlayerPrefs.GetInt("mat1lv", 5);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 6 && c_i >= 10)
                {
                    p_i = p_i - 6;
                    c_i = c_i - 6;
                    PlayerPrefs.GetInt("mat2lv", 5);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 6 && c_i >= 10)
                {
                    p_i = p_i - 6;
                    c_i = c_i - 6;
                    PlayerPrefs.GetInt("shelflv", 4);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 6 && c_i >= 10)
                {
                    p_i = p_i - 6;
                    c_i = c_i - 6;
                    PlayerPrefs.GetInt("iceboxlv", 2);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
        }
        shopNY_obj.SetActive(false);
    }
    void shopOk()
    {
        PlayerPrefs.SetInt(str + "h", p_i);
        PlayerPrefs.SetInt(str + "c", c_i);
    }
    public void OpenRe()
    {
        shopReform_obj.SetActive(true);
        shopIng_obj.SetActive(false);
    }

    public void OpenIn()
    {
        shopReform_obj.SetActive(false);
        shopIng_obj.SetActive(true);
    }

    public void buyShopN()
    {
        shopNY_obj.SetActive(false);
    }


    //야시장
    public void BuyFoodShop()
    {
        foodBuy_obj.SetActive(true);
    }
    public void BuyFoodShopY()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int p_i;
        p_i = PlayerPrefs.GetInt(str1 + "cv", 0);
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        //붕어빵,핫도그,닭강정,문어빵0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 6)
                {
                    p_i = p_i - 6;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 3;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 7)
                {
                    p_i = p_i - 7;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 4;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 7)
                {
                    p_i = p_i - 7;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 4;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 7)
                {
                    p_i = p_i - 7;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 4;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
        }
        foodBuy_obj.SetActive(false);
    }

    void BuyFoodOk()
    {
        PlayerPrefs.SetInt("lovepoint", point_i);
        allClose();
        audio_obj.GetComponent<SoundEvt>().foodSound();
        PlayerPrefs.Save();
    }


    public void BuyFoodShopN()
    {
        foodBuy_obj.SetActive(false);
    }

    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorP.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorP;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorP.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorP;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }


    /// <summary>
    /// 상점넘버
    /// </summary>
    #region
    public void num0()
    {
        shopNum = 0;
    }
    public void num1()
    {
        shopNum = 1;
    }
    public void num2()
    {
        shopNum = 2;
    }
    public void num3()
    {
        shopNum = 3;
    }
    public void num4()
    {
        shopNum = 4;
    }
    public void num5()
    {
        shopNum = 5;
    }
    public void num6()
    {
        shopNum = 6;
    }
    #endregion
}

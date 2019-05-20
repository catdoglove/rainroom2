using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkShop : MonoBehaviour {

    //이밴트와 상점창
    public GameObject eventPaint_obj, eventPet_obj, foodShop_obj, inShop_obj, basicShop_obj;
    public GameObject blackClose_obj;
    public int shopNum;
    //거리의화가
    public int[] eventPaint_i;
    public GameObject[] eventPaintImg_obj;
    public GameObject allPaint_obj;
    public Sprite[] moviePaint_spr, specialPaint_spr, storyPaint_spr;
    //야시장
    public GameObject foodBuy_obj;
    public int point_i;
    public Color colorP;
    public GameObject needToast_obj;
    //공원상점
    public GameObject shopReform_obj, shopIng_obj;
    public GameObject shopNY_obj,met_obj,met2_obj,icebox_obj,shelf_obj;
    //소리
    public GameObject audio_obj;
    // Use this for initialization
    void Start () {
        colorP = new Color(1f, 1f, 1f);
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
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.GetInt("paint",0);
            eventPaint_i[i] = Random.Range(0, 3);
            switch (i)
            {
                case 0:
                    eventPaintImg_obj[0].GetComponent<Image>().sprite = moviePaint_spr[eventPaint_i[0]];
                    break;
                case 1:
                    eventPaintImg_obj[1].GetComponent<Image>().sprite = specialPaint_spr[eventPaint_i[1]];
                    break;
                case 2:
                    eventPaintImg_obj[2].GetComponent<Image>().sprite = storyPaint_spr[eventPaint_i[2]];
                    break;
            }
        }
    }
    public void OpenPetShop()
    {
        OpenClose();
        eventPet_obj.SetActive(true);
    }
    public void OpenFoodShop()
    {
        OpenClose();
        foodShop_obj.SetActive(true);
    }
    public void OpenInShop()
    {
        inShop_obj.SetActive(true);
    }
    public void OpenBasicShop()
    {
        OpenClose();
        basicShop_obj.SetActive(true);
    }


    //상점
    public void BuyShopY()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int p_i;
        p_i = PlayerPrefs.GetInt(str1 + "cv", 0);
        //붕어빵,핫도그,닭강정,문어빵0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 6)
                {
                    p_i = p_i - 6;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
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

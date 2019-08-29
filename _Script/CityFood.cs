using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityFood : MonoBehaviour {

    public GameObject truckWindow_obj, truckWindowYN_obj;

    public Sprite[] truck_spr;
    public GameObject truckImg_obj,truckToast_obj;
    public GameObject[] truckSoldout_obj;

    public GameObject foodWindow_obj;
    public GameObject foodBuy_obj, selectFood_obj, foodIlust_obj;
    public Sprite[] selectFood_spr;
    public int point_i;
    public Color color, colorT;
    public GameObject needToast_obj;
    public Text dia_txt;
    public int shopNum,p_i,truckH_i,truckC_i;
    public string str;

    public Text coldRain_txt, hotRain_txt;
    public int coldRain_i, hotRain_i;
    public int[] TruckH_i, TruckC_i,FoodD_i;
    int ct = 0;

    //외물물건
    public GameObject putToast_obj;

    //소리
    public GameObject audio_obj;

    // Use this for initialization
    void Start ()
    {
        price();
        color = new Color(1f, 1f, 1f);
        colorT = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");

    }

    void truckCheck()
    {
        if (PlayerPrefs.GetInt("carot", 0)==1)
        {
            truckSoldout_obj[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("cucumber", 0) == 1)
        {
            truckSoldout_obj[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("paprika", 0) == 1)
        {
            truckSoldout_obj[2].SetActive(true);
        }
        
    }


    
    public void BuyTruckShopYN()
    {
        truckImg_obj.GetComponent<Image>().sprite = truck_spr[shopNum];
        truckWindowYN_obj.SetActive(true);
    }
    public void BuyTruckShopY()
    {
        str = PlayerPrefs.GetString("code", "");

        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);

        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        ct = 0;
        //당근 오이 파프리카
        BuyTruckOk();
        if (ct == 1)
        {
            switch (shopNum)
            {
                case 0:
                    if (ct == 1)
                    {
                        PlayerPrefs.SetInt("carot", 1);
                    }
                    break;
                case 1:
                    if (ct == 1)
                    {
                        PlayerPrefs.SetInt("cucumber", 1);

                    }
                    break;
                case 2:
                    if (ct == 1)
                    {
                        PlayerPrefs.SetInt("paprika", 1);
                    }
                    break;
            }
            truckCheck();
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
        truckWindowYN_obj.SetActive(false);
    }
    void BuyTruckOk()
    {

        if (coldRain_i >= TruckC_i[shopNum] && hotRain_i >= TruckH_i[shopNum])
        {
            coldRain_i = coldRain_i - TruckC_i[shopNum];
            hotRain_i = hotRain_i - TruckH_i[shopNum];

            PlayerPrefs.SetInt(str + "c", coldRain_i);
            PlayerPrefs.SetInt(str + "h", hotRain_i);

            coldRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hotRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);


            PlayerPrefs.Save();
            ct = 1;
        }
        else
        {
            ct = 0;
            needMoney();
        }
    }

    void price()
    {
        //당근
        TruckC_i[0] = 1200;
        TruckH_i[0] = 100;
        //오이
        TruckC_i[1] = 1100;
        TruckH_i[1] = 110;
        //파프리카
        TruckC_i[2] = 1500;
        TruckH_i[2] = 170;

        //커피
        FoodD_i[0]=7;
        //에이드
        FoodD_i[1] = 8;
        //토스트
        FoodD_i[2] = 11;
        //닭꼬치
        FoodD_i[3] = 13;
    }

   

    public void BuyTruckShopN()
    {
        truckWindowYN_obj.SetActive(false);
    }


    public void OpenActFoodShop()
    {
        if (foodWindow_obj.activeSelf == true)
        {
            foodWindow_obj.SetActive(false);
        }
        else
        {
            foodWindow_obj.SetActive(true);
            dia_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);
        }
    }
    public void OpenActTruckShop()
    {
        if (truckWindow_obj.activeSelf == true)
        {
            truckWindow_obj.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("iceboxlv", 0) >= 3)
            {
                truckCheck();
                truckWindow_obj.SetActive(true);
            }
            else
            {
                StopCoroutine("toastTruckFadeOut");
                StartCoroutine("toastTruckFadeOut");
                audio_obj.GetComponent<SoundEvt>().cancleSound();
            }
            coldRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hotRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
        }
    }
    public void BuyFoodShop()
    {
        foodBuy_obj.SetActive(true);
        selectFood_obj.GetComponent<Image>().sprite = selectFood_spr[shopNum];
    }
    public void BuyFoodShopY()
    {
        str = PlayerPrefs.GetString("code", "");
        p_i = PlayerPrefs.GetInt(str + "dm", 0);
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        //커피에이드 토스트 닭꼬치
        switch (shopNum)
        {
            case 0:
                if (p_i >= FoodD_i[shopNum])
                {
                    p_i = p_i - FoodD_i[shopNum];
                    PlayerPrefs.SetInt(str + "dm", p_i);
                    point_i = point_i + 7;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= FoodD_i[shopNum])
                {
                    p_i = p_i - FoodD_i[shopNum];
                    PlayerPrefs.SetInt(str + "dm", p_i);
                    point_i = point_i + 10;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= FoodD_i[shopNum])
                {
                    p_i = p_i - FoodD_i[shopNum];
                    PlayerPrefs.SetInt(str + "dm", p_i);
                    point_i = point_i + 11;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= FoodD_i[shopNum])
                {
                    p_i = p_i - FoodD_i[shopNum];
                    PlayerPrefs.SetInt(str + "dm", p_i);
                    point_i = point_i + 15;
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
        feed();
        dia_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);
        PlayerPrefs.SetInt("lovepoint", point_i);
        allClose();
        foodIlust_obj.SetActive(true);
        foodBuy_obj.SetActive(false);
        foodWindow_obj.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().foodSound();
        PlayerPrefs.Save();
    }
    public void CloseFoodIlust()
    {
        foodIlust_obj.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().buttonSound();
    }
    public void BuyFoodShopN()
    {
        foodBuy_obj.SetActive(false);
    }


    //20번 했을때
    void feed()
    {
        if (PlayerPrefs.GetInt("cffin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("cfcount", 0);
            if (c >= 19)
            {
                PlayerPrefs.SetInt("cffin", 1);
                PlayerPrefs.SetInt("setoutgoods", 4);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("cfcount", c);
            }
        }
    }
    public void ClosePutToast()
    {
        putToast_obj.SetActive(false);
    }



    public void allClose()
    {
        foodBuy_obj.SetActive(false);
        foodIlust_obj.SetActive(false);
        foodWindow_obj.SetActive(false);
        truckWindow_obj.SetActive(false);
        truckWindowYN_obj.SetActive(false);
    }



    //돈부족
    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }




    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = color;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }

    //트럭페이드아웃
    IEnumerator toastTruckFadeOut()
    {
        colorT.a = Mathf.Lerp(0f, 1f, 1f);
        truckToast_obj.GetComponent<Image>().color = colorT;
        truckToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorT.a = Mathf.Lerp(0f, 1f, i);
            truckToast_obj.GetComponent<Image>().color = colorT;
            yield return null;
        }
        truckToast_obj.SetActive(false);
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
    void num5()
    {
        shopNum = 5;
    }
    #endregion
}

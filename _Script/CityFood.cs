using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityFood : MonoBehaviour {

    public GameObject truckWindow_obj, truckWindowYN_obj;
    public GameObject foodWindow_obj;
    public GameObject foodBuy_obj, selectFood_obj, foodIlust_obj;
    public Sprite[] selectFood_spr;
    public int point_i;
    public Color color;
    public GameObject needToast_obj;
    public Text clover_txt;
    public int shopNum,p_i;
    public string str;

    //소리
    public GameObject audio_obj;

    // Use this for initialization
    void Start ()
    {
        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");

    }

    public void OpenTruckShop()
    {
        truckWindow_obj.SetActive(true);
    }
    public void BuyTruckShopYN()
    {
        truckWindowYN_obj.SetActive(true);
    }
    public void BuyTruckShopY()
    {
        truckWindowYN_obj.SetActive(true);
    }
    public void BuyTruckShopN()
    {
        truckWindowYN_obj.SetActive(true);
    }


    public void BuyFoodShop()
    {
        foodBuy_obj.SetActive(true);
        selectFood_obj.GetComponent<Image>().sprite = selectFood_spr[shopNum];
    }
    public void BuyFoodShopY()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        p_i = PlayerPrefs.GetInt(str1 + "dm", 0);
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        //커피에이드 토스트 닭꼬치
        switch (shopNum)
        {
            case 0:
                if (p_i >= 6)
                {
                    p_i = p_i - 6;
                    PlayerPrefs.SetInt(str1 + "dm", p_i);
                    point_i = point_i + 3;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 8)
                {
                    p_i = p_i - 8;
                    PlayerPrefs.SetInt(str1 + "dm", p_i);
                    point_i = point_i + 4;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 12)
                {
                    p_i = p_i - 12;
                    PlayerPrefs.SetInt(str1 + "dm", p_i);
                    point_i = point_i + 4;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 10)
                {
                    p_i = p_i - 10;
                    PlayerPrefs.SetInt(str1 + "dm", p_i);
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
        clover_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);
        PlayerPrefs.SetInt("lovepoint", point_i);
        allClose();
        foodIlust_obj.SetActive(true);
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



    public void allClose()
    {
        foodBuy_obj.SetActive(false);
        foodIlust_obj.SetActive(false);
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


}

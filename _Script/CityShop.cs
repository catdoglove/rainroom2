using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour {
    public GameObject reformShopWin_obj,paint_obj, fabric_obj, paintBtn_obj, fabricBtn_obj, fabricBuy_obj, paintBuy_obj;
    public GameObject interiorWin_obj,interiorBuy_obj;
    public GameObject[] interiorTape_obj, paintTape_obj, fabricTape_obj;
    public Sprite[] paintBtn_spr, fabricBtn_spr;

    public GameObject audio_obj, needToast_obj;
    Color colorP;

    public int itemIndex_i;

    // Use this for initialization
    void Start () {
		
	}
    //리폼상점창 열기
    public void OpenActReform()
    {
        if(reformShopWin_obj.activeSelf == true)
        {
            reformShopWin_obj.SetActive(false);
        }
        else
        {
            reformShopWin_obj.SetActive(true);
            //SetPaint();
            //SetFabric();
        }
    }

    //페인트샵
    public void SwitchPaint()
    {
        paintBtn_obj.GetComponent<Image>().sprite = paintBtn_spr[1];
        fabricBtn_obj.GetComponent<Image>().sprite = fabricBtn_spr[0];
        paint_obj.SetActive(true);
        fabric_obj.SetActive(false);
    }

    //원단샵
    public void SwitchFabric()
    {
        paintBtn_obj.GetComponent<Image>().sprite = paintBtn_spr[0];
        fabricBtn_obj.GetComponent<Image>().sprite = fabricBtn_spr[1];
        paint_obj.SetActive(false);
        fabric_obj.SetActive(true);
    }
    
    //인테리어 열기
    public void OpenActInterior()
    {
        if (interiorWin_obj.activeSelf == true)
        {
            interiorWin_obj.SetActive(false);
        }
        else
        {
            //물건 업그래이드 단계확인 부족하면 테이프로 가리기
            //침대 5레벨 일때 가능
            if (PlayerPrefs.GetInt("bedlv", 0) < 5)
            {
                interiorTape_obj[0].SetActive(true);
            }
            //냉장고 2레벨일때 가능
            if (PlayerPrefs.GetInt("iceboxlv", 0) < 2)
            {
                interiorTape_obj[1].SetActive(true);
            }
            //테이블 3레벨일때 가능
            if (PlayerPrefs.GetInt("desklv", 0) < 3)
            {
                interiorTape_obj[2].SetActive(true);
            }
            //전구? 4레벨일때 가능
            if (PlayerPrefs.GetInt("lightlv", 0) < 4)
            {
                interiorTape_obj[3].SetActive(true);
            }



            interiorWin_obj.SetActive(true);
        }
    }

    public void BuyInterior()
    {
        if (interiorBuy_obj.activeSelf == true)
        {
            interiorBuy_obj.SetActive(false);
        }
        else
        {
            interiorBuy_obj.SetActive(true);
        }
    }
    public void BuyPaint()
    {
        if (paintBuy_obj.activeSelf == true)
        {
            paintBuy_obj.SetActive(false);
        }
        else
        {
            paintBuy_obj.SetActive(true);
        }
    }
    public void BuyFabric()
    {

        if (fabricBuy_obj.activeSelf == true)
        {
            fabricBuy_obj.SetActive(false);
        }
        else
        {
            fabricBuy_obj.SetActive(true);
        }
    }

    void SetPaint()
    {
        //물건 업그래이드 단계확인 부족하면 테이프로 가리기
        //장식장 4레벨 일때 가능
        if (PlayerPrefs.GetInt("drawerlv", 0) < 4)
        {
            paintTape_obj[0].SetActive(true);
        }
        //선반 2레벨일때 가능
        if (PlayerPrefs.GetInt("shelflv", 0) < 2)
        {
            paintTape_obj[1].SetActive(true);
        }
        //테이블 6레벨일때 가능
        if (PlayerPrefs.GetInt("desklv", 0) < 3)
        {
            paintTape_obj[2].SetActive(true);
        }
        //서랍장 6레벨일때 가능
        if (PlayerPrefs.GetInt("cabinetlv", 0) < 6)
        {
            paintTape_obj[2].SetActive(true);
        }
        //책장 6레벨일때 가능
        if (PlayerPrefs.GetInt("booklv", 0) < 3)
        {
            paintTape_obj[2].SetActive(true);
        }
        //전구? 4?5레벨일때 가능
        if (PlayerPrefs.GetInt("lightlv", 0) < 4)
        {
            paintTape_obj[3].SetActive(true);
        }
    }

    void SetFabric()
    {
        //물건 업그래이드 단계확인 부족하면 테이프로 가리기
        //부엌매트 5레벨 일때 가능
        if (PlayerPrefs.GetInt("mat2lv", 0) < 5)
        {
            fabricTape_obj[0].SetActive(true);
        }
        //도어매트 2레벨일때 가능
        if (PlayerPrefs.GetInt("mat1lv", 0) < 2)
        {
            fabricTape_obj[1].SetActive(true);
        }
        //창문 8레벨일때 가능
        if (PlayerPrefs.GetInt("windowlv", 0) < 8)
        {
            fabricTape_obj[2].SetActive(true);
        }
        //러그 3레벨일때 가능
        if (PlayerPrefs.GetInt("ruglv", 0) < 3)
        {
            fabricTape_obj[2].SetActive(true);
        }
        //침대 6레벨일때 가능
        if (PlayerPrefs.GetInt("bedlv", 0) < 3)
        {
            fabricTape_obj[2].SetActive(true);
        }
        //벽지 3레벨일때 가능
        if (PlayerPrefs.GetInt("walllv", 0) < 3)
        {
            fabricTape_obj[3].SetActive(true);
        }
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



    public void setIndex0()
    {
        itemIndex_i = 0;
    }
    public void setIndex1()
    {
        itemIndex_i = 1;
    }
    public void setIndex2()
    {
        itemIndex_i = 2;
    }
    public void setIndex3()
    {
        itemIndex_i = 3;
    }
    public void setIndex4()
    {
        itemIndex_i = 4;
    }
    public void setIndex5()
    {
        itemIndex_i = 5;
    }
    public void setIndex6()
    {
        itemIndex_i = 6;
    }
    public void setIndex7()
    {
        itemIndex_i = 7;
    }
    public void setIndex8()
    {
        itemIndex_i = 8;
    }
}

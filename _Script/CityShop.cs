using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour {
    public GameObject reformShopWin_obj,paint_obj, fabric_obj, paintBtn_obj, fabricBtn_obj;
    public GameObject interiorWin_obj;
    public GameObject[] interiorTape_obj;
    public Sprite[] paintBtn_spr, fabricBtn_spr;

    public GameObject audio_obj, needToast_obj;
    Color colorP;

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
            //테이블 레벨일때 가능
            if (PlayerPrefs.GetInt("desklv", 0) < 3)
            {
                interiorTape_obj[2].SetActive(true);
            }
            //전구? 레벨일때 가능
            if (PlayerPrefs.GetInt("lightlv", 0) < 3)
            {
                interiorTape_obj[3].SetActive(true);
            }



            interiorWin_obj.SetActive(true);
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
}

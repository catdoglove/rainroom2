using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteShop : MonoBehaviour {

    public GameObject noteShopWindow_obj, noteShopBuyYN_obj, noteShopHelp_obj;
    public GameObject noteShopSign_obj, noteToast_obj;
    Color color;
    string str;

    public int priceShop_i;

    public int noteShopNum_i;

    // Use this for initialization
    void Start () {

        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
    }
	
    //문방구 열기
    public void ActNoteShop()
    {
        if (noteShopWindow_obj.activeSelf == true)
        {
            noteShopWindow_obj.SetActive(false);
        }
        else
        {
            noteShopWindow_obj.SetActive(true);
        }
    }
    //노트
    public void ButNote()
    {
        noteShopNum_i = 0;
        noteShopBuyYN_obj.SetActive(true);
    }
    //지우개
    public void ButEraser()
    {
        noteShopNum_i = 1;
        noteShopBuyYN_obj.SetActive(true);
    }

    //연필
    public void ButPencil()
    {
        noteShopNum_i = 2;
        noteShopBuyYN_obj.SetActive(true);
    }


    //도움말 열기
    public void ShowHelp()
    {
        noteShopHelp_obj.SetActive(true);
    }

    //도움말 닫기
    public void CloseHelp()
    {
        noteShopHelp_obj.SetActive(false);
    }

    //산다
    public void NoteShopBuyY()
    {
        int spade = PlayerPrefs.GetInt(str + "sd", 0);
        priceShop_i = 0;
        if (spade >= priceShop_i)
        {

        }
        noteShopBuyYN_obj.SetActive(false);
    }
    //안산다
    public void NoteShopBuyN()
    {
        noteShopBuyYN_obj.SetActive(false);
    }


    //스페이드없다
    IEnumerator toastPencleImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        noteToast_obj.GetComponent<Image>().color = color;
        noteToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            noteToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        noteToast_obj.SetActive(false);
    }
}

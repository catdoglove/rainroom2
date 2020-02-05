using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteShop : MonoBehaviour {

    public GameObject noteShopWindow_obj, noteShopBuyYN_obj, noteShopHelp_obj;
    public GameObject noteShopSign_obj;
    Color color;
    string str;

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
        noteShopBuyYN_obj.SetActive(false);
    }
    //안산다
    public void NoteShopBuyN()
    {
        noteShopBuyYN_obj.SetActive(false);
    }

}

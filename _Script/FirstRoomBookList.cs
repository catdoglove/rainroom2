using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomBookList : MonoBehaviour {


    //책장내용
    public GameObject itemListText_obj, itemListText_BG, itemListText_txt, itemListText_left, itemListText_book, itemListText_light, itemListText_seed, itemListText_wall, itemListText_window;
    public Sprite[] itemListBG, itemListTxt;




    //책장내용 열기

    public void openItemList()
    {
        trueList();
        itemListText_BG.GetComponent<Image>().sprite = itemListBG[0];
        itemListText_obj.SetActive(true);
    }


    public void closeItemList()
    {
        itemListText_left.SetActive(false);
        itemListText_txt.SetActive(false);
        itemListText_obj.SetActive(false);
    }

    //책관련
    public void showBookList()
    {
        ShowList();
        itemListText_book.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[0];
    }

    public void showBookA4()
    {
        itemListText_left.SetActive(true);
    }

    
    //창문관련
    public void showWindowList()
    {
        ShowList();
        itemListText_window.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[4];
    }
    public void showSeedList()
    {
        ShowList();
        itemListText_seed.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[2];
    }
    public void showWallList()
    {
        ShowList();
        itemListText_wall.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[3];
    }
    public void showLightList()
    {
        ShowList();
        itemListText_light.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[1];
    }

    void trueList()
    {
        itemListText_book.SetActive(true);
        itemListText_light.SetActive(true);
        itemListText_seed.SetActive(true);
        itemListText_wall.SetActive(true);
        itemListText_window.SetActive(true);
    }

    void ShowList()
    {
        trueList();
        itemListText_left.SetActive(false);
        itemListText_BG.GetComponent<Image>().sprite = itemListBG[1];
        itemListText_txt.SetActive(true);
    }



}

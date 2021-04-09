using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomBookList : MonoBehaviour {


    List<Dictionary<string, object>> data, data_book, data_light, data_seed, data_wall, data_window; //csv파일
    int itemAllArr; //총 줄수 
    int itemNowArr=2; //현재 줄

    //아이템 관련- 299책,  304벽지,  376전등, 472 창문,  299씨앗
    int itemck; // 등급
    string itTxt;
    int windowLv, seedLv, lightLv, wallLv;

    //책장내용
    public GameObject itemListText_obj, itemListText_BG, itemListText_txt, itemListText_left, itemListText_book, itemListText_light, itemListText_seed, itemListText_wall, itemListText_window;
    public GameObject bookArea, lightArea, seedArea, wallArea, windowArea, nextPage;
    public Sprite[] itemListBG, itemListTxt, itemLeftBG;
    public Text itemTxt;
    int pageNum;

    //이거 unity 세팅 다시하기..

    void Start()
    {
        data_book = CSVReader.Read("Talk/talk_book");
        data_light = CSVReader.Read("Talk/talk_light");
        data_seed = CSVReader.Read("Talk/talk_seed");
        data_wall = CSVReader.Read("Talk/talk_wall");
        data_window = CSVReader.Read("Talk/talk_window");
               
    }

    //책장내용 열기

    public void openItemList()
    {

        windowLv = PlayerPrefs.GetInt("windowlv", 0);
        seedLv = PlayerPrefs.GetInt("seedlv", 0) - 1;
        lightLv = PlayerPrefs.GetInt("lightlv", 0);
        wallLv = PlayerPrefs.GetInt("walllv", 0);

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
        //아이템 관련- 299책,  304벽지,  376전등, 472 창문,  289씨앗
        itemck = 299;
        ShowList();
        bookArea.SetActive(true);
        itemListText_book.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[0];
    }

    public void showBook0()
    {
        reloadLeft();
        bookGungBok(0);
    }

    public void showBook1()
    {
        reloadLeft();
        bookGungBok(1);
    }

    public void showBook2()
    {
        reloadLeft();
        bookGungBok(2);
    }

    public void showBook3()
    {
        reloadLeft();
        bookGungBok(3);
    }
    public void showBook4()
    {
        reloadLeft();
        bookGungBok(4);
        pageNum = 4;
        showLeftBG();
    }
    public void showBook5()
    {
        reloadLeft();
        bookGungBok(5);
        pageNum = 5;
        showLeftBG();
    }
    public void showBook6()
    {
        reloadLeft();
        bookGungBok(6);
        pageNum = 6;
        showLeftBG();
    }
    public void showBook7()
    {
        reloadLeft();
        bookGungBok(7);
        pageNum = 7;
        showLeftBG();
    }
    public void showBook8()
    {
        reloadLeft();
        bookGungBok(8);
        pageNum = 8;
        showLeftBG();
    }
    public void showBook9()
    {
        reloadLeft();
        bookGungBok(9);
        pageNum = 9;
        showLeftBG();
    }
    public void showBook10()
    {
        reloadLeft();
        bookGungBok(10);
        pageNum = 10;
        showLeftBG();
    }
    public void showBook11()
    {
        reloadLeft();
        bookGungBok(11);
        pageNum = 11;
        showLeftBG();
    }
    public void showBook12()
    {
        reloadLeft();
        bookGungBok(12);
        pageNum = 12;
        showLeftBG();
    }
    public void showBook13()
    {
        reloadLeft();
        bookGungBok(13);
        pageNum = 13;
        showLeftBG();
    }


    void bookGungBok(int num)
    {
        string itTxt = "" + data_book[itemNowArr]["book" + num];
        itTxt = itTxt.Replace("/", "");        
        itemTxt.text = itTxt;
        

    }
    


    //창문관련
    public void showWindowList()
    {
        itemck = 472;
        ShowList();
        windowArea.SetActive(true);
        itemListText_window.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[4];
    }

    public void showWindow0()
    {
        reloadLeft();
        windwoGungBok(0);
    }
    public void showWindow1()
    {
        reloadLeft();
        windwoGungBok(1);
    }
    public void showWindow2()
    {
        reloadLeft();
        windwoGungBok(2);
    }
    public void showWindow3()
    {
        reloadLeft();
        windwoGungBok(3);
        pageNum = 3;
        showLeftBG();
    }
    public void showWindow4()
    {
        reloadLeft();
        windwoGungBok(4);
        pageNum = 4;
        showLeftBG();
    }
    public void showWindow5()
    {
        reloadLeft();
        windwoGungBok(5);
        pageNum = 5;
        showLeftBG();
    }
    public void showWindow6()
    {
        reloadLeft();
        windwoGungBok(6);
        pageNum = 6;
        showLeftBG();
    }
    public void showWindow7()
    {
        reloadLeft();
        windwoGungBok(7);
        pageNum = 7;
        showLeftBG();
    }
    public void showWindow8()
    {
        reloadLeft();
        windwoGungBok(8);
        pageNum = 8;
        showLeftBG();
    }

    void windwoGungBok(int i)
    {
        string itTxt = "" + data_window[itemNowArr]["window" + i];
        itTxt = itTxt.Replace("/", "");
        itemTxt.text = itTxt;
    }
    

    //씨앗 관련
    public void showSeedList()
    {
        itemck = 289;
        ShowList();
        seedArea.SetActive(true);
        itemListText_seed.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[2];
    }

    public void showSeed0()
    {
        reloadLeft();
        seedGungBok(0);
    }

    public void showSeed1()
    {
        reloadLeft();
        seedGungBok(1);
    }

    public void showSeed2()
    {
        reloadLeft();
        seedGungBok(2);
    }

    public void showSeed3()
    {
        reloadLeft();
        seedGungBok(3);
    }
    public void showSeed4()
    {
        reloadLeft();
        seedGungBok(4);
        pageNum = 4;
        showLeftBG();
    }
    public void showSeed5()
    {
        reloadLeft();
        seedGungBok(5);
        pageNum = 5;
        showLeftBG();
    }
    public void showSeed6()
    {
        reloadLeft();
        seedGungBok(6);
        pageNum = 6;
        showLeftBG();
    }
    public void showSeed7()
    {
        reloadLeft();
        seedGungBok(7);
        pageNum = 7;
        showLeftBG();
    }
    public void showSeed8()
    {
        reloadLeft();
        seedGungBok(8);
        pageNum = 8;
        showLeftBG();
    }
    public void showSeed9()
    {
        reloadLeft();
        seedGungBok(9);
        pageNum = 9;
        showLeftBG();
    }

    void seedGungBok(int i)
    {
        string itTxt = "" + data_seed[itemNowArr]["seed" + i];
        itTxt = itTxt.Replace("/", "");        
        itemTxt.text = itTxt;
    }



    //벽지관련
    public void showWallList()
    {
        itemck = 304;
        ShowList();
        wallArea.SetActive(true);
        itemListText_wall.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[3];
    }

    public void showWall0()
    {
        reloadLeft();
        wallGungBok(0);
    }
    public void showWall1()
    {
        reloadLeft();
        wallGungBok(1);
    }
    public void showWall2()
    {
        reloadLeft();
        wallGungBok(2);
        pageNum = 2;
        showLeftBG();
    }
    public void showWall3()
    {
        reloadLeft();
        wallGungBok(3);
        pageNum = 3;
        showLeftBG();
    }

    void wallGungBok(int i)
    {
        string itTxt = "" + data_wall[itemNowArr]["wall" + i];
        itTxt = itTxt.Replace("/", "");
        itemTxt.text = itTxt;
    }




    //전등관련
    public void showLightList()
    {
        itemck = 376;
        ShowList();
        lightArea.SetActive(true);
        itemListText_light.SetActive(false);
        itemListText_txt.GetComponent<Image>().sprite = itemListTxt[1];
    }


    public void showLight0()
    {
        reloadLeft();
        LightGungBok(0);
    }
    public void showLight1()
    {
        reloadLeft();
        LightGungBok(1);
    }
    public void showLight2()
    {
        reloadLeft();
        LightGungBok(2);
    }
    public void showLight3()
    {
        reloadLeft();
        LightGungBok(3);
    }

    public void showLight4()
    {
        reloadLeft();
        LightGungBok(4);
        pageNum = 4;
        showLeftBG();
    }

    void LightGungBok(int i)
    {
        string itTxt = "" + data_light[itemNowArr]["light" + i];
        itTxt = itTxt.Replace("/", "");
        itemTxt.text = itTxt;
    }

    

    void trueList()
    {
        itemListText_book.SetActive(true);

        if (windowLv >= 8)
        {
            itemListText_window.SetActive(true);
        }
        else { itemListText_window.SetActive(false); }

        if (seedLv >= 9)
        {
            itemListText_seed.SetActive(true);
        }
        else { itemListText_seed.SetActive(false); }

        if (lightLv >= 4)
        {
            itemListText_light.SetActive(true);
        }
        else { itemListText_light.SetActive(false); }

        if (wallLv >= 3)
        {
            itemListText_wall.SetActive(true);
        }
        else { itemListText_wall.SetActive(false); }

    }

    void ShowList()
    {
        trueList();
        itemListText_left.SetActive(false);
        bookArea.SetActive(false);
        seedArea.SetActive(false);
        wallArea.SetActive(false);
        windowArea.SetActive(false);
        lightArea.SetActive(false);
        itemListText_BG.GetComponent<Image>().sprite = itemListBG[1];
        itemListText_txt.SetActive(true);
        itemNowArr = 0;
    }

    void reloadLeft()
    {
        itemListText_left.SetActive(true);
        itemNowArr = 0;
        nextPage.SetActive(false);
        itemListText_left.GetComponent<Image>().sprite = itemLeftBG[0];
    }

    void showLeftBG()
    {
        nextPage.SetActive(true);
        itemListText_left.GetComponent<Image>().sprite = itemLeftBG[1];
    }

    void callArr()
    {
        if (itemNowArr < itemAllArr) //대화 차례대로 보이기
        {
            itemNowArr++;
            //Debug.Log(itemNowArr);
        }
        else if (itemNowArr >= itemAllArr) //대화 줄 초기화
        {
            itemNowArr = 0;
            //Debug.Log("리셋");
        }

    }


    public void GungBok()
    {
        switch (itemck)
        {
            //아이템 관련-  책,   벽지,   전등,  창문,   씨앗
            case 299:


                if (pageNum == 12 || pageNum == 13)
                {
                    itemAllArr = 7;
                }
                else if (pageNum == 7 || pageNum == 8 || pageNum == 9 || pageNum == 10 || pageNum == 11)
                {
                    itemAllArr = 5;
                }
                else if (pageNum <= 6)
                {
                    itemAllArr = 3;
                }

                callArr();

                string itTxt = "" + data_book[itemNowArr]["book" + pageNum];
                itTxt = itTxt.Replace("/", "");
                itemTxt.text = itTxt;

                break;

            case 304:

                if (pageNum == 3)
                {
                    itemAllArr = 7;
                }
                else if (pageNum <= 2)
                {
                    itemAllArr = 3;
                }

                callArr();

                itTxt = "" + data_wall[itemNowArr]["wall" + pageNum];
                itTxt = itTxt.Replace("/", "");
                itemTxt.text = itTxt;

                break;

            case 376:

                itemAllArr = 3;

                callArr();

                itTxt = "" + data_light[itemNowArr]["light" + pageNum];
                itTxt = itTxt.Replace("/", "");
                itemTxt.text = itTxt;

                break;

            case 472:


                if (pageNum == 8)
                {
                    itemAllArr = 9;
                }
                else if (pageNum == 7 || pageNum == 6)
                {
                    itemAllArr = 5;
                }
                else if (pageNum <= 5)
                {
                    itemAllArr = 3;
                }

                callArr();

                itTxt = "" + data_window[itemNowArr]["window" + pageNum];
                itTxt = itTxt.Replace("/", "");
                itemTxt.text = itTxt;

                break;

            case 289:

                if (pageNum == 9)
                {
                    itemAllArr = 7;
                }
                else if (pageNum == 6 || pageNum == 7 || pageNum == 8)
                {
                    itemAllArr = 5;
                }
                else if (pageNum <= 5)
                {
                    itemAllArr = 3;
                }

                callArr();

                itTxt = "" + data_seed[itemNowArr]["seed" + pageNum];
                itTxt = itTxt.Replace("/", "");
                itemTxt.text = itTxt;

                break;
        }
    }



}

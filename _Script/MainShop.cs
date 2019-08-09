using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : ShopHandler {
    public GameObject GM, loadGM, GMtag, GM2;
	public Text coldRain_txt,hotRain_txt;

    public Text[] levels_txt;
    public Text[] coldPrice_txt;
    public Text[] hotPrice_txt;

    public Text[] lvNum_txt;

    public GameObject needhRain_obj,needcRain_obj;

    List<Dictionary<string, object>> data_hPrice, data_cPrice, data_itemName;

    string str;

    public GameObject buyYes_obj;
    public GameObject buyItemImg_obj;
    public Sprite[] buyItem_spr;

    //처음에박스값을10으로설정해준다 아래에서레벨을불러올때 박스값이10이면 박스에담겨있는물건이다 이물건은박스에서 꺼낼지물어본다
    //기본방에서 물건터치는박스로 막혀있다. 박스를 터치하면 상자치우기창이뜬다.
    public GameObject[] boxs_obj;


    public GameObject downBtn_obj, upBtn_obj, functionBtn_obj;
    public GameObject[] ItemListImg_obj;
    public Sprite[] upDown_spr;
    public int upDownCheck_i = 0;

    //기능성
    public GameObject[] functionTape_obj;
    public string function_txt;
    public GameObject[] functionBuyBtn_obj;
    public GameObject[] funcImgs_obj;

    public GameObject fucnYN_obj, funcImg_obj;
    public Sprite[] funcImg_spr,funcTxt_spr;

    public GameObject[] funcPrice_obj;
    //보관함
    public GameObject funcCabinet_obj;
    public GameObject[] funcBox_obj;
    public Sprite[] funcBox_spr;

    public int switch_i, waterCan_i, waterpurifier_i, reform_i, func_i;

    public string[] func_str;

    public GameObject shop_obj,close_obj,back_obj;
    //애완동물
    public GameObject petHotel_obj, petMarimo_obj, petRabbit_obj, petTutle_obj, petFish_obj;
    public Sprite[] marimo_spr, rabbit_spr, tutle_spr, goldfish_spr;
    //리폼
    public GameObject reform_obj,palette_obj;
    public GameObject[] paintcan_obj,paletteImg_obj;
    public GameObject[] matPalette_obj, mat2Palette_obj, shelfPalette_obj,lightPalette_obj, windowPalette_obj, drawerPalette_obj, bookPalette_obj, bedPalette_obj, deskPalette_obj, wallPalette_obj, rugPalette_obj, cabinetPalette_obj;
    public Sprite[] paintcan_spr,matPaint_spr, mat2Paint_spr, shelfPaint_spr;
    Color mColor;
    public GameObject[] selectMatPaint_obj, selectMat2Paint_obj, selectShelfPaint_obj, selectlight_obj, selectwindow_obj, selectdrawet_obj, selectbook_obj, selectbed_obj, selectdesk_obj, selectwall_obj, selectrug_obj, selectcabinet_obj, selectAll_obj;
    public GameObject[] reformPage_obj;

    //도어,부엌,선반,전구0~3  창문,장식장,책장,침대,테이블 4,5,6,7,8 벽지,러그,서랍장,가스렌지 9,10,11,12

    //부족하다창
    Color color;
    public GameObject needToast_obj;

    //소리
    public GameObject audio_obj;

    //도시가구이름
    public Text bedMax_txt,lightMax_txt,deskMax_txt;

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
        /*
         * 마법의 코드 자리
        */
        //초기화 코드 자리
        GM = GameObject.FindGameObjectWithTag("firstroomGM");
        GM2 = GameObject.FindGameObjectWithTag("GM2");
        loadGM =GameObject.FindGameObjectWithTag("loadGM");
        data_cPrice = CSVReader.Read("Price/f_coldrain");
        data_hPrice = CSVReader.Read("Price/f_hotrain");
        data_itemName = CSVReader.Read("Price/f_itemname");
    }

    public void ShopCoinLoad(){
        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
            loadGM = GameObject.FindGameObjectWithTag("loadGM");
            GM2 = GameObject.FindGameObjectWithTag("GM2");
            data_cPrice = CSVReader.Read("Price/f_coldrain");
            data_hPrice = CSVReader.Read("Price/f_hotrain");
            data_itemName = CSVReader.Read("Price/f_itemname");
        }
        
        if (PlayerPrefs.GetInt("unlockshop", 0) == 10)
        {
            downBtn_obj.SetActive(true);
            functionBtn_obj.SetActive(true);
        }
        else
        {
            downBtn_obj.SetActive(false);
            functionBtn_obj.SetActive(false);
        }

		str = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);
		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
        LvChange();
        OpenfunctionItem();
        //다락방
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            //박스
            if (PlayerPrefs.GetInt("bedbox", 0) == 10)
            {
                boxs_obj[1].SetActive(true);
            }
            else
            {
                boxs_obj[1].SetActive(false);
            }
            if (PlayerPrefs.GetInt("cabinetbox", 0) == 10)
            {
                boxs_obj[2].SetActive(true);
            }
            else
            {
                boxs_obj[2].SetActive(false);
            }
            if (PlayerPrefs.GetInt("deskbox", 0) == 10)
            {
                boxs_obj[3].SetActive(true);
            }
            else
            {
                boxs_obj[3].SetActive(false);
            }
            if (PlayerPrefs.GetInt("bookbox", 0) == 10)
            {
                boxs_obj[0].SetActive(true);
            }
            else
            {
                boxs_obj[0].SetActive(false);
            }
        }
            if (PlayerPrefs.GetInt("drawerbox", 0) == 10)
            {
                boxs_obj[4].SetActive(true);
            }
            else
            {
                boxs_obj[4].SetActive(false);
            }
            
    }

    public void ShopBuyYes()
    {
        if (hotRainPrice_i == 0 && coldRainPrice_i == 0){

        }
        else
        {
            //Debug.Log(itemIndex_i + "fd" + itemLevel_i);//////////////////////////////////////////////////////////////////

            if (coldRain_i >= coldRainPrice_i)
            {
                if (hotRain_i >= hotRainPrice_i)
                {
                    coldRain_i = coldRain_i - coldRainPrice_i;
                    PlayerPrefs.SetInt(str + "c", coldRain_i);
                    //Debug.Log(coldRainPrice_i);//////////////////////////////////////////////////////////////////
                    hotRain_i = hotRain_i - hotRainPrice_i;
                    PlayerPrefs.SetInt(str + "h", hotRain_i);
                    //Debug.Log(hotRainPrice_i);//////////////////////////////////////////////////////////////////
                    itemLevel_i++;
                    PlayerPrefs.SetInt(itemName_str + "lv", itemLevel_i);
                    achvcheck();
                    //이미지를바꿔주는 함수 단칸방에 있을 때에는 이미지를 바꿔주지 않는다.
                    SwitchByIndex();
                    PlayerPrefs.Save();
                    coldRain_txt.text = "" + coldRain_i;
                    hotRain_txt.text = "" + hotRain_i;
                    LvChange();
                    CloseShopBuy();
                    audio_obj.GetComponent<SoundEvt>().buttonSound();

                }
                else
                {
                    StartCoroutine("toastHotImgFadeOut");
                    needhRain_obj.SetActive(true);
                    CloseShopBuy();
                    //따듯한물부족
                    audio_obj.GetComponent<SoundEvt>().cancleSound();
                }
            }
            else
            {
                StartCoroutine("toastColdImgFadeOut");
                needcRain_obj.SetActive(true);
                CloseShopBuy();
                //빗물부족
                audio_obj.GetComponent<SoundEvt>().cancleSound();
            }
        }//endOfElse
    }

    /// <summary>
    /// 물건을 살때 이름을 불러오고 살까요창을 띄워준다
    /// </summary>
    public void ShopChageImage() {
        str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        
        itemName_str = shopItems_btn[itemIndex_i].name;
        itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
        
        hotRainPrice_i = (int)data_hPrice[itemLevel_i][itemName_str];
        coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];

        //맥스레벨일때
        if (hotRainPrice_i == 0 && coldRainPrice_i == 0)
        {
            
        }
        else
        {
            buyYes_obj.SetActive(true);
            buyItemImg_obj.GetComponent<Image>().sprite = buyItem_spr[itemIndex_i];
        }
       
    }

    public void closeRain()
    {
        needhRain_obj.SetActive(false);
        needcRain_obj.SetActive(false);
    }


    void SwitchByIndex()
    {
        //다락방
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            switch (itemIndex_i)
            {
                case 0:
                    if (PlayerPrefs.GetInt("booklv", 0) == 15)
                    {
                        GM.GetComponent<FirstRoomFunction>().bookcase_obj.SetActive(true);
                        GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].SetActive(false);
                        GM.GetComponent<FirstRoomFunction>().bookcase_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().bookcase_spr;
                    }
                    else
                    {
                        GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[itemLevel_i];
                    }
                    break;

                case 1:
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[itemLevel_i];
                    break;

                case 2:
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[itemLevel_i];
                    break;

                case 3:
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[itemLevel_i];
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[6].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[itemLevel_i];
                    break;

                case 4:
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[itemLevel_i];
                    break;

                case 5:
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall_spr[itemLevel_i];
                    GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[7].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall_spr[4 + itemLevel_i];
                    break;
                case 6:
                    
                    break;                
            }
        }
        else if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            switch (itemIndex_i)
            {
                case 5:
                    GM2.GetComponent<secondRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().wall_spr[itemLevel_i];
                    GM2.GetComponent<secondRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().wall2_spr[itemLevel_i];
                    break;
                case 6:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().light_spr[itemLevel_i];
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[13].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().light_spr[itemLevel_i];
                    break;
                case 7:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[12].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window2_spr[itemLevel_i];
                    break;
                case 8:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().mat_spr[itemLevel_i];
                    break;
                case 9:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().mat2_spr[itemLevel_i];
                    break;
                case 10:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().drawer_spr[itemLevel_i];
                    break;
                case 11:
                    GM2.GetComponent<secondRoomFunction>().secondRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().shelf_spr[itemLevel_i];
                    break;
            }
        }

    }

    /// <summary>
    /// 아이템의 레벨과 가격을 새로고침해준다.
    /// </summary>
    public void LvChange()
    {
        //다락방
        if (upDownCheck_i == 0)
        {
        }
        else if (upDownCheck_i == 1)//단칸방
        {

        }
        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
            loadGM = GameObject.FindGameObjectWithTag("loadGM");
            GM2 = GameObject.FindGameObjectWithTag("GM2");
            data_cPrice = CSVReader.Read("Price/f_coldrain");
            data_hPrice = CSVReader.Read("Price/f_hotrain");
        }
        for (int i = 0; i < 12; i++)
        {
            itemName_str = shopItems_btn[i].name;
            itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
            levels_txt[i].text = "" + data_itemName[itemLevel_i][itemName_str];
            lvNum_txt[i].text = "LV. " + itemLevel_i.ToString();
            hotRainPrice_i = (int)data_hPrice[itemLevel_i][itemName_str];
            //Debug.Log(""+ itemLevel_i+ itemName_str);
            coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];
            coldPrice_txt[i].text = coldRainPrice_i.ToString();
            hotPrice_txt[i].text = hotRainPrice_i.ToString();

            if (hotRainPrice_i == 0 && coldRainPrice_i == 0)
            {
                if (itemIndex_i == 6)
                {
                    if (PlayerPrefs.GetInt("switchshop", 0) == 0)
                    {
                        if (PlayerPrefs.GetInt("lightlv", 0) >= 4)
                        {
                            PlayerPrefs.SetInt("switchshop", 1);
                        }

                    }
                }
                itemName_str = shopItems_btn[i].name;
                itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
                levels_txt[i].text = "" + data_itemName[itemLevel_i][itemName_str];
                lvNum_txt[i].text = "LV.MAX";
                coldPrice_txt[i].text = "-";
                hotPrice_txt[i].text = "-";


            }else{ }
        }
        //도시가구 이름변경
        //deskMax_txt.text = "플라스틱 탁자 작은 책상 완전한 책상";
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 1)
        {
            bedMax_txt.text = "낡은 침대";
        }
        if(PlayerPrefs.GetInt("bedmaxlv", 0) >= 2)
        {
            bedMax_txt.text = "완전한 침대";
        }
        if (PlayerPrefs.GetInt("lightmaxlv", 0) >= 1)
        {
            lightMax_txt.text = "좋은 전등";
        }
        if (PlayerPrefs.GetInt("lightmaxlv", 0) >= 2)
        {
            lightMax_txt.text = "완전한 전등";
        }

    }

    public void CloseShopBuy()
    {
        buyYes_obj.SetActive(false);
        fucnYN_obj.SetActive(false);
    }
	
    public void DownShop()
    {
        upBtn_obj.GetComponent<Image>().sprite = upDown_spr[1];
        downBtn_obj.GetComponent<Image>().sprite = upDown_spr[2];
        functionBtn_obj.GetComponent<Image>().sprite = upDown_spr[5];
        ItemListImg_obj[1].SetActive(true);
        ItemListImg_obj[0].SetActive(false);
        ItemListImg_obj[2].SetActive(false);
        upDownCheck_i = 1;
    }

    public void Upshop()
    {
        upBtn_obj.GetComponent<Image>().sprite = upDown_spr[0];
        downBtn_obj.GetComponent<Image>().sprite = upDown_spr[3];
        functionBtn_obj.GetComponent<Image>().sprite = upDown_spr[5];
        ItemListImg_obj[1].SetActive(false);
        ItemListImg_obj[0].SetActive(true);
        ItemListImg_obj[2].SetActive(false);
        upDownCheck_i = 0;
    }

    public void functionShop()
    {
        upBtn_obj.GetComponent<Image>().sprite = upDown_spr[1];
        downBtn_obj.GetComponent<Image>().sprite = upDown_spr[3];
        functionBtn_obj.GetComponent<Image>().sprite = upDown_spr[4];
        ItemListImg_obj[1].SetActive(false);
        ItemListImg_obj[0].SetActive(false);
        ItemListImg_obj[2].SetActive(true);
        OpenfunctionItem();
    }

    /// <summary>
    /// 기능성창이 열렸을때 해금과 구매 여부를 판단해준다.
    /// </summary>
    public void OpenfunctionItem()
    {
        switch_i = PlayerPrefs.GetInt("switchshop", 0);
        waterCan_i = PlayerPrefs.GetInt("wateringcanshop", 0);
        waterpurifier_i = PlayerPrefs.GetInt("waterpurifiershop", 0);
        reform_i = PlayerPrefs.GetInt("reformshop", 0);
        //해금
        if (PlayerPrefs.GetInt("seedbox", 0) >= 1&& waterCan_i == 0)
        {
            PlayerPrefs.SetInt("wateringcanshop", 1);
            waterCan_i = 1;
        }
        
        if (switch_i == 1)
        {
            functionTape_obj[0].SetActive(false);
            functionBuyBtn_obj[0].SetActive(true);
        }
        else if (switch_i == 2)
        {
            functionTape_obj[0].SetActive(false);
            funcImgs_obj[0].GetComponent<Image>().sprite = funcImg_spr[0];
            funcPrice_obj[0].SetActive(true);
            funcPrice_obj[1].SetActive(false);

        }
        else if(switch_i==0)
        {
            functionTape_obj[0].SetActive(true);
        }

        if (waterCan_i == 1)
        {
            functionTape_obj[1].SetActive(false);
            functionBuyBtn_obj[1].SetActive(true);
        }
        else if (waterCan_i == 2)
        {
            functionTape_obj[1].SetActive(false);
            funcImgs_obj[1].GetComponent<Image>().sprite = funcImg_spr[1];
            funcPrice_obj[2].SetActive(true);
            funcPrice_obj[3].SetActive(false);
            functionTape_obj[4].SetActive(false);
        }
        else if (waterCan_i == 0)
        {
            functionTape_obj[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("storg", 0)==1)
        {
            functionTape_obj[4].SetActive(false);
        }

        if (waterpurifier_i == 1)
        {
            functionTape_obj[2].SetActive(false);
            functionBuyBtn_obj[2].SetActive(true);
        }
        else if (waterpurifier_i == 2)
        {
            functionTape_obj[2].SetActive(false);
            funcImgs_obj[2].GetComponent<Image>().sprite = funcImg_spr[2];
            funcPrice_obj[4].SetActive(true);
            funcPrice_obj[5].SetActive(false);
        }
        else if (waterpurifier_i == 0)
        {
            functionTape_obj[2].SetActive(true);
        }

        if (reform_i == 1)
        {
            functionTape_obj[3].SetActive(false);
            //functionBuyBtn_obj[3].SetActive(true);
        }
        else if (reform_i == 2)
        {
            functionTape_obj[3].SetActive(false);
        }
        else if (reform_i == 0)
        {
            functionTape_obj[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("shopfpethotel", 0) == 1)
        {
            functionTape_obj[5].SetActive(false);
        }
    }
    
    public void Fswitch0()
    {
        func_i = 0;
    }

    public void FwaterCan1()
    {
        func_i = 1;
    }

    public void Fwaterpurifier2()
    {
        func_i = 2;
    }

    public void Freform3()
    {
        func_i = 3;
    }

    public void FuctionBuy()
    {
        switch (func_i)
        {
            case 0:
                fucnYN_obj.SetActive(true);
                funcImg_obj.GetComponent<Image>().sprite = funcTxt_spr[func_i];
                break;
            case 1:
                fucnYN_obj.SetActive(true);
                funcImg_obj.GetComponent<Image>().sprite = funcTxt_spr[func_i];
                break;
            case 2:
                fucnYN_obj.SetActive(true);
                funcImg_obj.GetComponent<Image>().sprite = funcTxt_spr[func_i];
                break;
            case 3:
                //fucnYN_obj.SetActive(true);
                //funcImg_obj.GetComponent<Image>().sprite = funcImg_spr[func_i];
                break;
        }
    }

    public void FunctionYes()
    {
        str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        switch (func_i)
        {
            case 0:
                coldRainPrice_i = 0;
                hotRainPrice_i = 600;
                break;
            case 1:
                coldRainPrice_i = 500;
                hotRainPrice_i = 0;
                break;
            case 2:
                coldRainPrice_i = 200;
                hotRainPrice_i = 200;
                break;
            case 3:
                coldRainPrice_i = 0;
                hotRainPrice_i = 0;
                break;
        }
        if (coldRain_i >= coldRainPrice_i)
        {
            if (hotRain_i >= hotRainPrice_i)
            {
                coldRain_i = coldRain_i - coldRainPrice_i;
                PlayerPrefs.SetInt(str + "c", coldRain_i);
                hotRain_i = hotRain_i - hotRainPrice_i;
                PlayerPrefs.SetInt(str + "h", hotRain_i);
                coldRain_txt.text = "" + coldRain_i;
                hotRain_txt.text = "" + hotRain_i;
                PlayerPrefs.SetInt(func_str[func_i],2);
                funcImgs_obj[func_i].GetComponent<Image>().sprite = funcImg_spr[func_i];
                PlayerPrefs.Save();
                fucnYN_obj.SetActive(false);
                
                    switch (func_i)
                    {
                        case 0:
                        if (PlayerPrefs.GetInt("place", 0) == 1)
                        {
                            GM2.GetComponent<secondRoomFunction>().switch_obj.SetActive(true);
                        }
                        funcPrice_obj[0].SetActive(true);
                        funcPrice_obj[1].SetActive(false);
                        functionBuyBtn_obj[0].SetActive(false);
                            break;
                        case 1:
                        if (PlayerPrefs.GetInt("place", 0) == 1)
                        {
                            GM2.GetComponent<secondRoomFunction>().WaterCan_obj.SetActive(true);
                        }
                        funcPrice_obj[2].SetActive(true);
                        funcPrice_obj[3].SetActive(false);
                        functionBuyBtn_obj[1].SetActive(false);
                            break;
                        case 2:
                        if (PlayerPrefs.GetInt("place", 0) == 1)
                        {
                            GM2.GetComponent<secondRoomFunction>().WaterPurifiler_obj.SetActive(true);
                        }
                        funcPrice_obj[4].SetActive(true);
                        funcPrice_obj[5].SetActive(false);
                        functionBuyBtn_obj[2].SetActive(false);
                            break;
                        case 3:
                            break;
                    }
            }
            else
            {
                StartCoroutine("toastHotImgFadeOut");
                needhRain_obj.SetActive(true);
                Needfalse();
                CloseShopBuy();
                //따듯한물부족
            }
        }
        else
        {
            StartCoroutine("toastColdImgFadeOut");
            needcRain_obj.SetActive(true);
            Needfalse();
            CloseShopBuy();
            //빗물부족
        }
    }

    //보관함
    public void OpenFuncCabinet()
    {
        if (PlayerPrefs.GetInt("wateringcanshop", 0) >= 2)
        {
            funcBox_obj[0].SetActive(true);
        }
        else
        {
            funcBox_obj[0].SetActive(false);
        }
        if (PlayerPrefs.GetInt("leafget", 0) >= 1)
        {
            funcBox_obj[1].SetActive(true);
        }
        else
        {
            funcBox_obj[1].SetActive(false);
        }
        if (PlayerPrefs.GetInt("paintinroom", 0) >= 1)
        {
            funcBox_obj[2].SetActive(true);
        }
        else
        {
            funcBox_obj[2].SetActive(false);
        }
        shop_obj.SetActive(false);
        close_obj.SetActive(false);
        back_obj.SetActive(false);
        funcCabinet_obj.SetActive(true);
        if(PlayerPrefs.GetInt("putwatercan", 1) >= 1)
        {
            funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[0];
        }
        else
        {
            funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[1];
        }

        if (PlayerPrefs.GetInt("putleaf", 1) >= 1)
        {
            funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[2];
        }
        else
        {
            funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[3];
        }
        if (PlayerPrefs.GetInt("putframe", 1) >= 1)
        {
            funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[4];
        }
        else
        {
            funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[5];
        }
    }
    //식물 보관
    public void PutPlant()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            if (GM2.GetComponent<SecondRoomTime>().plant_obj.activeSelf == true)
            {
                GM2.GetComponent<SecondRoomTime>().plant_obj.SetActive(false);
                PlayerPrefs.SetInt("putleaf",0);
                funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[3];
            }
            else
            {
                GM2.GetComponent<SecondRoomTime>().plant_obj.SetActive(true);
                PlayerPrefs.SetInt("putleaf", 1);
                funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[2];
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("putleaf", 1) == 1)
            {
                PlayerPrefs.SetInt("putleaf", 0);
                funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[3];
            }
            else
            {
                PlayerPrefs.SetInt("putleaf", 1);
                funcBox_obj[1].GetComponent<Image>().sprite = funcBox_spr[2];
            }

        }
    }
    //그림 보관
    public void PutFrame()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            if (GM.GetComponent<MainPaint>().paintFrame_obj.activeSelf == true)
            {
                GM.GetComponent<MainPaint>().paintFrame_obj.SetActive(false);
                PlayerPrefs.SetInt("putframe", 0);
                funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[5];
            }
            else
            {
                GM.GetComponent<MainPaint>().paintFrame_obj.SetActive(true);
                PlayerPrefs.SetInt("putframe", 1);
                funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[4];
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("putframe", 1) == 1)
            {
                PlayerPrefs.SetInt("putframe", 0);
                funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[5];
            }
            else
            {
                PlayerPrefs.SetInt("putframe", 1);
                funcBox_obj[2].GetComponent<Image>().sprite = funcBox_spr[4];
            }

        }
    }
    //물뿌리개 보관
    public void PutWaterCan()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            if (GM2.GetComponent<secondRoomFunction>().WaterCan_obj.activeSelf == true)
            {
                GM2.GetComponent<secondRoomFunction>().WaterCan_obj.SetActive(false);
                PlayerPrefs.SetInt("putwatercan", 0);
                funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[1];
            }
            else
            {
                GM2.GetComponent<secondRoomFunction>().WaterCan_obj.SetActive(true);
                PlayerPrefs.SetInt("putwatercan", 1);
                funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[0];
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("putwatercan", 1) == 1)
            {
                PlayerPrefs.SetInt("putwatercan", 0);
                funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[1];
            }
            else
            {
                PlayerPrefs.SetInt("putwatercan", 1);
                funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[0];
            }

        }
    }

    public void CloseFuncCabinet()
    {
        funcCabinet_obj.SetActive(false);
    }

    //애완동물
    public void OpenPetHotel()
    {
        shop_obj.SetActive(false);
        close_obj.SetActive(false);
        back_obj.SetActive(false);
        petHotel_obj.SetActive(true);
        if (PlayerPrefs.GetInt("marimo", 0) == 1)
        {
            petMarimo_obj.SetActive(true);
            if (PlayerPrefs.GetInt("setmarimo", 0) == 0)
            {
                petMarimo_obj.GetComponent<Image>().sprite = marimo_spr[0];
            }
        }
        if (PlayerPrefs.GetInt("rabbit", 0) == 1)
        {
            petRabbit_obj.SetActive(true);
            if (PlayerPrefs.GetInt("setrabbit", 0) == 0)
            {
                petRabbit_obj.GetComponent<Image>().sprite = rabbit_spr[0];
            }
        }
        if (PlayerPrefs.GetInt("tutle", 0) == 1)
        {
            petTutle_obj.SetActive(true);
            if (PlayerPrefs.GetInt("settutle", 0) == 0)
            {
                petTutle_obj.GetComponent<Image>().sprite = tutle_spr[0];
            }
        }
        if (PlayerPrefs.GetInt("goldfish", 0) == 1)
        {
            petFish_obj.SetActive(true);
            if (PlayerPrefs.GetInt("setgoldfish", 0) == 0)
            {
                petFish_obj.GetComponent<Image>().sprite = goldfish_spr[0];
            }
        }
    }
    public void ClosePetHotel()
    {
        petHotel_obj.SetActive(false);
    }
    public void moveMarimo()
    {
        if (PlayerPrefs.GetInt("setmarimo", 0) == 1)
        {
            petMarimo_obj.GetComponent<Image>().sprite = marimo_spr[0];
            PlayerPrefs.SetInt("setmarimo", 0);
            if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                GM.GetComponent<FirstRoomFunction>().roomMarimo_obj.SetActive(false);
            }
        }
        else
        {
            petMarimo_obj.GetComponent<Image>().sprite = marimo_spr[1];
            PlayerPrefs.SetInt("setmarimo", 1);
            if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                GM.GetComponent<FirstRoomFunction>().roomMarimo_obj.SetActive(true);
            }
        }
    }
    public void moveRabbit()
    {
        if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
        {
            petRabbit_obj.GetComponent<Image>().sprite = rabbit_spr[0];
            PlayerPrefs.SetInt("setrabbit", 0);

            if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                GM.GetComponent<FirstRoomFunction>().roomRabbit_obj.SetActive(false);
            }
        }
        else
        {
            petRabbit_obj.GetComponent<Image>().sprite = rabbit_spr[1];
            PlayerPrefs.SetInt("setrabbit", 1);
            if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                GM.GetComponent<FirstRoomFunction>().roomRabbit_obj.SetActive(true);
            }
        }
    }
    public void moveTutle()
    {
        if (PlayerPrefs.GetInt("settutle", 0) == 1)
        {
            petTutle_obj.GetComponent<Image>().sprite = tutle_spr[0];
            PlayerPrefs.SetInt("settutle", 0);
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                GM2.GetComponent<secondRoomFunction>().roomTutle_obj.SetActive(false);
            }
        }
        else
        {
            petTutle_obj.GetComponent<Image>().sprite = tutle_spr[1];
            PlayerPrefs.SetInt("settutle", 1);
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                GM2.GetComponent<secondRoomFunction>().roomTutle_obj.SetActive(true);
            }
        }
    }
    public void moveFish()
    {
        if (PlayerPrefs.GetInt("setgoldfish", 0) == 1)
        {
            petFish_obj.GetComponent<Image>().sprite = goldfish_spr[0];
            PlayerPrefs.SetInt("setgoldfish", 0);
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                GM2.GetComponent<secondRoomFunction>().roomGoldfish_obj.SetActive(false);
            }
        }
        else
        {
            petFish_obj.GetComponent<Image>().sprite = goldfish_spr[1];
            PlayerPrefs.SetInt("setgoldfish", 1);
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                GM2.GetComponent<secondRoomFunction>().roomGoldfish_obj.SetActive(true);
            }
        }
    }

    //리폼 산것만 팔레트띄우기
    public void OpenReform()
    {
        shop_obj.SetActive(false);
        close_obj.SetActive(false);
        back_obj.SetActive(false);
        reform_obj.SetActive(true);
        palette_obj.SetActive(false);
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("shoppalette"+i, 0) == 1)
            {
                paintcan_obj[i].GetComponent<Image>().sprite = paintcan_spr[i];
                paintcan_obj[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void reformRight()
    {
        reformPage_obj[0].SetActive(false);
        reformPage_obj[1].SetActive(true);
    }
    public void reformLeft()
    {
        reformPage_obj[0].SetActive(true);
        reformPage_obj[1].SetActive(false);
    }

    public void CloseReform()
    {
        reform_obj.SetActive(false);
        palette_obj.SetActive(false);
    }
    //파레트
    public void ClosePalette()
    {
        palette_obj.SetActive(false);
    }
    //도어
    public void OpenMatPalette()
    {
        if(PlayerPrefs.GetInt("shoppalette0", 0) == 1)
        {
            palette_obj.SetActive(true);
            selectAllFalse();
            paletteImg_obj[0].SetActive(true);
            selectMatPaint_obj[PlayerPrefs.GetInt("setmatpalette", 0)].SetActive(true);
        }
        for(int i = 0; i < 7; i++)
        {
            matPalette_obj[i].SetActive(false);
            if (PlayerPrefs.GetInt("shoppalette0" + i, 0) == 1)
            {
                matPalette_obj[i].SetActive(true);
            }
        }
    }
    public void SetMatPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().matImg_obj.GetComponent<Image>().sprite= matPaint_spr[1];
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    mColor = new Color(0.80f, 0.9f, 0.99f);
                    break;
                case 3:
                    mColor = new Color(0.99f, 0.9f, 0.80f);
                    break;
            }
            GM2.GetComponent<secondRoomFunction>().matImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setmatpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnMat()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            mColor = new Color(1f, 1f, 1f);
            GM2.GetComponent<secondRoomFunction>().matImg_obj.GetComponent<Image>().color = mColor;
            GM2.GetComponent<secondRoomFunction>().matImg_obj.GetComponent<Image>().sprite = matPaint_spr[0];
        }
        PlayerPrefs.SetInt("setmatpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    public void returnMat2()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            mColor = new Color(1f, 1f, 1f);
            GM2.GetComponent<secondRoomFunction>().matImg2_obj.GetComponent<Image>().color = mColor;
            GM2.GetComponent<secondRoomFunction>().matImg2_obj.GetComponent<Image>().sprite = mat2Paint_spr[0];
        }
        PlayerPrefs.SetInt("setmat2palette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    public void returnShelf()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            mColor = new Color(1f, 1f, 1f);
            GM2.GetComponent<secondRoomFunction>().shelfImg_obj.GetComponent<Image>().color = mColor;
            GM2.GetComponent<secondRoomFunction>().shelfImg_obj.GetComponent<Image>().sprite = shelfPaint_spr[0];
        }
        PlayerPrefs.SetInt("setshelfpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }

    public void SetMa2tPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().matImg2_obj.GetComponent<Image>().sprite = mat2Paint_spr[1];
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    mColor = new Color(0.80f, 0.9f, 0.99f);
                    break;
                case 3:
                    mColor = new Color(0.99f, 0.9f, 0.80f);
                    break;
            }
            GM2.GetComponent<secondRoomFunction>().matImg2_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setmat2palette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }

    public void SetShelfPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().shelfImg_obj.GetComponent<Image>().sprite = shelfPaint_spr[1];
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    mColor = new Color(0.80f, 0.94f, 0.99f);
                    break;
                case 3:
                    mColor = new Color(0.99f, 0.9f, 0.75f);
                    break;
            }
            GM2.GetComponent<secondRoomFunction>().shelfImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setshelfpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    //전등
    public void SetLightPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().lightImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().lightMax_spr[itemIndex_i];
            GM2.GetComponent<secondRoomFunction>().lightImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().lightMax_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setlightpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnLight()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().lightImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().lightMax_spr[0];
            GM2.GetComponent<secondRoomFunction>().lightImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().lightMax_spr[0];
        }
        PlayerPrefs.SetInt("setlightpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    //창문
    public void SetWindowPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().windowImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWindow_spr[itemIndex_i];
            GM2.GetComponent<secondRoomFunction>().windowImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWindow2_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setwindowpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnWindow()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            GM2.GetComponent<secondRoomFunction>().windowImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWindow_spr[0];
            GM2.GetComponent<secondRoomFunction>().windowImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWindow2_spr[0];
        }
        PlayerPrefs.SetInt("setwindowpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    //장식장
    public void SetDrawerPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.75f, 0.95f, 0.99f);
                    break;
                case 2:
                    mColor = new Color(0.99f, 0.85f, 0.60f);
                    break;
                case 3:
                    mColor = new Color(0.70f, 0.70f, 0.70f);
                    break;
                case 4:
                    mColor = new Color(0.70f, 0.70f, 0.70f);
                    break;
            }
            GM2.GetComponent<secondRoomFunction>().drawerImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setdrawerpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }   
    public void returnDrawer()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            mColor = new Color(1f, 1f, 1f);
            GM2.GetComponent<secondRoomFunction>().drawerImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setdrawerpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }

    //책장
    public void SetBookPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().bookcase_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformBookcase_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setbookpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnBook()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().bookcase_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformBookcase_spr[0];
        }
        PlayerPrefs.SetInt("setbookpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    //침대
    public void SetBedPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().bedMax_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformBed_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setbedpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnBed()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().bedMax_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformBed_spr[0];
        }
        PlayerPrefs.SetInt("setbedpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    //책상
    public void SetDeskPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().deskImg_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformDesk_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setdeskpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnDesk()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().deskImg_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformDesk_spr[0];
        }
        PlayerPrefs.SetInt("setdeskpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }

    //벽지
    public void SetWallPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformWall_spr[itemIndex_i];
            GM.GetComponent<FirstRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformWall2_spr[itemIndex_i];
        }

        if (PlayerPrefs.GetInt("place", 0) == 1)//방
        {
            GM2.GetComponent<secondRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWall_spr[itemIndex_i];
            GM2.GetComponent<secondRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWall2_spr[itemIndex_i];
        }
        PlayerPrefs.SetInt("setwallpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnWall()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            GM.GetComponent<FirstRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformWall_spr[0];
            GM.GetComponent<FirstRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = GM.GetComponent<FirstRoomFunction>().reformWall2_spr[0];
        }
        if (PlayerPrefs.GetInt("place", 0) == 1)//방
        {
            GM2.GetComponent<secondRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWall_spr[0];
            GM2.GetComponent<secondRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = GM2.GetComponent<secondRoomFunction>().reformWall2_spr[0];
        }
        PlayerPrefs.SetInt("setwallpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }
    //러그
    public void SetRugPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.76f, 0.9f, 0.99f);
                    break;
                case 2:
                    mColor = new Color(0.95f, 0.80f, 0.80f);
                    break;
                case 3:
                    mColor = new Color(0.65f, 0.65f, 0.65f);
                    break;
                case 4:
                    mColor = new Color(0.70f, 0.70f, 0.70f);
                    break;
            }
            GM.GetComponent<FirstRoomFunction>().rugImg_obj.GetComponent<Image>().color = mColor;
            GM.GetComponent<FirstRoomFunction>().rugImg2_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setrugpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnRug()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            mColor = new Color(1f, 1f, 1f);
            GM.GetComponent<FirstRoomFunction>().rugImg_obj.GetComponent<Image>().color = mColor;
            GM.GetComponent<FirstRoomFunction>().rugImg2_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setrugpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }

    // 서랍장
    public void SetCabinetPalette()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            switch (itemIndex_i)
            {
                case 1:
                    mColor = new Color(0.6f, 0.80f, 0.99f);
                    break;
                case 2:
                    mColor = new Color(0.99f, 0.80f, 0.80f);
                    break;
                case 3:
                    mColor = new Color(0.60f, 0.60f, 0.60f);
                    break;
                case 4:
                    mColor = new Color(0.70f, 0.70f, 0.70f);
                    break;
            }
            GM.GetComponent<FirstRoomFunction>().cabinetImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setcabinetpalette", itemIndex_i);
        selectAllFalse();
        selectAll_obj[itemIndex_i].SetActive(true);
    }
    public void returnCabinet()
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)//방
        {
            mColor = new Color(1f, 1f, 1f);
            GM.GetComponent<FirstRoomFunction>().cabinetImg_obj.GetComponent<Image>().color = mColor;
        }
        PlayerPrefs.SetInt("setcabinetpalette", 0);
        selectAllFalse();
        selectAll_obj[0].SetActive(true);
    }

    void selectAllFalse()
    {
        selectAll_obj[0].SetActive(false);
        selectAll_obj[1].SetActive(false);
        selectAll_obj[2].SetActive(false);
        selectAll_obj[3].SetActive(false);
        selectAll_obj[4].SetActive(false);
        selectAll_obj[5].SetActive(false);
    }


    //부엌
    public void OpenMat2Palette()
    {
        if (PlayerPrefs.GetInt("shoppalette1", 0) == 1)
        {
            palette_obj.SetActive(true);
            paletteImg_obj[0].SetActive(false);
            paletteImg_obj[1].SetActive(true);
            paletteImg_obj[2].SetActive(false);
            selectMat2Paint_obj[PlayerPrefs.GetInt("setmat2palette", 0)].SetActive(true);
        }
        for (int i = 0; i < 6; i++)
        {
            mat2Palette_obj[i].SetActive(false);
            if (PlayerPrefs.GetInt("shoppalette1" + i, 0) == 1)
            {
                mat2Palette_obj[i].SetActive(true);
            }

        }
    }
    //선반
    public void OpenShelfPalette()
    {
        if (PlayerPrefs.GetInt("shoppalette2", 0) == 1)
        {
            palette_obj.SetActive(true);
            paletteImg_obj[0].SetActive(false);
            paletteImg_obj[1].SetActive(false);
            paletteImg_obj[2].SetActive(true);
            selectShelfPaint_obj[PlayerPrefs.GetInt("setshelfpalette", 0)].SetActive(true);
        }
        for (int i = 0; i < 6; i++)
        {
            shelfPalette_obj[i].SetActive(false);
            if (PlayerPrefs.GetInt("shoppalette2" + i, 0) == 1)
            {
                shelfPalette_obj[i].SetActive(true);
            }

        }
    }

    public void OpenSetPalette()
    {
        if (PlayerPrefs.GetInt("shoppalette"+itemIndex_i, 0) == 1)
        {
            palette_obj.SetActive(true);
            for(int i=0; i < 12; i++)
            {
                paletteImg_obj[i].SetActive(false);
            }
            //도어,부엌,선반,전구0~3  창문,장식장,책장,침대,테이블 4,5,6,7,8 벽지,러그,서랍장,가스렌지 9,10,11,12
            selectAllFalse();
            switch (itemIndex_i)
            {
                case 0:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setmatpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        matPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            matPalette_obj[i].SetActive(true);
                        }
                    }
                    break;
                case 1:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setmat2palette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        mat2Palette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            mat2Palette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 2:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setshelfpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        shelfPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            shelfPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 3:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setlightpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        lightPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            lightPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 4:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setwindowpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        windowPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            windowPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 5:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setdrawerpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        drawerPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            drawerPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 6:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setbookpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        bookPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            bookPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 7:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setbedpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        bedPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            bedPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 8:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setdeskpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        deskPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            deskPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 9:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setwallpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        wallPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            wallPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 10:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setrugpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        rugPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            rugPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 11:
                    paletteImg_obj[itemIndex_i].SetActive(true);
                    selectAll_obj[PlayerPrefs.GetInt("setcabinetpalette", 0)].SetActive(true);
                    for (int i = 0; i < 6; i++)
                    {
                        cabinetPalette_obj[i].SetActive(false);
                        if (PlayerPrefs.GetInt("shoppalette" + itemIndex_i + i, 0) == 1)
                        {
                            cabinetPalette_obj[i].SetActive(true);

                        }
                    }
                    break;
                case 12:
                    break;

            }
        }
    }

    /// <summary>
    /// 완전한 물건일때 숲 또는 바다에서 
    /// 얻은 페인트가 있는지 확인하고 
    /// 있다면 팔레트를 킬수 있게 한다.
    /// </summary>
    void checkMaxtresure()
    {
        //침대
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2&& PlayerPrefs.GetInt("shoppalette74", 0)==1)
        {
            PlayerPrefs.SetInt("shoppalette7", 1);
        }

        //전구
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2 && PlayerPrefs.GetInt("shoppalette34", 0) == 1)
        {
            PlayerPrefs.SetInt("shoppalette3", 1);
        }

        //책상
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2 && PlayerPrefs.GetInt("shoppalette84", 0) == 1)
        {
            PlayerPrefs.SetInt("shoppalette8", 1);
        }
    }

    void SetPaletteSwitch()
    {

    }

    void Needfalse()
    {
        fucnYN_obj.SetActive(false);
    }
    //온수가 부족하다
    IEnumerator toastHotImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needhRain_obj.GetComponent<Image>().color = color;
        needhRain_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needhRain_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needhRain_obj.SetActive(false);
    }

    //빗물이가 부족하다
    IEnumerator toastColdImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needcRain_obj.GetComponent<Image>().color = color;
        needcRain_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needcRain_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needcRain_obj.SetActive(false);
    }
    //업적
    void achvcheck()
    {
        //업적
        if (itemName_str == "book")
        {
            if (PlayerPrefs.GetInt("booklv") >= 15 && PlayerPrefs.GetInt("allbook", 0) == 0)
            {
                PlayerPrefs.SetInt("allbook", 1);
                //단칸방
                if (PlayerPrefs.GetInt("place", 0) == 0)
                {
                    GM.GetComponent<AchievementShow>().achievementCheck(21, 0);
                }
                else
                {

                    GM2.GetComponent<AchievementShow>().achievementCheck(21, 0);
                }
            }
        }
        //Debug.Log(itemName_str + PlayerPrefs.GetInt("windowlv") + "window" + PlayerPrefs.GetInt("allwindow", 0));
        if (itemName_str == "window")
        {
            if (PlayerPrefs.GetInt("windowlv") >= 8 && PlayerPrefs.GetInt("allwindow", 0) == 0)
            {
                PlayerPrefs.SetInt("allwindow", 1);
                //단칸방
                if (PlayerPrefs.GetInt("place", 0) == 0)
                {
                    GM.GetComponent<AchievementShow>().achievementCheck(20, 0);
                }
                else
                {

                    GM2.GetComponent<AchievementShow>().achievementCheck(20, 0);
                }

            }
        }
    }
}

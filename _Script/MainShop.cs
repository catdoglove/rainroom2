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
    public GameObject funcCabinet_obj;
    public GameObject[] funcBox_obj;
    public Sprite[] funcBox_spr;

    public int switch_i, waterCan_i, waterpurifier_i, reform_i, func_i;

    public string[] func_str;

    public GameObject shop_obj,close_obj,back_obj;
    

    //부족하다창
    Color color;
    public GameObject needToast_obj;

    //소리
    public GameObject audio_obj;

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);
        //GM.GetComponent<LoadingData> ().;
        //PlayerPrefs.SetInt("booklv",14);
        string str = PlayerPrefs.GetString("code", "");
        //PlayerPrefs.SetInt("seedlv",0);
        //PlayerPrefs.SetInt(str + "c", 99999);
        //PlayerPrefs.SetInt(str + "h", 9999);
        //PlayerPrefs.SetInt(str + "ht", 99);
        //PlayerPrefs.SetInt("lovelv", 3);
        //PlayerPrefs.SetInt("seedlv", 10);
        //PlayerPrefs.SetInt("allflower", 0);
        //PlayerPrefs.SetInt("allflowerplus", 0);
        //PlayerPrefs.SetInt("allflowerb", 0);
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("bedlv", 0);
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
        }else if(PlayerPrefs.GetInt("place", 0) == 1)//단칸방
        {
            if (PlayerPrefs.GetInt("drawerbox", 0) == 10)
            {
                boxs_obj[4].SetActive(true);
            }
            else
            {
                boxs_obj[4].SetActive(false);
            }

        }
    }

    public void ShopBuyYes()
    {
        if (hotRainPrice_i == 0 && coldRainPrice_i == 0){

        }
        else
        {
            Debug.Log(itemIndex_i + "fd" + itemLevel_i);//////////////////////////////////////////////////////////////////

            if (coldRain_i >= coldRainPrice_i)
            {
                if (hotRain_i >= hotRainPrice_i)
                {
                    coldRain_i = coldRain_i - coldRainPrice_i;
                    PlayerPrefs.SetInt(str + "c", coldRain_i);
                    Debug.Log(coldRainPrice_i);//////////////////////////////////////////////////////////////////
                    hotRain_i = hotRain_i - hotRainPrice_i;
                    PlayerPrefs.SetInt(str + "h", hotRain_i);
                    Debug.Log(hotRainPrice_i);//////////////////////////////////////////////////////////////////
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
        //단칸방
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
            Debug.Log(""+ itemLevel_i+ itemName_str);
            coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];
            coldPrice_txt[i].text = coldRainPrice_i.ToString();
            hotPrice_txt[i].text = hotRainPrice_i.ToString();

            if (hotRainPrice_i == 0 && coldRainPrice_i == 0)
            {
                if (itemIndex_i == 6)
                {
                    if (PlayerPrefs.GetInt("switchshop", 0) == 0)
                    {
                        PlayerPrefs.SetInt("switchshop", 1);

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
                coldRainPrice_i = 100;
                hotRainPrice_i = 50;
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
                coldRainPrice_i = 100;
                hotRainPrice_i = 50;
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
        if (PlayerPrefs.GetInt("wateringcanshop", 0) == 2)
        {
            funcBox_obj[0].SetActive(true);
        }
        else
        {
            funcBox_obj[0].SetActive(false);
        }
            shop_obj.SetActive(false);
        close_obj.SetActive(false);
        back_obj.SetActive(false);
        funcCabinet_obj.SetActive(true);
        if(PlayerPrefs.GetInt("putwatercan", 1) == 1)
        {
            funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[0];
        }
        else
        {
            funcBox_obj[0].GetComponent<Image>().sprite = funcBox_spr[1];
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
                PlayerPrefs.SetInt("putwatercan",0);
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
        Debug.Log(itemName_str + PlayerPrefs.GetInt("windowlv") + "window" + PlayerPrefs.GetInt("allwindow", 0));
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

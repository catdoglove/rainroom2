using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : ShopHandler {
	public GameObject GM,loadGM,GMtag;
	public Text coldRain_txt,hotRain_txt;

    public Text[] levels_txt;
    public Text[] coldPrice_txt;
    public Text[] hotPrice_txt;


    public GameObject needhRain_obj,needcRain_obj;

    List<Dictionary<string, object>> data_hPrice, data_cPrice, data_itemName;

    string str;

    public GameObject buyYes_obj;

    //처음에박스값을10으로설정해준다 아래에서레벨을불러올때 박스값이10이면 박스에담겨있는물건이다 이물건은박스에서 꺼낼지물어본다
    //기본방에서 물건터치는박스로 막혀있다. 박스를 터치하면 상자치우기창이뜬다.
    public GameObject[] boxs_obj;


    // Use this for initialization
    void Start () {
        //GM.GetComponent<LoadingData> ().;
        //PlayerPrefs.SetInt("booklv",0);
        string str = PlayerPrefs.GetString("code", "");
        //PlayerPrefs.SetInt(str + "c", 999999);
        //PlayerPrefs.SetInt(str + "h", 99999);
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("bedlv", 0);
        GM = GameObject.FindGameObjectWithTag("firstroomGM");
        loadGM =GameObject.FindGameObjectWithTag("loadGM");
        data_cPrice = CSVReader.Read("Price/f_coldrain");
        data_hPrice = CSVReader.Read("Price/f_hotrain");
        //data_itemName = CSVReader.Read("Price/f_itemname");                               나중에추가해줄것
    }

    public void ShopCoinLoad(){

        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
            loadGM = GameObject.FindGameObjectWithTag("loadGM");
            data_cPrice = CSVReader.Read("Price/f_coldrain");
            data_hPrice = CSVReader.Read("Price/f_hotrain");
        }
        
		str = PlayerPrefs.GetString ("code", "");

		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);

		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
        LvChange();

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

    public void ShopBuyYes()
    {
        if (hotRainPrice_i == 0 && coldRainPrice_i == 0){}
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
                    
                    //이미지를바꿔주는 함수 단칸방에 있을 때에는 이미지를 바꿔주지 않는다.
                    if(PlayerPrefs.GetInt("place", 0) == 0)
                    {
                        SwitchByIndex();
                    }
                    PlayerPrefs.Save();
                    coldRain_txt.text = "" + coldRain_i;
                    hotRain_txt.text = "" + hotRain_i;
                    LvChange();
                    CloseShopBuy();
                }
                else
                {
                    needhRain_obj.SetActive(true);
                    //따듯한물부족
                }
            }
            else
            {
                needcRain_obj.SetActive(true);
                //빗물부족
            }
        }//endOfElse
    }
 

    public void ShopChageImage() {
        str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);

        
        itemName_str = shopItems_btn[itemIndex_i].name;
        itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
        
        hotRainPrice_i = (int)data_hPrice[itemLevel_i][itemName_str];
        coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];

        //맥스레벨일때
        if (hotRainPrice_i == 0 && coldRainPrice_i == 0) {
            
        }
        else
        {
            buyYes_obj.SetActive(true);
        }
       
    }

    public void closeRain()
    {
        needhRain_obj.SetActive(false);
        needcRain_obj.SetActive(false);
    }


    void SwitchByIndex()
    {
        switch (itemIndex_i)
        {
            case 0:
                GM.GetComponent <FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[itemLevel_i];
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
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[7].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall_spr[4+itemLevel_i];
                break;

            case 6:
                break;

            case 7:
                 break;
                

            
        }

    }

    /// <summary>
    /// 아이템의 레벨과 가격을 새로고침해준다
    /// </summary>
    public void LvChange()
    {
        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
            loadGM = GameObject.FindGameObjectWithTag("loadGM");
            data_cPrice = CSVReader.Read("Price/f_coldrain");
            data_hPrice = CSVReader.Read("Price/f_hotrain");
        }
        for (int i = 0; i < 6; i++)
        {
            itemName_str = shopItems_btn[i].name;
            itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
            levels_txt[i].text = "LV. "+itemLevel_i.ToString();
            ///////////////////////////////////////////////////////////////////////////////////이름더해주기 data_itemName[itemLevel_i][itemName_str]
            hotRainPrice_i = (int)data_hPrice[itemLevel_i][itemName_str];
            coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];
            coldPrice_txt[i].text = coldRainPrice_i.ToString();
            hotPrice_txt[i].text = hotRainPrice_i.ToString();

            if (hotRainPrice_i == 0 && coldRainPrice_i == 0) {
                itemName_str = shopItems_btn[i].name;
                itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
                levels_txt[i].text = "LV. MAX";
                coldPrice_txt[i].text = "0";
                hotPrice_txt[i].text = "0";
            }
            else
            { }
            }
    }

    public void CloseShopBuy()
    {
        buyYes_obj.SetActive(false);
    }
	

}

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

    List<Dictionary<string, object>> data_hPrice, data_cPrice;


    // Use this for initialization
    void Start () {
        //GM.GetComponent<LoadingData> ().;
        //PlayerPrefs.SetInt("booklv",0);
        //string str = PlayerPrefs.GetString("code", "");
        //PlayerPrefs.SetInt(str + "c", 999999);
        //PlayerPrefs.SetInt(str + "h", 99999);
        data_cPrice = CSVReader.Read("Price/f_coldrain");
        data_hPrice = CSVReader.Read("Price/f_hotrain");
    }

    public void ShopCoinLoad(){

        
		
		string str = PlayerPrefs.GetString ("code", "");

		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);

		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
        LvChange();

    }
    public void callShopButtonName()
    {

        itemName_str = shopItems_btn[itemIndex_i].name;
        itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);

        //딕셔너리로2차열하기
        //GM.GetComponent<LoadingData> ().;

    }

    public void ShopChageImage() {
        string str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);

        callShopButtonName();

        itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
        
        hotRainPrice_i = (int)data_hPrice[itemLevel_i][itemName_str];
        coldRainPrice_i = (int)data_cPrice[itemLevel_i][itemName_str];
        if () { }


            Debug.Log(itemIndex_i+"fd"+itemLevel_i);//////////////////////////////////////////////////////////////////

        if (coldRain_i >= coldRainPrice_i) {
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
                SwitchByIndex();
                PlayerPrefs.Save();
                coldRain_txt.text = "" + coldRain_i;
                hotRain_txt.text = "" + hotRain_i;
                LvChange();
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
                break;

            case 4:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[itemLevel_i];
                break;

            case 5:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 6:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 7:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 8:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 10:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 11:
                GM.GetComponent<FirstRoomFunction>().fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;
        }

    }

    public void LvChange()
    {
        for(int i = 0; i < 1; i++)
        {
            itemName_str = shopItems_btn[i].name;
            itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);
            levels_txt[i].text = itemLevel_i.ToString();
            coldPrice_txt[i].text = data_cPrice[itemLevel_i][itemName_str].ToString();
            hotPrice_txt[i].text = data_hPrice[itemLevel_i][itemName_str].ToString();
            
        }
    }
	

}

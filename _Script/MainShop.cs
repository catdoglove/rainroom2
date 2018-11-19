using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : ShopHandler {
	public GameObject GM;
	public Text coldRain_txt,hotRain_txt;
    public GameObject[] fisrtRoomItem_obj;

    public GameObject needhRain_obj,needcRain_obj;

    List<Dictionary<string, object>> data_hPrice, data_cPrice;


    // Use this for initialization
    void Start () {
        //GM.GetComponent<LoadingData> ().;
        //data_cPrice = CSVReader.Read("Price/f_coldrain");
        //data_hPrice = CSVReader.Read("Price/f_hotrain");
    }

    public void ShopCoinLoad(){

        
		
		string str = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);

		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
	}

    public void ShopChageImage() {
        string str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);

        callShopButtonName();
        hotRainPrice_i = (int)data_hPrice[itemIndex_i][itemName_str];
        coldRainPrice_i = (int)data_cPrice[itemIndex_i][itemName_str];

        itemLevel_i = PlayerPrefs.GetInt(itemName_str + "lv", 0);

        if (coldRain_i >= coldRainPrice_i) {
            if (hotRain_i >= hotRainPrice_i)
            {
                coldRain_i = coldRain_i - coldRainPrice_i;
                PlayerPrefs.SetInt(str + "c", coldRain_i);

                hotRain_i = hotRain_i - hotRainPrice_i;
                PlayerPrefs.SetInt(str + "h", hotRain_i);

                itemLevel_i++;
                PlayerPrefs.SetInt(itemName_str + "lv", itemLevel_i);

                SwitchByIndex();
                
                PlayerPrefs.Save();
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
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 1:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 2:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 3:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 4:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 5:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 6:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 7:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 8:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 10:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;

            case 11:
                fisrtRoomItem_obj[itemIndex_i].GetComponent<Image>().sprite = GM.GetComponent<LoadingData>().window_spr[itemLevel_i];
                break;
        }

    }
	

}

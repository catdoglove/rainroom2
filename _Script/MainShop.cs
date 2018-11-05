using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : ShopHandler {
	public GameObject GM;
	public Text coldRain_txt,hotRain_txt;

    List<Dictionary<string, object>> data_hPrice, data_cPrice;


    // Use this for initialization
    void Start () {
        //GM.GetComponent<LoadingData> ().;
        //data_cPrice = CSVReader.Read("");
        //data_hPrice = CSVReader.Read("");
    }

    public void shopCoinLoad(){
		
		string str = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);

		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
	}

    public void ShopChageImage() {
        if (coldRain_i >= coldRainPrice_i) {

        }

        if (hotRain_i >= hotRainPrice_i)
        {

        }



        switch (itemIndex_i)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 10:
                break;

            case 11:
                break;
        }
    }
	

}

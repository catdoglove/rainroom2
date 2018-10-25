using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : ShopHandler {
	public GameObject GM;
	public Text coldRain_txt,hotRain_txt;


	// Use this for initialization
	void Start () {
		//GM.GetComponent<LoadingData> ().;


	}

	public void shopCoinLoad(){
		
		string str = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str+"h", 0);

		coldRain_txt.text = "" + coldRain_i;
		hotRain_txt.text = "" + hotRain_i;
	}
	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoomFunction : CavasData {

	public GameObject beadalWindow_obj;

	public GameObject boxClean_obj;

	public GameObject GMNotdistroy;

	// Use this for initialization
	void Start () {
		GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");
		
	}


	public void openBeadal(){
		
		if (beadalWindow_obj.activeSelf == true) {
			beadalWindow_obj.SetActive (false);
		} else {
			beadalWindow_obj.SetActive (true);
		}
	}
	public void closeBeadal(){
		beadalWindow_obj.SetActive (false);
	}

	public void boxOpen(){
		boxClean_obj.SetActive (true);
	}
	public void boxYes(){

		string str1;
		str1 = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str1+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str1+"h", 0);

		if (coldRain_i >= 50 && hotRain_i >= 25) {

			coldRain_i = coldRain_i - 50;
			PlayerPrefs.SetInt (str1 + "c", coldRain_i);

			hotRain_i = hotRain_i - 25;
			PlayerPrefs.SetInt (str1 + "h", hotRain_i);

			//스위치케이스문으로읽어온박스넘버를보고저장해줘서치운박스를구분함
			//박스는처음에는없는걸로하고안치운박스면만들어주는걸로해서오류나도
			//박스가보이는일은없게하자

			PlayerPrefs.Save ();
		} else {

		}
	}
	public void boxNo(){
		boxClean_obj.SetActive (false);
	}


}

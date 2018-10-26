using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomFunction : CavasData {

	public GameObject beadalWindow_obj;

	public GameObject boxClean_obj;

	public GameObject GMNotdistroy;


	public int window_i,book_i,bed_i,desk_i,stand_i,tapestry_i;
	public GameObject windowImg_obj,bookImg_obj,deskImg_obj,standImg_obj,tapestryImg_obj,bedImg_obj;

	// Use this for initialization
	void Start () {
		GMNotdistroy = GameObject.FindGameObjectWithTag ("MGMtag");
		/*
		window_i = PlayerPrefs.GetInt ("window", 0);

		PlayerPrefs.GetInt ("book",0);
		PlayerPrefs.GetInt ("bed",0);
		PlayerPrefs.GetInt ("rug",0);
		PlayerPrefs.GetInt ("bookbox",0);
		PlayerPrefs.GetInt ("poster",0);
		PlayerPrefs.GetInt ("desk",0);
		PlayerPrefs.GetInt ("tapestry",0);
		PlayerPrefs.GetInt ("stand",0);


		windowImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData> ().window_spr [window_i];
		bookImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().book_spr [book_i];
		bedImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().bed_spr [bed_i];
		deskImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().desk_spr [desk_i];
		standImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().stand_spr [stand_i];
		tapestryImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().tapestry_spr [tapestry_i];
		*/


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

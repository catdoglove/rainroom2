﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondRoomFunction : CavasData {

	public GameObject GMNotdistroy;
	

	// Use this for initialization
	void Start () {
		//GM을 찾아불러온 데이터들 가져오기

		GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");

		/*
		PlayerPrefs.GetInt ("window",0);
		PlayerPrefs.GetInt ("book",0);
		PlayerPrefs.GetInt ("gasrange",0);
		PlayerPrefs.GetInt ("icebox",0);
		PlayerPrefs.GetInt ("shelf",0);
		PlayerPrefs.GetInt ("drawing",0);
		PlayerPrefs.GetInt ("mat",0);
		PlayerPrefs.GetInt ("flower",0);
		PlayerPrefs.GetInt ("light",0);
		PlayerPrefs.GetInt ("umbrella",0);
		*/
	}


}
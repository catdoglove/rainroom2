using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavasData : CommonDate {


	public GameObject[] canvasPack_cvs;

	public void changeSight(){
		if (canvasPack_cvs [0].activeSelf == true) {
			for (int i = 3; i < 6; i++) {
				canvasPack_cvs [i].SetActive (true);
            }
			for (int i = 0; i < 3; i++) {
				canvasPack_cvs [i].SetActive (false);
			}
            PlayerPrefs.SetInt("front", 2);
        } else {
			for (int i = 3; i < 6; i++) {
				canvasPack_cvs [i].SetActive (false);
            }
			for (int i = 0; i < 3; i++) {
				canvasPack_cvs [i].SetActive (true);
            }
            PlayerPrefs.SetInt("front", 1);
        }
	}
}

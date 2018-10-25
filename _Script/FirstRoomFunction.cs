using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoomFunction : CavasData {

	// Use this for initialization
	void Start () {
		
	}
	


	public void changeSight(){
		if (canvasPack_cvs [0].activeSelf == true) {
			for (int i = 3; i < 6; i++) {
				canvasPack_cvs [i].SetActive (true);
			}
			for (int i = 0; i < 3; i++) {
				canvasPack_cvs [i].SetActive (false);
			}
		} else {
			for (int i = 3; i < 6; i++) {
				canvasPack_cvs [i].SetActive (false);
			}
			for (int i = 0; i < 3; i++) {
				canvasPack_cvs [i].SetActive (true);
			}
		}
	}


}

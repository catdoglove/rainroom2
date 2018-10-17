using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainBtnEvt : MonoBehaviour {

	public GameObject[] MainBtn_obj;
	public GameObject[] MainWindow_obj;
	public Text test_txt;
	public string test_str;

	// Use this for initialization
	void Start () {
		setScreen ();
		StartCoroutine ("testText");
	}

	public void showButtons(){
		allClose ();
		if (MainBtn_obj [0].activeSelf == true) {
			for (int i = 0; i < 3; i++) {
				MainBtn_obj [i].SetActive (false);
			}
		} else {
			for (int i = 0; i < 3; i++) {
				MainBtn_obj [i].SetActive (true);
			}
		}
	}

	public void openInfoWindow(){
		if (MainWindow_obj [0].activeSelf == true) {
			MainWindow_obj [0].SetActive (false);
		} else {
			allClose ();
			MainWindow_obj [0].SetActive (true);
		}
	}
	public void openShopWindow(){
		if (MainWindow_obj [1].activeSelf == true) {
			MainWindow_obj [1].SetActive (false);
		} else {
			allClose ();
			MainWindow_obj [1].SetActive (true);
		}
	}

	public void openOptionWindow(){
		if (MainWindow_obj [2].activeSelf == true) {
			MainWindow_obj [2].SetActive (false);
		} else {
			allClose ();
			MainWindow_obj [2].SetActive (true);
		}
	}

	void allClose(){
		MainWindow_obj[0].SetActive(false);
		MainWindow_obj[1].SetActive(false);
		MainWindow_obj[2].SetActive(false);
	}


	//테스트텍스트
	IEnumerator testText(){
		test_str = "";
		for (int i = 0; i < 3; i++) {
			test_str = test_str + "MainBtn_obj [i]:" + MainBtn_obj [i].ToString ();
		}

		test_str = test_str + "MainWindow_obj [0]:" + MainWindow_obj [0].ToString ();

		test_txt.text = test_str;

		yield return null;
	}


	void setScreen(){
		//스크린화면해상도에맞춰조절,화면꺼지지않게
		#region

		float screenNum =(float)Screen.height/(float)Screen.width;
		if (screenNum < 0.57f) {

			Screen.SetResolution (Screen.width, Screen.width / 16 * 9, true);

		} else if (screenNum >= 0.57f && screenNum < 0.62f) {

			Screen.SetResolution (Screen.width, Screen.width / 5 * 3, true);

		} else if (screenNum >= 0.62f && screenNum < 0.65f) {

			Screen.SetResolution (Screen.width, Screen.width / 16 * 10, true);

		} else if (screenNum >= 0.65f && screenNum < 0.7f) {

			Screen.SetResolution (Screen.width, Screen.width / 3 * 2, true);

		} else if (screenNum >= 0.7f) {

			Screen.SetResolution (Screen.width, Screen.width / 4 * 3, true);

		} else {
			Screen.SetResolution (Screen.width, Screen.width / 3 * 2, true);
		}

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		#endregion
	}
}

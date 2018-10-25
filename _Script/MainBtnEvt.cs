using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainBtnEvt : CommonDate {

	public GameObject[] MainBtn_obj;
	public GameObject[] MainWindow_obj;
	public Text test_txt;
	public string test_str;




	// Use this for initialization
	void Start () {
		setScreen ();
		StartCoroutine ("testText");

		//처음코드설정

		#region
		int c=0;
		string str="";
		if (c == PlayerPrefs.GetInt ("first", 0)) {

			for (int i = 0; i < 16; i++) {
				int a = Random.Range (0, 16);//0~15

				switch (a) {
				case 0:
					str = str + "0";
					break;
				case 1:
					str = str + "1";
					break;
				case 2:
					str = str + "2";
					break;
				case 3:
					str = str + "3";
					break;
				case 4:
					str = str + "4";
					break;
				case 5:
					str = str + "5";
					break;
				case 6:
					str = str + "6";
					break;
				case 7:
					str = str + "7";
					break;
				case 8:
					str = str + "8";
					break;
				case 9:
					str = str + "9";
					break;
				case 10:
					str = str + "a";
					break;
				case 11:
					str = str + "b";
					break;
				case 12:
					str = str + "c";
					break;
				case 13:
					str = str + "d";
					break;
				case 14:
					str = str + "e";
					break;
				case 15:
					str = str + "f";
					break;
				default:
					break;
				}

				//코인이 저장되는 이름을 자기의 코드로해줌
			}

			PlayerPrefs.SetString ("code", str);
			PlayerPrefs.SetInt ("first", 1);
			PlayerPrefs.Save ();
		}//endOfIf

		#endregion

		/*데이터불러오기
		 * 상자와 물건가구들 단계확인 이미지변경
		 * GM찾아서 가져오기
		 * player에 저장된 정보는 그때그때 가져오자 값이 변경될때만 바꾸기
		*/


	}//endofstart




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
			MainWindow_obj [3].SetActive (false);
		} else {
			allClose ();
			MainWindow_obj [0].SetActive (true);
			MainWindow_obj [3].SetActive (true);
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

	public void allClose(){
		MainWindow_obj[0].SetActive(false);
		MainWindow_obj[1].SetActive(false);
		MainWindow_obj[2].SetActive(false);
		MainWindow_obj [3].SetActive (false);
	}




	//테스트텍스트
	IEnumerator testText(){
		test_str = "";


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


	public void talkTest(){
		int talk = PlayerPrefs.GetInt ("talk", 5);
		if (talk <= 0) {
			talk = 0;
		} else {
			talk--;
		}
		PlayerPrefs.SetInt("talk",talk);
		PlayerPrefs.Save ();
	}


}

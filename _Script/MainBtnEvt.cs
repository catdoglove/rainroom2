using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainBtnEvt : CavasData
{

	public GameObject[] MainBtn_obj;
	public GameObject[] MainWindow_obj;
	public Text test_txt;
	public string test_str;

    public GameObject close_obj;
    public GameObject backBlackImg_obj;

    //메뉴펼쳐주기
    public GameObject menuBack_obj;
    public Vector2 menuBack_vet;
    public GameObject GM, GM2;
    
    //문장속도
    float speedF = 0.03f;
    public Text tspeed_txt;
    public GameObject speed_obj, speed_toast;
    
    // Use this for initialization
    void Start () {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0);

        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
        }
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
            PlayerPrefs.SetInt("bookbox", 10);
            PlayerPrefs.SetInt("deskbox", 10);
            PlayerPrefs.SetInt("bedbox", 10);
            PlayerPrefs.SetInt("cabinetbox", 10);

            PlayerPrefs.SetInt("icebox", 10);
            PlayerPrefs.SetInt("gasrangebox", 10);
            PlayerPrefs.SetInt("drawerbox", 10);
            PlayerPrefs.SetInt("seedbox", -10);


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

    
    /// <summary>
    /// 메뉴를펼쳐준다
    /// </summary>
	public void showButtons(){
		allClose ();
		if (MainBtn_obj [0].activeSelf == true) {
            StopCoroutine("menuFlowBack");
            StartCoroutine("menuFlow");
            MainBtn_obj[0].SetActive(false);
        } else {
            StopCoroutine("menuFlow");
            StartCoroutine("menuFlowBack");
            MainBtn_obj[0].SetActive(true);
        }
	}

	public void openInfoWindow(){
		if (MainWindow_obj [0].activeSelf == true) {
			MainWindow_obj [0].SetActive (false);
			MainWindow_obj [3].SetActive (false);
        } else {
			allClose ();
            windowsOpen();
            MainWindow_obj [0].SetActive (true);
			MainWindow_obj [3].SetActive (true);
		}
	}
	public void openShopWindow(){
		if (MainWindow_obj [1].activeSelf == true) {
			MainWindow_obj [1].SetActive (false);
		} else {
			allClose ();
            windowsOpen();
            MainWindow_obj [1].SetActive (true);
		}
	}
	public void openOptionWindow(){
		if (MainWindow_obj [2].activeSelf == true) {
			MainWindow_obj [2].SetActive (false);
		} else {
			allClose ();
            windowsOpen();
            MainWindow_obj [2].SetActive (true);
		}
	}
	public void allClose(){
		MainWindow_obj[0].SetActive(false);
		MainWindow_obj[1].SetActive(false);
		MainWindow_obj[2].SetActive(false);
		MainWindow_obj [3].SetActive (false);
        backBlackImg_obj.SetActive(false);
        close_obj.SetActive(false);
    }

    public void windowsOpen()
    {
        backBlackImg_obj.SetActive(true);
        close_obj.SetActive(true);
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
    

    public void Sight()
    {
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("GM2");
            }
            GM2.GetComponent<secondRoomFunction>().changeSight();
        }
        else if(PlayerPrefs.GetInt("place", 0) == 0)
        {
            if (GM == null)
            {
                GM = GameObject.FindGameObjectWithTag("firstroomGM");
            }
            GM.GetComponent<FirstRoomFunction>().changeSight();
        }
        
    }
    
    IEnumerator menuFlow()
    {
        menuBack_vet = menuBack_obj.transform.position;
            while (menuBack_vet.y >= 0.4f)
            {
                menuBack_vet.y = menuBack_vet.y - 0.6f;
            if (menuBack_vet.y <= 0.4f)
            {
                menuBack_vet.y = 0.39f;
            }
                menuBack_obj.transform.position = menuBack_vet;
                yield return null;
            }
        menuBack_vet.y = 0.2f;
        menuBack_obj.transform.position = menuBack_vet;
    }

    IEnumerator menuFlowBack()
    {
        menuBack_vet = menuBack_obj.transform.position;
            while (menuBack_vet.y <= 6f)
            {
                menuBack_vet.y = menuBack_vet.y + 0.6f;
                menuBack_obj.transform.position = menuBack_vet;
                yield return null;
            }
        menuBack_vet.y = 6.15f;
        menuBack_obj.transform.position = menuBack_vet;
    }



    public void TalkSpeedFast()
    {
        speedF = 0.01f;
        StartCoroutine("closeToast");
    }

    public void TalkSpeedNor()
    {
        speedF = 0.03f;
        StartCoroutine("closeToast");
    }

    public void TalkSpeedSlow()
    {
        speedF = 0.05f;
        StartCoroutine("closeToast");
    }

    public void talkSpeedSet()
    {
        speed_obj.SetActive(true);
    }


    //토스트
    IEnumerator closeToast()
    {

        PlayerPrefs.SetFloat("talkspeed", speedF);
        PlayerPrefs.Save();

        if (speedF == 0.01f)
        {
            tspeed_txt.text = "대화 속도 '빠름'으로 변경";
        }
        else if (speedF == 0.03f)
        {
            tspeed_txt.text = "대화 속도 '보통'으로 변경";
        }
        else if (speedF == 0.05f)
        {
            tspeed_txt.text = "대화 속도 '느림'으로 변경";
        }

        speed_obj.SetActive(false);
        speed_toast.SetActive(true);
        yield return new WaitForSeconds(3f);
        speed_toast.SetActive(false);


    }

}

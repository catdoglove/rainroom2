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
    public GameObject GM, GM2,GMtag;

    //문장속도
    float speedF = 0.06f;
    public Text tspeed_txt;
    public GameObject speed_obj, speed_toast;
    public GameObject [] speedBtn;
    public Sprite[] speedbtnCK;

    //닫을때같이닫는것
    public GameObject YN_obj;

    //도움말
    public GameObject Help_obj;
    public Sprite[] help_spr;
    public Sprite[] helpf_spr;
    public GameObject helpfrist_obj;
    int help = 0;

    public void CloseHelpf()
    {
        if (help == 0)
        {
            help = 1;
            helpfrist_obj.GetComponent<Image>().sprite = helpf_spr[1];
        }
        else if (help == 1)
        {
            helpfrist_obj.GetComponent<Image>().sprite = helpf_spr[2];
            help = 2;
        }
        else
        {
            help = 0;
            helpfrist_obj.GetComponent<Image>().sprite = helpf_spr[0];
            helpfrist_obj.SetActive(false);
        }
    }

    public void CloseHelp()
    {
        Help_obj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            allClose();
            GMtag.GetComponent<MainShop>().CloseFuncCabinet();
            if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                if (GM == null)
                {
                    GM = GameObject.FindGameObjectWithTag("firstroomGM");
                }
                GM.GetComponent<FirstRoomFunction>().closeTitle();
                GM.GetComponent<WindowMiniGame>().CloseMiniGame();
                GM.GetComponent<FirstRoomFunction>().closeBeadal();
                GM.GetComponent<UnityADS>().closeAdYN();
                GM.GetComponent<FirstRoomBookList>().closeItemList();
                GM.GetComponent<FirstRoomSticker>().CloseFrame();
                GM.GetComponent<FirstRoomFunction>().boxNo();
                GM.GetComponent<SleepTime>().closeDiary();
                GM.GetComponent<SleepTime>().CloseSleep();
            }
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                if (GM2 == null)
                {
                    GM2 = GameObject.FindGameObjectWithTag("GM2");
                }
                GM2.GetComponent<secondRoomFunction>().AllClose();
                GM2.GetComponent<secondRoomFunction>().boxClose();
                GM2.GetComponent<GasrangeEvt>().Closefood();
                GM2.GetComponent<GasrangeEvt>().CloseIceBox();
                GM2.GetComponent<SeedTime>().CloseSeed();
                GM2.GetComponent<UnityADS>().closeAdYN();
            }

        }
    }


    void Awake()
    {
        if (PlayerPrefs.GetInt("helpf", 0) == 0)
        {
            helpfrist_obj.SetActive(true);
            PlayerPrefs.SetInt("helpf", 1);
        }
        //방의 위치를 사다리쪽으로 2로해준다
        PlayerPrefs.SetInt("front", 2);
    }

    // Use this for initialization
    void Start () {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.06f);
        speedBtnCKFunction(PlayerPrefs.GetInt("speedTalkCheck", 0));

        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
        }
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            GM2 = GameObject.FindGameObjectWithTag("GM2");
        }
		setScreen ();
		//StartCoroutine ("testText");

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
            PlayerPrefs.SetInt("drawerbox", 10);

            PlayerPrefs.SetInt("iceboxbox", 10);
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
        if(PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            allClose();
            if (MainBtn_obj[0].activeSelf == true)
            {
                StopCoroutine("menuFlowBack");
                StartCoroutine("menuFlow");
                MainBtn_obj[0].SetActive(false);
            }
            else
            {
                StopCoroutine("menuFlow");
                StartCoroutine("menuFlowBack");
                MainBtn_obj[0].SetActive(true);
            }
        }
	}

	public void openInfoWindow(){
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            if (MainWindow_obj[0].activeSelf == true)
            {
                MainWindow_obj[0].SetActive(false);
                MainWindow_obj[3].SetActive(false);
                if (PlayerPrefs.GetInt("infohelpfirst", 0) == 0)
                {
                    OpenHelpInfo();
                    PlayerPrefs.SetInt("infohelpfirst", 1);
                }
            }
            else
            {
                allClose();
                windowsOpen();
                MainWindow_obj[0].SetActive(true);
                MainWindow_obj[3].SetActive(true);
            }
        }
	}
	public void openShopWindow(){
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            if (MainWindow_obj[1].activeSelf == true)
            {
                MainWindow_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("shophelpfirst", 0) == 0)
                {
                    OpenHelpShop();
                    PlayerPrefs.SetInt("shophelpfirst", 1);
                }
            }
            else
            {
                allClose();
                windowsOpen();
                MainWindow_obj[1].SetActive(true);
            }
        }
	}
	public void openOptionWindow(){
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            if (MainWindow_obj[2].activeSelf == true)
            {
                MainWindow_obj[2].SetActive(false);
                if (PlayerPrefs.GetInt("optionhelpfirst", 0) == 0)
                {
                    OpenHelpOption();
                    PlayerPrefs.SetInt("optionhelpfirst", 1);
                }
            }
            else
            {
                allClose();
                windowsOpen();
                MainWindow_obj[2].SetActive(true);
            }
        }
	}
	public void allClose(){
		MainWindow_obj[0].SetActive(false);
		MainWindow_obj[1].SetActive(false);
		MainWindow_obj[2].SetActive(false);
		MainWindow_obj [3].SetActive (false);
        backBlackImg_obj.SetActive(false);
        close_obj.SetActive(false);
        speed_obj.SetActive(false);
        YN_obj.SetActive(false);
        Help_obj.SetActive(false);
        helpfrist_obj.SetActive(false);
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
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                if (GM2 == null)
                {
                    GM2 = GameObject.FindGameObjectWithTag("GM2");
                }
                GM2.GetComponent<secondRoomFunction>().changeSight();
                GM2.GetComponent<secondRoomFunction>().characterTurn();
            }
            else if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                if (GM == null)
                {
                    GM = GameObject.FindGameObjectWithTag("firstroomGM");
                }
                GM.GetComponent<FirstRoomFunction>().changeSight();
                GM.GetComponent<FirstRoomFunction>().characterTurn();
            }
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
        speedF = 0.03f;
        StartCoroutine("closeToast");
    }

    public void TalkSpeedNor()
    {
        speedF = 0.06f;
        StartCoroutine("closeToast");
    }

    public void TalkSpeedSlow()
    {
        speedF = 0.08f;
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

        if (speedF == 0.03f)
        {
            tspeed_txt.text = "대화 속도 '빠름'으로 변경";
            speedBtnCKFunction(3);
        }
        else if (speedF == 0.06f)
        {
            tspeed_txt.text = "대화 속도 '보통'으로 변경";
            speedBtn[1].GetComponent<Image>().sprite = speedbtnCK[1];
            speedBtnCKFunction(5);
        }
        else if (speedF == 0.08f)
        {
            tspeed_txt.text = "대화 속도 '느림'으로 변경";
            speedBtn[0].GetComponent<Image>().sprite = speedbtnCK[0];
            speedBtnCKFunction(8);
        }

        speed_obj.SetActive(false);
        Color colorN;
        colorN = new Color(1f, 1f, 1f);
        colorN.a = Mathf.Lerp(0f, 1f, 1f);
        speed_toast.GetComponent<Image>().color = colorN;
        speed_toast.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorN.a = Mathf.Lerp(0f, 1f, i);
            speed_toast.GetComponent<Image>().color = colorN;
            yield return null;
        }
        speed_toast.SetActive(false);

    }


    public void OpenHelpShop()
    {
        Help_obj.SetActive(true);
        Help_obj.GetComponent<Image>().sprite = help_spr[0];
    }
    public void OpenHelpInfo()
    {
        Help_obj.SetActive(true);
        Help_obj.GetComponent<Image>().sprite = help_spr[1];
    }
    public void OpenHelpOption()
    {
        Help_obj.SetActive(true);
        Help_obj.GetComponent<Image>().sprite = help_spr[2];
    }

    public void OpenHelpf()
    {
        helpfrist_obj.SetActive(true);
    }


    void speedBtnCKFunction(int speedF)
    {
        int sf = speedF;
        switch (sf)
        {//0,1,2 - 3,4,5
            //느림
            case 8:
                speedBtn[0].GetComponent<Image>().sprite = speedbtnCK[0];
                speedBtn[1].GetComponent<Image>().sprite = speedbtnCK[4];
                speedBtn[2].GetComponent<Image>().sprite = speedbtnCK[5];
                break;
                
            //보통
            case 5:
                speedBtn[0].GetComponent<Image>().sprite = speedbtnCK[3];
                speedBtn[1].GetComponent<Image>().sprite = speedbtnCK[1];
                speedBtn[2].GetComponent<Image>().sprite = speedbtnCK[5];
                break;
            
            //빠름
            case 3:
                speedBtn[0].GetComponent<Image>().sprite = speedbtnCK[3];
                speedBtn[1].GetComponent<Image>().sprite = speedbtnCK[4];
                speedBtn[2].GetComponent<Image>().sprite = speedbtnCK[2];
                break;

        }

        PlayerPrefs.SetInt("speedTalkCheck", sf);
    }

}

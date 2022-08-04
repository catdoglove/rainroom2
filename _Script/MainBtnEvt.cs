using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainBtnEvt : CavasData
{

    public GameObject[] MainBtn_obj;
    public GameObject[] MainWindow_obj;
    public Text test_txt;

    public GameObject close_obj;
    public GameObject backBlackImg_obj;

    //메뉴펼쳐주기
    public GameObject menuBack_obj, menuBefore_obj, menuAfter_obj, showBefore_obj, showAfter_obj;
    public Vector2 menuBack_vet;
    public GameObject GM, GM2, GMtag;

    //문장속도
    float speedF = 0.05f;
    public Text tspeed_txt;
    public GameObject speed_obj, speed_toast;
    public GameObject[] speedBtn;
    public Sprite[] speedbtnCK;

    //닫을때같이닫는것
    public GameObject YN_obj;

    //도움말
    public GameObject Help_obj;
    public Sprite[] help_spr;
    public Sprite[] helpf_spr;
    public GameObject helpfrist_obj;
    public GameObject helpPark_obj, helpCity_obj;
    public Sprite[] helpP_spr, helpC_spr;
    int help = 0;

    //외출
    public GameObject comeHome_obj;
    public GameObject shop_obj, goHome_obj;

    //화폐변환
    public GameObject h_obj, c_obj, d_obj;
    //미리 씬을 불러오기
    AsyncOperation async;

    //쿠폰
    public GameObject coupon_obj, couponTxt_obj, couponTxtF_obj;
    public InputField InputField;
    public Text InputField_txt, couponeMsg;

    public GameObject reformHelp_obj, StorageHelp_obj;


    //상세표시
    public GameObject have_obj, showCheck_obj, talkChange_obj, showImage_obj;
    public Text rain_txt, horRain_txt, heart_txt;
    public Sprite showHave1_spr, showHave2_spr, talkChange1_spr, talkChange2_spr;
    public Sprite[] heart_spr;
    public Vector2 showHave_vet;

    //보물찾기
    public GameObject[] tre_obj;
    public GameObject treWin_obj, treDone_obj;

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
        else if (help == 2)
        {
            helpfrist_obj.GetComponent<Image>().sprite = helpf_spr[3];
            help = 3;
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
        HMemoryDestroy();
    }

    void HMemoryDestroy()
    {
        help_spr[0] = null;
        help_spr[1] = null;
        help_spr[2] = null;
        Help_obj.GetComponent<Image>().sprite = null;
        Resources.UnloadUnusedAssets();
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
                GM.GetComponent<NoteStoreFunction>().noteWindow_obj.SetActive(false);
                GM.GetComponent<NoteStoreFunction>().noteWriteOKBtn_obj.SetActive(false);
                GM.GetComponent<NoteStoreFunction>().titleWriteOKBtn_obj.SetActive(false);
                GM.GetComponent<NoteStoreFunction>().StopCoroutine("moveC");
                GM.GetComponent<NoteStoreFunction>().StopCoroutine("Lines");
                GM.GetComponent<FirstRoomFunction>().closeTitle();
                GM.GetComponent<WindowMiniGame>().CloseMiniGame();
                GM.GetComponent<FirstRoomFunction>().closeBeadal();
                GM.GetComponent<UnityADS>().closeAdYN();
                GM.GetComponent<FirstRoomBookList>().closeItemList();
                GM.GetComponent<FirstRoomSticker>().CloseFrame();
                GM.GetComponent<FirstRoomFunction>().boxNo();
                GM.GetComponent<SleepTime>().CloseSleep();
                //GM.GetComponent<MainShop>().palette_obj.SetActive(false);
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
    void Start()
    {
        menuBack_vet = menuBack_obj.transform.position;
        PlayerPrefs.SetFloat("mbx", menuBack_vet.x);
        menuBack_vet = menuBefore_obj.transform.position;
        PlayerPrefs.SetFloat("mby1", menuBack_vet.y);
        menuBack_vet = menuAfter_obj.transform.position;
        PlayerPrefs.SetFloat("mby2", menuBack_vet.y);
        menuBack_vet = menuBack_obj.transform.position;

        showHave_vet = have_obj.transform.position;
        PlayerPrefs.SetFloat("shx", showHave_vet.x);
        showHave_vet = showBefore_obj.transform.position;
        PlayerPrefs.SetFloat("shy1", showHave_vet.y);
        showHave_vet = showAfter_obj.transform.position;
        PlayerPrefs.SetFloat("shy2", showHave_vet.y);
        showHave_vet = have_obj.transform.position; ;

        PlayerPrefs.SetInt("showhavec", 1);
        //돈표시끄고키기
        if (PlayerPrefs.GetInt("showmehave", 0) == 1)
        {
            have_obj.SetActive(true);
            showCheck_obj.GetComponent<Image>().sprite = showHave2_spr;
            talkChange_obj.GetComponent<Image>().sprite = talkChange2_spr;
        }

        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        speedBtnCKFunction(PlayerPrefs.GetInt("speedTalkCheck", 0));

        if (GM == null)
        {
            GM = GameObject.FindGameObjectWithTag("firstroomGM");
        }
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            GM2 = GameObject.FindGameObjectWithTag("GM2");
        }
        setScreen();
        StartCoroutine("updateSec");
        //처음코드설정


        #region
        int c = 0;
        string str = "";
        if (c == PlayerPrefs.GetInt("first", 0))
        {

            for (int i = 0; i < 16; i++)
            {
                int a = Random.Range(0, 16);//0~15

                switch (a)
                {
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

            PlayerPrefs.SetString("code", str);
            PlayerPrefs.SetInt("bookbox", 10);
            PlayerPrefs.SetInt("deskbox", 10);
            PlayerPrefs.SetInt("bedbox", 10);
            PlayerPrefs.SetInt("cabinetbox", 10);
            PlayerPrefs.SetInt("drawerbox", 10);

            PlayerPrefs.SetInt("iceboxbox", 10);
            PlayerPrefs.SetInt("gasrangebox", 10);
            PlayerPrefs.SetInt("drawerbox", 10);
            PlayerPrefs.SetInt("seedbox", -10);


            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.Save();
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
    public void showButtons()
    {
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
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

    public void openInfoWindow()
    {
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
    public void openShopWindow()
    {
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
    public void openOptionWindow()
    {
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
    public void allClose()
    {
        MainWindow_obj[0].SetActive(false);
        MainWindow_obj[1].SetActive(false);
        MainWindow_obj[2].SetActive(false);
        MainWindow_obj[3].SetActive(false);
        backBlackImg_obj.SetActive(false);
        close_obj.SetActive(false);
        speed_obj.SetActive(false);
        YN_obj.SetActive(false);
        Help_obj.SetActive(false);
        HMemoryDestroy();
        helpfrist_obj.SetActive(false);
        if (PlayerPrefs.GetInt("outtrip", 0) == 1)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("parkGM");
            }
            GM2.GetComponent<FirstRoomSticker>().ReleseImage();
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("cityGM");
            }
            GM2.GetComponent<FirstRoomSticker>().ReleseImage();
        }
        else if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("GM2");
            }
            GM2.GetComponent<FirstRoomSticker>().ReleseImage();
        }
        else if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            if (GM == null)
            {
                GM = GameObject.FindGameObjectWithTag("firstroomGM");
            }
            GM.GetComponent<FirstRoomSticker>().ReleseImage();
        }
    }


    public void windowsOpen()
    {
        backBlackImg_obj.SetActive(true);
        close_obj.SetActive(true);
    }


    void setScreen()
    {
        //스크린화면해상도에맞춰조절,화면꺼지지않게
        #region

        /*
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
        */

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        #endregion

    }


    public void Sight()
    {
        if (PlayerPrefs.GetInt("outtrip", 0) == 1)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("parkGM");
            }
            GM2.GetComponent<Parkfunction>().changeSight();
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("cityGM");
            }
            GM2.GetComponent<CityFunction>().changeSight();
        }
        else
        {
            HomeSight();
        }
    }

    void HomeSight()
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

        menuBack_vet.x = PlayerPrefs.GetFloat("mbx", 0);
        while (menuBack_vet.y >= (PlayerPrefs.GetFloat("mby2", 0) + 0.55f))
        {
            menuBack_vet.y = menuBack_vet.y - 0.6f;
            menuBack_obj.transform.position = menuBack_vet;
            yield return null;
        }
        menuBack_vet.y = PlayerPrefs.GetFloat("mby2", 0);
        menuBack_obj.transform.position = menuBack_vet;
    }

    IEnumerator menuFlowBack()
    {
        menuBack_vet = menuBack_obj.transform.position;
        menuBack_vet.x = PlayerPrefs.GetFloat("mbx", 0);
        while (menuBack_vet.y <= PlayerPrefs.GetFloat("mby1", 0))
        {
            menuBack_vet.y = menuBack_vet.y + 0.6f;
            menuBack_obj.transform.position = menuBack_vet;
            yield return null;
        }
        menuBack_vet.y = PlayerPrefs.GetFloat("mby1", 0);
        menuBack_obj.transform.position = menuBack_vet;
    }


    //돈표시


    IEnumerator menuFlowInfo()
    {
        showHave_vet = have_obj.transform.position;
        showHave_vet.x = PlayerPrefs.GetFloat("shx", 0);
        while (showHave_vet.y >= (PlayerPrefs.GetFloat("shy2", 0) + 0.55f))
        {
            showHave_vet.y = showHave_vet.y - 0.6f;
            have_obj.transform.position = showHave_vet;
            yield return null;
        }
        showHave_vet.y = PlayerPrefs.GetFloat("shy2", 0);
        have_obj.transform.position = showHave_vet;
    }

    IEnumerator menuFlowBackInfo()
    {
        showHave_vet = have_obj.transform.position;
        showHave_vet.x = PlayerPrefs.GetFloat("shx", 0);
        while (showHave_vet.y <= PlayerPrefs.GetFloat("shy1", 0))
        {
            showHave_vet.y = showHave_vet.y + 0.6f;
            have_obj.transform.position = showHave_vet;
            yield return null;
        }
        showHave_vet.y = PlayerPrefs.GetFloat("shy1", 0);
        have_obj.transform.position = showHave_vet;
    }


    /// <summary>
    /// 상세를펼쳐준다
    /// </summary>
    public void showHave()
    {
        if (PlayerPrefs.GetInt("achievemove", 0) == 0)
        {
            if (PlayerPrefs.GetInt("showhavec", 0) == 1)
            {
                StopCoroutine("menuFlowBackInfo");
                StartCoroutine("menuFlowInfo");
                PlayerPrefs.SetInt("showhavec", 0);
            }
            else
            {
                StopCoroutine("menuFlowInfo");
                StartCoroutine("menuFlowBackInfo");
                PlayerPrefs.SetInt("showhavec", 1);
            }
        }
    }




    public void TalkSpeedFast()
    {
        speedF = 0.001f;
        StartCoroutine("closeToast");
    }

    public void TalkSpeedNor()
    {
        speedF = 0.04f;
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

        if (speedF == 0.001f)
        {
            tspeed_txt.text = "대화 속도 '빠름'으로 변경";
            speedBtnCKFunction(3);
        }
        else if (speedF == 0.04f)
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


    //토스트
    IEnumerator closetreToast()
    {
        
        PlayerPrefs.Save();
        
            tspeed_txt.text = "다락방에 무언가 생겼다";
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
        help_spr[0] = Resources.Load<Sprite>("UI/menu/help05");
        Help_obj.GetComponent<Image>().sprite = help_spr[0];
    }
    public void OpenHelpInfo()
    {
        Help_obj.SetActive(true);
        help_spr[1] = Resources.Load<Sprite>("UI/menu/help04");
        Help_obj.GetComponent<Image>().sprite = help_spr[1];
    }
    public void OpenHelpOption()
    {
        Help_obj.SetActive(true);
        help_spr[2] = Resources.Load<Sprite>("UI/menu/help06");
        Help_obj.GetComponent<Image>().sprite = help_spr[2];
    }

    public void OpenHelpf()
    {

        if (PlayerPrefs.GetInt("outtrip", 0) == 1)
        {
            helpPark_obj.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            helpCity_obj.SetActive(true);
        }
        else
        {
            helpfrist_obj.SetActive(true);
        }

    }

    public void CloseHelpP()
    {
        if (help == 0)
        {
            help = 1;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[1];
        }
        else
        {
            help = 0;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[0];
            helpPark_obj.SetActive(false);
        }

    }
    public void CloseHelpC()
    {
        if (help == 0)
        {
            helpCity_obj.GetComponent<Image>().sprite = helpC_spr[1];
            help = 1;
        }
        else
        {
            help = 0;
            helpCity_obj.GetComponent<Image>().sprite = helpC_spr[0];
            helpCity_obj.SetActive(false);
        }
    }

    //쿠폰입력
    public void openCoupon()
    {
        coupon_obj.SetActive(true);
        InputField.text = "";
        InputField_txt.text = "";
    }
    public void SetCoupon()
    {

        couponTxtF_obj.SetActive(true);
        /*
        if (InputField_txt.text == "mask4you")
        {
            if (PlayerPrefs.GetInt("s200518", 0) == 0)
            {
                string str = PlayerPrefs.GetString("code", "");
                //쿠폰보상
                int h = PlayerPrefs.GetInt(str + "ht", 0) + 5;
                PlayerPrefs.SetInt(str + "ht", h);

                int hr = PlayerPrefs.GetInt(str + "h", 0) + 100;
                PlayerPrefs.SetInt(str + "h", hr);

                PlayerPrefs.SetInt("s200518", 999);
                couponTxt_obj.SetActive(true);
                coupon_obj.SetActive(false);
            }
            else
            {
                couponTxt_obj.SetActive(true);
                couponeMsg.text = "이미 지급되었습니다.";
            }
        }
        else
        {
            couponTxtF_obj.SetActive(true);
        }
        */

        PlayerPrefs.Save();
    }

    /*if(InputField_txt.text== "welcomerain2")
        {
            if (PlayerPrefs.GetInt("609102", 0) == 0)
            {
                string str = PlayerPrefs.GetString("code", "");
                //쿠폰보상
                int h = PlayerPrefs.GetInt(str + "ht", 0) + 5;
                PlayerPrefs.SetInt(str + "ht", h);
                PlayerPrefs.SetInt("609102", 60);
                couponTxt_obj.SetActive(true);
                coupon_obj.SetActive(false);
            }
        }

        
        if (InputField_txt.text == "happynewyear")
        {
            if (PlayerPrefs.GetInt("200103", 0) == 0)
            {
                string str = PlayerPrefs.GetString("code", "");
                //쿠폰보상
                int d = PlayerPrefs.GetInt(str + "dm", 0) + 3;
                int c = PlayerPrefs.GetInt(str + "cv", 0) + 3;
                int h = PlayerPrefs.GetInt(str + "ht", 0) + 3;
                PlayerPrefs.SetInt(str + "dm", d);
                PlayerPrefs.SetInt(str + "cv", c);
                PlayerPrefs.SetInt(str + "ht", h);
                PlayerPrefs.SetInt("200103", 999);
                couponTxt_obj.SetActive(true);
                coupon_obj.SetActive(false);
            }
        */

    public void closeCoupon()
    {
        coupon_obj.SetActive(false);
        couponTxt_obj.SetActive(false);
        couponTxtF_obj.SetActive(false);
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

    public void goHome()
    {
        MemoryDestroy();

        //외출중
        PlayerPrefs.SetInt("outorhome", 0);
        PlayerPrefs.SetInt("outtrip", 0);
        PlayerPrefs.SetInt("front", 2);
        StartCoroutine("LoadOut");
        comeHome_obj.SetActive(false);
        shop_obj.SetActive(true);
        goHome_obj.SetActive(false);
    }

    public void OpenGoHome()
    {
        goHome_obj.SetActive(true);
        StopCoroutine("menuFlow");
        StartCoroutine("menuFlowBack");
        StopCoroutine("menuFlowInfo");
        StartCoroutine("menuFlowBackInfo");
        PlayerPrefs.SetInt("showhavec", 1);

        MainBtn_obj[0].SetActive(true);
    }
    public void CloseGoHome()
    {
        goHome_obj.SetActive(false);
    }

    /// <summary>
    /// 집으로 돌아가기 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }


    //화폐변환
    public void SetClover()
    {
        h_obj.SetActive(false);
        c_obj.SetActive(true);
        d_obj.SetActive(false);
    }
    public void SetDiamond()
    {
        h_obj.SetActive(false);
        c_obj.SetActive(false);
        d_obj.SetActive(true);
    }
    public void SetH()
    {
        h_obj.SetActive(true);
        c_obj.SetActive(false);
        d_obj.SetActive(false);
    }

    //리폼창도움말
    public void ActReformHelp()
    {
        if (reformHelp_obj.activeSelf == true)
        {
            reformHelp_obj.SetActive(false);
        }
        else
        {
            reformHelp_obj.SetActive(true);
        }
    }


    //리폼창도움말
    public void ActStorageHelp()
    {
        if (StorageHelp_obj.activeSelf == true)
        {
            StorageHelp_obj.SetActive(false);
        }
        else
        {
            StorageHelp_obj.SetActive(true);
        }
    }

    public void ActShowHave()
    {
        if (PlayerPrefs.GetInt("showmehave", 0) == 1)
        {
            showCheck_obj.GetComponent<Image>().sprite = showHave1_spr;
            talkChange_obj.GetComponent<Image>().sprite = talkChange1_spr;
            have_obj.SetActive(false);
            PlayerPrefs.SetInt("showmehave", 0);
        }
        else
        {
            showCheck_obj.GetComponent<Image>().sprite = showHave2_spr;
            talkChange_obj.GetComponent<Image>().sprite = talkChange2_spr;
            have_obj.SetActive(true);
            PlayerPrefs.SetInt("showmehave", 1);
        }
    }
    public void closeShow()
    {
        StopCoroutine("menuFlowInfo");
        StartCoroutine("menuFlowBackInfo");
    }

    IEnumerator updateSec()
    {
        int w = 0;
        while (w == 0)
        {

            //최대량 제한
            string str = PlayerPrefs.GetString("code", "");

            if (PlayerPrefs.GetInt(str + "c", 0) > 999999)
            {
                PlayerPrefs.SetInt(str + "c", 999999);
            }
            if (PlayerPrefs.GetInt(str + "h", 0) > 99999)
            {
                PlayerPrefs.SetInt(str + "h", 99999);
            }
            if (PlayerPrefs.GetInt(str + "ht", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "ht", 999);
            }
            if (PlayerPrefs.GetInt(str + "dm", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "dm", 999);
            }
            if (PlayerPrefs.GetInt(str + "cv", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "cv", 999);
            }


            rain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0); ;
            horRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0); ;

            if (PlayerPrefs.GetInt("outtrip", 0) == 1)
            {
                showImage_obj.GetComponent<Image>().sprite = heart_spr[1];
                heart_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0); ;
            }
            else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
            {
                showImage_obj.GetComponent<Image>().sprite = heart_spr[2];
                heart_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0); ;
            }
            else
            {
                showImage_obj.GetComponent<Image>().sprite = heart_spr[0];
                heart_txt.text = "" + PlayerPrefs.GetInt(str + "ht", 0); ;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void showInfoLink()
    {
        Application.OpenURL("https://bit.ly/3uMwAsU");
    }

    /// <summary>
    /// 메모리 해제 함수
    /// </summary>
    void MemoryDestroy()
    {
        if (PlayerPrefs.GetInt("outtrip", 0) == 1) //공원
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("parkGM");
            }
            GM2.GetComponent<ParkTime>().bg_front.GetComponent<Image>().sprite = null;
            GM2.GetComponent<ParkTime>().bg_back.GetComponent<Image>().sprite = null;
            GM2.GetComponent<ParkTime>().moonbangu_img.GetComponent<Image>().sprite = null;
            GM2.GetComponent<ParkTime>().bas_obj.GetComponent<Image>().sprite = null;
            GM2.GetComponent<Parkfunction>().blackimg.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2) //도시
        {
            if (GM2 == null)
            {
                GM2 = GameObject.FindGameObjectWithTag("cityGM");
            }
            GM2.GetComponent<CityTime>().bg_front.GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityTime>().bg_back.GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityShop>().interiorTape_obj[0].GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityShop>().interiorTape_obj[1].GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityShop>().interiorTape_obj[2].GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityShop>().interiorTape_obj[3].GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityShop>().interiorTape_obj[4].GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityFunction>().noteSign_obj.GetComponent<Image>().sprite = null;
            GM2.GetComponent<CityFunction>().blackimg.SetActive(true);
        }

    }

    //모은쪽지 확인
    public void CheckTre()
    {
        int a = 0;
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("gettre" + i, 0) == 1)
            {
                tre_obj[i].SetActive(true);
                a++;
            }
        }
        if (a >= 6)
        {
            treDone_obj.SetActive(true);
            PlayerPrefs.SetInt("putmap", 1);
        }
        treWin_obj.SetActive(true);
    }


    public void CloseTre()
    {
        treWin_obj.SetActive(false);
        if (PlayerPrefs.GetInt("putmap" , 0) == 1)
        {
            StartCoroutine("closetreToast");
        }
    }

}

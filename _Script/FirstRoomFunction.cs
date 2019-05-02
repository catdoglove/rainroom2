using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomFunction : CavasData {
    //앞뒤바꾸기
    public GameObject character_obj;

    //배달
    public GameObject beadalWindow_obj, beadalType1_obj, beadalType2_obj, beadalYesNo_obj, beadalIllust_obj, beadalFood_obj;
    public int buyFood_i, beadalType_i;
    public int point_i;
    public Sprite[] beadalYN_spr,beadalFood_spr;
    public Sprite beadalT1_spr, beadalT2_spr;
    public GameObject dish_obj, beadalYet_obj;
    public Text[] heart_txt;

    public GameObject beadalTime_obj;

    //쿠폰
    public GameObject[] couponType1_obj, couponType2_obj, couponComplete_obj;
    //public GameObject useCoupon_obj;
    public GameObject coupon_obj;
    public GameObject couponFood_obj;
    public Sprite[] couponFood_spr;
    
    //모두모으면 버튼이나타나게 한다

	public GameObject GMNotdistroy, firstGM;


    public GameObject needToast_obj, beadalYetToast_obj;
    Color colorN,colorB,colorL;

    //하트
    public int heart_i;

    public Text boxHeart_txt;
    public Text boxTotal_txt;

    public GameObject[] fisrtRoomItem_obj;


    public int window_i, book_i, bed_i, desk_i, stand_i, tapestry_i, rug_i, poster_i, cabinet_i, wall_i;
    public GameObject windowImg_obj, bookImg_obj, deskImg_obj, standImg_obj, tapestryImg_obj, bedImg_obj, rugImg_obj, cabinetImg_obj, rugImg2_obj, wallImg_obj, wallImg2_obj;
    public GameObject ladderImg_obj;
    public Sprite ladder_spr;
    public GameObject moreCoinWindow_obj;
    public GameObject bookcase_obj;
    public Sprite bookcase_spr;

    public int bookBox_i;
    public GameObject bookBox_obj, bedBox_obj, deskBox_obj, cabinetBox_obj, ladderBox_obj;
    public GameObject needMore_obj;
    public GameObject boxClean_obj;
    //public Sprite[] boxItem_spr;
    public GameObject boxLv_obj;
    
    public string boxName_str;
    public int boxs_i;
    public Text boxTxt_txt, boxneed_txt;

    public GameObject loadGM;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    //업적액자
    public GameObject frame_obj;
    public Sprite frameOpen_spr;


    //도움말열기
    public GameObject Help_obj;
    public Sprite[] help_spr;

    public GameObject GM;

    //밤
    public GameObject dayRoom;

    //소리
    public GameObject audio_obj;


    //타이틀닫기
    public GameObject titleImg;
    public void closeTitle()
    {
        titleImg.SetActive(false);
    }



    // Use this for initialization
    void Start () {
       // PlayerPrefs.SetInt("downst", 0);
        //PlayerPrefs.SetInt("countladderst", 0);
        //만약 잠을 자고 있다면 들어왔을때 첫화면이 침대쪽 화면으로
        if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
        {
            changeSight();
            characterTurn();
        }
        colorN = new Color(1f,1f,1f);
        colorB = new Color(1f, 1f, 1f);
        colorL = new Color(1f, 1f, 1f);
        PlayerPrefs.SetInt("place", 0);
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;

        
        //장소초기화
        PlayerPrefs.SetInt("place", 0);

        //로딩화면에서 불러온 정보를 찾아오기 위해서 태그로 지엠을 찾아준다
        GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");
        loadGM = GameObject.Find("loadGM");

        //방에 처음 들어왔을때 각각 단계에 따라 이미지 바꿔주기
        
		//window_i = PlayerPrefs.GetInt ("windowlv", 0);
		book_i = PlayerPrefs.GetInt ("booklv",0);
		bed_i = PlayerPrefs.GetInt ("bedlv",0);
		rug_i = PlayerPrefs.GetInt ("ruglv",0);
        wall_i = PlayerPrefs.GetInt("walllv", 0);
		poster_i = PlayerPrefs.GetInt ("posterlv",0);
		desk_i = PlayerPrefs.GetInt ("desklv",0);
		tapestry_i = PlayerPrefs.GetInt ("tapestrylv",0);
		stand_i = PlayerPrefs.GetInt ("standlv",0);
        cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
        if(PlayerPrefs.GetInt("frameopen", 0) == 1)
        {
            frame_obj.GetComponent<Image>().sprite = frameOpen_spr;
            frame_obj.GetComponent<Button>().interactable = true;
        }

        //여기에 박스인것들은 대화버튼들 비활성화시켜놓기
        //박스
        if (PlayerPrefs.GetInt("bedbox", 0)==10)
        {
            bedBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("cabinetbox", 0) == 10)
        {
            cabinetBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("deskbox", 0) == 10)
        {
            deskBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("bookbox", 0) == 10)
        {
            bookBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("ladderbox", 0) == 0)
        {
            ladderBox_obj.SetActive(true);
        }
        setItems();

        //낮밤
        setDay();
    }

    public void setItems()
    {
        //windowImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData> ().window_spr [window_i];
        if (PlayerPrefs.GetInt("booklv", 0) == 15)
        {
            bookcase_obj.GetComponent<Image>().sprite = bookcase_spr;
            bookcase_obj.SetActive(true);
            bookImg_obj.SetActive(false);
        }
        else
        {
            bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
        }
        bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
        deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
        //standImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().stand_spr [stand_i];
        rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
        rugImg2_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
        wallImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall_spr[wall_i];
        wallImg2_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall2_spr[wall_i];
        //deskImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().desk_spr [desk_i];
        //tapestryImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().tapestry_spr [tapestry_i];
        cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];
        if (ladderBox_obj.activeSelf == false)
        {
            ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
        }
    }

    //전단지열기
    public void openBeadal(){
        ShowCoupon();
        if (PlayerPrefs.GetInt("beadal", 0)==0)
        {
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
            heart_txt[0].text = "" + heart_i;
            heart_txt[1].text = "" + heart_i;
            if (beadalWindow_obj.activeSelf == true)
            {
                beadalWindow_obj.SetActive(false);
            }
            else
            {
                beadalWindow_obj.SetActive(true);
            }
        }
        else
        {
            StopCoroutine("toastBImgFadeOut");
            beadalYet_obj.SetActive(true);
            StartCoroutine("toastBImgFadeOut");
            //아직배부름
        }
	}


    //배달시키기
#region
    public void BuyFood1() {
        buyFood_i = 1;
    }
    public void BuyFood2()
    {
        buyFood_i = 2;
    }
    public void BuyFood3()
    {
        buyFood_i = 3;
    }
    public void BuyFood4()
    {
        buyFood_i = 4;
    }
    public void BuyFood5()
    {
        buyFood_i = 5;
    }
    public void BuyFood6()
    {
        buyFood_i = 6;
    }
    public void BuyFood7()
    {
        buyFood_i = 7;
    }
    public void BuyFood8()
    {
        buyFood_i = 8;
    }
#endregion
    //어떤음식인지 받아온 숫자로 판단해서 그음식에 맞게 처리를 해준다
    public void BuyFoodYes()
    {
        beadalYesNo_obj.SetActive(false);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        switch (buyFood_i)
        {
            case 1:
                if (heart_i >= 4)
                {
                    heart_i = heart_i - 4;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 3;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (heart_i >= 6)
                {
                    heart_i = heart_i - 6;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 7;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 7;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 9;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 4:
                if (heart_i >= 8)
                {
                    heart_i = heart_i - 8;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 11;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 5:
                if (heart_i >= 6)
                {
                    heart_i = heart_i - 6;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 7;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 6:
                if (heart_i >= 8)
                {
                    heart_i = heart_i - 8;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 11;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 7:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 7;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 9;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 8:
                if (heart_i >= 10)
                {
                    heart_i = heart_i - 10;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 13;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
        }
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        heart_txt[0].text = "" + heart_i;
        heart_txt[1].text = "" + heart_i;
        PlayerPrefs.Save();
    }

    void BeadalYesF()
    {
        PlayerPrefs.SetInt("beadal", 1);
        PlayerPrefs.SetInt("lovepoint", point_i);
        closeBeadal();
        beadalIllust_obj.SetActive(true);
        PlayerPrefs.SetString("foodLastTime", System.DateTime.Now.ToString());
        audio_obj.GetComponent<SoundEvt>().foodSound();
    }

    public void CleanDish()
    {
        float xx = dish_obj.transform.position.x;
        float yy = dish_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        PlayerPrefs.SetInt("dishw", 1);
        GM.GetComponent<GetFadeout>().getRainFade();
        dish_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        coldRain_i = coldRain_i + 20;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.Save();
    }

    public void CloseBeadalIllust()
    {
        beadalIllust_obj.SetActive(false);
        dish_obj.SetActive(true);
        closeBeadal();
    }

    public void buyFoodNo()
    {
        beadalYesNo_obj.SetActive(false);
    }

    public void beadalType1()
    {
        beadalType1_obj.SetActive(true);
        beadalType_i = 0;
    }
    public void beadalType2()
    {
        beadalType2_obj.SetActive(true);
        beadalType_i = 1;
    }

    public void BeadalTypeClose()
    {
        beadalType1_obj.SetActive(false);
        beadalType2_obj.SetActive(false);
        beadalYesNo_obj.SetActive(false);
    }

    public void closeBeadal(){
		beadalWindow_obj.SetActive (false);
        beadalType1_obj.SetActive(false);
        beadalType2_obj.SetActive(false);
        beadalYesNo_obj.SetActive(false);
    }

    public void OpenBeadalYN()
    {
        beadalFood_obj.GetComponent<Image>().sprite = beadalFood_spr[buyFood_i];
        beadalYesNo_obj.SetActive(true);
        if (beadalType_i == 0)
        {
            beadalYesNo_obj.GetComponent<Image>().sprite = beadalT1_spr;
        }
        else
        {

            beadalYesNo_obj.GetComponent<Image>().sprite = beadalT2_spr;
        }
    }

    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

	public void boxOpen(){
        boxTxt_txt.text = ""+ boxs_i;

        boxClean_obj.SetActive (true);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTotal_txt.text = ""+heart_i;
	}

    public void boxBed()
    {
        boxName_str = "bed";
        boxneed_txt.text = "";
        boxs_i = 3;
    }
    public void boxCabinet()
    {
        boxName_str = "cabinet";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxBook()
    {
        boxName_str = "book";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxDesk()
    {
        boxName_str = "desk";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxLadder()
    {
        boxName_str = "ladder";
        boxneed_txt.text = "호감Lv.3 달성하기";
        boxs_i = 5;
    }

    public void boxYes(){

		string str1;
		str1 = PlayerPrefs.GetString ("code", "");
        heart_i = PlayerPrefs.GetInt (str1+"ht", 0);
        if (boxName_str == "ladder")
        {
            if (PlayerPrefs.GetInt("lovelv", 0) >= 3)
            {
                if (heart_i >= boxs_i)
                {
                    heart_i = heart_i - boxs_i;
                    boxHeart_txt.text = "" + boxs_i;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    PlayerPrefs.SetInt(boxName_str + "box", 1);
                    PlayerPrefs.SetInt(boxName_str + "lv", 1);
                    PlayerPrefs.Save();
                    //소리
                    audio_obj.GetComponent<SoundEvt>().boxSound();
                    if (PlayerPrefs.GetInt("bedbox", 0) == 1)
                    {
                        bedBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("cabinetbox", 0) == 1)
                    {
                        cabinetBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("deskbox", 0) == 1)
                    {
                        deskBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("bookbox", 0) == 1)
                    {
                        bookBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("ladderbox", 0) == 1)
                    {
                        ladderBox_obj.SetActive(false);
                        ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
                    }
                    book_i = PlayerPrefs.GetInt("booklv", 0);
                    if (book_i >= 15)
                    {

                    }
                    else
                    {
                        bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
                    }
                    bed_i = PlayerPrefs.GetInt("bedlv", 0);
                    desk_i = PlayerPrefs.GetInt("desklv", 0);
                    cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
                    rug_i = PlayerPrefs.GetInt("ruglv", 0);

                    bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
                    deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
                    cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];

                    rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
                    boxClean_obj.SetActive(false);
                    checkach();
                }
                else
                {
                    needMoney();
                    boxClean_obj.SetActive(false);
                    audio_obj.GetComponent<SoundEvt>().cancleSound();
                }
            }
            else
            {
                StopCoroutine("toastLadderFadeOut");
                StartCoroutine("toastLadderFadeOut");
                boxLv_obj.SetActive(true);
            }
        }
        else
        {

            if (heart_i >= boxs_i)
            {
                heart_i = heart_i - boxs_i;
                boxHeart_txt.text = "" + boxs_i;
                PlayerPrefs.SetInt(str1 + "ht", heart_i);
                PlayerPrefs.SetInt(boxName_str + "box", 1);
                PlayerPrefs.SetInt(boxName_str + "lv", 1);
                PlayerPrefs.Save();
                //소리
                audio_obj.GetComponent<SoundEvt>().boxSound();
                if (PlayerPrefs.GetInt("bedbox", 0) == 1)
                {
                    bedBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("cabinetbox", 0) == 1)
                {
                    cabinetBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("deskbox", 0) == 1)
                {
                    deskBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("bookbox", 0) == 1)
                {
                    bookBox_obj.SetActive(false);

                }
                if (PlayerPrefs.GetInt("ladderbox", 0) == 1)
                {
                    ladderBox_obj.SetActive(false);
                    ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
                }
                book_i = PlayerPrefs.GetInt("booklv", 0);
                bed_i = PlayerPrefs.GetInt("bedlv", 0);
                desk_i = PlayerPrefs.GetInt("desklv", 0);
                cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
                rug_i = PlayerPrefs.GetInt("ruglv", 0);
                if (book_i<15)
                {
                    bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
                }
                bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
                deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
                cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];

                rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
                boxClean_obj.SetActive(false);

                checkach();
            }
            else
            {
                needMoney();
                audio_obj.GetComponent<SoundEvt>().cancleSound();
                boxClean_obj.SetActive(false);
            }

        }
    }

	public void boxNo(){
		boxClean_obj.SetActive (false);
        needMore_obj.SetActive(false);
    }

    public void closeNeed()
    {
        needMore_obj.SetActive(false);
        beadalYet_obj.SetActive(false);
    }
    
    IEnumerator toastBImgFadeOut()
    {
        colorB.a = Mathf.Lerp(0f, 1f, 1f);
        beadalYetToast_obj.GetComponent<Image>().color = colorB;
        beadalTime_obj.GetComponent<Image>().color = colorB;
        beadalYetToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorB.a = Mathf.Lerp(0f, 1f, i);
            beadalYetToast_obj.GetComponent<Image>().color = colorB;
            beadalTime_obj.GetComponent<Image>().color = colorB;
            yield return null;
        }
        beadalYetToast_obj.SetActive(false);
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorN.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorN;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorN.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorN;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }
    //사다리페이드아웃
    IEnumerator toastLadderFadeOut()
    {
        colorL.a = Mathf.Lerp(0f, 1f, 1f);
        boxLv_obj.GetComponent<Image>().color = colorL;
        boxLv_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorL.a = Mathf.Lerp(0f, 1f, i);
            boxLv_obj.GetComponent<Image>().color = colorL;
            yield return null;
        }
        boxLv_obj.SetActive(false);
    }

    public void OpenCoupon()
    {
        if (coupon_obj.activeSelf == true)
        {
            coupon_obj.SetActive(false);
        }
        else
        {
            if (beadalType_i == 0)
            {
                couponFood_obj.GetComponent<Image>().sprite = couponFood_spr[0];
            }
            else
            {
                couponFood_obj.GetComponent<Image>().sprite = couponFood_spr[1];

            }
            coupon_obj.SetActive(true);
        }
    }
    public void OpenHelpBeadal()
    {
        Help_obj.SetActive(true);
        Help_obj.GetComponent<Image>().sprite = help_spr[0];
    }
    public void CloseHelp()
    {
        Help_obj.SetActive(false);
    }

    public void useCouponY()
    {
        audio_obj.GetComponent<SoundEvt>().foodSound();
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        point_i = point_i + 3;
        PayCoupon();
        BeadalYesF();
        coupon_obj.SetActive(false);
    }
    public void useCouponN()
    {
        coupon_obj.SetActive(false);
    }

    void ShowCoupon()
    {
        int coup = PlayerPrefs.GetInt("coupon1", 0);
        for(int i = 0; i < coup; i++)
        {
            couponType1_obj[i].SetActive(true);
        }
        if (coup == 10)
        {
            couponComplete_obj[0].SetActive(true);
        }
        coup = PlayerPrefs.GetInt("coupon2", 0);
        for (int i = 0; i < coup; i++)
        {
            couponType2_obj[i].SetActive(true);
        }
        if (coup == 10)
        {
            couponComplete_obj[1].SetActive(true);
        }
    }
    void PayCoupon()
    {
        if (beadalType_i == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                couponType1_obj[i].SetActive(false);
            }
            PlayerPrefs.SetInt("coupon1", 0);
            couponComplete_obj[0].SetActive(false);
        }
        
        if (beadalType_i == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                couponType2_obj[i].SetActive(false);
            }
            PlayerPrefs.SetInt("coupon2", 0);
            couponComplete_obj[1].SetActive(false);
        }
        
    }
    
    public void characterTurn()
    {
        if (character_obj.transform.rotation.y == 0)
        {
            character_obj.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            character_obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    
    //업적
    void checkach()
    {
        int cts = PlayerPrefs.GetInt("countboxst", 0);
        cts++;
        Debug.Log("boxst");
        PlayerPrefs.SetInt("countboxst", cts);
        if (cts >= 9 && PlayerPrefs.GetInt("boxst", 0) < 3)
        {
            PlayerPrefs.SetInt("boxst", 3);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 2);
        }
        else if (cts >= 5 && PlayerPrefs.GetInt("boxst", 0) < 2)
        {
            PlayerPrefs.SetInt("boxst", 2);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("boxst", 0) < 1)
        {
            PlayerPrefs.SetInt("boxst", 1);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 0);
        }
    }


    public void setDay()
    {
        System.DateTime time = System.DateTime.Now;
        if (time.ToString("tt") == "PM")
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k >= 6)
            {
                dayRoom.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k < 6)
            {
                dayRoom.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }

}

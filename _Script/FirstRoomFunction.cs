using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomFunction : CavasData {
    //앞뒤바꾸기
    public GameObject character_obj;

    //배달
    public GameObject beadalWindow_obj, beadalType1_obj, beadalType2_obj, beadalYesNo_obj, beadalIllust_obj, beadalFood_obj;
    public int buyFood_i;
    public int point_i;
    public Sprite[] beadalYN_spr,beadalFood_spr;
    public GameObject dish_obj, beadalYet_obj;
    public Text[] heart_txt;

    //쿠폰
    public GameObject[] couponType1_obj, couponType2_obj, couponComplete_obj;
    public GameObject useCoupon_obj;
    public GameObject coupon_obj;
    
    //모두모으면 버튼이나타나게 한다

	public GameObject GMNotdistroy;


    public GameObject needToast_obj, beadalYetToast_obj;
    Color color;

    //하트
    public int heart_i;

    public Text boxHeart_txt;

    public GameObject[] fisrtRoomItem_obj;


    public int window_i, book_i, bed_i, desk_i, stand_i, tapestry_i, rug_i, poster_i, cabinet_i, wall_i;
    public GameObject windowImg_obj, bookImg_obj, deskImg_obj, standImg_obj, tapestryImg_obj, bedImg_obj, rugImg_obj, cabinetImg_obj, rugImg2_obj, wallImg_obj, wallImg2_obj;
    public GameObject ladderImg_obj;
    public Sprite ladder_spr;
    public GameObject moreCoinWindow_obj;

    public int bookBox_i;
    public GameObject bookBox_obj, bedBox_obj, deskBox_obj, cabinetBox_obj, ladderBox_obj;
    public GameObject needMore_obj;
    public GameObject boxClean_obj;
    //public Sprite[] boxItem_spr;
    

    public string boxName_str;



    public GameObject loadGM;


    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    // Use this for initialization
    void Start () {

        color = new Color(1f,1f,1f);

        //string str1;
        //str1 = PlayerPrefs.GetString("code", "");
        //PlayerPrefs.SetInt(str1 + "ht", 999999);

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
    }

    public void setItems()
    {
        //windowImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData> ().window_spr [window_i];
        bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
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
                }
                else
                {
                    needMoney();
                }
                break;
            case 4:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 8;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 9;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
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
                }
                else
                {
                    needMoney();
                }
                break;
            case 6:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 7;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 8;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
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
    }

    public void CleanDish()
    {
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
    }
    public void beadalType2()
    {
        beadalType2_obj.SetActive(true);
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
    }

    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
    }

	public void boxOpen(){
		boxClean_obj.SetActive (true);
	}

    public void boxBed()
    {
        boxName_str = "bed";
    }
    public void boxCabinet()
    {
        boxName_str = "cabinet";
    }
    public void boxBook()
    {
        boxName_str = "book";
    }
    public void boxDesk()
    {
        boxName_str = "desk";
    }
    public void boxLadder()
    {
        boxName_str = "ladder";
    }

    public void boxYes(){

		string str1;
		str1 = PlayerPrefs.GetString ("code", "");
        int heart_i;
        heart_i = PlayerPrefs.GetInt (str1+"ht", 0);

		if (heart_i >= 3) {

            heart_i = heart_i - 3;
            boxHeart_txt.text = "" + 3;
            PlayerPrefs.SetInt (str1 + "ht", heart_i);
            PlayerPrefs.SetInt(boxName_str + "box", 1);
            PlayerPrefs.SetInt(boxName_str + "lv", 1);
            PlayerPrefs.Save ();

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
            
            bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
            bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
            deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
            cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];

            rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
            boxClean_obj.SetActive(false);
        } else {
            needMoney();
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
        color.a = Mathf.Lerp(0f, 1f, 1f);
        beadalYetToast_obj.GetComponent<Image>().color = color;
        beadalYetToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            beadalYetToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        beadalYetToast_obj.SetActive(false);

    }

    //토스트페이드아웃

    IEnumerator toastNImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = color;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }

    public void OpenCoupon()
    {
        if (coupon_obj.activeSelf == true)
        {
            coupon_obj.SetActive(false);
        }
        else
        {
            if (buyFood_i == 1)
            {

            }
            coupon_obj.SetActive(true);
        }
    }

    public void useCouponY()
    {
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        point_i = point_i + 3;
        PayCoupon();
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
        if (buyFood_i == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                couponType1_obj[i].SetActive(false);
            }
            PlayerPrefs.SetInt("coupon1", 0);
            couponComplete_obj[0].SetActive(false);
        }
        
        if (buyFood_i == 2)
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

  
}

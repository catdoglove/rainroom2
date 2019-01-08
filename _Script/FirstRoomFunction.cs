using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomFunction : CavasData {

    public GameObject beadalWindow_obj, beadalType1_obj, beadalType2_obj;
    public int point_i;
	

	public GameObject GMNotdistroy;

    //하트
    public int heart_i;

    public GameObject[] fisrtRoomItem_obj;


    public int window_i, book_i, bed_i, desk_i, stand_i, tapestry_i, rug_i, poster_i, cabinet_i, wall_i;
    public GameObject windowImg_obj, bookImg_obj, deskImg_obj, standImg_obj, tapestryImg_obj, bedImg_obj, rugImg_obj, cabinetImg_obj, rugImg2_obj, wallImg_obj, wallImg2_obj;

    public GameObject moreCoinWindow_obj;

    public int bookBox_i;
    public GameObject bookBox_obj, bedBox_obj, deskBox_obj, cabinetBox_obj;
    public GameObject needMore_obj;
    public GameObject boxClean_obj;
    //public Sprite[] boxItem_spr;
    
    

    public string boxName_str;



    public GameObject loadGM;

	// Use this for initialization
	void Start () {
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
    }

    //전단지열기
    public void openBeadal(){
        if (PlayerPrefs.GetInt("beadal", 0)==0)
        {
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
            //아직배부름
        }
	}

    //배달시키기
    public void BuyFood1() {
        //호감도 10~20상승
        //하트가격4~8
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        if (heart_i >= 4)
        {
            heart_i = heart_i - 4;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
            point_i = PlayerPrefs.GetInt("lovepoint", 0);
            point_i = point_i + 10;
            PlayerPrefs.SetInt("lovepoint", point_i);
        }
        else
        {
            needMore_obj.SetActive(true);
            //돈부족함
        }
    }
    public void BuyFood2()
    {
        //호감도 10~20상승
        //하트가격4~8
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        if (heart_i >= 5)
        {
            heart_i = heart_i - 5;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
            point_i = PlayerPrefs.GetInt("lovepoint", 0);
            point_i = point_i + 12;
            PlayerPrefs.SetInt("lovepoint", point_i);
        }
        else
        {
            needMore_obj.SetActive(true);
            //돈부족함
        }
    }
    public void BuyFood3()
    {
        //호감도 10~20상승
        //하트가격4~8
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        if (heart_i >= 7)
        {
            heart_i = heart_i - 7;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
            point_i = PlayerPrefs.GetInt("lovepoint", 0);
            point_i = point_i + 17;
            PlayerPrefs.SetInt("lovepoint", point_i);
        }
        else
        {
            needMore_obj.SetActive(true);
            //돈부족함
        }
    }

    public void BuyFood4()
    {
        //호감도 10~20상승
        //하트가격4~8
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        if (heart_i >= 8)
        {
            heart_i = heart_i - 8;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
            point_i = PlayerPrefs.GetInt("lovepoint", 0);
            point_i = point_i + 20;
            PlayerPrefs.SetInt("lovepoint", point_i);
        }
        else
        {
            needMore_obj.SetActive(true);
            //돈부족함
        }
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
    }

    public void closeBeadal(){
		beadalWindow_obj.SetActive (false);
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


    public void boxYes(){

		string str1;
		str1 = PlayerPrefs.GetString ("code", "");
		coldRain_i = PlayerPrefs.GetInt (str1+"c", 0);
		hotRain_i = PlayerPrefs.GetInt (str1+"h", 0);

		if (coldRain_i >= 50 && hotRain_i >= 25) {

			coldRain_i = coldRain_i - 50;
			PlayerPrefs.SetInt (str1 + "c", coldRain_i);

			hotRain_i = hotRain_i - 25;
			PlayerPrefs.SetInt (str1 + "h", hotRain_i);

            PlayerPrefs.SetInt(boxName_str+"box", 1);
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
            needMore_obj.SetActive(true);
            //돈부족
        }
	}

	public void boxNo(){
		boxClean_obj.SetActive (false);
        needMore_obj.SetActive(false);
    }

    public void closeNeed()
    {
        needMore_obj.SetActive(false);
    }

    

}

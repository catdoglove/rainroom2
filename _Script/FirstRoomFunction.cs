using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomFunction : CavasData {

	public GameObject beadalWindow_obj;

	public GameObject boxClean_obj;

	public GameObject GMNotdistroy;

    //하트
    public int heart_i;

    public GameObject[] fisrtRoomItem_obj;


    public int window_i,book_i,bed_i,desk_i,stand_i,tapestry_i,rug_i,poster_i, cabinet_i;
	public GameObject windowImg_obj,bookImg_obj,deskImg_obj,standImg_obj,tapestryImg_obj,bedImg_obj,rugImg_obj, cabinetImg_obj;

    public GameObject moreCoinWindow_obj;

    public int bookBox_i;
    public GameObject bookBox_obj;

    public GameObject loadGM;

	// Use this for initialization
	void Start () {

		//로딩화면에서 불러온 정보를 찾아오기 위해서 태그로 지엠을 찾아준다
		GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");


        //방에 처음 들어왔을때 각각 단계에 따라 이미지 바꿔주기

        
		//window_i = PlayerPrefs.GetInt ("windowlv", 0);
		book_i = PlayerPrefs.GetInt ("booklv",0);
		bed_i = PlayerPrefs.GetInt ("bedlv",0);
		rug_i = PlayerPrefs.GetInt ("ruglv",0);
		
		poster_i = PlayerPrefs.GetInt ("posterlv",0);
		desk_i = PlayerPrefs.GetInt ("desklv",0);
		tapestry_i = PlayerPrefs.GetInt ("tapestrylv",0);
		stand_i = PlayerPrefs.GetInt ("standlv",0);
        cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);



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
        //deskImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().desk_spr [desk_i];
        //tapestryImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().tapestry_spr [tapestry_i];
        cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];
    }

    //전단지열기
    public void openBeadal(){
		
		if (beadalWindow_obj.activeSelf == true) {
			beadalWindow_obj.SetActive (false);
		} else {
			beadalWindow_obj.SetActive (true);
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
        }
        else
        {
            //돈부족함
        }
    }


	public void closeBeadal(){
		beadalWindow_obj.SetActive (false);
	}

	public void boxOpen(){
		boxClean_obj.SetActive (true);
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

            //스위치케이스문으로읽어온박스넘버를보고저장해줘서치운박스를구분함
            //박스는처음에는없는걸로하고안치운박스면만들어주는걸로해서오류나도
            //박스가보이는일은없게하자
            //그냥함수를다만들어주자

            //PlayerPrefs.SetInt("bedbox", 1);
            //PlayerPrefs.SetInt("bookbox", 1);

            PlayerPrefs.Save ();
		} else {
            //돈부족
		}
	}
	public void boxNo(){
		boxClean_obj.SetActive (false);
	}

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour {

	public GameObject infoWindow_obj,infoBackWindow_obj;
    public GameObject face_obj;
    public Sprite[] face_spr;


    public GameObject cBook_obj, cWindow_obj, cWall_obj, cBed_obj, cCabinet_obj, cDesk_obj, cLight_obj, cDrawer_obj, cGasrange_obj, cIcebox_obj, CShelf_obj, cBox_obj, cLadder_obj;
    public Text[] itemList_txt;
    public GameObject[] checkLine_obj;

	Vector3 rect_scl,backRect_scl;

    public Slider love_sld;
    public int love_i = 0;
    public int loveLv_i = 0;

	int turnCk_i;
    
    public GameObject reverseBtn_obj, reverseBackBtn_obj;



    // Use this for initialization
    void Start () {

        //호감도
        love_i = PlayerPrefs.GetInt("lovepoint", 0);
        //호감레벨
        loveLv_i = PlayerPrefs.GetInt("lovelv", 0);
    }

   public void infoShow()
    {
        PlayerPrefs.SetInt("talk", 5);///////////////////////////////////////테스트용나중에꼭지울것/////////////////////////////////////
        //호감도
        love_i = PlayerPrefs.GetInt("lovepoint", 0);
        love_sld.maxValue = PlayerPrefs.GetInt("lovemax", 40);
        love_sld.value = love_i;
        //호감레벨
        loveLv_i = PlayerPrefs.GetInt("lovelv", 0);
        face_obj.GetComponent<Image>().sprite = face_spr[loveLv_i];
        InfoCheckList();
    }

	public void infoWindowTurn(){
        reverseBtn_obj.SetActive(false);
        StopCoroutine ("backTurning2");
		StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StartCoroutine ("turning");
        reverseBackBtn_obj.SetActive(true);
	}

	public void infoBackWindowTurn(){
        reverseBackBtn_obj.SetActive(false);
        StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StopCoroutine ("turning");
		StartCoroutine ("backTurning2");
        reverseBtn_obj.SetActive(true);
    }




	IEnumerator turning(){
		rect_scl = infoWindow_obj.transform.localScale;

			while (rect_scl.x >= 0f) {
				rect_scl.x = rect_scl.x - 0.1f;
				infoWindow_obj.transform.localScale = rect_scl;
				yield return null;
			}
		StartCoroutine ("backTurning");

	}


	IEnumerator backTurning(){
		backRect_scl = infoBackWindow_obj.transform.localScale;

        while (backRect_scl.x <= 1f)
        {
            backRect_scl.x = backRect_scl.x + 0.1f;
            infoBackWindow_obj.transform.localScale = backRect_scl;
            yield return null;
        }
	}



	IEnumerator turning2(){
		rect_scl = infoWindow_obj.transform.localScale;

		while (rect_scl.x <= 1f) {
			rect_scl.x = rect_scl.x + 0.1f;
			infoWindow_obj.transform.localScale = rect_scl;
			yield return null;
		}

		rect_scl.x = 1f;
		infoWindow_obj.transform.localScale = rect_scl;

	}


	IEnumerator backTurning2(){
		backRect_scl = infoBackWindow_obj.transform.localScale;

		while (backRect_scl.x >= 0f) {
			backRect_scl.x=backRect_scl.x-0.1f;
			infoBackWindow_obj.transform.localScale = backRect_scl;
			yield return null;
		}
		backRect_scl.x=0f;
		infoBackWindow_obj.transform.localScale = backRect_scl;
		StartCoroutine ("turning2");
	}




    void InfoCheckList()
    {
        closeListAll();
        switch (loveLv_i)
        {
            case 0:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.2  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 2)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cDesk_obj.SetActive(true);
                itemList_txt[5].text = "Lv.1  달성";
                checkLine_obj[5].SetActive(false);
                if (PlayerPrefs.GetInt("desklv", 0) >= 1)
                {
                    checkLine_obj[5].SetActive(true);
                }
                break;
            case 1:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.4  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 4)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cCabinet_obj.SetActive(true);
                itemList_txt[4].text = "Lv.1  달성";
                checkLine_obj[4].SetActive(false);
                if (PlayerPrefs.GetInt("cabinetlv", 0) >= 1)
                {
                    checkLine_obj[4].SetActive(true);
                }
                break;
            case 2:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.6  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 6)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cBed_obj.SetActive(true);
                itemList_txt[3].text = "Lv.1  달성";
                checkLine_obj[3].SetActive(false);
                if (PlayerPrefs.GetInt("bedlv", 0) >= 1)
                {
                    checkLine_obj[3].SetActive(true);
                }
                cLadder_obj.SetActive(true);
                checkLine_obj[12].SetActive(false);
                if (PlayerPrefs.GetInt("ladderlv", 0) >= 1)
                {
                    checkLine_obj[12].SetActive(true);
                }
                itemList_txt[12].text = "사다리상자정리";
                break;
            case 3:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.7  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 7)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.1  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 1)
                {
                    checkLine_obj[1].SetActive(true);
                }
                cWall_obj.SetActive(true);
                itemList_txt[2].text = "Lv.1  달성";
                checkLine_obj[2].SetActive(false);
                if (PlayerPrefs.GetInt("walllv", 0) >= 1)
                {
                    checkLine_obj[2].SetActive(true);
                }
                break;
            case 4:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.8  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 8)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.3  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 3)
                {
                    checkLine_obj[1].SetActive(true);
                }
                cGasrange_obj.SetActive(true);
                itemList_txt[8].text = "가스버너 꺼내기";
                checkLine_obj[8].SetActive(false);
                if (PlayerPrefs.GetInt("gasrangelv", 0) >= 1)
                {
                    checkLine_obj[8].SetActive(true);
                }
                break;
            case 5:
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.5  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 5)
                {
                    checkLine_obj[1].SetActive(true);
                }
                cBox_obj.SetActive(true);
                itemList_txt[11].text = "현관 박스 정리";
                checkLine_obj[11].SetActive(false);
                cIcebox_obj.SetActive(true);
                itemList_txt[9].text = "식품용박스꺼내기";
                checkLine_obj[9].SetActive(false);
                if (PlayerPrefs.GetInt("iceboxlv", 0) >= 1)
                {
                    checkLine_obj[9].SetActive(true);
                }
                break;
            case 6:
                cLight_obj.SetActive(true);
                itemList_txt[6].text = "Lv.1  달성";
                checkLine_obj[6].SetActive(false);
                if (PlayerPrefs.GetInt("lightlv", 0) >= 1)
                {
                    checkLine_obj[6].SetActive(true);
                }
                cDrawer_obj.SetActive(true);
                itemList_txt[7].text = "Lv.1  달성";
                checkLine_obj[7].SetActive(false);
                if (PlayerPrefs.GetInt("drawerlv", 0) >= 1)
                {
                    checkLine_obj[7].SetActive(true);
                }
                break;
            case 7:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.10  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 10)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.6  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 6)
                {
                    checkLine_obj[1].SetActive(true);
                }
                break;
            case 8:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.11  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 11)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "LV.7  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 7)
                {
                    checkLine_obj[1].SetActive(true);
                }
                break;
            case 9:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.12  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 12)
                {
                    checkLine_obj[0].SetActive(true);
                }
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.9  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 9)
                {
                    checkLine_obj[1].SetActive(true);
                }
                break;
            case 10:
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "Lv.10  달성";
                checkLine_obj[1].SetActive(false);
                if (PlayerPrefs.GetInt("windowlv", 0) >= 10)
                {
                    checkLine_obj[1].SetActive(true);
                }
                break;
            case 11:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "Lv.14  달성";
                checkLine_obj[0].SetActive(false);
                if (PlayerPrefs.GetInt("booklv", 0) >= 14)
                {
                    checkLine_obj[0].SetActive(true);
                }
                break;
        }
    }

    void closeListAll()
    {
        cBook_obj.SetActive(false);
        cWindow_obj.SetActive(false);
        cWall_obj.SetActive(false);
        cBed_obj.SetActive(false);
        cCabinet_obj.SetActive(false);
        cDesk_obj.SetActive(false);
        cLight_obj.SetActive(false);
        cDrawer_obj.SetActive(false);
        cGasrange_obj.SetActive(false);
        cIcebox_obj.SetActive(false);
        CShelf_obj.SetActive(false);
        cBox_obj.SetActive(false);
        cLadder_obj.SetActive(false);
    }
}

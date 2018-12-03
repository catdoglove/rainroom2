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

	Vector3 rect_scl,backRect_scl;

    public Slider love_sld;
    public int love_i = 0;
    public int loveLv_i = 0;

	int turnCk_i;




	// Use this for initialization
	void Start () {

        //호감도
        love_i = PlayerPrefs.GetInt("lovepoint", 0);
        //호감레벨
        loveLv_i = PlayerPrefs.GetInt("lovelv", 0);
    }

   public void infoShow()
    {
        //호감도
        love_i = PlayerPrefs.GetInt("lovepoint", 0);
        love_sld.value = love_i;
        //호감레벨
        loveLv_i = PlayerPrefs.GetInt("lovelv", 0);
        //face_obj.GetComponent<Image>().sprite = face_spr[loveLv_i];
    }

	public void infoWindowTurn(){
		StopCoroutine ("backTurning2");
		StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StartCoroutine ("turning");
	}

	public void infoBackWindowTurn(){
		StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StopCoroutine ("turning");
		StartCoroutine ("backTurning2");
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
                itemList_txt[0].text = "2";
                cDesk_obj.SetActive(true);
                itemList_txt[5].text = "1";
                break;
            case 1:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "4";
                cCabinet_obj.SetActive(true);
                itemList_txt[4].text = "1";
                break;
            case 2:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "6";
                cBed_obj.SetActive(true);
                itemList_txt[3].text = "1";
                cLadder_obj.SetActive(true);
                itemList_txt[12].text = "1";
                break;
            case 3:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "7";
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "1";
                cWall_obj.SetActive(true);
                itemList_txt[2].text = "1";
                break;
            case 4:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "8";
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "3";
                cGasrange_obj.SetActive(true);
                itemList_txt[8].text = "1";
                break;
            case 5:
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "5";
                cBox_obj.SetActive(true);
                itemList_txt[11].text = "외출";
                break;
            case 6:
                cLight_obj.SetActive(true);
                itemList_txt[6].text = "1";
                cDrawer_obj.SetActive(true);
                itemList_txt[7].text = "1";
                break;
            case 7:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "10";
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "6";
                break;
            case 8:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "11";
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "7";
                break;
            case 9:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "12";
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "9";
                break;
            case 10:
                cWindow_obj.SetActive(true);
                itemList_txt[1].text = "10";
                break;
            case 11:
                cBook_obj.SetActive(true);
                itemList_txt[0].text = "14";
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

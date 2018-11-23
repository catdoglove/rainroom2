using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour {

	public GameObject infoWindow_obj,infoBackWindow_obj;
    public GameObject face_obj;
    public Sprite[] face_spr;

	Vector3 rect_scl,backRect_scl;

    public Slider love_sld;
    public int love_i = 0;
    public int loveLv_i = 0;

	int turnCk_i;


    public GameObject[] checkImg_obj;
    public Text[] checkList_txt;


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

    void InfoCheckList()
    {

        switch (loveLv_i)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
        }
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
}

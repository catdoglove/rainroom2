using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBox : MonoBehaviour {

    public GameObject endWindow_obj;
    public GameObject[] endItem_obj, endHint_obj;

	// Use this for initialization
	void Start () {
		
	}
	
    public void ActEnding()
    {
        if (endWindow_obj.activeSelf == true)
        {
            endWindow_obj.SetActive(false);
        }
        else
        {
            endWindow_obj.SetActive(true);
        }
    }

    void checkEnd()
    {
        //대화
        PlayerPrefs.GetInt("talkending", 0);
        //첫공원
        PlayerPrefs.GetInt("parkending", 0);
        //첫도시
        PlayerPrefs.GetInt("cityending", 0);
        //우유10번
        PlayerPrefs.GetInt("milkending", 0);
        //바다10번
        PlayerPrefs.GetInt("seaending", 0);
        //나뭇잎40번
        PlayerPrefs.GetInt("leafending", 0);
        //그림모두
        PlayerPrefs.GetInt("pictureending", 0);
        //모든요리
        PlayerPrefs.GetInt("cookending", 0);
        //호감도
        PlayerPrefs.GetInt("likeending", 0);
    }
}

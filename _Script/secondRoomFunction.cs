using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondRoomFunction : CavasData {

	public GameObject GMNotdistroy;


	public int window_i, book_i, gasrange_i, icebox_i, shelf_i, drawing_i, mat_i,flower_i,light_i,umbrella_i;
	public GameObject bookImg_obj,windowImg_obj,gasrangeImg_obj,iceboxImg_obj,shelfImg_obj,drawingImg_obj,matImg_obj,flowerImg_obj,lightImg_obj,umbrellaImg_obj;

    public GameObject WaterPurifilerWindow_obj;
    public GameObject coldToHot_obj, hotToCold_obj;

    // Use this for initialization
    void Start () {
		//GM을 찾아불러온 데이터들 가져오기

		//GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");

		/*
		window_i = PlayerPrefs.GetInt ("window",0);
		book_i = PlayerPrefs.GetInt ("book",0);
		gasrange_i = gasrange_i = PlayerPrefs.GetInt ("gasrange",0);
		icebox_i = PlayerPrefs.GetInt ("icebox",0);
		shelf_i = PlayerPrefs.GetInt ("shelf",0);
		drawing_i = PlayerPrefs.GetInt ("drawing",0);
		mat_i = PlayerPrefs.GetInt ("mat",0);
		flower_i = PlayerPrefs.GetInt ("flower",0);
		light_i = PlayerPrefs.GetInt ("light",0);
		umbrella_i = PlayerPrefs.GetInt ("umbrella",0);



		windowImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData> ().window_spr [window_i];
		bookImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().book_spr [book_i];
		gasrangeImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().gasrange_spr [gasrange_i];
		icebox_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().icebox_spr [icebox_i];
		shelf_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().shelf_spr [shelf_i];
		drawing_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().drawing_spr [drawing_i];
		deskImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().desk_spr [desk_i];
		matImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().mat_spr [mat_i];
		flowerImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().flower_spr [flower_i];
		lightImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().light_spr [light_i];
		umbrellaImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().umbrella_spr [umbrella_i];


        

		*/
	}



    public void OpenWaterPurifiler()
    {
        if (WaterPurifilerWindow_obj.activeSelf == true)
        {
            WaterPurifilerWindow_obj.SetActive(false);
            CloseWaterYN();
        }
        else
        {
            WaterPurifilerWindow_obj.SetActive(true);
        }
    }
    public void ColdToHot()
    {
        coldToHot_obj.SetActive(true);
    }
    public void HotToCold()
    {
        hotToCold_obj.SetActive(true);
    }

    public void ColdToHotYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
        if (coldRain_i >= 400)
        {
            coldRain_i = coldRain_i - 400;
            hotRain_i = hotRain_i + 10;
        }
        PlayerPrefs.SetInt(str1 + "c", coldRain_i);
        PlayerPrefs.SetInt(str1 + "h", hotRain_i);
        PlayerPrefs.Save();
    }

    public void HotToColdYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
        if (hotRain_i >= 20)
        {
            coldRain_i = coldRain_i + 200;
            hotRain_i = hotRain_i - 20;
        }
        PlayerPrefs.SetInt(str1 + "c", coldRain_i);
        PlayerPrefs.SetInt(str1 + "h", hotRain_i);
        PlayerPrefs.Save();
    }

    public void CloseWaterYN()
    {
        coldToHot_obj.SetActive(false);
        hotToCold_obj.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secondRoomFunction : CavasData {

	public GameObject GMNotdistroy;


	public int window_i, book_i, gasrange_i, icebox_i, shelf_i, drawing_i, mat_i, mat2_i, flower_i,light_i,umbrella_i, drawer_i;
	public GameObject bookImg_obj,windowImg_obj, drawerImg_obj, windowImg2_obj, gasrangeImg_obj,iceboxImg_obj,shelfImg_obj,drawingImg_obj,matImg_obj, matImg2_obj, flowerImg_obj,lightImg_obj, lightImg2_obj, umbrellaImg_obj;

    public GameObject WaterPurifilerWindow_obj;
    public GameObject coldToHot_obj, hotToCold_obj;
    public Text WaterPurifilerH_txt, WaterPurifilerC_txt;

    public GameObject needhRain_obj, needcRain_obj;

    public GameObject[] secondRoomItem_obj;
    // Use this for initialization
    void Start () {
		//GM을 찾아불러온 데이터들 가져오기

		GMNotdistroy = GameObject.FindGameObjectWithTag ("loadGM");

		
		window_i = PlayerPrefs.GetInt ("windowlv",0);
		gasrange_i = PlayerPrefs.GetInt ("gasrangelv", 0);
		icebox_i = PlayerPrefs.GetInt ("iceboxlv", 0);
		shelf_i = PlayerPrefs.GetInt ("shelflv", 0);
		//drawing_i = PlayerPrefs.GetInt ("drawing",0);
		mat_i = PlayerPrefs.GetInt ("mat1lv", 0);
        mat2_i = PlayerPrefs.GetInt("mat2lv", 0);
        flower_i = PlayerPrefs.GetInt ("flower", 0);
		light_i = PlayerPrefs.GetInt ("lightlv", 0);
		umbrella_i = PlayerPrefs.GetInt ("umbrellalv", 0);
        drawer_i = PlayerPrefs.GetInt("drawerlv", 0);


        drawerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().drawer_spr[drawer_i];
        windowImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData> ().window_spr [window_i];
        windowImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().window2_spr[window_i];
        //bookImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().book_spr [book_i];
        gasrangeImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().gasrange_spr [gasrange_i];
		iceboxImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().icebox_spr [icebox_i];
		shelfImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().shelf_spr [shelf_i];
		//drawing_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().drawing_spr [drawing_i];
		matImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().mat_spr [mat_i];
        matImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().mat2_spr[mat2_i];
        flowerImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().flower_spr [flower_i];
		lightImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().light_spr [light_i];
        lightImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().light_spr[light_i];
        //umbrellaImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().umbrella_spr [umbrella_i];
        
    }


    /// <summary>
    /// 정수기로 찬물 따듯한물을 바꿔준다
    /// </summary>
    public void OpenWaterPurifiler()
    {
        if (WaterPurifilerWindow_obj.activeSelf == true)
        {
            WaterPurifilerWindow_obj.SetActive(false);
            CloseWaterYN();
        }
        else
        {
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
            hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
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
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
        }
        else
        {
            //찬물부족
            needcRain_obj.SetActive(true);
        }
       
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
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
        }
        else
        {
            //따듯한물부족
            needhRain_obj.SetActive(true);
        }
        
    }

    public void CloseWaterYN()
    {
        coldToHot_obj.SetActive(false);
        hotToCold_obj.SetActive(false);
    }

    public void needClose()
    {
        needcRain_obj.SetActive(false);
        needhRain_obj.SetActive(false);

    }


}

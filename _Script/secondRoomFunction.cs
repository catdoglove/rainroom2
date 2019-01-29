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

    public GameObject seedBox_obj,iceBoxBox_obj, gasrangeBox_obj, drawerBox_obj;
    public GameObject boxClean_obj;
    public string boxName_str;

    public GameObject close_obj;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    // Use this for initialization
    void Start () {
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = 2500f;
        menuBlock_obj.transform.position = menuBlock_vet;

        
        //GM을 찾아불러온 데이터들 가져오기
        GMNotdistroy = GameObject.FindGameObjectWithTag ("loadGM");
		window_i = PlayerPrefs.GetInt ("windowlv",0);
		gasrange_i = PlayerPrefs.GetInt ("gasrangelv", 0);
		icebox_i = PlayerPrefs.GetInt ("iceboxlv", 0);
		shelf_i = PlayerPrefs.GetInt ("shelflv", 0);
		//drawing_i = PlayerPrefs.GetInt ("drawing",0);
		mat_i = PlayerPrefs.GetInt ("mat1lv", 0);
        mat2_i = PlayerPrefs.GetInt("mat2lv", 0);
        flower_i = PlayerPrefs.GetInt ("seed", 0);
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




        //박스
        if (PlayerPrefs.GetInt("icebox", 0) == 10)
        {
            iceBoxBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("gasrangebox", 0) == 10)
        {
            gasrangeBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("drawerbox", 0) == 10)
        {
            drawerBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
            seedBox_obj.SetActive(true);
        }
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


    public void boxOpen()
    {
        boxClean_obj.SetActive(true);
    }
    public void boxSeed()
    {
        boxName_str = "seed";
    }
    public void boxDrawer()
    {
        boxName_str = "drawer";
    }
    public void boxGas()
    {
        boxName_str = "gasrange";
    }
    public void boxIce()
    {
        boxName_str = "icebox";
    }


    public void boxYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);

        if (coldRain_i >= 50 && hotRain_i >= 25)
        {
            coldRain_i = coldRain_i - 50;
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);

            hotRain_i = hotRain_i - 25;
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);

            PlayerPrefs.SetInt(boxName_str + "box", 1);
            PlayerPrefs.SetInt(boxName_str + "lv", 1);
            
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("seedbox", 0) == 1)
            {
                seedBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("drawerbox", 0) == 1)
            {
                drawerBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("gasrangebox", 0) == 1)
            {
                gasrangeBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("iceboxbox", 0) == 1)
            {
                iceBoxBox_obj.SetActive(false);
            }
            flower_i = PlayerPrefs.GetInt("seedlv", 0);
            drawer_i = PlayerPrefs.GetInt("drawerlv", 0);
            gasrange_i = PlayerPrefs.GetInt("gasrangelv", 0);
            icebox_i = PlayerPrefs.GetInt("iceboxlv", 0);

            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[flower_i];
            drawerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().drawer_spr[drawer_i];
            gasrangeImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().gasrange_spr[gasrange_i];
            iceboxImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().icebox_spr[icebox_i];

            boxClean_obj.SetActive(false);
        }
        else
        {
            //needMore_obj.SetActive(true);
            //돈부족
        }
    }

    public void boxClose()
    {
        boxClean_obj.SetActive(false);
    }

    public void OpenClose()
    {
        close_obj.SetActive(true);
    }


    public void AllClose()
    {
        close_obj.SetActive(false);
        WaterPurifilerWindow_obj.SetActive(false);
        CloseWaterYN();
    }
}

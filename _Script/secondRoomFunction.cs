using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secondRoomFunction : CavasData {

    //앞뒤 캐릭터
    public GameObject character_obj;


    public GameObject GMNotdistroy;


	public int window_i, book_i, gasrange_i, icebox_i, shelf_i, drawing_i, mat_i, mat2_i, flower_i,light_i,umbrella_i, drawer_i,wall_i;
	public GameObject bookImg_obj,windowImg_obj, drawerImg_obj, windowImg2_obj, gasrangeImg_obj,iceboxImg_obj,shelfImg_obj,drawingImg_obj,matImg_obj, matImg2_obj, flowerImg_obj,lightImg_obj, lightImg2_obj, umbrellaImg_obj, WaterCan_obj, WaterPurifiler_obj;
    public GameObject wallImg_obj, wallImg2_obj;
    public GameObject switch_obj;
    public Sprite[] switch_spr;
    public Sprite[] wall_spr, wall2_spr;
    public GameObject iceBoxBtn_obj;

    //정수기
    public GameObject WaterPurifilerWindow_obj;
    public GameObject coldToHot_obj, hotToCold_obj;
    public Text WaterPurifilerH_txt, WaterPurifilerC_txt;
    public GameObject hot_obj,cold_obj;

    public GameObject needhRain_obj, needcRain_obj, needMore_obj;

    public GameObject[] secondRoomItem_obj;

    public GameObject seedBox_obj,iceBoxBox_obj, gasrangeBox_obj, drawerBox_obj,doorBox_obj;
    public GameObject boxClean_obj;
    public string boxName_str;

    public GameObject close_obj;

    public GameObject umb_obj;

    //물부족창
    public GameObject needToast_obj;
    Color color;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    //하트
    public int heart_i;
    public Text boxHeart_txt;
    public Text boxTotal_txt, boxTxt_txt;


    //도움말
    public GameObject Help_obj;

    public int boxs_i;


    public GameObject goOutWindow_obj;

    public GameObject GM2;
    //밤
    public GameObject dayRoom;
    public Sprite[] day_spr;

    // Use this for initialization
    void Start ()
    {
        color = new Color(1f, 1f, 1f);
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;
        
        //GM을 찾아불러온 데이터들 가져오기
        GMNotdistroy = GameObject.FindGameObjectWithTag ("loadGM");
		window_i = PlayerPrefs.GetInt ("windowlv",0);
		gasrange_i = PlayerPrefs.GetInt ("gasrangelv", 0);
		icebox_i = PlayerPrefs.GetInt ("iceboxlv", 0);
		shelf_i = PlayerPrefs.GetInt ("shelflv", 0);
        //drawing_i = PlayerPrefs.GetInt ("drawing",0);
        wall_i = PlayerPrefs.GetInt("walllv", 0);
        mat_i = PlayerPrefs.GetInt ("mat1lv", 0);
        mat2_i = PlayerPrefs.GetInt("mat2lv", 0);
        flower_i = PlayerPrefs.GetInt ("seedlv", 0);
		light_i = PlayerPrefs.GetInt ("lightlv", 0);
		umbrella_i = PlayerPrefs.GetInt ("umbrellalv", 0);
        drawer_i = PlayerPrefs.GetInt("drawerlv", 0);



        wallImg_obj.GetComponent<Image>().sprite = wall_spr[wall_i];
        wallImg2_obj.GetComponent<Image>().sprite = wall2_spr[wall_i];
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
        if (PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[0];
        }
        else
        {
            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[flower_i];
        }
		lightImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().light_spr [light_i];
        lightImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().light_spr[light_i];
        //umbrellaImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().umbrella_spr [umbrella_i];

        if (PlayerPrefs.GetInt("wateringcanshop", 0) == 2)
        {
            WaterCan_obj.SetActive(true);
            if (PlayerPrefs.GetInt("putwatercan", 1) == 0)
            {
                WaterCan_obj.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("waterpurifiershop", 0) == 2)
        {
            WaterPurifiler_obj.SetActive(true);
        }

        if (PlayerPrefs.GetInt("switchshop", 0) == 2)
        {
            switch_obj.SetActive(true);
            if (PlayerPrefs.GetInt("lightover", 0) == 1)
            {
                switch_obj.GetComponent<Image>().sprite = switch_spr[1];
            }

        }
        //박스
        if (PlayerPrefs.GetInt("iceboxbox", 0) == 10)
        {
            iceBoxBox_obj.SetActive(true);
        }
        else
        {
            iceBoxBtn_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("gasrangebox", 0) == 10)
        {
            gasrangeBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("drawerbox", 0) == 10)
        {
            drawerBox_obj.SetActive(true);
        }
        else
        {
            drawerBox_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
            seedBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("doorbox", 0) == 1)
        {
            doorBox_obj.SetActive(false);
            umb_obj.SetActive(true);
        }
        else
        {
            umb_obj.SetActive(false);
        }
        //낮밤
        if (PlayerPrefs.GetInt("dayday", 0) == 1)
        {
            dayRoom.SetActive(true);
        }
    }
    /// <summary>
    /// 단칸방 스위치 켜기
    /// </summary>
    public void TurnOnSwitch()
    {
        if (PlayerPrefs.GetInt("lightover", 0)==1)
        {
            //꺼짐
            dayRoom.GetComponent<Image>().sprite = day_spr[0];
            switch_obj.GetComponent<Image>().sprite = switch_spr[0];
            PlayerPrefs.SetInt("lightover", 0);
        }
        else
        {
            //켜짐
            dayRoom.GetComponent<Image>().sprite = day_spr[1];
            
            switch_obj.GetComponent<Image>().sprite = switch_spr[1];
            PlayerPrefs.SetInt("lightover", 1);
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
            hot_obj.SetActive(true);
            coldRain_i = coldRain_i - 400;
            hotRain_i = hotRain_i + 10;
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
        }
        else
        {
            //찬물부족
            needcRain_obj.SetActive(true);
            StartCoroutine("toastCImgFadeOut");
        }
        coldToHot_obj.SetActive(false);
    }

    public void HotToColdYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
        if (hotRain_i >= 20)
        {
            cold_obj.SetActive(true);
            coldRain_i = coldRain_i + 200;
            hotRain_i = hotRain_i - 20;
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
        }
        else
        {
            //따듯한물부족
            needhRain_obj.SetActive(true);
        }

        hotToCold_obj.SetActive(false);
    }

    public void closeCH()
    {
        hot_obj.SetActive(false);
        cold_obj.SetActive(false);
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
        boxHeart_txt.text = "" + boxs_i;

        boxClean_obj.SetActive(true);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTotal_txt.text = "" + heart_i;
    }
    public void boxSeed()
    {
        boxName_str = "seed";
        boxs_i = 4;
    }
    public void boxDrawer()
    {
        boxName_str = "drawer";
        boxs_i = 1;
    }
    public void boxGas()
    {
        boxName_str = "gasrange";
        boxs_i = 4;
    }
    public void boxIce()
    {
        boxName_str = "icebox";
        boxs_i = 4;
    }

    public void boxDoor()
    {
        boxName_str = "door";
        boxs_i = 10;
    }


    public void boxYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int heart_i;
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTxt_txt.text = "" + boxs_i;
        if (heart_i >= boxs_i)
        {
            heart_i = heart_i - boxs_i;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);

            PlayerPrefs.SetInt(boxName_str + "box", 1);
            PlayerPrefs.SetInt(boxName_str + "lv", 1);
            checkach();
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
                iceBoxBtn_obj.SetActive(true);
            }
            if (PlayerPrefs.GetInt("doorbox", 0) == 1)
            {
                doorBox_obj.SetActive(false);
                umb_obj.SetActive(true);
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
            needMoney();
            boxClean_obj.SetActive(false);
            //돈부족
        }
    }
    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
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
        goOutWindow_obj.SetActive(false);
    }


    //토스트가 사라지게
    IEnumerator toastCImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needcRain_obj.GetComponent<Image>().color = color;
        needcRain_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needcRain_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needcRain_obj.SetActive(false);
    }

    //캐릭터회전
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

    public void OpenHelpCook()
    {
        Help_obj.SetActive(true);
    }
    public void CloseHelp()
    {
        Help_obj.SetActive(false);
    }

    //외출창띄우기
    public void OpenGoOut()
    {
        goOutWindow_obj.SetActive(true);
    }
    public void CloseGoOut()
    {
        goOutWindow_obj.SetActive(false);
    }
    public void GoOutY()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        int hp = 20;
        if(heart_i >= hp)
        {
            heart_i = heart_i - hp;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
        }
    }

    //업적
    void checkach()
    {
        int cts = PlayerPrefs.GetInt("countboxst", 0);
        cts++;
        Debug.Log("boxst");
        PlayerPrefs.SetInt("countboxst", cts);
        if (cts >= 10 && PlayerPrefs.GetInt("boxst", 0) < 3)
        {
            PlayerPrefs.SetInt("boxst", 3);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 2);
        }
        else if (cts >= 5 && PlayerPrefs.GetInt("boxst", 0) < 2)
        {
            PlayerPrefs.SetInt("boxst", 2);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("boxst", 0) < 1)
        {
            PlayerPrefs.SetInt("boxst", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 0);
        }
    }
}

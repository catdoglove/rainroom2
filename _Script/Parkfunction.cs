using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Parkfunction : CavasData
{
    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;
    //밤
    public GameObject dayRoom, dayRoom2;
    public GameObject nightShop_obj,dayShop_obj;
    //소리
    public GameObject audio_obj;
    //랜덤
    public int eventRand_i;
    public GameObject eventPet_obj, eventPaint_obj, event_obj;
    public Sprite[] event_spr;
    //도움말
    public GameObject helpPark_obj;
    public Sprite[] helpP_spr;
    int help;
    //
    public GameObject GMtag,GMP;
    public GameObject leafPr_obj,leafWin_obj;

    private void Awake()
    {
        /*
        //나뭇잎
        int lf = PlayerPrefs.GetInt("leafcount", 0);
        if (lf >= 100)
        {
            leafPr_obj.SetActive(true);
        }
        */

        //외출업적
        if (PlayerPrefs.GetInt("acgocheck", 0) == 1)
        {
            checkachOut();
            PlayerPrefs.SetInt("acgocheck", 0);
        }


        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMtag.GetComponent<MainBtnEvt>().comeHome_obj.SetActive(true);
        GMtag.GetComponent<MainBtnEvt>().shop_obj.SetActive(false);
        GMtag.GetComponent<MainBtnEvt>().SetClover();
        //공원에 처음 왔을때
        if (PlayerPrefs.GetInt("parkfirst", 0)==0)
        {
            helpPark_obj.SetActive(true);
            PlayerPrefs.SetInt("parkfirst", 1);
            PlayerPrefs.Save();
        }
    }

    // Use this for initialization
    void Start () {

        //밤낮
        setDay();
        //이밴트 랜덤 -첫외출에는 무조건 안나오면 어떨까?
        eventRand_i = Random.Range(0, 5);
        if (eventRand_i < 2)
        {
            eventPet_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[1];
        }
        else if(eventRand_i < 4)
        {
            eventPaint_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[2];
        }
        else
        {
            event_obj.GetComponent<Image>().sprite = event_spr[0];
        }
        if (PlayerPrefs.GetInt("dayday", 0) == 1)
        {
            eventPaint_obj.SetActive(false);
            eventPet_obj.SetActive(false);
            event_obj.GetComponent<Image>().sprite = event_spr[0];
        }
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;
    }
    //도움말
    public void CloseHelpP()
    {
        if (help == 0)
        {
            help = 1;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[1];
        }
        else
        {
            help = 0;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[0];
            helpPark_obj.SetActive(false);
        }
    }

    //밤낮
    public void setDay()
    {
        System.DateTime time = System.DateTime.Now;
        if (time.ToString("tt") == "PM")
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k >= 6)
            {
                dayRoom.SetActive(true);
                dayRoom2.SetActive(true);
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                nightShop_obj.SetActive(false);
                dayShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k < 6)
            {
                dayRoom.SetActive(true);
                dayRoom2.SetActive(true);
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                nightShop_obj.SetActive(false);
                dayShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }

    //외출업적
    void checkachOut()
    {
        int cts = PlayerPrefs.GetInt("countgooutst", 0);
        cts++;
        PlayerPrefs.SetInt("countgooutst", cts);
        //Debug.Log("tal" + PlayerPrefs.GetInt("gooutst", 0) + "cts" + cts);
        if (cts >= 100 && PlayerPrefs.GetInt("gooutst", 0) < 3)
        {
            PlayerPrefs.SetInt("gooutst", 3);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 2);
        }
        else if (cts >= 30 && PlayerPrefs.GetInt("gooutst", 0) < 2)
        {
            PlayerPrefs.SetInt("gooutst", 2);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("gooutst", 0) < 1)
        {
            PlayerPrefs.SetInt("gooutst", 1);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 0);
        }
        PlayerPrefs.Save();
    }

    //양동이
    public void getBas()
    {
        string str = PlayerPrefs.GetString("code", "");
        int myc = PlayerPrefs.GetInt(str + "c", 0);
        if (PlayerPrefs.GetInt("basketrain", 0) != 0)
        {
            audio_obj.GetComponent<SoundEvt>().waterSound();
        }
        if(PlayerPrefs.GetInt("basketrain", 0) >= 100)
        {
            PlayerPrefs.SetInt("basketrain", 100);
        }
        myc = myc + PlayerPrefs.GetInt("basketrain", 0);
        PlayerPrefs.SetInt(str + "c", myc);
        PlayerPrefs.SetInt("basketrain", 0);

    }

    public void leafPlant()
    {
        leafPr_obj.SetActive(false);
        leafWin_obj.SetActive(true);
        PlayerPrefs.SetInt("leafget", 1);
        PlayerPrefs.SetInt("leafcount", 0);
    }
}

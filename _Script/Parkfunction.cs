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
    public GameObject dayRoom;
    public GameObject nightShop_obj,dayShop_obj;
    //소리
    public GameObject audio_obj;
    //랜덤
    public int eventRand_i;
    public GameObject eventPet_obj, eventPaint_obj, event_obj;
    public Sprite[] event_spr;

    //
    public GameObject GMtag;


    private void Awake()
    {
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMtag.GetComponent<MainBtnEvt>().comeHome_obj.SetActive(true);
        GMtag.GetComponent<MainBtnEvt>().shop_obj.SetActive(false);
        GMtag.GetComponent<MainBtnEvt>().SetClover();
    }

    // Use this for initialization
    void Start () {
        
        //밤낮
        setDay();
        //이밴트 랜덤 -첫외출에는 무조건 안나오면 어떨까?
        eventRand_i = Random.Range(0, 3);
        if (eventRand_i == 0)
        {
            eventPet_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[1];
        }
        else if(eventRand_i == 1)
        {
            eventPaint_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[2];
        }
        else
        {
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
                if (canvasPack_cvs[0].activeSelf == true)
                {
                    dayRoom.SetActive(true);
                }
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
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
                if (canvasPack_cvs[0].activeSelf == true)
                {
                    dayRoom.SetActive(true);
                }
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                nightShop_obj.SetActive(false);
                dayShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {

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
                dayRoom.SetActive(true);
                nightShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                nightShop_obj.SetActive(false);
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
                nightShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                nightShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }


}

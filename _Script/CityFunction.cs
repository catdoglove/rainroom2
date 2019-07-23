using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFunction : MonoBehaviour {
    public GameObject GMtag;
    public GameObject buildToast_obj;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    public GameObject inHelp_obj, reHelp_obj;

    //밤
    public GameObject dayRoom, dayRoom2;
    public GameObject nightShop_obj, dayShop_obj, helpCity_obj;

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
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMtag.GetComponent<MainBtnEvt>().comeHome_obj.SetActive(true);
        GMtag.GetComponent<MainBtnEvt>().shop_obj.SetActive(false);
        GMtag.GetComponent<MainBtnEvt>().SetDiamond();


        //밤낮
        setDay();



        //도시에 처음 왔을때
        if (PlayerPrefs.GetInt("cityfirst", 0) == 0)
        {
            helpCity_obj.SetActive(true);
            PlayerPrefs.SetInt("cityfirst", 1);
            PlayerPrefs.Save();
        }
    }

    public void OpenActHelpI()
    {
        if (inHelp_obj.activeSelf == true)
        {
            inHelp_obj.SetActive(false);
        }
        else
        {
            inHelp_obj.SetActive(true);
        }
    }
    public void OpenActHelpR()
    {
        if (reHelp_obj.activeSelf == true)
        {
            reHelp_obj.SetActive(false);
        }
        else
        {
            reHelp_obj.SetActive(true);
        }
    }
    public void OpenActHelpF()
    {
        if (helpCity_obj.activeSelf == true)
        {
            helpCity_obj.SetActive(false);
        }
        else
        {
            helpCity_obj.SetActive(true);
        }
    }


    public void building()
    {
        if (buildToast_obj.activeSelf == true)
        {
            buildToast_obj.SetActive(false);
        }
        else
        {
            buildToast_obj.SetActive(true);
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
                //nightShop_obj.SetActive(true);
                //dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                //nightShop_obj.SetActive(false);
                //dayShop_obj.SetActive(true);
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
                //nightShop_obj.SetActive(true);
                //dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                //nightShop_obj.SetActive(false);
                //dayShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }
}

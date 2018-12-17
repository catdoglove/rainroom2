using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedTime : MonoBehaviour {

    public int seed_i,seedWater_i, coldRain_i;
    public int[] seedCPrice_i;

    string lastTime;

    public GameObject seedWindow_obj, seedYetWindow_obj;
    public GameObject seedImg_obj;
    public Sprite[] seed_spr;

    int minute;
    int hours;


    public GameObject loadGM;

    // Use this for initialization
    void Start () {
        loadGM = GameObject.FindGameObjectWithTag("loadGM");

        SeedTimeFlow();
    }


    void SeedTimeFlow()
    {
        seed_i = PlayerPrefs.GetInt("seed", 0);
        seedWater_i = PlayerPrefs.GetInt("seedWater", 0);
        System.DateTime d = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastTime = PlayerPrefs.GetString("seedLastTime", d.ToString());
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        hours = (int)compareTime.TotalHours;
        minute = (int)compareTime.TotalMinutes;
        minute = minute - (minute / 60) * 60;
        minute = 59 - minute;
        hours = 1 - hours;
        if (hours < 0)
        {
            if (seedWater_i > seed_i)
            {
                seed_i = PlayerPrefs.GetInt("seed", 0);
                seed_i++;
                PlayerPrefs.SetInt("seed", 0);
                seedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().flowerpot_spr[seed_i];
                PlayerPrefs.Save();
            }
        }
    }

    public void TouchSeed()
    {
        SeedTimeFlow();
        if (hours < 0)
        {
            if (seedWater_i == seed_i)
            {
                seedWindow_obj.SetActive(true);
            }
            else {
                seedYetWindow_obj.SetActive(true);
            }
            
        }
        else
        {
            seedYetWindow_obj.SetActive(true);
        }
    }

    public void CloseSeed()
    {
        seedWindow_obj.SetActive(false);
        seedYetWindow_obj.SetActive(false);
    }

    public void SeedYes()
    {
        string str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);

        if (coldRain_i >= seedCPrice_i[seed_i])
        {
            coldRain_i = coldRain_i - seedCPrice_i[seed_i];
            PlayerPrefs.SetInt(str + "c", coldRain_i);

            PlayerPrefs.SetString("seedLastTime", System.DateTime.Now.ToString());
            seedWater_i = PlayerPrefs.GetInt("seedWater", 0);
            seedWater_i++;
            PlayerPrefs.SetInt("seedWater", 0);
            PlayerPrefs.Save();
        }
        else
        {
            //물부족캄
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedTime : MonoBehaviour {

    public int seed_i,seedWater_i, coldRain_i;
    public int[] seedCPrice_i;

    string lastTime;

    public GameObject seedWindow_obj, seedYetWindow_obj, needWaterWindow_obj;
    public GameObject seedImg_obj;
    public Sprite[] seed_spr;
    public Text seed_txt;

    public GameObject seedTime_obj;
    
    int minute;
    int hours;
    
    public GameObject loadGM;

    Color colorN, colorY;

    //씨앗
    public Text seedTime_txt;

    // Use this for initialization
    void Start () {
        colorN = new Color(1f, 1f, 1f);
        colorY = new Color(1f, 1f, 1f);
        loadGM = GameObject.FindGameObjectWithTag("loadGM");
        if(PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
        }
        else
        {
            StartCoroutine("TimeCheck");
        }
    }
    
    void SeedTimeFlow()
    {
        seed_i = PlayerPrefs.GetInt("seedlv", 0);
        seedWater_i = PlayerPrefs.GetInt("seedWater", 1);
        System.DateTime d = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastTime = PlayerPrefs.GetString("seedLastTime", d.ToString());
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        hours = (int)compareTime.TotalHours;
        minute = (int)compareTime.TotalMinutes;
        minute = minute - (minute / 60) * 60;
        minute = 1 - minute;
        hours =  0 - hours;

        string strb = string.Format(@"{0:00}" + ":", hours) + string.Format(@"{0:00}", minute);
        seedTime_txt.text = strb;
        if (minute <= 0 && hours == 0)
        {
            hours = -1;
        }
            if (hours < 0)
        {
            seedTime_txt.text = "00:00";
            if (seedWater_i > seed_i)
            {
                seed_i = PlayerPrefs.GetInt("seedlv", 0);
                seed_i++;
                PlayerPrefs.SetInt("seedlv", seed_i);
                seedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().flowerpot_spr[seed_i];
                PlayerPrefs.Save();
            }
        }
        seedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().flowerpot_spr[seed_i];
    }

    public void TouchSeed()
    {
        SeedTimeFlow();
        
        if (hours < 0)
        {
            if (seedWater_i == seed_i)
            {
                seed_txt.text = ""+ seedCPrice_i[seed_i-1];
                seedWindow_obj.SetActive(true);
            }
            else {
                seedYetWindow_obj.SetActive(true);
            }
            
        }
        else
        {
            StopCoroutine("toastYetWaterFadeOut");
            StartCoroutine("toastYetWaterFadeOut");
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

        if (coldRain_i >= seedCPrice_i[seed_i-1])
        {
            coldRain_i = coldRain_i - seedCPrice_i[seed_i-1];
            PlayerPrefs.SetInt(str + "c", coldRain_i);

            PlayerPrefs.SetString("seedLastTime", System.DateTime.Now.ToString());
            seedWater_i = PlayerPrefs.GetInt("seedWater", 1);
            seedWater_i++;
            PlayerPrefs.SetInt("seedWater", seedWater_i);
            PlayerPrefs.Save();
        }
        else
        {
            //물부족캄
            StopCoroutine("toastneedWaterFadeOut");
            StartCoroutine("toastneedWaterFadeOut");
            needWaterWindow_obj.SetActive(true);
        }
    }

    //물부족
    IEnumerator toastneedWaterFadeOut()
    {
        colorN.a = Mathf.Lerp(0f, 1f, 1f);
        needWaterWindow_obj.GetComponent<Image>().color = colorN;
        needWaterWindow_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorN.a = Mathf.Lerp(0f, 1f, i);
            needWaterWindow_obj.GetComponent<Image>().color = colorN;
            yield return null;
        }
        needWaterWindow_obj.SetActive(false);
    }

    //아직 축축함
    IEnumerator toastYetWaterFadeOut()
    {
        colorY.a = Mathf.Lerp(0f, 1f, 1f);
        seedYetWindow_obj.GetComponent<Image>().color = colorY;
        seedYetWindow_obj.SetActive(true);
        seedTime_obj.GetComponent<Image>().color = colorY;
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorY.a = Mathf.Lerp(0f, 1f, i);
            seedYetWindow_obj.GetComponent<Image>().color = colorY;
            seedTime_obj.GetComponent<Image>().color = colorY;
            yield return null;
        }
        seedYetWindow_obj.SetActive(false);
    }

    IEnumerator TimeCheck()
    {
        int a =0;
        while (a == 0)
        {
            SeedTimeFlow();
            yield return new WaitForSeconds(2f);
        }

    }
}

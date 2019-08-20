using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedTime : MonoBehaviour {

    public int seed_i,seedWater_i, coldRain_i;
    public int[] seedCPrice_i,seedHtPrice_i;

    string lastTime;

    public GameObject seedWindow_obj, seedYetWindow_obj, needWaterWindow_obj;
    public GameObject seedImg_obj, waterPot_obj;
    public Sprite[] seed_spr, waterPot_spr;
    public Text seed_txt,seedHt_txt;

    public GameObject seedTime_obj;
    
    int minute;
    int hours;
    
    public GameObject loadGM;

    Color colorN, colorY, colorC;

    //씨앗
    public Text seedTime_txt;

    //소리
    public GameObject audio_obj;


    //씨앗팔레트
    public GameObject flowerColor_obj,flowerPotColor_obj, ColorWindow_obj;
    public Sprite[] flowerColor_spr, flowerPotColor_spr;
    public int num_i,flower_i,pot_i;
    public GameObject oldFlower_obj,newFlower_obj,oldWatercan_obj,newWatercan_obj;

    public GameObject flowerImg_obj, potImg_obj;
    public Sprite[] flowerImg_spr, potImg_spr;
    int fn=1,pn=1;
    public GameObject toastColor_obj;

    // Use this for initialization
    void Start () {
        flower_i = 0;
        pot_i = 0;
        colorN = new Color(1f, 1f, 1f);
        colorY = new Color(1f, 1f, 1f);
        colorC = new Color(1f, 1f, 1f);
        loadGM = GameObject.FindGameObjectWithTag("loadGM");
        if(PlayerPrefs.GetInt("seedbox", 0) == -10)
        {

        }
        else
        {
            StartCoroutine("TimeCheck");
        }

        if (PlayerPrefs.GetInt("getflowerpalette", 0) >= 1)
        {
            newFlower_obj.SetActive(true);
            oldFlower_obj.SetActive(false);
            newWatercan_obj.SetActive(true);
            int f = PlayerPrefs.GetInt("setflower", 0);
            flowerColor_obj.GetComponent<Image>().sprite = flowerImg_spr[f];

            f = PlayerPrefs.GetInt("setflowerpot", 0);
            flowerPotColor_obj.GetComponent<Image>().sprite = potImg_spr[f];
        }
    }

    public void OpenActflowerColor()
    {
        if (ColorWindow_obj.activeSelf == true)
        {
            ColorWindow_obj.SetActive(false);
        }
        else
        {
            ColorWindow_obj.SetActive(true);
            flower_i = PlayerPrefs.GetInt("setflower", 0);
            flowerImg_obj.GetComponent<Image>().sprite = flowerImg_spr[flower_i];
            pot_i = PlayerPrefs.GetInt("setflowerpot", 0);
            potImg_obj.GetComponent<Image>().sprite = potImg_spr[pot_i];
        }
    }

    void Color()
    {
        oldFlower_obj.SetActive(false);
        newFlower_obj.SetActive(true);
    }
    public void SetColor()
    {
        flowerPotColor_obj.GetComponent<Image>().sprite = flowerPotColor_spr[num_i];
        PlayerPrefs.GetInt("setflower", num_i);
    }
    public void returnColor()
    {
        flowerPotColor_obj.GetComponent<Image>().sprite = flowerPotColor_spr[0];
        PlayerPrefs.GetInt("setflower", num_i);
    }
    public void SetPotColor()
    {
        flowerPotColor_obj.GetComponent<Image>().sprite = flowerPotColor_spr[num_i];
        PlayerPrefs.GetInt("setflowerpot", num_i);
    }
    public void returnPotColor()
    {
        flowerPotColor_obj.GetComponent<Image>().sprite = flowerPotColor_spr[0];
        PlayerPrefs.GetInt("setflower", num_i);
    }
    
    

    public void num0()
    {
        num_i = 0;
    }
    public void num1()
    {
        num_i = 0;
    }
    public void num2()
    {
        num_i = 0;
    }
    public void num3()
    {
        num_i = 0;
    }
    public void num4()
    {
        num_i = 0;
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
        minute = 59 - minute;
        hours =  11 - hours;

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
            waterPot_obj.GetComponent<Image>().sprite = waterPot_spr[1];
        }
        seedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().flowerpot_spr[seed_i];
    }

    public void TouchSeed()
    {
        if (seed_i >= 10)
        {

        }
        else
        {
            SeedTimeFlow();

            if (hours < 0)
            {
                if (seedWater_i == seed_i)
                {
                    seed_txt.text = "" + seedCPrice_i[seed_i - 1];
                    seedHt_txt.text = "" + seedHtPrice_i[seed_i - 1];
                    seedWindow_obj.SetActive(true);
                    audio_obj.GetComponent<SoundEvt>().buttonSound();
                }
                else
                {
                    seedYetWindow_obj.SetActive(true);
                    audio_obj.GetComponent<SoundEvt>().buttonSound();
                }
            }
            else
            {
                StopCoroutine("toastYetWaterFadeOut");
                StartCoroutine("toastYetWaterFadeOut");
                seedYetWindow_obj.SetActive(true);
            }
        }
    }

    public void CloseSeed()
    {
        seedWindow_obj.SetActive(false);
        seedYetWindow_obj.SetActive(false);
    }

    public void SeedYes()
    {
        if (seed_i == 10)
        {
        }
        else
        {
            string str = PlayerPrefs.GetString("code", "");
            coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
            int ht = PlayerPrefs.GetInt(str + "ht", 0);
            if (coldRain_i >= seedCPrice_i[seed_i - 1])
            {
                if (ht >= seedHtPrice_i[seed_i - 1])
                {
                    waterPot_obj.GetComponent<Image>().sprite = waterPot_spr[0];
                    coldRain_i = coldRain_i - seedCPrice_i[seed_i - 1];
                    ht = ht - seedHtPrice_i[seed_i - 1];
                    PlayerPrefs.SetInt(str + "c", coldRain_i);
                    PlayerPrefs.SetInt(str + "ht", ht);
                    PlayerPrefs.SetString("seedLastTime", System.DateTime.Now.ToString());
                    seedWater_i = PlayerPrefs.GetInt("seedWater", 1);
                    seedWater_i++;
                    PlayerPrefs.SetInt("seedWater", seedWater_i);
                    PlayerPrefs.Save();
                    audio_obj.GetComponent<SoundEvt>().waterSound();
                }
                else
                {
                    StopCoroutine("toastneedWaterFadeOut");
                    StartCoroutine("toastneedWaterFadeOut");
                    audio_obj.GetComponent<SoundEvt>().cancleSound();
                }
                
            }
            else
            {
                //물부족캄
                StopCoroutine("toastneedWaterFadeOut");
                StartCoroutine("toastneedWaterFadeOut");
                needWaterWindow_obj.SetActive(true);
                audio_obj.GetComponent<SoundEvt>().cancleSound();
            }
        }
    }

    public void flowerLeft()
    {
        if (flower_i > 0)
        {
            flower_i--;
            flowerImg_obj.GetComponent<Image>().sprite = flowerImg_spr[flower_i];
            if (PlayerPrefs.GetInt("flowerpalette" + flower_i, 0) == 1)
            {
                fn = 1;
            }
            else
            {
                fn = 0;
                if (flower_i == 0)
                {

                    fn = 1;
                }
                else
                {
                    flowerImg_obj.GetComponent<Image>().sprite = flowerImg_spr[5];
                }
            }
        }
    }
    public void flowerRight()
    {
        if (flower_i < 4)
        {
            flower_i++;
            flowerImg_obj.GetComponent<Image>().sprite = flowerImg_spr[flower_i];
            if (PlayerPrefs.GetInt("flowerpalette" + flower_i, 0) == 1)
            {
                fn = 1;
            }
            else
            {
                fn = 0;
                flowerImg_obj.GetComponent<Image>().sprite = flowerImg_spr[5];
            }
        }
    }

    public void potLeft()
    {
        if (pot_i > 0)
        {
            pot_i--;
            potImg_obj.GetComponent<Image>().sprite = potImg_spr[pot_i];
            if (PlayerPrefs.GetInt("flowerpotpalette" + pot_i, 0) == 1)
            {
                pn = 1;
            }
            else
            {
                pn = 0;
                if (pot_i == 0)
                {

                    pn = 1;
                }
                else
                {
                    potImg_obj.GetComponent<Image>().sprite = potImg_spr[5];
                }
            }
        }
    }
    public void potRight()
    {
        if (pot_i < 4)
        {
            pot_i++;
            potImg_obj.GetComponent<Image>().sprite = potImg_spr[pot_i];
            if (PlayerPrefs.GetInt("flowerpotpalette" + pot_i, 0) == 1)
            {
                pn = 1;
            }
            else
            {
                pn = 0;
                potImg_obj.GetComponent<Image>().sprite = potImg_spr[5];
            }
        }
    }

    public void closeColor()
    {
        int k = fn + pn;
        if (k == 2)
        {
            ColorWindow_obj.SetActive(false);
            PlayerPrefs.SetInt("setflower", flower_i);
            flowerColor_obj.GetComponent<Image>().sprite = flowerImg_spr[flower_i];
            PlayerPrefs.SetInt("setflowerpot", pot_i);
            flowerPotColor_obj.GetComponent<Image>().sprite = potImg_spr[pot_i];
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
        else
        {
            StopCoroutine("toastneedColorFadeOut");
            StartCoroutine("toastneedColorFadeOut");
            audio_obj.GetComponent<SoundEvt>().cancleSound();
        }
    }
    
    //꽃색깔을 구매안했다
    IEnumerator toastneedColorFadeOut()
    {
        colorC.a = Mathf.Lerp(0f, 1f, 1f);
        toastColor_obj.GetComponent<Image>().color = colorC;
        toastColor_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorC.a = Mathf.Lerp(0f, 1f, i);
            toastColor_obj.GetComponent<Image>().color = colorC;
            yield return null;
        }
        toastColor_obj.SetActive(false);
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
        waterPot_obj.GetComponent<Image>().sprite = waterPot_spr[0];
        int a =0;
        while (a == 0)
        {
            SeedTimeFlow();
            yield return new WaitForSeconds(2f);
        }

    }

    
}

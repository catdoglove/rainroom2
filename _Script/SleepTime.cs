using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepTime : MonoBehaviour {

    //잠
    public GameObject sleepWindow_obj, sleepBlind_obj, sleepLight_obj, sleepStar_obj,sleepGone_obj;
    public Text sleepTime_txt;
    public int minute, hours;
    string lastTime;

    public GameObject[] sleep_obj;

    //꿈일기
    public GameObject dream_obj;
    public GameObject dreamBtn_obj;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
        {
            StartCoroutine("sleepTimecheck");
            sleepBlind_obj.SetActive(true);
            sleep_obj[0].SetActive(true);
            sleepGone_obj.SetActive(false);
        }
        
    }

    public void OpenSleep()
    {
        sleepWindow_obj.SetActive(true);
    }

    public void CloseSleep()
    {
        sleepWindow_obj.SetActive(false);
    }

    public void SleepY()
    {
        StopCoroutine("sleepTimecheck");
        StartCoroutine("sleepTimecheck");
        PlayerPrefs.SetInt("nowsleep", 1);
        PlayerPrefs.SetString("sleepLastTime", System.DateTime.Now.ToString());
        sleepWindow_obj.SetActive(false);
        sleepBlind_obj.SetActive(true);
        sleep_obj[0].SetActive(true);
        sleepGone_obj.SetActive(false);
        dreamBtn_obj.SetActive(false);
        PlayerPrefs.Save();
    }

    void SleepTimeFlow()
    {
        System.DateTime d = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastTime = PlayerPrefs.GetString("sleepLastTime", d.ToString());
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        hours = (int)compareTime.TotalHours;
        minute = (int)compareTime.TotalMinutes;
        minute = minute - (minute / 60) * 60;
        minute = 2 - minute;
        hours = 0 - hours;
        if (minute < 0)
        {
        }
    }

    //매초시간흐르게
    IEnumerator sleepTimecheck()
    {
        int aa=0;
        while (aa == 0)
        {
            SleepTimeFlow();
            string str = string.Format(@"{0:00}" + ":", hours) + string.Format(@"{0:00}", minute);
            if (minute <= 0)
            {
                sleepTime_txt.text = "00:00";
                sleepBlind_obj.SetActive(false);
                sleep_obj[0].SetActive(false);
                sleepGone_obj.SetActive(true);
                //StopCoroutine("sleepTimecheck");
                if(PlayerPrefs.GetInt("nowsleep", 0) == 1)
                {
                    dreamBtn_obj.SetActive(true);
                }
                PlayerPrefs.SetInt("nowsleep", 0);
                PlayerPrefs.Save();
            }
            else
            {
                sleepTime_txt.text = str;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    
    //꿈일기 창띄우기
    public void ShowDream()
    {
        dream_obj.SetActive(true);
        dreamBtn_obj.SetActive(false);
    }
}

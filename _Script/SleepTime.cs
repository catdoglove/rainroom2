using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class SleepTime : MonoBehaviour {

    //잠
    public GameObject sleepWindow_obj, sleepBlind_obj, sleepLight_obj, sleepStar_obj,sleepGone_obj;
    public Text sleepTime_txt;
    public int minute, hours;
    string lastTime;

    public GameObject[] sleep_obj;
    public int sleepBed_i;
    int n;

    //꿈일기
    public GameObject dream_obj;
    public GameObject dreamBtn_obj;

    public GameObject firstGM;

    List<Dictionary<string, object>> data_diary;
    string text_str; //실질적 대사출력
    string[] Text_cut; //대사 끊기
    int nowArr = 0; //현재 줄
    int[] randArr;//난수 필

    public GameObject[] diary_obj;
    public Text diary_today, diary_dream; //선언 및 보여질


    // Use this for initialization
    void Start () {
        n = PlayerPrefs.GetInt("bedlv", 0);
        if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
        {
            StartCoroutine("sleepTimecheck");
            sleepBlind_obj.SetActive(true);
            sleep_obj[n-1].SetActive(true);
            sleepGone_obj.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("sleepTxt", 0);
        }


        data_diary = CSVReader.Read("Talk/deardiary"); //대사 불러오기   

    }

    public int[] GetRandomInt(int length) //66
    {
        randArr = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
            while (true)
            {
                randArr[i] = Random.Range(0, length); //0~(line_txt-1)
                isSame = false;

                for (int j = 0; j < i; ++j)
                {
                    if (randArr[j] == randArr[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArr;
    }


    public void OpenSleep()
    {
        n = PlayerPrefs.GetInt("bedlv", 0);
        sleepWindow_obj.SetActive(true);
    }

    public void CloseSleep()
    {
        sleepWindow_obj.SetActive(false);
    }

    

    public void SleepY()
    {
        n = PlayerPrefs.GetInt("bedlv", 0);
        StopCoroutine("sleepTimecheck");
        StartCoroutine("sleepTimecheck");
        PlayerPrefs.SetInt("nowsleep", 1);
        PlayerPrefs.SetInt("sleepTxt", 1);
        int s = PlayerPrefs.GetInt("countinsleepst", 0);
        s++;
        PlayerPrefs.SetInt("countinsleepst", s);
        if (s >= 50 && PlayerPrefs.GetInt("insleepst", 0) < 3)
        {
            PlayerPrefs.SetInt("insleepst", 3);
            firstGM.GetComponent<AchievementShow>().achievementCheck(5, 2);
        }
        else if (s >= 10 && PlayerPrefs.GetInt("insleepst", 0) < 2)
        {
            PlayerPrefs.SetInt("insleepst", 2);
            firstGM.GetComponent<AchievementShow>().achievementCheck(5, 1);
        }
        else if (s >= 1 && PlayerPrefs.GetInt("insleepst", 0) < 1)
        {
            PlayerPrefs.SetInt("insleepst", 1);
            firstGM.GetComponent<AchievementShow>().achievementCheck(5, 0);
        }
        PlayerPrefs.SetString("sleepLastTime", System.DateTime.Now.ToString());
        sleepWindow_obj.SetActive(false);
        sleepBlind_obj.SetActive(true);
        sleep_obj[n-1].SetActive(true);
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
        minute = 59 - minute;
        hours = 5 - hours;
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
            if (minute <= 0 && hours == 0)
            {
                hours = -1;
            }
            string str = string.Format(@"{0:00}" + ":", hours) + string.Format(@"{0:00}", minute);
            if (hours <= 0)
            {
                sleepTime_txt.text = "00:00";
                sleepBlind_obj.SetActive(false);
                sleep_obj[n-1].SetActive(false);
                sleepGone_obj.SetActive(true);
                //StopCoroutine("sleepTimecheck");
                if(PlayerPrefs.GetInt("nowsleep", 0) == 1)
                {
                    dreamBtn_obj.SetActive(true);
                }
                PlayerPrefs.SetInt("nowsleep", 0);
                PlayerPrefs.SetInt("sleepTxt", 0);
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
        string str;
        str = PlayerPrefs.GetString("code", "");
        int c = PlayerPrefs.GetInt(str + "c", 0);
        int h = PlayerPrefs.GetInt(str + "h", 0);
        c = c + 100;
        h = h + 25;
        int loveExp = PlayerPrefs.GetInt("lovepoint", 0);
        loveExp = loveExp + 10;
        PlayerPrefs.SetInt("lovepoint", loveExp);
        PlayerPrefs.SetInt(str + "c", c);
        PlayerPrefs.SetInt(str + "h", h);
        PlayerPrefs.Save();

        dream_obj.SetActive(true);
        dreamBtn_obj.SetActive(false);

        lineReload();
        text_str = " " + data_diary[randArr[nowArr-1]]["일기"];
        Text_cut = text_str.Split('/');

        if (Text_cut[0] == " a")
        {
            diary_obj[0].SetActive(true);
            diary_today.text = Text_cut[1];
        }
        else if (Text_cut[0] == " b")
        {
            diary_obj[1].SetActive(true);
            diary_dream.text = Text_cut[1];

        }

    }

    void lineReload() // 대화 차례대로 보여주기 및 대화줄 초기화
    {
        if (nowArr == 0) // 난수 돌리기
        {
            GetRandomInt(65); //9>allArr[loveLv] ★꼭 바꾸기 테스트용
            nowArr++;
            //Debug.Log("돌리기");
        }
        else if (nowArr < 65) //대화 차례대로 보이기
        {
            nowArr++;
            //Debug.Log("보이기");
        }
        else if (nowArr >= 65) //대화 줄 초기화
        {
            GetRandomInt(65);
            nowArr = 0;
            nowArr++;
            //Debug.Log("초기화");
        }

    }


    public void closeDiary()
    {
        dream_obj.SetActive(false);
    }

}

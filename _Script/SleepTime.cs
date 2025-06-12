using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class SleepTime : MonoBehaviour {

    //잠
    public GameObject sleepWindow_obj, sleepBlind_obj,sleepGone_obj, sleepHelp_obj;
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

    //스위치 별
    public GameObject stars_obj;
    public GameObject switchBtn_obj;
    public Sprite[] switch_spr;
    public GameObject rabbit_obj, rabbitSleep_obj;
    public GameObject sleepMax_obj;
    public GameObject[] sleepColor_obj;

    public Animator sleep_ani, sleepOne_ani;

    // Use this for initialization
    void Start () {


        n = PlayerPrefs.GetInt("bedlv", 0);
        if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
        {
            StartCoroutine("sleepTimecheck");
            sleepBlind_obj.SetActive(true);

            if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 1)
            {
                sleepMax_obj.SetActive(true);
                if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2)
                {
                    sleepMax_obj.SetActive(false);
                    int m = PlayerPrefs.GetInt("setbedpalette", 0) + 2;
                    string s = "sleepbed"+m;
                    sleepMax_obj.SetActive(true);
                    sleep_ani.Play(s, -1, 0f);
                    //sleepColor_obj[PlayerPrefs.GetInt("setbedpalette", 0)].SetActive(true);
                }
            }
            else
            {
                sleep_obj[0].SetActive(true);
                //int k = n + 1;
                sleepOne_ani.Play("sleep"+n, -1, 0f);
            }

            sleepGone_obj.SetActive(false);
            if(PlayerPrefs.GetInt("switchshop", 0) == 2)
            {
                switchBtn_obj.SetActive(true);
            }
            if(PlayerPrefs.GetInt("starsover", 0) == 1)
            {
                stars_obj.SetActive(true);
                switchBtn_obj.GetComponent<Image>().sprite = switch_spr[1];
            }
            if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
            {
                rabbit_obj.SetActive(false);
                rabbitSleep_obj.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("sleepTxt", 0);
            sleepMax_obj.SetActive(false);
            if (PlayerPrefs.GetInt("showdir", 0) == 1)
            {
                dreamBtn_obj.SetActive(true);
            }
        }


        data_diary = CSVReader.Read("Talk/deardiary"); //대사 불러오기   

    }

    public void TurnOnSwitch()
    {
        if (stars_obj.activeSelf == true)
        {
            //꺼짐
            stars_obj.SetActive(false);
            switchBtn_obj.GetComponent<Image>().sprite = switch_spr[0];
            PlayerPrefs.SetInt("starsover", 0);
        }
        else
        {
            //켜짐
            stars_obj.SetActive(true);
            switchBtn_obj.GetComponent<Image>().sprite = switch_spr[1];
            PlayerPrefs.SetInt("starsover", 1);
        }
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
        PlayerPrefs.SetInt("showdir", 1);

        PlayerPrefs.SetString("sleepLastTime", System.DateTime.UtcNow.ToString());
        sleepWindow_obj.SetActive(false);
        sleepBlind_obj.SetActive(true);

        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 1)
        {
            sleepMax_obj.SetActive(true);
            if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2)
            {
                sleepMax_obj.SetActive(false);
                int m = PlayerPrefs.GetInt("setbedpalette", 0) + 2;
                string s = "sleepbed" + m;
                sleepMax_obj.SetActive(true);
                sleep_ani.Play(s, -1, 0f);
                //sleepColor_obj[PlayerPrefs.GetInt("setbedpalette", 0)].SetActive(true);
            }
        }
        else
        {
            sleep_obj[0].SetActive(true);
            //int k = n + 1;
            sleepOne_ani.Play("sleep" + n, -1, 0f);
        }
        sleepGone_obj.SetActive(false);
        dreamBtn_obj.SetActive(false);

        if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
        {
            rabbit_obj.SetActive(false);
            rabbitSleep_obj.SetActive(true);
        }
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("switchshop", 0) == 2)
        {
            switchBtn_obj.SetActive(true);
        }

    }

    void SleepTimeFlow()
    {
        System.DateTime d = System.DateTime.UtcNow.AddHours(-6);
        lastTime = PlayerPrefs.GetString("sleepLastTime", d.ToString());
        try
        {
            System.DateTime lastDateTimem2 = System.DateTime.Parse(lastTime);
        }
        catch (System.Exception)
        {
            lastTime = System.DateTime.UtcNow.AddHours(-6).ToString();
        }
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.UtcNow - lastDateTime;
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

                if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 1)
                {

                    if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
                    {
                        rabbit_obj.SetActive(true);
                        rabbitSleep_obj.SetActive(false);
                    }
                    sleepMax_obj.SetActive(false);
                    if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2)
                    {
                        sleepMax_obj.SetActive(false);
                        int m = PlayerPrefs.GetInt("setbedpalette", 0) + 2;
                        string s = "sleepbed" + m;
                        //sleepMax_obj.SetActive(true);
                        sleep_ani.Play(s, -1, 0f);
                        //sleepColor_obj[PlayerPrefs.GetInt("setbedpalette", 0)].SetActive(true);
                    }
                }
                else
                {
                    sleep_obj[0].SetActive(false);
                    if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
                    {
                        rabbit_obj.SetActive(true);
                        rabbitSleep_obj.SetActive(false);
                    }
                }
                sleepGone_obj.SetActive(true);
                //StopCoroutine("sleepTimecheck");
                if(PlayerPrefs.GetInt("nowsleep", 0) == 1)
                {
                    if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
                    {
                        rabbit_obj.SetActive(true);
                        rabbitSleep_obj.SetActive(false);
                    }
                    dreamBtn_obj.SetActive(true);
                    int s = PlayerPrefs.GetInt("countinsleepst", 0);
                    s++;
                    PlayerPrefs.SetInt("countinsleepst", s);
                    if (s >= 50 && PlayerPrefs.GetInt("insleepst", 0) < 3)
                    {
                        PlayerPrefs.SetInt("insleepst", 3);
                        firstGM.GetComponent<AchievementShow>().achievementCheck(5, 2);
                    }
                    else if (s >= 25 && PlayerPrefs.GetInt("insleepst", 0) < 2)
                    {
                        PlayerPrefs.SetInt("insleepst", 2);
                        firstGM.GetComponent<AchievementShow>().achievementCheck(5, 1);
                    }
                    else if (s >= 1 && PlayerPrefs.GetInt("insleepst", 0) < 1)
                    {
                        PlayerPrefs.SetInt("insleepst", 1);
                        firstGM.GetComponent<AchievementShow>().achievementCheck(5, 0);
                    }
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
        dream_obj.SetActive(true);

        if (PlayerPrefs.GetInt("randdiary", -99) == -99)
        {
            lineReload();
            PlayerPrefs.SetInt("randdiary", randArr[nowArr - 1]);
        }

        text_str = " " + data_diary[PlayerPrefs.GetInt("randdiary", 0)]["일기"];
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
        dreamBtn_obj.SetActive(false);
        dream_obj.SetActive(false);
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
        PlayerPrefs.SetInt("showdir", 0);
        PlayerPrefs.SetInt("randdiary", -99);
    }

    //도움말 열기
    public void ShowHelp()
    {
        sleepHelp_obj.SetActive(true);
    }

    //도움말 닫기
    public void CloseHelp()
    {
        sleepHelp_obj.SetActive(false);
    }
}

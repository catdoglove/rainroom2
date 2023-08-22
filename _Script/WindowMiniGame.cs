using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowMiniGame : MonoBehaviour {

    public GameObject GM;

    public GameObject miniGameWindow_obj, miniBackWindow_obj;

    public GameObject minicat_obj;

    public float cMoveX = 0f;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    //낮밤
    public GameObject dayLight_obj,dayRoom_obj,dayWindow_obj, nightchangeWindow, nightchangeWindow2;
    public Sprite[] dayLight_spr;

    //우유
    public GameObject milk_obj, milkBtn_obj,milkWindow_obj;
    public Sprite[] milk_spr;
    public Text milk_txt1, milk_txt2, milk_txt3, milkDay_txt, milkTime_txt;
    public GameObject toast_obj;
    Color color;
    public Button milkad_btn;
    public GameObject milkAdWin_obj;
    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;
    public GameObject Audio_obj;

    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;

    List<Dictionary<string, object>> data_milk;
    string text_str; //실질적 대사출력
    string Text_cut; //대사 끊기
    int nowArr = 1; //현재 줄

    //시즌
    public GameObject window_season_obj;
    public GameObject[] season_btns;
    public Sprite[] season_spr;

    public GameObject windowImg;
    public Sprite[] window_spr;
    public GameObject seasonArea;


    // Use this for initialization
    void Start () 
    {
        color = new Color(1f, 1f, 1f);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);

        if (PlayerPrefs.GetInt("windowLeafCK", 0) == 0)
        {
            windowImg.GetComponent<Image>().sprite = window_spr[1];
            seasonArea.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("windowLeafCK", 0) == 1)
        {
            windowImg.GetComponent<Image>().sprite = window_spr[0];
            seasonArea.SetActive(false);
        }



        if (PlayerPrefs.GetInt("windowsumm21", 0) == 1)
        {
            season_btns[1].SetActive(true);
        }
        

        int switch_on = PlayerPrefs.GetInt("windowbackset", 4);
        switch (switch_on)
        {
            case 0:
                window_season_obj.GetComponent<Image>().sprite = season_spr[0];
                window_season_obj.SetActive(true);
                break;
            case 1:
                window_season_obj.GetComponent<Image>().sprite = season_spr[1];
                window_season_obj.SetActive(true);
                break;
            case 2:
                window_season_obj.GetComponent<Image>().sprite = season_spr[2];
                window_season_obj.SetActive(true);
                break;
            case 3:
                window_season_obj.GetComponent<Image>().sprite = season_spr[3];
                window_season_obj.SetActive(true);
                break;
            case 4:
                window_season_obj.SetActive(false);
                break;
            default:
                break;
        }
        PlayerPrefs.Save();
        
        data_milk = CSVReader.Read("Talk/todaymilk");
    }


    public void showSeasonChange()
    {
        if (PlayerPrefs.GetInt("windowLeafCK", 0) == 0)
        {
            windowImg.GetComponent<Image>().sprite = window_spr[0];
            seasonArea.SetActive(false);
            PlayerPrefs.SetInt("windowLeafCK", 1);
        }
        else if (PlayerPrefs.GetInt("windowLeafCK", 0) == 1)
        {
            windowImg.GetComponent<Image>().sprite = window_spr[1];
            seasonArea.SetActive(true);
            PlayerPrefs.SetInt("windowLeafCK", 0);
        }

     }

    public void SetWindowSpring()
    {
        window_season_obj.GetComponent<Image>().sprite = season_spr[0];
        window_season_obj.SetActive(true);
        PlayerPrefs.SetInt("windowbackset", 0);
    }
    public void SetWindowSummer()
    {
        window_season_obj.GetComponent<Image>().sprite = season_spr[1];
        window_season_obj.SetActive(true);
        PlayerPrefs.SetInt("windowbackset", 1);
    }

    public void SetWindowFall()
    {
        window_season_obj.GetComponent<Image>().sprite = season_spr[2];
        window_season_obj.SetActive(true);
        PlayerPrefs.SetInt("windowbackset", 2);
    }
    public void SetWindowWinter()
    {
        window_season_obj.GetComponent<Image>().sprite = season_spr[3];
        window_season_obj.SetActive(true);
        PlayerPrefs.SetInt("windowbackset", 3);
    }
    public void SetWindowRe()
    {
        window_season_obj.SetActive(false);
        PlayerPrefs.SetInt("windowbackset", 4);
    }


    public void OpenMiniGame()
    {
        //우유확인
        milk();
        System.DateTime time = System.DateTime.Now;
        if (int.Parse(time.ToString("HH")) >= 12)
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                //밤
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[0];
                dayWindow_obj.SetActive(true);
                dayRoom_obj.SetActive(true);
                nightchangeWindow.SetActive(true);
                nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday",1);
            }
            else
            {
                //낮
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[1];
                dayWindow_obj.SetActive(false);
                dayRoom_obj.SetActive(false);
                nightchangeWindow.SetActive(false);
                nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[0];
                dayWindow_obj.SetActive(true);
                dayRoom_obj.SetActive(true);
                nightchangeWindow.SetActive(true);
                nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[1];
                dayRoom_obj.SetActive(false);
                dayWindow_obj.SetActive(false);
                nightchangeWindow.SetActive(false);
                nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }

        miniGameWindow_obj.SetActive(true);
        PlayerPrefs.SetInt("miniopen",1);
        if(PlayerPrefs.GetInt("windowcatrand", 0) <= 10)
        {
            PlayerPrefs.SetInt("windowcatrand", 999);
            int cat = Random.Range(-2, 2);
            cMoveX = cat;
            minicat_obj.transform.position = new Vector3(cMoveX, minicat_obj.transform.position.y, minicat_obj.transform.position.z);
            minicat_obj.SetActive(true);
        }
        PlayerPrefs.SetInt("windowairplane", 999);
        PlayerPrefs.Save();
    }

    public void CloseMiniGame()
    {
        milkWindow_obj.SetActive(false);
        miniGameWindow_obj.SetActive(false);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        minicat_obj.SetActive(false);
    }

    public void ball1()
    {
        float xx = GM.GetComponent<MainTime>().moveX1;
        float yy = GM.GetComponent<MainTime>().balloon_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        GM.GetComponent<MainTime>().randball1_i = 0;
        GM.GetComponent<MainTime>().moveX1 = 15.4f;
        GM.GetComponent<MainTime>().balloon_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().moveX1, GM.GetComponent<MainTime>().balloon_obj.transform.position.y, GM.GetComponent<MainTime>().balloon_obj.transform.position.z);
    }

    public void ball2()
    {
        float xx = GM.GetComponent<MainTime>().moveX2;
        float yy = GM.GetComponent<MainTime>().balloonR_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        GM.GetComponent<MainTime>().randball2_i = 0;
        GM.GetComponent<MainTime>().moveX2 = 15.4f;
        GM.GetComponent<MainTime>().balloonR_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().moveX2, GM.GetComponent<MainTime>().balloonR_obj.transform.position.y, GM.GetComponent<MainTime>().balloonR_obj.transform.position.z);
    }

    public void TouchBallon()
    {
        
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().endBMove_i = 0;
    }

    public void TouchCat()
    {
        float xx = cMoveX;
        float yy = minicat_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        achievementfunc2();
        minicat_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 10;
        hotRain_i = hotRain_i + 6;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    public void TouchAirplane()
    {
        float xx = GM.GetComponent<MainTime>().pMoveX;
        float yy = GM.GetComponent<MainTime>().airplane_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        achievementfunc();
        GM.GetComponent<MainTime>().pMoveX = 17.4f;
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 50;
        hotRain_i = hotRain_i + 20;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowairplane", 999);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().airplane_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().pMoveX, GM.GetComponent<MainTime>().airplane_obj.transform.position.y, GM.GetComponent<MainTime>().airplane_obj.transform.position.z);
        GM.GetComponent<MainTime>().plane_i = 0;
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }


    void milk()
    {
        //초기값을가져옵니다
        System.DateTime dateTimenow = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //str로장되어있는과거접속시간을가져옵니다
        string lastTimem = PlayerPrefs.GetString("milktime", dateTimenow.ToString());
        //형변환을해줍니다
        System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);
        //계산
        System.TimeSpan compareTimem = System.DateTime.Now - lastDateTimem;
        int hour;
        int minute;
        hour = (int)compareTimem.TotalHours;
        minute =(int)compareTimem.TotalMinutes;
        minute = minute - (minute / 60) * 60;
        hour = 9 - hour;
        minute = 59 - minute;
        if(hour==0&& minute == 0)
        {
            hour = -1;
        }
        if (hour <0)
        {
            milk_obj.GetComponent<Image>().sprite = milk_spr[1];
            milkBtn_obj.SetActive(true);
        }
        else
        {
            milk_obj.GetComponent<Image>().sprite = milk_spr[0];
            milkBtn_obj.SetActive(false);
        }
    }

    public void GetMilk()
    {
        string str = PlayerPrefs.GetString("code", "");
        milkWindow_obj.SetActive(true);
        milkDay_txt.text = System.DateTime.Now.ToString("yyyy.MM.dd");
        milk_txt1.text = "";
        milk_txt2.text = "";
        milk_txt3.text = "";
        int cm = PlayerPrefs.GetInt(str + "c", 0);
        int hm = PlayerPrefs.GetInt(str + "h", 0);
        int htm = PlayerPrefs.GetInt(str + "ht", 0);
        cm = cm + 100;
        hm = hm + 10;
        htm = htm + 2;

        GM.GetComponent<AdmobADSMilk>().Toast_obj2.SetActive(true);
        GM.GetComponent<AdmobADSMilk>().Toast_contain2.SetActive(true);
        GM.GetComponent<AdmobADSMilk>().Toast_contain3.SetActive(false);

        if (PlayerPrefs.GetInt("milkadc", 0) == 1)
        {
            cm = cm + 100;
            hm = hm + 10;
            htm = htm + 2;
            PlayerPrefs.SetInt("milkadc", 0);
            PlayerPrefs.SetInt("setmilkadc", 0);
        }
        PlayerPrefs.SetInt(str + "c", cm);
        PlayerPrefs.SetInt(str + "h", hm);
        PlayerPrefs.SetInt(str + "ht", htm);

        //시간초기화
        PlayerPrefs.SetString("milktime", System.DateTime.Now.ToString());
        milk_obj.GetComponent<Image>().sprite = milk_spr[0];
        milkBtn_obj.SetActive(false);
        
        nowArr = PlayerPrefs.GetInt("milkText", 1);

        if (nowArr == 1)
        {
            nowArr++;
        }
        else if (nowArr < 100) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= 100) //대화 줄 초기화
        {
            nowArr = 0;
            nowArr++;
        }
        
        text_str = " " + data_milk[nowArr - 1]["1"];
        Text_cut = "오늘의" + text_str;
        milk_txt1.text = Text_cut;
        text_str = " " + data_milk[nowArr - 1]["2"];
        Text_cut = text_str;
        milk_txt2.text = Text_cut;
        text_str = " " + data_milk[nowArr - 1]["3"];
        Text_cut = text_str;
        milk_txt3.text = Text_cut;

        PlayerPrefs.SetInt("milkText", nowArr);

        //엔딩우유
        endg();

    }
    
    //우유광고
    public void MilkYesNo()
    {
        milkAdWin_obj.SetActive(true);

    }
    public void MilkYes()
    {
        milkAdWin_obj.SetActive(false);
        GetMilk();
    }
    public void MilkNo()
    {
        milkAdWin_obj.SetActive(false);
    }
    
    public void MilkAd()
    {
        //PlayerPrefs.SetInt("adrunout", 1);
        PlayerPrefs.SetInt("setmilkadc", 1);
    }






    public void toastMilk()
    {
        StopCoroutine("toastMilkTime");
        StartCoroutine("toastMilkTime");
    }

    public void CloseMlik()
    {
        milkWindow_obj.SetActive(false);
    }

    /// <summary>
    /// 엔딩대화
    /// </summary>
    void endg()
    {
        int k = 0;
        if (PlayerPrefs.GetInt("milkending", 0) == 0)
        {
            if (PlayerPrefs.GetInt("milkendcnt", 0) >= 9)
            {
                //수집완료
                PlayerPrefs.SetInt("milkending", 1);
                GM.GetComponent<EndingBox>().shopNum = 7;
                GM.GetComponent<EndingBox>().PlayEnd();
                GM.GetComponent<EndingBox>().end_ani.Play("endMlik1", -1, 0f);
            }
            else
            {
                k = PlayerPrefs.GetInt("milkendcnt", 0);
                k++;
                PlayerPrefs.SetInt("milkendcnt", k);
            }
        }
    }

    public void CloseEnd()
    {
        endWindow_obj.SetActive(false);
        Audio_obj.GetComponent<SoundEvt>().cancleSound();

        //소리
        m_end.clip = sp_original;
        m_end.Play();
    }

    public void endR()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i == 1)
        {
            endR_obj.SetActive(false);
            endClose_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endL_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }
    public void endL()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj.SetActive(false);
        if (end_i == 1)
        {
            endL_obj.SetActive(false);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endR_obj.SetActive(true);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }


    //업적
    void achievementfunc()
    {
        
        int cts = PlayerPrefs.GetInt("countairplanest", 0);
        cts++;
        PlayerPrefs.SetInt("countairplanest", cts);
        if (cts >= 100 && PlayerPrefs.GetInt("airplanest", 0) < 3)
        {
            PlayerPrefs.SetInt("airplanest", 3);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 2);
        }
        else if (cts >= 10 && PlayerPrefs.GetInt("airplanest", 0) < 2)
        {
            PlayerPrefs.SetInt("airplanest", 2);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("airplanest", 0) < 1)
        {
            PlayerPrefs.SetInt("airplanest", 1);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 0);
        }
    }

    void achievementfunc2()
    {
        
        int cts = PlayerPrefs.GetInt("countpetcatst", 0);
        cts++;
        PlayerPrefs.SetInt("countpetcatst", cts);
        if (cts >= 500 && PlayerPrefs.GetInt("petcatst", 0) < 3)
        {
            PlayerPrefs.SetInt("petcatst", 3);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 2);
        }
        else if (cts >= 100 && PlayerPrefs.GetInt("petcatst", 0) < 2)
        {
            PlayerPrefs.SetInt("petcatst", 2);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("petcatst", 0) < 1)
        {
            PlayerPrefs.SetInt("petcatst", 1);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 0);
        }
        //Debug.Log("cat" + PlayerPrefs.GetInt("petcatst", 0) + "count" + PlayerPrefs.GetInt("countpetcatst", 0));
    }

    void achievement()
    {
        StartCoroutine("achievementIn");
    }



    IEnumerator achievementOut()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 1f; i > -0.2f; i -= 0.05f)
        {
            moveY = moveY + 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
    }
    IEnumerator achievementIn()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 0f; i < 1.2f; i += 0.05f)
        {
            moveY = moveY - 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine("achievementOut");
    }


    IEnumerator toastMilkTime()
    {
        //초기값을가져옵니다
        System.DateTime dateTimenow = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //str로장되어있는과거접속시간을가져옵니다
        string lastTimem = PlayerPrefs.GetString("milktime", dateTimenow.ToString());
        //형변환을해줍니다
        System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);
        //계산
        System.TimeSpan compareTimem = System.DateTime.Now - lastDateTimem;
        int hour;
        int minute;
        hour = (int)compareTimem.TotalHours;
        minute = (int)compareTimem.TotalMinutes;
        minute = minute - (minute / 60) * 60;
        hour = 9 - hour;
        minute = 59 - minute;
        if (minute < 0)
        {
            minute = 0;
        }
        if (hour < 0)
        {
            hour = 0;
        }
        milkTime_txt.text = string.Format(@"{0:00}" + ":", hour) + string.Format(@"{0:00}", minute);

        color.a = Mathf.Lerp(0f, 1f, 1f);
        toast_obj.GetComponent<Image>().color = color;
        toast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        toast_obj.SetActive(false);
    }
    

}

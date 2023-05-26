using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Parkfunction : CavasData
{
    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;
    //미리 씬을 불러오기
    AsyncOperation async;
    //밤
    public GameObject dayRoom, dayRoom2;
    public GameObject nightShop_obj,dayShop_obj;
    //소리
    public GameObject audio_obj;
    //랜덤
    public int eventRand_i;
    public GameObject eventPet_obj, eventPaint_obj, event_obj, eventWait_obj, eventWin_obj;
    public Sprite[] event_spr;
    //도움말
    public GameObject helpPark_obj;
    public Sprite[] helpP_spr;
    int help;
    //
    public GameObject GMtag,GMP, blackimg;
    public GameObject leafPr_obj,leafWin_obj;
    public GameObject eventNight_obj,eventNightPet_obj;

    //숲으로가기
    public GameObject mountainWindow_obj,mountainAD_obj,mountainToast_obj, needToast_obj;
    Color color, colorP;
    string str;
    public Text outPrice_txt, outTime_txt, hPrice_txt;
    public GameObject outP_obj, outGo_obj, outAd_obj, outAdBtn_obj;
    
    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj,endL_obj,endClose_obj;


    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;

    public GameObject tre5_obj;

    private void Awake()
    {
        //초기화
        str = PlayerPrefs.GetString("code", "");
        colorP = new Color(1f, 1f, 1f);
        color = new Color(1f, 1f, 1f);

        
        //나뭇잎
        int lf = PlayerPrefs.GetInt("leafcount", 0);
        if (lf >= 100)
        {
            leafPr_obj.SetActive(true);
        }
        
        //외출시 스페이드 얻기
        if (PlayerPrefs.GetInt("outspade", 0) >= 1)
        {
            PlayerPrefs.SetInt("outspade", 1);
        }

        //외출업적
        if (PlayerPrefs.GetInt("acgocheck", 0) == 1)
        {
            checkachOut();
            PlayerPrefs.SetInt("acgocheck", 0);
        }
        
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMtag.GetComponent<MainBtnEvt>().comeHome_obj.SetActive(true);
        GMtag.GetComponent<MainBtnEvt>().shop_obj.SetActive(false);
        GMtag.GetComponent<MainBtnEvt>().SetClover();
        //공원에 처음 왔을때
        if (PlayerPrefs.GetInt("parkfirst", 0)==0)
        {
            helpPark_obj.SetActive(true);
            PlayerPrefs.SetInt("parkfirst", 1);
            PlayerPrefs.Save();
        }
        else
        {
            endg();
        }


        //보물찾기
        if (PlayerPrefs.GetInt("gettre5", 0) == 1)
        {
            tre5_obj.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        //밤낮
        setDay();
        //이밴트 랜덤 
        eventRand_i = Random.Range(0, 5);

        if (eventRand_i < 2)
        {
            eventPet_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[1];
        }
        else if(eventRand_i < 4)
        {
            eventPaint_obj.SetActive(true);
            event_obj.GetComponent<Image>().sprite = event_spr[2];
        }
        else
        {
            event_obj.GetComponent<Image>().sprite = event_spr[0];
            eventWait_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("dayday", 0) == 1)
        {
            eventPaint_obj.SetActive(false);
            eventPet_obj.SetActive(false);
            event_obj.GetComponent<Image>().sprite = event_spr[0];
            if (eventRand_i < 2)
            {
                event_obj.SetActive(false);
                eventNight_obj.SetActive(true);
            }
            else if (eventRand_i < 4)
            {
                event_obj.SetActive(false);
                eventNightPet_obj.SetActive(true);
            }
        }
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;

        if (PlayerPrefs.GetInt("parkgock", 0) == 0)
        {

            if (PlayerPrefs.GetInt("outorhome", 0) == 1)
            {
                eventWait_obj.SetActive(true);
                if (PlayerPrefs.GetInt("dayday", 0) == 1)
                {
                    eventPaint_obj.SetActive(false);
                    eventPet_obj.SetActive(false);
                    event_obj.GetComponent<Image>().sprite = event_spr[0];
                    eventNight_obj.SetActive(false);
                    eventNightPet_obj.SetActive(false);
                    event_obj.SetActive(true);
                }
                else
                {
                    eventPet_obj.SetActive(false);
                    eventPaint_obj.SetActive(false);
                    event_obj.GetComponent<Image>().sprite = event_spr[0];
                }
            }

        }
        //외출중
        PlayerPrefs.SetInt("outorhome", 1);
        PlayerPrefs.SetInt("parkgock", 0);
        PlayerPrefs.Save();

    }

    public void ParaWait()
    {
        eventWin_obj.SetActive(true);
    }
    public void ParaWaitY()
    {
        int cvp = PlayerPrefs.GetInt(str + "cv", 0);
        int randp = Random.Range(0, 4);

        if (PlayerPrefs.GetInt("dayday", 0) == 1)//밤
        {
            if (cvp >= 4)
            {
                cvp = cvp - 4;
                PlayerPrefs.SetInt(str + "cv", cvp);
                PlayerPrefs.Save();

                if (randp < 2)
                {
                    event_obj.SetActive(false);
                    eventNight_obj.SetActive(true);
                }
                else if (randp < 4)
                {
                    event_obj.SetActive(false);
                    eventNightPet_obj.SetActive(true);
                }
                else
                {
                    event_obj.SetActive(false);
                    eventNightPet_obj.SetActive(true);
                }

                eventWait_obj.SetActive(false);
            }
            else
            {
                needMoney();
            }
        }
        else
        {
            if (cvp >= 4)
            {
                cvp = cvp - 4;
                PlayerPrefs.SetInt(str + "cv", cvp);
                PlayerPrefs.Save();
                if (randp < 2)
                {
                    eventPet_obj.SetActive(true);
                    event_obj.GetComponent<Image>().sprite = event_spr[1];
                }
                else if (randp < 4)
                {
                    eventPaint_obj.SetActive(true);
                    event_obj.GetComponent<Image>().sprite = event_spr[2];
                }
                else
                {
                    eventPaint_obj.SetActive(true);
                    event_obj.GetComponent<Image>().sprite = event_spr[2];
                }
                eventWait_obj.SetActive(false);
            }
            else
            {
                needMoney();
            }
        }
        eventWin_obj.SetActive(false);

    }
    public void ParaWaitN()
    {
        eventWin_obj.SetActive(false);
    }

    //도움말
    public void CloseHelpP()
    {
        if (help == 0)
        {
            help = 1;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[1];
        }
        else
        {
            help = 0;
            helpPark_obj.GetComponent<Image>().sprite = helpP_spr[0];
            helpPark_obj.SetActive(false);
            endg();
        }
    }

    //밤낮
    public void setDay()
    {

        //이밴트 랜덤 
        eventRand_i = Random.Range(0, 3);
        System.DateTime time = System.DateTime.Now;
        if (time.ToString("tt") == "PM" || time.ToString("tt") == "오후")
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
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
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
                dayRoom.SetActive(true);
                dayRoom2.SetActive(true);
                nightShop_obj.SetActive(true);
                dayShop_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                nightShop_obj.SetActive(false);
                dayShop_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }


    /// <summary>
    /// 엔딩공원
    /// </summary>
    void endg()
    {
        if (PlayerPrefs.GetInt("parkending", 0) == 0)
        {
            PlayerPrefs.SetInt("parkending", 1);
            GMP.GetComponent<EndingBox>().shopNum = 5;
            GMP.GetComponent<EndingBox>().PlayEnd();
            GMP.GetComponent<EndingBox>().end_ani.Play("endPark1", -1, 0f);
            //소리
            m_end.clip = sp_end;
            m_end.Play();
        }
    }

    public void CloseEnd()
    {
        endWindow_obj.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().cancleSound();

        //소리
        m_end.clip = sp_original;
        m_end.Play();
    }

    public void endR()
    {
        audio_obj.GetComponent<SoundEvt>().turnSound();
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
        audio_obj.GetComponent<SoundEvt>().turnSound();
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

    //외출업적
    void checkachOut()
    {
        int cts = PlayerPrefs.GetInt("countgooutst", 0);
        cts++;
        PlayerPrefs.SetInt("countgooutst", cts);
        if (cts >= 100 && PlayerPrefs.GetInt("gooutst", 0) < 3)
        {
            PlayerPrefs.SetInt("gooutst", 3);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 2);
        }
        else if (cts >= 30 && PlayerPrefs.GetInt("gooutst", 0) < 2)
        {
            PlayerPrefs.SetInt("gooutst", 2);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("gooutst", 0) < 1)
        {
            PlayerPrefs.SetInt("gooutst", 1);
            GMP.GetComponent<AchievementShow>().achievementCheck(7, 0);
        }
        PlayerPrefs.Save();
    }


    //산으로가기
    public void GoMountainWindow()
    {
        if (mountainWindow_obj.activeSelf == true)
        {
            mountainWindow_obj.SetActive(false);
            mountainAD_obj.SetActive(false);
        }
        else
        {
            hPrice_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            if (PlayerPrefs.GetInt("dayday", 0) == 1)
            {
                //밤이라 못감
                mountainToast_obj.SetActive(true);
                StartCoroutine("toastMountainFadeOut");
            }
            else
            {
                //시간흐르게
                StopCoroutine("outTime");
                StartCoroutine("outTime");
                mountainWindow_obj.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("ForUnityADSnewReward", 77);
    }
    public void WaitShow()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            outAd_obj.SetActive(true);
        }
    }

    public void GoMountian()
    {
        int hotR_i;
        hotR_i = PlayerPrefs.GetInt(str + "h", 0);
        int hp_i = 240;
        if (PlayerPrefs.GetInt("foresttime", 9) == 4)
        {
            hp_i = hp_i - 120;
        }
        if (hotR_i >= hp_i)//240온수가 있는가?
        {
            PlayerPrefs.SetString("outlasttimepark", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("outtrip", 3);
            
            hotR_i = hotR_i - hp_i;
            PlayerPrefs.SetInt(str + "h", hotR_i);
            PlayerPrefs.SetInt("foresttime", 9);
            PlayerPrefs.Save();

            hPrice_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            //업적
            //PlayerPrefs.SetInt("acgocheck", 1);
            //checkachOut();
            MemoryDestroy();
            StartCoroutine("LoadOut");
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
        else
        {
            audio_obj.GetComponent<SoundEvt>().cancleSound();
            needMoney();
            mountainAD_obj.SetActive(false);
            mountainWindow_obj.SetActive(false);
            //돈부족
        }
    }


    IEnumerator outTime()
    {
        int a = 0;
        while (a == 0)
        {
            if (PlayerPrefs.GetInt("foresttime", 9) == 4)
            {
                outAdBtn_obj.GetComponent<Button>().interactable = false;
                outPrice_txt.text = "120";
            }
            if (PlayerPrefs.GetInt("outtimeonpark", 0) == 0)
            {
                outTime_txt.text = "00:00";
                outGo_obj.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("outtimeonpark", 1);
            }
            else
            {
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                System.DateTime lastDateTime = System.DateTime.Parse(PlayerPrefs.GetString("outlasttimepark", dateTime.ToString()));
                System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
                int m = (int)compareTime.TotalMinutes;
                int sec = (int)compareTime.TotalSeconds;
                sec = sec - (sec / 60) * 60;
                sec = 59 - sec;
                m = PlayerPrefs.GetInt("foresttime", 9) - m;
                string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
                outTime_txt.text = strb;
                if (m < 0)
                {
                    outTime_txt.text = "00:00";
                    PlayerPrefs.SetInt("outtimeonpark", 0);
                    PlayerPrefs.Save();
                    outGo_obj.GetComponent<Button>().interactable = true;
                }
                else
                {
                    outGo_obj.GetComponent<Button>().interactable = false;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    //산으로가기
    public void ADWindowYN()
    {
        if (outAd_obj.activeSelf == true)
        {
            outAd_obj.SetActive(false);
        }
        else
        {
            outAd_obj.SetActive(true);
        }
    }

    //어떻게 갈까 창열기
    public void OpenMountainAD()
    {
        mountainAD_obj.SetActive(true);
    }


    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }


    //양동이
    public void getBas()
    {
        string str = PlayerPrefs.GetString("code", "");
        int myc = PlayerPrefs.GetInt(str + "c", 0);
        if (PlayerPrefs.GetInt("basketrain", 0) != 0)
        {
            audio_obj.GetComponent<SoundEvt>().waterSound();
        }
        if(PlayerPrefs.GetInt("basketrain", 0) >= 100)
        {
            PlayerPrefs.SetInt("basketrain", 100);
        }
        myc = myc + PlayerPrefs.GetInt("basketrain", 0);
        PlayerPrefs.SetInt(str + "c", myc);
        PlayerPrefs.SetInt("basketrain", 0);

    }

    public void leafPlant()
    {
        leafPr_obj.SetActive(false);
        leafWin_obj.SetActive(true);
        PlayerPrefs.SetInt("leafget", 1);
        PlayerPrefs.SetInt("leafcount", 0);
    }
    public void CloseLeaf()
    {
        leafWin_obj.SetActive(false);
    }



    //밤에는 못가
    IEnumerator toastMountainFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        mountainToast_obj.GetComponent<Image>().color = color;
        mountainToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            mountainToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        mountainToast_obj.SetActive(false);
    }


    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorP.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorP;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorP.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorP;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }

    /// <summary>
    /// 메모리 해제 함수
    /// </summary>
    void MemoryDestroy()
    {
        blackimg.SetActive(true);
        GMP.GetComponent<ParkTime>().bg_front.GetComponent<Image>().sprite = null;
        GMP.GetComponent<ParkTime>().bg_back.GetComponent<Image>().sprite = null;
        GMP.GetComponent<ParkTime>().moonbangu_img.GetComponent<Image>().sprite = null;
        GMP.GetComponent<ParkTime>().bas_obj.GetComponent<Image>().sprite = null;
    }


    public void OpenTra5()
    {
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre5_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre5", 1);
        GMtag.GetComponent<MainBtnEvt>().CheckTre();
    }

    
}

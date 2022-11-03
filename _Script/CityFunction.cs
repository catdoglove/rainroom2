using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CityFunction : CavasData
{
    public GameObject GMtag, GMC;
    public GameObject buildToast_obj, blackimg;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    public GameObject inHelp_obj, reHelp_obj,foHelp_obj,TuHelp_obj;

    //밤
    public GameObject dayRoom, dayRoom2;
    public GameObject nightShop_obj, dayShop_obj, helpCity_obj;
    public GameObject noteSign_obj;
    //도움말
    int help=0;
    public Sprite[] helpC_spr;

    //바다로가기
    public GameObject seaWindow_obj, seaAD_obj, seaToast_obj, needToast_obj;
    Color color, colorP;
    string str;
    public Text outPrice_txt, outTime_txt, hPrice_txt;
    public GameObject outP_obj, outGo_obj, outAd_obj, outAdBtn_obj;
    public GameObject audio_obj;

    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;


    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;

    //미리 씬을 불러오기
    AsyncOperation async;

    public GameObject tre3_obj, tre4_obj;

    // Use this for initialization
    void Start () {


        color = new Color(1f, 1f, 1f);

        str = PlayerPrefs.GetString("code", "");


        //외출시 스페이드 얻기
        if (PlayerPrefs.GetInt("outspade", 0) >= 1)
        {
            PlayerPrefs.SetInt("outspade", 1);
        }

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
        //외출업적
        if (PlayerPrefs.GetInt("acgocheck", 0) == 1)
        {
            checkachOut();
            PlayerPrefs.SetInt("acgocheck", 0);
        }
        //도시에 처음 왔을때
        if (PlayerPrefs.GetInt("cityfirst", 0) == 0)
        {
            helpCity_obj.SetActive(true);
            PlayerPrefs.SetInt("cityfirst", 1);
            PlayerPrefs.Save();
        }
        else
        {
            endg();
        }

        //외출중
        PlayerPrefs.SetInt("outorhome", 2);
        PlayerPrefs.Save();


        //보물찾기
        if (PlayerPrefs.GetInt("gettre3", 0) == 1)
        {
            tre3_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gettre4", 0) == 1)
        {
            tre4_obj.SetActive(false);
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
            GMC.GetComponent<AchievementShow>().achievementCheck(7, 2);
        }
        else if (cts >= 30 && PlayerPrefs.GetInt("gooutst", 0) < 2)
        {
            PlayerPrefs.SetInt("gooutst", 2);
            GMC.GetComponent<AchievementShow>().achievementCheck(7, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("gooutst", 0) < 1)
        {
            PlayerPrefs.SetInt("gooutst", 1);
            GMC.GetComponent<AchievementShow>().achievementCheck(7, 0);
        }
        PlayerPrefs.Save();
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


    public void OpenActHelpT()
    {
        if (TuHelp_obj.activeSelf == true)
        {
            TuHelp_obj.SetActive(false);
        }
        else
        {
            TuHelp_obj.SetActive(true);
        }
    }


    public void OpenActHelpFood()
    {
        if (foHelp_obj.activeSelf == true)
        {
            foHelp_obj.SetActive(false);
        }
        else
        {
            foHelp_obj.SetActive(true);
        }
    }

    public void OpenActHelpF()
    {
        if (help == 0)
        {
            helpCity_obj.GetComponent<Image>().sprite = helpC_spr[1];
            helpCity_obj.SetActive(true);
            help = 1;
        }
        else if (help == 1)
        {
            helpCity_obj.GetComponent<Image>().sprite = helpC_spr[0];
            helpCity_obj.SetActive(false);
            help = 2;
            endg();
        }
    }
    /// <summary>
    /// 엔딩도시
    /// </summary>
    void endg()
    {
        if (PlayerPrefs.GetInt("cityending", 0) == 0)
        {
            PlayerPrefs.SetInt("cityending", 1);
            GMC.GetComponent<EndingBox>().shopNum = 6;
            GMC.GetComponent<EndingBox>().PlayEnd();
            GMC.GetComponent<EndingBox>().end_ani.Play("endCity1", -1, 0f);
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


    //바다가기
    public void GoSeaWindow()
    {
        if (seaWindow_obj.activeSelf == true)
        {
            seaWindow_obj.SetActive(false);
            seaAD_obj.SetActive(false);
        }
        else
        {
            hPrice_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            if (PlayerPrefs.GetInt("dayday", 0) == 1)
            {
                //밤이라 못감
                seaToast_obj.SetActive(true);
                StartCoroutine("toastSeaFadeOut");
            }
            else
            {
                //시간흐르게
                StopCoroutine("outTime");
                StartCoroutine("outTime");
                seaWindow_obj.SetActive(true);
            }
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

    public void WaitShow()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            outAd_obj.SetActive(true);
        }
    }

    //어떻게 갈까 창열기
    public void OpenSeaAD()
    {
        seaAD_obj.SetActive(true);
    }

    //버스정류장 바다
    public void ActbusGo()
    {
        int hotR_i;
        hotR_i = PlayerPrefs.GetInt(str + "h", 0);
        int hp_i = 240;
        if (PlayerPrefs.GetInt("seatime", 9) == 4)
        {
            hp_i = hp_i - 120;
        }
        if (hotR_i >= hp_i)//240온수가 있는가?
        {
            PlayerPrefs.SetString("outlasttimecity", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("outtrip", 4);
            hotR_i = hotR_i - hp_i;
            PlayerPrefs.SetInt(str + "h", hotR_i);
            PlayerPrefs.SetInt("seatime", 9);
            PlayerPrefs.Save();
            hPrice_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            MemoryDestroy();
            StartCoroutine("LoadOut");
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
        else
        {
            audio_obj.GetComponent<SoundEvt>().cancleSound();
            needMoney();
            seaAD_obj.SetActive(false);
            seaWindow_obj.SetActive(false);
            //돈부족
        }
    }


    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }


    //버스바다시간
    IEnumerator outTime()
    {
        int a = 0;
        while (a == 0)
        {
            if (PlayerPrefs.GetInt("seatime", 9) == 4)
            {
                outAdBtn_obj.GetComponent<Button>().interactable = false;
                outPrice_txt.text = "120";
            }
            if (PlayerPrefs.GetInt("outtimeoncity", 0) == 0)
            {
                outTime_txt.text = "00:00";
                outGo_obj.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("outtimeoncity", 1);
            }
            else
            {
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                System.DateTime lastDateTime = System.DateTime.Parse(PlayerPrefs.GetString("outlasttimecity", dateTime.ToString()));
                System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
                int m = (int)compareTime.TotalMinutes;
                int sec = (int)compareTime.TotalSeconds;
                sec = sec - (sec / 60) * 60;
                sec = 59 - sec;
                m = PlayerPrefs.GetInt("seatime", 9) - m;
                string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
                outTime_txt.text = strb;
                if (m < 0)
                {
                    outTime_txt.text = "00:00";
                    PlayerPrefs.SetInt("outtimeoncity", 0);
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



    //밤에는 못가
    IEnumerator toastSeaFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        seaToast_obj.GetComponent<Image>().color = color;
        seaToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            seaToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        seaToast_obj.SetActive(false);
    }


    //돈이 부족하다
    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }


    //온수가 부족하다
    IEnumerator toastNImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = color;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }


    //밤낮
    public void setDay()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("UI/noteStore/cirt_note_enter");

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
                PlayerPrefs.SetInt("dayday", 1);
                noteSign_obj.GetComponent<Image>().sprite = sprites[1];
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
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
                PlayerPrefs.SetInt("dayday", 1);
                noteSign_obj.GetComponent<Image>().sprite = sprites[1];
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                dayRoom2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }


    /// <summary>
    /// 메모리 해제 함수
    /// </summary>
    void MemoryDestroy()
    {
        blackimg.SetActive(true);
        GMC.GetComponent<CityTime>().bg_front.GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityTime>().bg_back.GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityShop>().interiorTape_obj[0].GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityShop>().interiorTape_obj[1].GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityShop>().interiorTape_obj[2].GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityShop>().interiorTape_obj[3].GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityShop>().interiorTape_obj[4].GetComponent<Image>().sprite = null;
        GMC.GetComponent<CityFunction>().noteSign_obj.GetComponent<Image>().sprite = null;
    }

    public void OpenTr3()
    {
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre3_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre3", 1);
        GMtag.GetComponent<MainBtnEvt>().CheckTre();
    }

    public void OpenTra4()
    {
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre4_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre4", 1);
        GMtag.GetComponent<MainBtnEvt>().CheckTre();
    }

    /// <summary>
    /// 업적 팝업 터치 인포 뒷면 보기
    /// </summary>
    public void AcBtnShowInfoBack()
    {
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        if (GMtag.GetComponent<MainBtnEvt>().MainWindow_obj[0].activeSelf)
        {
            GMtag.GetComponent<MainInfo>().infoWindowTurn();
        }
        else
        {
            GMtag.GetComponent<MainInfo>().infoShow();
            GMtag.GetComponent<MainBtnEvt>().openInfoWindow();
            GMtag.GetComponent<MainInfo>().infoWindowTurn();
        }
    }
}

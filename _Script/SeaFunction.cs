using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeaFunction : MonoBehaviour {

    //미리 씬을 불러오기
    AsyncOperation async;

    public GameObject squral_obj, sign_obj, right_obj, left_obj, box_obj,tesureWindow_obj, squralWindow_obj, resultWindow_obj;
    public GameObject backGround_obj, backGround2_obj,sqH_obj, audio_obj;
    public GameObject[] tresure_obj, boxRL_obj;
    public Sprite[] tresure_spr,background_spr;
    public int moveCount_i,randomGet_i,tresureCount_i,sign_i,sq_i;
    public int[] tresureSet_i;
    public Text clover_txt,resultClover_txt;
    string str;
    int haveClover_i;
    int clover_i = 0;
    //
    public Text moveCount_txt;

    public Animator walk_Ani;

    public Text sign_text;
    public GameObject signWindow_obj;
    List<Dictionary<string, object>> data_sign;
    string text_str; //실질적 대사출력
    string Text_cut; //대사 끊기
    int nowArr = 1; //현재 줄

    int ik;
    //
    Color colorN;
    public GameObject needToast_obj;

    //외물물건
    public GameObject putToast_obj;
    public GameObject shrimp_obj, shrimpWindow_obj;

    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;

    public AudioSource m_end;
    public AudioClip sp_end, sp_original;
    public GameObject GM;

    /// <summary>
    ///  병글씨
    /// </summary>
    void signText()
    {
        nowArr = PlayerPrefs.GetInt("nowarrsign", 1);
        text_str = " " + data_sign[nowArr - 1]["bottle"];
        Text_cut = "" + text_str;
        sign_text.text = Text_cut;
        nowArr++;
        if (nowArr >= 100)
        {
            nowArr = 1;
        }
        PlayerPrefs.SetInt("nowarrsign", nowArr);
    }

    //주꾸미
    void shrimp()
    {
        int sh = Random.Range(0, 10);
        if (sh == 1)
        {
            shrimp_obj.SetActive(true);
        }
    }
    public void ActShrimp()
    {
        if (shrimpWindow_obj.activeSelf == true)
        {
            shrimpWindow_obj.SetActive(false);
            shrimp_obj.SetActive(false);
            //PlayerPrefs.SetInt("shrimp", 1);
            //PlayerPrefs.SetInt("ggomak", 1);
            //PlayerPrefs.SetInt("icebars", 1);
            PlayerPrefs.SetInt("ggomak2", 1);
        }
        else
        {
            shrimpWindow_obj.SetActive(true);
        }
    }

    //글씨창 여닫기
    public void ActSingText()
    {
        if (signWindow_obj.activeSelf == true)
        {
            signWindow_obj.SetActive(false);
        }
        else
        {
            signWindow_obj.SetActive(true);
        }
    }


    List<Dictionary<string, object>> data;
    // Use this for initialization
    void Start()
    {

        //외출시 스페이드 얻기
        if (PlayerPrefs.GetInt("outspade", 0) >= 1)
        {
            PlayerPrefs.SetInt("outspade", 0);
        }
        else if(PlayerPrefs.GetInt("outspade", 0) >= -1)
        {
            PlayerPrefs.SetInt("outspade", -2);
        }

        PlayerPrefs.SetString("outlasttimecity", System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("seatime", 9);
        PlayerPrefs.Save();
        data_sign = CSVReader.Read("Talk/bottle_sea");
        signText();

        //상자 안에              /12그림/ 관련 리폼색이                들어있어
        //슬슬 돌아가야겠다.
        //값초기화
        moveCount_i = 0;
        tresureCount_i = 0;
        randomGet_i = 0;
        sign_i = 1;
        sq_i = 1;

        PlayerPrefs.SetInt("parkminiclover", 0);
        PlayerPrefs.SetInt("bufcolor", 88);
        PlayerPrefs.SetInt("bufcolor2", 88);
        PlayerPrefs.SetInt("bufmus", 0);

        str = PlayerPrefs.GetString("code", "");
        haveClover_i = PlayerPrefs.GetInt(str + "dm", 0);
        
        //상자가 등장할 장소 지정 최대두번
        randomGet_i = Random.Range(0, 3);
        if (randomGet_i == 1)
        {
            tresureCount_i = 2;
        }
        else
        {
            tresureCount_i = 1;
        }
        for (int i = 0; i < tresureCount_i; i++)
        {
            int ts = Random.Range(0, 5);
            if (tresureSet_i[ts] == 1)
            {
                if (ts == 0)
                {
                    ts++;
                }
                else
                {
                    ts--;
                }
            }
            tresureSet_i[ts] = 1;
        }

        for (int i = 0; i < 5; i++)
        {
            if (tresureSet_i[i] == 0)
            {
                
                randomGet_i = Random.Range(0, 3);
                if (randomGet_i == 0&& sign_i==1)
                {
                    //상자없는자리에 표지판 확률등장
                    sign_i = 0;
                    tresureSet_i[i] = 2;
                }
                else
                {
                    randomGet_i = Random.Range(0, 5);
                    if (randomGet_i == 0 && sq_i == 1)
                    {
                        //표지판 등장 안할때 다람쥐 확률등장
                        sq_i = 0;
                        tresureSet_i[i] = 3;
                    }
                }
            }
        }
        //만약 표지판 한군대도 없으면 앞에자리에 넣어주기
        if (sign_i == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (tresureSet_i[i] == 0&& sign_i == 1)
                {
                    sign_i = 0;
                    tresureSet_i[i] = 2;
                }
            }
        }
    }//End OF Start
    
    public void GoLeft()
    {
        MoveTo();
    }
    public void GoRight()
    {
        MoveTo();
    }

    void MoveTo()
    {


        sqH_obj.SetActive(false);
        box_obj.SetActive(false);
        squral_obj.SetActive(false);
        sign_obj.SetActive(false);

        if (moveCount_i >= 5)
        {
            //집으로돌아갈까?
            OpenResult();
        }
        else
        {
            shrimp_obj.SetActive(false);
            if (PlayerPrefs.GetInt("ggomak2", 0) == 0)
            {
                shrimp();
            }
            walk_Ani.Play("mountain", -1, 0f);
            //배경변경
            //backGround_obj.GetComponent<Image>().sprite = background_spr[moveCount_i];
            if (backGround_obj.activeSelf == true)
            {
                backGround_obj.SetActive(false);
                backGround2_obj.SetActive(true);
            }
            else
            {
                backGround_obj.SetActive(true);
                backGround2_obj.SetActive(false);
            }
            //방마다 있는 이밴트들(상자 표지판 다람쥐)
            if (tresureSet_i[moveCount_i] == 1)
            {
                box_obj.SetActive(true);
            }
            else if (tresureSet_i[moveCount_i] == 2)
            {
                sign_obj.SetActive(true);
                feedb();
            }
            else if (tresureSet_i[moveCount_i] == 3)
            {
                squral_obj.SetActive(true);
            }



            //박스가 등장할때 왼쪽오른쪽을 결정
            if (box_obj.activeSelf == true)
            {
                int v = Random.Range(0, 2);
                boxRL_obj[0].SetActive(false);
                boxRL_obj[1].SetActive(false);
                boxRL_obj[v].SetActive(true);
            }
            moveCount_i++;
            moveCount_txt.text = "" + (5 - moveCount_i)+"/5";
        }
    }

    public void OpenActFeed()
    {
        if (squralWindow_obj.activeSelf == true)
        {
            squralWindow_obj.SetActive(false);
        }
        else
        {
            squralWindow_obj.SetActive(true);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
    }

    //조개찾기
    public void FeedY()
    {
        int v = PlayerPrefs.GetInt(str + "c", 0);
        if (v >= 200)
        {
            //먹이를 주었다.
            squralWindow_obj.SetActive(false);
            v = v - 200;
            PlayerPrefs.SetInt(str + "c", v);
            PlayerPrefs.Save();
            sqH_obj.SetActive(true);
            audio_obj.GetComponent<SoundEvt>().boxSound();
            int e;
            e = PlayerPrefs.GetInt("lovepoint", 0);
            e = e + 1;
            PlayerPrefs.SetInt("lovepoint", e);
            feed();
            endg();
        }
        else
        {
            //부족하다 토스트
            StopCoroutine("toastNImgFadeOut");
            StartCoroutine("toastNImgFadeOut");
            audio_obj.GetComponent<SoundEvt>().cancleSound();
        }
    }

    //30번 했을때
    void feed()
    {
        if (PlayerPrefs.GetInt("crfin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("crcount", 0);
            if (c >= 29)
            {
                PlayerPrefs.SetInt("crfin", 1);
                PlayerPrefs.SetInt("setoutgoods", 6);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("crcount", c);
            }
        }
    }

    //50번 했을때
    void feedb()
    {
        if (PlayerPrefs.GetInt("bottlefin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("bottlecount", 0);
            if (c >= 49)
            {
                PlayerPrefs.SetInt("bottlefin", 1);
                PlayerPrefs.SetInt("setoutgoods", 7);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("bottlecount", c);
            }
        }
    }

    public void ClosePutToast()
    {
        putToast_obj.SetActive(false);
    }


    public void touchBox()
    {
        audio_obj.GetComponent<SoundEvt>().boxSound();
        randomGet_i = Random.Range(0, 10);
        tresure_obj[0].SetActive(false);
        tresure_obj[1].SetActive(false);
        tresure_obj[2].SetActive(false);
        tesureWindow_obj.SetActive(true);
        if (randomGet_i == 0)
        {
            //컬러칩또는 최대 1/50 확률로
            randomGet_i = Random.Range(0, 13);

            if (PlayerPrefs.GetInt("seaweed", 0) == 0)
            {
                if (randomGet_i != 12)
                {
                    randomGet_i = Random.Range(2, 13);
                }
            }
            if (randomGet_i == 12)
            {
                if (PlayerPrefs.GetInt("seaweed", 0) == 1)
                {
                    //클로버로 대체
                    GetClover();
                }
                else
                {
                    //버섯
                    tresure_obj[2].SetActive(true);
                    PlayerPrefs.SetInt("bufmus", 1);
                }
            }
            else
            {
                //확률보정
                sum();
                if (PlayerPrefs.GetInt("bufcolor", 99) == randomGet_i || PlayerPrefs.GetInt("shoppalette" + randomGet_i + "4", 0) == 1)
                {
                    //클로버로 대체
                    GetClover();

                }
                else
                {
                    //컬러칩
                    if (PlayerPrefs.GetInt("bufcolor", randomGet_i) == 88)
                    {
                        tresure_obj[1].SetActive(true);
                        tresure_obj[1].GetComponent<Image>().sprite = tresure_spr[randomGet_i];
                        PlayerPrefs.SetInt("bufcolor", randomGet_i);
                    }
                    else
                    {
                        tresure_obj[1].SetActive(true);
                        tresure_obj[1].GetComponent<Image>().sprite = tresure_spr[randomGet_i];
                        PlayerPrefs.SetInt("bufcolor2", randomGet_i);
                    }
                }
            }
        }
        else
        {
            //클로버
            GetClover();
        }
        box_obj.SetActive(false);
    }
    //클로버얻기
    void GetClover()
    {
        tresure_obj[0].SetActive(true);
        randomGet_i = Random.Range(0, 100);
        if (randomGet_i > 4)
        {
            //1~2개획득
            randomGet_i = Random.Range(0, 10);
            if (randomGet_i >= 7)
            {
                //2개
                clover_i = 2;
            }
            else
            {
                //1개
                clover_i = 1;
            }
        }
        else
        {
            //3~5개획득
            randomGet_i = Random.Range(0, 11);
            if (randomGet_i == 1)
            {
                //5개
                clover_i = 5;
            }
            else
            {
                //3,4개
                clover_i = Random.Range(3, 5);
            }
        }
        clover_txt.text = "" + clover_i;
        int sum = PlayerPrefs.GetInt("parkminiclover", 0);
        sum = sum + clover_i;
        PlayerPrefs.SetInt("parkminiclover", sum);
    }


    public void CloseBox()
    {
        tesureWindow_obj.SetActive(false);
    }

    //산책 결과 불러오기
    void OpenResult()
    {
        int buf = PlayerPrefs.GetInt("bufcolor", 88);
        int buf2 = PlayerPrefs.GetInt("bufcolor2", 88);
        int mus = PlayerPrefs.GetInt("bufmus", 0);

        //컬러칩2개 미역
        if (buf!=88)
        {
            PlayerPrefs.SetInt("shoppalette" + buf+"4", 1);
        }
        if (buf2 != 88)
        {
            PlayerPrefs.SetInt("shoppalette" + buf2 + "4", 1);
        }
        if (mus == 1)
        {
            PlayerPrefs.SetInt("seaweed", 1);
        }

        //클로버
        haveClover_i = PlayerPrefs.GetInt(str + "dm", 0);
        haveClover_i = haveClover_i + PlayerPrefs.GetInt("parkminiclover", 0);
        PlayerPrefs.SetInt(str + "dm", haveClover_i);

        resultWindow_obj.SetActive(true);
        PlayerPrefs.SetInt("bufcolor", 88);
        PlayerPrefs.SetInt("bufcolor2", 88);
        PlayerPrefs.SetInt("bufmus", 0);
        PlayerPrefs.SetInt("parkminiclover", 0);
        PlayerPrefs.Save();
    }

    //미니게임끝
    public void FinishMini()
    {

        PlayerPrefs.SetInt("outtrip", 2);
        StartCoroutine("LoadOut");
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorN.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorN;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorN.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorN;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }


    //씬불러오기
    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }

    void sum()
    {

        ik = 0;
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + i + "4", 0) == 1)
            {
                ik++;
            }
        }
        if (ik > 3)
        {
            redo();
            redo();
        }
        if (ik > 4)
        {
            redo();
        }
        if (ik > 5)
        {
            redo();
        }
        if (ik > 7)
        {
            redo();
        }
        if (ik > 9)
        {
            redo();
        }
    }

    void redo()
    {
        if (PlayerPrefs.GetInt("shoppalette" + randomGet_i + "4", 0) == 1)
        {
            randomGet_i++;
        }
        if (randomGet_i >= 12)
        {
            randomGet_i = 0;
        }
    }


    /// <summary>
    /// 엔딩바다
    /// </summary>
    void endg()
    {
        int k = 0;
        if (PlayerPrefs.GetInt("seaending", 0) == 0)
        {
            if (PlayerPrefs.GetInt("seaendcnt", 0) >= 9)
            {
                //수집완료
                PlayerPrefs.SetInt("seaending", 1);
                GM.GetComponent<EndingBox>().shopNum = 3;
                GM.GetComponent<EndingBox>().PlayEnd();
                GM.GetComponent<EndingBox>().end_ani.Play("endSea1", -1, 0f);
            }
            else
            {
                k = PlayerPrefs.GetInt("seaendcnt", 0);
                k++;
                PlayerPrefs.SetInt("seaendcnt", k);
            }
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
}

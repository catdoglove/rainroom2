using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MountianFunction : MonoBehaviour {

    //미리 씬을 불러오기
    AsyncOperation async;

    public GameObject squral_obj, sign_obj, right_obj, left_obj, box_obj,tesureWindow_obj, squralWindow_obj, resultWindow_obj;
    public GameObject backGround_obj, backGround2_obj;
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

    List<Dictionary<string, object>> data;
    // Use this for initialization
    void Start()
    {
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
        haveClover_i = PlayerPrefs.GetInt(str + "cv", 0);
        
        //상자가 등장할 장소 지정 최대두번
        randomGet_i = Random.Range(0, 2);
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
    
    //밤에는 산책을 할수없다
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
        }
    }

    public void FeedY()
    {
        int v = PlayerPrefs.GetInt(str + "c", 0);
        if (v >= 200)
        {
            //먹이를 주었다.
            squralWindow_obj.SetActive(false);
            v = v - 200;
            PlayerPrefs.SetInt(str + "c", v);
        }
        else
        {
            //부족하다 토스트
        }
    }


    public void touchBox()
    {
        randomGet_i = Random.Range(0, 10);
        tresure_obj[0].SetActive(false);
        tresure_obj[1].SetActive(false);
        tresure_obj[2].SetActive(false);
        tesureWindow_obj.SetActive(true);
        if (randomGet_i == 0)
        {
            //컬러칩또는 버섯
            randomGet_i = Random.Range(0, 13);
            if (randomGet_i == 12)
            {
                //버섯
                tresure_obj[2].SetActive(true);
                PlayerPrefs.SetInt("bufmus", 1);
            }
            else
            {
                if(PlayerPrefs.GetInt("bufcolor", 99)== randomGet_i|| PlayerPrefs.GetInt("shoppalette"+ randomGet_i + "4" , 0) == 1)
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
                        PlayerPrefs.SetInt("bufcolor", randomGet_i);
                    }
                    else
                    {
                        tresure_obj[1].SetActive(true);
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
        if (randomGet_i > 9)
        {
            //1~2개획득
            randomGet_i = Random.Range(0, 3);
            if (randomGet_i == 1)
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
            randomGet_i = Random.Range(0, 10);
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

        //컬러칩2개 버섯
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
            PlayerPrefs.SetInt("mushroom", 1);
        }

        //클로버
        haveClover_i = PlayerPrefs.GetInt(str + "cv", 0);
        haveClover_i = haveClover_i + PlayerPrefs.GetInt("parkminiclover", 0);
        PlayerPrefs.SetInt(str + "cv", haveClover_i);

        resultWindow_obj.SetActive(true);
        resultClover_txt.text = "" + PlayerPrefs.GetInt("parkminiclover", 0);
        PlayerPrefs.SetInt("bufcolor", 88);
        PlayerPrefs.SetInt("bufcolor2", 88);
        PlayerPrefs.SetInt("bufmus", 0);
        PlayerPrefs.SetInt("parkminiclover", 0);
    }

    //미니게임끝
    public void FinishMini()
    {

        PlayerPrefs.SetInt("outtrip", 1);
        StartCoroutine("LoadOut");
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
}

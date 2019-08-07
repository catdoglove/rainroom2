using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MountianFunction : MonoBehaviour {

    //미리 씬을 불러오기
    AsyncOperation async;

    public GameObject squral_obj, sign_obj, right_obj, left_obj, box_obj,tesureWindow_obj;

    public GameObject[] tresure_obj, boxRL_obj;
    public Sprite[] tresure_spr;
    public int moveCount_i,randomGet_i,tresureCount_i;
    public int[] tresureSet_i;
    public Text clover_txt;
    string str;
    int haveClover_i;
    // Use this for initialization
    void Start () {
        //값초기화
        moveCount_i = 0;
        tresureCount_i = 0;
        randomGet_i = 0;
        
        PlayerPrefs.SetInt("parkminiclover", 0);
        PlayerPrefs.SetInt("bufcolor", 0);
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

    }
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
            FinishMini();
        }
        else
        {
            if (tresureSet_i[moveCount_i] == 1)
            {
                box_obj.SetActive(true);
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
        }
    }

    public void touchBox()
    {
        int clover_i = 0;
        randomGet_i = Random.Range(0, 10);
        tresure_obj[0].SetActive(false);
        tresure_obj[1].SetActive(false);
        tresure_obj[2].SetActive(false);
        tesureWindow_obj.SetActive(true);
        if (randomGet_i == 0)
        {
            //컬러칩또는 버섯
            randomGet_i = Random.Range(0, 16);
            if (randomGet_i == 0)
            {
                //버섯
                tresure_obj[2].SetActive(true);
                PlayerPrefs.SetInt("bufmus", 1);
            }
            else
            {
                //컬러칩
                tresure_obj[1].SetActive(true);
                PlayerPrefs.SetInt("bufcolor", randomGet_i);

            }
        }
        else
        {
            //클로버
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
            int sum = PlayerPrefs.GetInt("parkminiclover",0);
            sum = sum + clover_i;
            PlayerPrefs.SetInt("parkminiclover", sum);
        }
    }


    public void CloseBox()
    {
        tesureWindow_obj.SetActive(false);
    }

    //미니게임끝
    void FinishMini()
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

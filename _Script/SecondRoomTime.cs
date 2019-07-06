using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondRoomTime : MonoBehaviour {

    //이동
    public float moveX1, moveX2, moveY;
    public GameObject dust1_obj, dust2_obj;
    public int randDust1_i, randDust2_i;

    //요리
    public Text beadalTime_txt;


    // Use this for initialization
    void Start ()
    {
        //업데이트대신쓴다
        StartCoroutine("UpdateSec");
    }

    /// <summary>
    /// 1초당변경하는것들
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateSec()
    {
            int a = 0;
        while (a == 0)
        {
            beadal();

            if (randDust1_i == 1)
            {
                StopCoroutine("goDust1");
                StartCoroutine("goDust1");
            }
            else
            {
                randDust1_i = Random.Range(0, 56);
            }
            if (randDust2_i == 1)
            {
                StopCoroutine("goDust2");
                StartCoroutine("goDust2");
            }
            else
            {
                randDust2_i = Random.Range(0, 56);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    //음식시간아랫방
    void beadal()
    {
        System.DateTime lastDateTime = System.DateTime.Parse(PlayerPrefs.GetString("cookLastTime", System.DateTime.Now.ToString()));
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        int m = (int)compareTime.TotalMinutes;
        int sec = (int)compareTime.TotalSeconds;
        sec = sec - (sec / 60) * 60;
        sec = 59 - sec;
        m = 59 - m;
        string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
        beadalTime_txt.text = strb;
        if (m < 0)
        {
            beadalTime_txt.text = "00:00";
            //배달이 온
            PlayerPrefs.SetInt("cooked", 0);
            PlayerPrefs.Save();
        }
    }
    IEnumerator goDust1()
    {
        while (randDust1_i == 1)
        {
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                dust1_obj.SetActive(true);
            }
            else
            {
                dust1_obj.SetActive(false);
            }
                moveX1 = moveX1 + 0.05f;
            if (moveX1 >= 9.4)
            {
                moveX1 = -9.4f;
                randDust1_i = 0;
            }
            dust1_obj.transform.position = new Vector3(moveX1, moveY, dust1_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator goDust2()
    {
        while (randDust2_i == 1)
        {
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                dust2_obj.SetActive(true);
            }
            else
            {
                dust2_obj.SetActive(false);
            }

            moveX2 = moveX2 - 0.05f;
            if (moveX2 <= -9.4)
            {
                moveX2 = 9.4f;
                randDust2_i = 0;
            }
            dust2_obj.transform.position = new Vector3(moveX2, moveY, dust1_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }

}

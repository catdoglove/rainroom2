﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomTime : MonoBehaviour {

    //이동
    public float moveX1, moveX2, moveY;
    public GameObject dust1_obj, dust2_obj;
    public int randDust1_i, randDust2_i;

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
            if (randDust1_i == 1)
            {
                StopCoroutine("goDust1");
                StartCoroutine("goDust1");
            }
            else
            {
                randDust1_i = Random.Range(0, 30);
            }
            if (randDust2_i == 1)
            {
                StopCoroutine("goDust2");
                StartCoroutine("goDust2");
            }
            else
            {
                randDust2_i = Random.Range(0, 30);
            }

            yield return new WaitForSeconds(1f);
        }
    }


    IEnumerator goDust1()
    {
        while (randDust1_i == 1)
        {
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
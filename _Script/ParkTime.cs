using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkTime : MonoBehaviour
{

    string str;
    int randLeaf_i;
    float lx,ly;
    public GameObject leaf_obj;
    // Use this for initialization
    void Start()
    {
        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSec");
    }
    IEnumerator updateSec()
    {
        int a = 0;
        while (a == 0)
        {
            //최대량 제한
            if (PlayerPrefs.GetInt(str + "c", 0) > 999999)
            {
                PlayerPrefs.SetInt(str + "c", 999999);
            }
            if (PlayerPrefs.GetInt(str + "h", 0) > 99999)
            {
                PlayerPrefs.SetInt(str + "h", 99999);
            }
            if (PlayerPrefs.GetInt(str + "ht", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "ht", 999);
            }
            if (PlayerPrefs.GetInt(str + "cv", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "cv", 999);
            }

            //나뭇잎
            if (randLeaf_i == 1)
            {
                leaf_obj.SetActive(false);
                leaf_obj.transform.position = new Vector3(lx, ly, leaf_obj.transform.position.z);
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    leaf_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    randLeaf_i = Random.Range(0, 2);
                    lx = Random.Range(-6, 4);
                    ly = Random.Range(-4, 1);
                }
            }

            //저장하고 1초동안 대기
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
        }
    }

    //나뭇잎 눌렀을때
    public void touchLeaf()
    {
        leaf_obj.SetActive(false);
        randLeaf_i = 0;
        int c = PlayerPrefs.GetInt(str + "c", 0);
        c = c + 20;
        PlayerPrefs.SetInt(str + "c", c);
    }

}

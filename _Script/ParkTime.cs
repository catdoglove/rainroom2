using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkTime : MonoBehaviour
{

    string str;
    int randLeaf_i,lx;
    GameObject leaf_obj;
    // Use this for initialization
    void Start()
    {
        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSec");
    }
    IEnumerator updateSec()
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
        
        //나뭇잎
        if (randLeaf_i == 1)
        {
            leaf_obj.SetActive(false);
            leaf_obj.transform.position = new Vector3(lx, 4.7f, leaf_obj.transform.position.z);
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                leaf_obj.SetActive(true);
            }


        }
        else
        {
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                randLeaf_i = Random.Range(0, 15);
                lx = Random.Range(-5, 5);
            }

        }



        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f);
    }

}

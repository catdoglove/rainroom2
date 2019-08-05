using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountianFunction : MonoBehaviour {

    public GameObject squral_obj, sign_obj, right_obj, left_obj, box_obj;

    public GameObject[] tresure_obj, boxRL_obj;
    public Sprite[] tresure_spr;
    public int moveCount_i,randomGet_i,tresureCount_i;
    public int[] tresureSet_i;
	// Use this for initialization
	void Start () {
        moveCount_i = 0;
        tresureCount_i = 0;
        randomGet_i = 0;


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

        if (moveCount_i >= 5)
        {
            //집으로돌아갈까?

            PlayerPrefs.SetInt("outtrip", 3);
            StartCoroutine("LoadOut");
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
        }
    }
}

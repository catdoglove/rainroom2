using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTime : MonoBehaviour {


    public float spiX, spiY;
	public int randSpider_i;
	public GameObject spider_obj;

    public float bMoveX, bMoveY;
    public int endBMove_i;
    public GameObject balloon_obj, balloonR_obj;

    public int airplane_i, cat_i;


    // Use this for initialization
    void Start () {
		//업데이트대신쓴다
		StartCoroutine ("updateSec");
		
	}
	
	IEnumerator updateSec(){
		int a = 0;
		while (a == 0) {
            cat_i = PlayerPrefs.GetInt("windowcatrand", 0);
            if (cat_i == 999)
            {

            }
            else if (cat_i <= 10 && cat_i > 0)
            {
                cat_i--;
            }
            else
            {
                cat_i = Random.Range(0, 20);
            }
            PlayerPrefs.SetInt("windowcatrand", cat_i);


            beadal();
            //거미
            if (randSpider_i == 1) {
                spider_obj.transform.position = new Vector3(spiX, spider_obj.transform.position.y, spider_obj.transform.position.z);

			} else {
				randSpider_i = Random.Range (0, 4);
                spiX = Random.Range(-6, 6);
			}
            //풍선
            if (PlayerPrefs.GetInt("miniopen", 0) == 1)
            {
                if (PlayerPrefs.GetInt("balloon", 0) == 8)
                {
                    checkBalloon();
                    PlayerPrefs.SetInt("miniopen", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("balloon", Random.Range(7, 10));

                }
            }
            else
            {
                /*
                if (PlayerPrefs.GetInt("balloon", 0) == 8)
                {

                }
                else
                {
                    PlayerPrefs.SetInt("balloon", Random.Range(5, 10));

                }
                */
            }


            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
		}
	}


	

	

	void beadal(){
		System.DateTime lastDateTime = System.DateTime.Parse (PlayerPrefs.GetString ("foodLastTime", System.DateTime.Now.ToString ()));
		System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
		int m = (int)compareTime.TotalMinutes;
		int sec = (int)compareTime.TotalSeconds;
		sec = sec - (sec / 60) * 60;
		sec = 59 - sec;
		m = 1 - m;
		if (m < 0) {
            //배달이 온
            PlayerPrefs.SetInt("beadal", 0);
            PlayerPrefs.Save();
		}
	}

   



  

    IEnumerator goBalloon()
    {
        while (endBMove_i == 1)
        {
            if (PlayerPrefs.GetInt("balloonrnd", 0) >= 5)
            {
                bMoveX = bMoveX + 0.5f;
                if (PlayerPrefs.GetInt("balloon", 0) != 8)
                {
                    bMoveX = 15.4f;
                }
                if (bMoveX >= 5.4)
                {
                    bMoveX = 15.4f;
                    endBMove_i = 0;
                    PlayerPrefs.SetInt("miniopen", 1);
                }
                balloon_obj.transform.position = new Vector3(bMoveX, balloon_obj.transform.position.y, balloon_obj.transform.position.z);
            }
            else
            {
                bMoveX = bMoveX - 0.5f;
                if (PlayerPrefs.GetInt("balloon", 0) != 8)
                {
                    bMoveX = -15.4f;
                }
                if (bMoveX <= -5.4)
                {
                    bMoveX = -15.4f;
                    endBMove_i = 0;
                    PlayerPrefs.SetInt("miniopen", 1);
                }
                balloonR_obj.transform.position = new Vector3(bMoveX, balloonR_obj.transform.position.y, balloonR_obj.transform.position.z);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void checkBalloon()
    {
        int br = Random.Range(0, 10);
        PlayerPrefs.SetInt("balloonrnd", br);
        if (PlayerPrefs.GetInt("balloon", 0) == 8)
        {
            if (PlayerPrefs.GetInt("balloonrnd", 0)>=5)
            {
                bMoveX = -5.2f;
            }
            else
            {
                bMoveX = 5.2f;
            }
            
            endBMove_i = 1;
            StopCoroutine("goBalloon");
            StartCoroutine("goBalloon");
        }
    }
   

    }

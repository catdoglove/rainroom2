using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTime : MonoBehaviour {


    public float spiX, spiY;
	public int randSpider_i;
	public GameObject spider_obj;

    public float bMoveX, bMoveY, moveX1, moveX2, moveY, moveY2;
    public int endBMove_i;

    public float pMoveX = 5.4f, pMoveY;
    public int endPMove_i;

    public GameObject balloon_obj, balloonR_obj, airplane_obj;
    public int randball1_i, randball2_i;

    public int airplane_i, cat_i,plane_i;

    public Text beadalTime_txt;

    //별
    public int randStar_i;
    public GameObject star_obj;
    public float starX, starY;


    string str;
    // Use this for initialization
    void Start () {
        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine ("updateSec");
    }
	
	IEnumerator updateSec(){
		int a = 0;
		while (a == 0) {
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

            //고양이
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
                cat_i = Random.Range(0, 242);
            }
            PlayerPrefs.SetInt("windowcatrand", cat_i);

            //비행기
            airplane_i = PlayerPrefs.GetInt("windowairplane", 0);
            if (airplane_i == 999)
            {
                
                if (plane_i == 4)
                {
                    
                    StopCoroutine("goAirplane");
                    StartCoroutine("goAirplane");
                    PlayerPrefs.SetInt("windowairplane", 0);
                }
                else
                {
                    plane_i = Random.Range(0, 500);
                }
            }
            //풍선
            ball();
            //배달
            beadal();
            //거미
            if (randSpider_i == 1) {
                spider_obj.SetActive(false);
                    spider_obj.transform.position = new Vector3(spiX, 4.7f, spider_obj.transform.position.z);
                if(PlayerPrefs.GetInt("front", 1) == 2)
                {
                    spider_obj.SetActive(true);
                }
                

            } else {
                if (PlayerPrefs.GetInt("front", 1) == 2)
                {
                    randSpider_i = Random.Range(0, 15);
                    spiX = Random.Range(-5, 5);
                }
                
			}
            //잠잘때 별
            if (randStar_i == 1)
            {
                star_obj.SetActive(false);
                star_obj.transform.position = new Vector3(starX, starY, star_obj.transform.position.z);
                if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
                {
                    star_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
                {
                    randStar_i = Random.Range(0, 15);
                    starX = Random.Range(-5, 5);
                    starY = Random.Range(-1, 3);
                }
            }

            

            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
		}
	}

    void ball()
    {
        if (randball1_i == 1)
        {
            StopCoroutine("goball1");
            StartCoroutine("goball1");
        }
        else
        {
            randball1_i = Random.Range(0, 80);
            if (randball1_i == 1)
            {
                moveX1 = -5.2f;
            }
        }
        if (randball2_i == 1)
        {
            
            StopCoroutine("goball2");
            StartCoroutine("goball2");
        }
        else
        {
            
            randball2_i = Random.Range(0, 80);
            if (randball2_i == 1)
            {
                moveX2 = 5.2f;
            }
        }
    }

	

	
    //배달시간
	void beadal(){
		System.DateTime lastDateTime = System.DateTime.Parse (PlayerPrefs.GetString ("foodLastTime", System.DateTime.Now.ToString ()));
		System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
		int m = (int)compareTime.TotalMinutes;
		int sec = (int)compareTime.TotalSeconds;
		sec = sec - (sec / 60) * 60;
		sec = 59 - sec;
		m = 59 - m;
        string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
        beadalTime_txt.text = strb;
        if (m < 0) {
            beadalTime_txt.text = "00:00";
            //배달이 온
            PlayerPrefs.SetInt("beadal", 0);
            PlayerPrefs.Save();
		}
	}
    


    /*
    //풍선
    IEnumerator goBalloon()
    {
        while (endBMove_i == 1)
        {
            if (PlayerPrefs.GetInt("balloonrnd", 0) >= 5)
            {
                bMoveX = bMoveX + 0.05f;
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
                bMoveX = bMoveX - 0.05f;
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

    //풍선
    
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
    */

    //비행기코루틴
    IEnumerator goAirplane()
    {
        while (plane_i == 4)
        {
            if (pMoveX > 7)
            {
                pMoveX = 5.4f;
            }
                pMoveX = pMoveX -0.1f;
                if (pMoveX <= -5.4)
                {
                PlayerPrefs.SetInt("windowairplane", 0);
                pMoveX = 17.4f;
                plane_i = 0;
                }
            airplane_obj.transform.position = new Vector3(pMoveX, airplane_obj.transform.position.y, airplane_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }



    IEnumerator goball1()
    {
        while (randball1_i == 1)
        {
            if (moveX1 >= 15f)
            {
                //moveX1 = -5.2f;
                randball1_i = 0;
            }
            moveX1 = moveX1 + 0.05f;
            if (moveX1 >= 5.4)
            {
                moveX1 = 15.4f;
                randball1_i = 0;
            }
            balloon_obj.transform.position = new Vector3(moveX1, moveY, balloon_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator goball2()
    {
        while (randball2_i == 1)
        {
            if (moveX2 <= -15f)
            {
                //moveX2 = 5.2f;
                randball2_i = 0;
            }
            moveX2 = moveX2 - 0.05f;
            if (moveX2 <= -5.4)
            {
                moveX2 = -15.4f;
                randball2_i = 0;
            }
            balloonR_obj.transform.position = new Vector3(moveX2, moveY2, balloonR_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }


}

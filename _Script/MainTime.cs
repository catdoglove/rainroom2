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


    // Use this for initialization
    void Start () {
		//업데이트대신쓴다
		StartCoroutine ("updateSec");
		
	}
	
	IEnumerator updateSec(){
		int a = 0;
		while (a == 0) {
			
			if (randSpider_i == 1) {
                spider_obj.transform.position = new Vector3(spiX, spider_obj.transform.position.y, spider_obj.transform.position.z);

			} else {
				randSpider_i = Random.Range (0, 4);
                spiX = Random.Range(-6, 6);
			}
            if (PlayerPrefs.GetInt("balloon", 0) == 8)
            {
                
            }
            else
            {
                PlayerPrefs.SetInt("balloon", Random.Range(0, 10));
                
            }

            
            

            yield return new WaitForSeconds(1f);
		}
	}


	

	

	void baedal(){
		System.DateTime lastDateTime = System.DateTime.Parse (PlayerPrefs.GetString ("foodLastTime", System.DateTime.Now.ToString ()));
		System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
		int m = (int)compareTime.TotalMinutes;
		int sec = (int)compareTime.TotalSeconds;
		sec = sec - (sec / 60) * 60;
		sec = 59 - sec;
		m = 1 - m;
		if (m < 0) {
			//배달이 온
		}
	}


    void windowMiniGame()
    {
        //창이 안켜져 있을경우 풍선이 지나갈 확률 10초마다 판단
        //창이   켜져 있을경우 동일하게 판단
        //풍선이 나올때는 양방향으로 지나갈 수 있게 하되 한 화면에 여러개 나오지 않도록
        //풍선은 물을 준다
        //비행기는 풍선과 별도로 지나간다 비행기는 하트등을준다
        //새,나뭇잎등은 이동하지않고 나온다
        //셋엑티브 트루일경우에 바로등장해서 이동시켜주고 아닐경우
        //숫자만저장해뒀다가 오픈할때 설정해준다
    }

    IEnumerator goBalloon()
    {
        int br = Random.Range(0, 10);
        PlayerPrefs.SetInt("balloonrnd", br);
        while (endBMove_i == 1)
        {
            
            if (br >= 5)
            {
                bMoveX = bMoveX + 0.005f;
                if (PlayerPrefs.GetInt("balloon", 0) != 8)
                {
                    bMoveX = 15.4f;
                }
                if (bMoveX >= 5.4)
                {
                    bMoveX = 15.4f;
                    endBMove_i = 0;
                }
                balloon_obj.transform.position = new Vector3(bMoveX, balloon_obj.transform.position.y, balloon_obj.transform.position.z);
            }
            else
            {
                bMoveX = bMoveX - 0.005f;
                if (PlayerPrefs.GetInt("balloon", 0) != 8)
                {
                    bMoveX = -15.4f;
                }
                if (bMoveX <= -5.4)
                {
                    bMoveX = -15.4f;
                    endBMove_i = 0;
                }
                balloonR_obj.transform.position = new Vector3(bMoveX, balloon_obj.transform.position.y, balloon_obj.transform.position.z);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void checkBalloon()
    {
        if (PlayerPrefs.GetInt("balloon", 0) == 8)
        {
            if (PlayerPrefs.GetInt("balloonrnd", 0)>=5)
            {
                bMoveX = 5.2f;
            }
            else
            {
                bMoveX = -5.2f;
            }
            
            endBMove_i = 1;
            StartCoroutine("goBalloon");
        }
    }
   

    }

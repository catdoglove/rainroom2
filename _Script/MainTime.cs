using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTime : MonoBehaviour {

	//이동
	public float moveX1,moveX2,moveY;
    public float spiX, spiY;
	public int randDust1_i,randDust2_i,randSpider_i;
	public GameObject dust1_obj, dust2_obj, spider_obj;
    

	// Use this for initialization
	void Start () {
		//업데이트대신쓴다
		StartCoroutine ("updateSec");
		
	}
	
	IEnumerator updateSec(){
		int a = 0;
		while (a == 0) {
			if (randDust1_i == 1) {
				StopCoroutine ("goDust1");
				StartCoroutine ("goDust1");
			} else {
				randDust1_i = Random.Range (0, 30);
			}
			if (randDust2_i == 1) {
				StopCoroutine ("goDust2");
				StartCoroutine ("goDust2");
			} else {
				randDust2_i = Random.Range (0, 30);
			}
			if (randSpider_i == 1) {
                spider_obj.transform.position = new Vector3(spiX, spider_obj.transform.position.y, spider_obj.transform.position.z);

			} else {
				randSpider_i = Random.Range (0, 4);
                spiX = Random.Range(-6, 6);
			}

            

			yield return new WaitForSeconds(1f);
		}
	}


	IEnumerator goDust1(){
		while (randDust1_i == 1) {
			moveX1 = moveX1 + 0.05f;
			if (moveX1 >= 9.4) {
				moveX1 = -9.4f;
				randDust1_i = 0;
			}
			dust1_obj.transform.position = new Vector3 (moveX1, moveY, dust1_obj.transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator goDust2(){
		while (randDust2_i == 1) {
			moveX2 = moveX2 - 0.05f;
			if (moveX2 <= -9.4) {
				moveX2 = 9.4f;
				randDust2_i = 0;
			}
			dust2_obj.transform.position = new Vector3 (moveX2, moveY, dust1_obj.transform.position.z);
			yield return new WaitForSeconds(0.1f);
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
        yield return new WaitForSeconds(0.1f);
    }
   

    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTime : MonoBehaviour {

	//이동
	public float moveX1,moveX2,moveY;
	public int randDust1_i,randDust2_i,randSpider_i;
	public GameObject dust1_obj, dust2_obj;

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

			} else {
				//randSpider_i = Random.Range (0, 20);
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




}

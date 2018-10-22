using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTimeHandler : MonoBehaviour {

	//비
	public int getRain,rain;
	//비상점텍스트
	public Text rainNum;
	//시간
	public int talk;
	public Text talkTime_txt,talkNum,heartNum;
	string lastTime;

	//대화시간


	// Use this for initialization
	void Start () {
		//빗물
		//collectRain ();
		//대화
		StartCoroutine ("talkTimeFlow");
		
	}




	void collectRain(){

		//세이브불러오기
		rain = PlayerPrefs.GetInt ("rain", 0);

		//모인 빗물
		//현재시간을가져옵니다
		System.DateTime dateTimenow = System.DateTime.Now;
		//str로장되어있는과거접속시간을가져옵니다
		string lastTimem = PlayerPrefs.GetString("lastTime",dateTimenow.ToString());
		//형변환을해줍니다
		System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);
		//계산
		System.TimeSpan compareTimem =  System.DateTime.Now - lastDateTimem;
		//1분당1씩줍니다
		getRain = (int)compareTimem .TotalMinutes;

		//최초실행
		if(PlayerPrefs.GetInt("coin",-1)==-1&&getRain>20000){
			getRain = 0;
		}
		//부정행위방지
		if (getRain>36000) {//5일치 이상 모았을때
			if (getRain > 100000) {//2주일 되었을 때
				getRain = 0;
				//warningTxt.text = "빗물이 너무 모여 물탱크가 터져버렸다."+"\n겨우 수리했다.";
				//warningBtn.SetActive(true);
			} else {
				getRain = 36000; //물탱크가 꽉 찼다
				//warningTxt.text = "장기간 방치로 인해 물탱크기능이 멈췄다."+"\n이제 작동한다.";
				//warningBtn.SetActive(true);
			}
		}else if(getRain<0){
			//경고
		}

		rain = rain + getRain;
		PlayerPrefs.SetInt ("rain", rain);
		rainNum.text = rain.ToString();
		PlayerPrefs.SetString("lastTime",dateTimenow.ToString());
		PlayerPrefs.Save ();
		
	}



	//대화시간코루틴
	IEnumerator talkTimeFlow(){
		int minute;
		int sec;
		int a = 0;
		while (a == 0) {
			talk = PlayerPrefs.GetInt ("talk", 5);

			lastTime = PlayerPrefs.GetString ("TalkLastTime", System.DateTime.Now.ToString ());
			System.DateTime lastDateTime = System.DateTime.Parse (lastTime);
			System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
			minute = (int)compareTime.TotalMinutes;
			sec = (int)compareTime.TotalSeconds;
			sec = sec - (sec / 60) * 60;
			sec = 59 - sec;
			minute = 1 - minute;


			if (minute < 0) {
				while (minute < 0) {
					minute = minute + 1;
					sec = sec + 59;
					talk++;
				}
				PlayerPrefs.SetString ("TalkLastTime", System.DateTime.Now.ToString ());
				talkTime_txt.text = "01:59";
			} else {
				string str = string.Format (@"{0:00}" + ":", minute) + string.Format (@"{0:00}", sec);
				talkTime_txt.text = "" + str;
			}

			talkNum.text = talk.ToString ();
			if (talk >= 5) {
				talkTime_txt.text = "00:00";
				talk = 5;
				talkNum.text = talk.ToString ();
				PlayerPrefs.SetString ("TalkLastTime", System.DateTime.Now.ToString ());
			} 
			PlayerPrefs.SetInt ("talk", talk);
			PlayerPrefs.Save ();
		
			yield return new WaitForSeconds(0.1f);
		}
	}
	

}

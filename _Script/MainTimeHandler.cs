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

    public int coldRain_i, hotRain_i;

    //경고
    public GameObject warring_obj;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetInt("emergencyCODE3", 0) == 0)
        {


            System.DateTime turnBackTime = System.DateTime.UtcNow.AddHours(-10);
            PlayerPrefs.SetString("milktime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-6);
            PlayerPrefs.SetString("sleepLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("outLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-13);
            PlayerPrefs.SetString("seedLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-10);
            PlayerPrefs.SetString("plantLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("cookLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("outlasttimepark", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("outlasttimecity", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("foodLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-1);
            PlayerPrefs.SetString("TalkLastTime", turnBackTime.ToString());

            turnBackTime = System.DateTime.UtcNow.AddHours(-10);
            PlayerPrefs.SetString("lastTime", turnBackTime.ToString());

            PlayerPrefs.SetInt("secf", 0);
            PlayerPrefs.SetInt("secf0", 0);
            PlayerPrefs.SetInt("secf1", 0);
            PlayerPrefs.SetInt("secf2", 0);
            PlayerPrefs.SetInt("secf3", 0);



            PlayerPrefs.SetInt("emergencyCODE3", 99);
        }

            //빗물
            collectRain ();
		//대화
		StartCoroutine ("talkTimeFlow");
        //이부분은 생성될때 한번만 실행된다
        //돈디스트로이로 씬을 넘어가도 다시 실행되지 않는다
        if (talk >= 5)
        {
            PlayerPrefs.SetString("TalkLastTime", System.DateTime.UtcNow.ToString());
        }
    }




	void collectRain(){

        string str = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        

		//모인 빗물
		//현재시간을가져옵니다
		System.DateTime dateTimenow = System.DateTime.UtcNow;
		//str로장되어있는과거접속시간을가져옵니다
		string lastTimem = PlayerPrefs.GetString("lastTime",dateTimenow.ToString());
        //형변환을해줍니다
        try
        {
            System.DateTime lastDateTimem2 = System.DateTime.Parse(lastTimem);
        }
        catch (System.Exception)
        {
            lastTimem = System.DateTime.UtcNow.AddHours(-1).ToString();
        }

        System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);

		//계산
		System.TimeSpan compareTimem =  System.DateTime.UtcNow - lastDateTimem;
		//1분당1씩줍니다
		getRain = (int)compareTimem .TotalMinutes;
        //최초실행
        //if(PlayerPrefs.GetInt("coin",-1)==-1&&getRain>20000){
        //	getRain = 0;
        //}
        //부정행위방지
        if (getRain > 10080)//7200
        {//7일치 이상 모았을때
                getRain = 0;
            warring_obj.SetActive(true);
        }
        coldRain_i = coldRain_i + getRain;
		PlayerPrefs.SetInt (str + "c", coldRain_i);
		//rainNum.text = coldRain_i.ToString();
		PlayerPrefs.SetString("lastTime",dateTimenow.ToString());
		PlayerPrefs.Save ();

        //빗물이 마이너스일때
        if (coldRain_i<0)
        {
            PlayerPrefs.SetInt(str + "c", -9);
            PlayerPrefs.Save();
        }
	}
    public void closeWarring()
    {
        warring_obj.SetActive(false);
    }



	//대화시간코루틴
	IEnumerator talkTimeFlow(){
		int minute;
		int sec;
		int a = 0;
		while (a == 0) {
            int justOne = 99;
            talk = PlayerPrefs.GetInt ("talk", 5);
            System.DateTime dateTime = System.DateTime.UtcNow.AddHours(-1);
            lastTime = PlayerPrefs.GetString ("TalkLastTime", dateTime.ToString ());            
            System.DateTime lastDateTime = System.DateTime.UtcNow;

            try
            {
                System.DateTime lastDateTime2 = System.DateTime.Parse(lastTime);
            }
            catch (System.Exception)
            {
                justOne = 0;
                lastDateTime = System.DateTime.UtcNow.AddHours(-1);
            }

            if(justOne == 99)
            {
                lastDateTime = System.DateTime.Parse(lastTime);
            }
            System.TimeSpan compareTime = System.DateTime.UtcNow - lastDateTime;


            if ((int)compareTime.TotalSeconds < 0)
            {
                compareTime = System.DateTime.UtcNow - System.DateTime.UtcNow;
            }
            minute = (int)compareTime.TotalMinutes;
			sec = (int)compareTime.TotalSeconds;
            sec = sec - (sec / 60) * 60;
            sec = 59 - sec;
			minute = 4 - minute;
            
            if (minute < 0) {
				while (minute < 0) {
					minute = minute + 5;
					//sec = sec + 59;
					talk++;
				}
                //시간을 중간부터 하기위해
                //PlayerPrefs.SetInt("timeminhelp", 4-minute);
                //PlayerPrefs.SetInt("timesechelp", 59-sec);
                //Debug.Log("minute" + minute+ "sec" + sec);
                //Debug.Log(""+System.DateTime.Now.ToString());
                PlayerPrefs.SetString ("TalkLastTime", System.DateTime.UtcNow.ToString ());
				//talkTime_txt.text = "04:59";
			} else {
				string str = string.Format (@"{0:00}" + ":", minute) + string.Format (@"{0:00}", sec);
				talkTime_txt.text = "" + str;
			}

			talkNum.text = talk.ToString ();
			if (talk >= 5) {
				talkTime_txt.text = "00:00";
				talk = 5;
				talkNum.text = talk.ToString ();
            }
			PlayerPrefs.SetInt ("talk", talk);
			PlayerPrefs.Save ();
		
			yield return new WaitForSeconds(0.1f);
		}
	}



}

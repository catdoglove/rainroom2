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

    public float pMoveX = 35.4f, pMoveY;
    public int endPMove_i;

    public GameObject balloon_obj, balloonR_obj, airplane_obj, snow_obj,snowYs_obj, snowYe_obj;
    public int randball1_i, randball2_i, snow_i, snowImg_i;
    public float snowY_f, snowX_f;
    public int airplane_i, cat_i,plane_i;
    public Sprite[] snow_spr, snow_spr2, snow_spr3, snow_spr4;
    public Text beadalTime_txt;

    //별
    public int randStar_i;
    public GameObject star_obj;
    public float starX, starY;
    string str;

    int bg=0;

    //비
    public GameObject rianAni_obj;
    public Sprite[] rainAni_spr;
    int ran=0;
    
    public GameObject GM;

    public GameObject blackAd_obj;

    //해상도별위치
    public GameObject airplaneEnd_obj, balloonEnd_obj, balloonREnd_obj;

    // Use this for initialization
    void Start () {

        seasoncheck();
        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine ("updateSec");
        StartCoroutine("rainani");

    }

    void rainmove()
    {
        rianAni_obj.GetComponent<Image>().sprite = rainAni_spr[ran];
        ran++;
        if (ran >= 3)
        {
            ran = 0;
        }

    }
    
    //비
    IEnumerator rainani()
    {
        int rs=4;
        while (rs == 4)
        {
            rainmove();
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator updateSec(){
		int a = 0;
		while (a == 0) {

            
            if (PlayerPrefs.GetInt("blad", 0)==1)
            {
                blackAd_obj.SetActive(false);
                PlayerPrefs.SetInt("blad", 0);
            }

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
                cat_i = Random.Range(0, 245);
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
                    plane_i = Random.Range(0, 6);
                }
            }
            //풍선
            ball();
            //배달
            beadal();
            //눈
            snow();
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
                    randSpider_i = Random.Range(0, 24);
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
                //moveX1 = -5.2f;
                moveX1 = balloonREnd_obj.transform.position.x;
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
                //moveX2 = 5.2f;
                moveX2 = balloonEnd_obj.transform.position.x;
            }
        }
    }


    void snow()
    {
        if (snow_i == 1)
        {
            if (bg == 0)
            {
                StopCoroutine("fallSnow");
                StartCoroutine("fallSnow");
            }
            bg = 1;
        }
        else
        {
            snow_i = Random.Range(0, 35);
            snowX_f = Random.Range(-2, 4);
            if (snow_i == 1)
            {
                snowY_f = snowYs_obj.transform.position.y;
            }
        }
    }

    public void touchSnow()
    {

        float xx = snow_obj.transform.position.x;
        float yy = snow_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        PlayerPrefs.SetInt("dishw", 1);

        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        coldRain_i = coldRain_i + 1;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.Save();
        
        snowY_f = -15.4f;
        snow_i = 0;
        snow_obj.transform.position = new Vector3(snowX_f, snowY_f, snow_obj.transform.position.z);
        bg = 0;


        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
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
            /*
            if (pMoveX > 7)
            {
                pMoveX = 5.4f;
            }
                pMoveX = pMoveX -0.1f;
            if (pMoveX <= -5.3)
            {
                PlayerPrefs.SetInt("windowairplane", 0);
                pMoveX = 18.4f;
                plane_i = 0;
            }
            */

            if (pMoveX > 7)
            {
                pMoveX = balloonEnd_obj.transform.position.x;
            }
            pMoveX = pMoveX - 0.1f;
            if (airplane_obj.transform.position.x <= airplaneEnd_obj.transform.position.x)
            {
                PlayerPrefs.SetInt("windowairplane", 0);
                pMoveX = 18.4f;
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
            /*
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
            */

            if (moveX1 >= 15f)
            {
                //moveX1 = -5.2f;
                randball1_i = 0;
            }
            moveX1 = moveX1 + 0.05f;

            if (balloon_obj.transform.position.x >= balloonEnd_obj.transform.position.x)
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
            /*
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
            */


            if (moveX2 <= -15f)
            {
                //moveX2 = 5.2f;
                randball2_i = 0;
            }
            moveX2 = moveX2 - 0.05f;

            if (balloonR_obj.transform.position.x <= balloonREnd_obj.transform.position.x)
            {
                moveX2 = -15.4f;
                randball2_i = 0;
            }

            balloonR_obj.transform.position = new Vector3(moveX2, moveY2, balloonR_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator fallSnow()
    {
        while (snow_i == 1)
        {
            if (snowY_f <= -15f)
            {
                snow_i = 0;
                bg = 0;
            }
            snowY_f = snowY_f - 0.3f;
            if (snowY_f <= snowYe_obj.transform.position.y)
            {
                snowY_f = -15.4f;
                snow_i = 0;
                bg = 0;
            }
            snow_obj.transform.position = new Vector3(snowX_f, snowY_f, snow_obj.transform.position.z);

            
            if (PlayerPrefs.GetInt("seasonCODE", 0) == 10)
            {
                snow_obj.GetComponent<Image>().sprite = snow_spr[snowImg_i];
            } else if (PlayerPrefs.GetInt("seasonCODE", 0) == 20)
            {
                snow_obj.GetComponent<Image>().sprite = snow_spr2[snowImg_i];
            }
            else if (PlayerPrefs.GetInt("seasonCODE", 0) == 30)
            {
                snow_obj.GetComponent<Image>().sprite = snow_spr3[snowImg_i];
            }
            else if (PlayerPrefs.GetInt("seasonCODE", 0) == 40)
            {
                snow_obj.GetComponent<Image>().sprite = snow_spr4[snowImg_i];
            }

            snowImg_i++;
            if (snowImg_i >= 8)
            {
                snowImg_i = 0;
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    void seasoncheck()
    {
        //계절체크
        string mon = System.DateTime.Now.ToString("MM");

        int mon_i = int.Parse(mon);

        if (mon_i == 3 || mon_i == 4 || mon_i == 5) //봄 10
        {
            PlayerPrefs.SetInt("seasonCODE", 10);
            snow_obj.GetComponent<Image>().sprite = snow_spr[0];
        }
        else if (mon_i == 6 || mon_i == 7 || mon_i == 8) //여름 20
        {
            PlayerPrefs.SetInt("seasonCODE", 20);
            snow_obj.GetComponent<Image>().sprite = snow_spr2[0];
        }
        else if (mon_i == 9 || mon_i == 10 || mon_i == 11) //가을 30
        {
            PlayerPrefs.SetInt("seasonCODE", 30);
            snow_obj.GetComponent<Image>().sprite = snow_spr3[0];
        }
        else if (mon_i == 12 || mon_i == 1 || mon_i == 2) //겨울 40
        {
            PlayerPrefs.SetInt("seasonCODE", 40);
            snow_obj.GetComponent<Image>().sprite = snow_spr4[0];
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityTime : MonoBehaviour {

    string str;
    public GameObject GMC;
    public GameObject paper_obj;
    public int randPaper_i;
    public float px, py;
    public GameObject coin_obj;
    public int randCoin_i;
    public float cx, cy;
    public GameObject blackAd_obj;

    //외물물건
    public GameObject putToast_obj;

    public Sprite[] background_front, background_back;
    public GameObject bg_front, bg_back;

    private void Awake()
    {
        StopCoroutine("updateSec");
        StartCoroutine("updateSec");
    }
    // Use this for initialization
    void Start () {

        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StopCoroutine("updateSec");
        StartCoroutine("updateSec");

        //계절체크
        string mon = System.DateTime.Now.ToString("MM");

        int mon_i = int.Parse(mon);

        if (PlayerPrefs.GetInt("seasonCODE", 0) == 10)  //봄 10
        {
            background_front[0] = Resources.Load<Sprite>("UI/city/city_background (3)");
            background_back[0] = Resources.Load<Sprite>("UI/city/city_background2 (3)");

            bg_front.GetComponent<Image>().sprite = background_front[0];
            bg_back.GetComponent<Image>().sprite = background_back[0];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 20)  //여름 20
        {
            background_front[1] = Resources.Load<Sprite>("UI/city/city_background");
            background_back[1] = Resources.Load<Sprite>("UI/city/city_background2");

            bg_front.GetComponent<Image>().sprite = background_front[1];
            bg_back.GetComponent<Image>().sprite = background_back[1];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 30)  //가을 30
        {
            background_front[2] = Resources.Load<Sprite>("UI/city/city_background");
            background_back[2] = Resources.Load<Sprite>("UI/city/city_background2");

            bg_front.GetComponent<Image>().sprite = background_front[2];
            bg_back.GetComponent<Image>().sprite = background_back[2];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 40) //겨울 40
        {
            background_front[3] = Resources.Load<Sprite>("UI/city/city_background (2)");
            background_back[3] = Resources.Load<Sprite>("UI/city/city_background2 (2)");

            bg_front.GetComponent<Image>().sprite = background_front[3];
            bg_back.GetComponent<Image>().sprite = background_back[3];
        }


    }


    //1초당 업데이트
    IEnumerator updateSec()
    {
        int a = 0;
        while (a == 0)
        {
            if (PlayerPrefs.GetInt("positionpaperfirst", 0) == 0)
            {
                randPaper_i =1;
            }


            if (PlayerPrefs.GetInt("blad", 0) == 1)
            {
                blackAd_obj.SetActive(false);
                PlayerPrefs.SetInt("blad", 0);
            }
            //전단지
            if (randPaper_i == 1)
            {
                paper_obj.SetActive(false);

                if(PlayerPrefs.GetInt("positionpaperfirst", 0) == 0)
                {
                    paper_obj.SetActive(true);
                    Vector3 position = paper_obj.transform.localPosition;
                    position.x = 74f;
                    position.y = -261f;

                    px = position.x;
                    py = position.y;

                    paper_obj.transform.localPosition = position;
                }
                else
                {
                    paper_obj.transform.position = new Vector3(px, py, paper_obj.transform.position.z);
                }


                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    paper_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    randPaper_i = Random.Range(0, 17);
                    px = Random.Range(-2, 6);
                    py = Random.Range(-3, -2);
                }
            }



            //코인
            if (randCoin_i == 1)
            {
                coin_obj.SetActive(false);
                coin_obj.transform.position = new Vector3(cx, cy, coin_obj.transform.position.z);
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    coin_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    randCoin_i = Random.Range(0, 999);
                    cx = Random.Range(-2, 6);
                    cy = Random.Range(-3, -2);
                }
            }

            //저장하고 1초동안 대기
            PlayerPrefs.SetString("outLastTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
        }//endOfWhile
    }



    //전단지 눌렀을때
    public void touchPaper()
    {
        feed();
        PlayerPrefs.SetFloat("watposy", py);
        PlayerPrefs.SetFloat("watposx", px);
        GMC.GetComponent<GetFadeout>().getRainFade();
        paper_obj.SetActive(false);
        randPaper_i = 0;
        int c = PlayerPrefs.GetInt(str + "c", 0);
        c = c + 5;
        PlayerPrefs.SetInt(str + "c", c);
        int h = PlayerPrefs.GetInt(str + "h", 0);
        h = h + 3;
        PlayerPrefs.SetInt(str + "h", h);
        PlayerPrefs.SetInt("positionpaperfirst", 999);
    }

    //100번 했을때
    void feed()
    {
        if (PlayerPrefs.GetInt("cpfin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("cpcount", 0);
            if (c >= 99)
            {
                PlayerPrefs.SetInt("cpfin", 1);
                PlayerPrefs.SetInt("setoutgoods", 3);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("cpcount", c);
            }
        }
    }


    //코인
    public void touchCoin()
    {
        coin_obj.SetActive(false);
        randCoin_i = 0;
        int d = PlayerPrefs.GetInt(str + "dm", 0);
        d = d + 1;
        PlayerPrefs.SetInt(str + "dm", d);
    }

}

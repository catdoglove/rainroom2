using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTime : MonoBehaviour {

    string str;
    public GameObject GMC;
    public GameObject paper_obj;
    public int randPaper_i;
    public float px, py;
    // Use this for initialization
    void Start () {

        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSec");


    }


    //1초당 업데이트
    IEnumerator updateSec()
    {
        int a = 0;
        while (a == 0)
        {
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
            if (PlayerPrefs.GetInt(str + "cv", 0) > 999)
            {
                PlayerPrefs.SetInt(str + "cv", 999);
            }

            //전단지
            if (randPaper_i == 1)
            {
                paper_obj.SetActive(false);
                paper_obj.transform.position = new Vector3(px, py, paper_obj.transform.position.z);
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    paper_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    randPaper_i = Random.Range(0, 3);
                    px = Random.Range(-2, 6);
                    py = Random.Range(-4, -2);
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
    }

}

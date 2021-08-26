using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ParkTime : MonoBehaviour
{

    string str;
    int randLeaf_i, bas_i, rainBas_i;
    float lx,ly;
    public GameObject leaf_obj;
    public GameObject GMP;

    //양동이
    public GameObject bas_obj;
    public Sprite[] bas_spr;

    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;
    public GameObject Audio_obj;


    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;
    public GameObject blackAd_obj;


    public Sprite[] background_front, background_back, moonbangu_spr;
    public GameObject bg_front, bg_back, moonbangu_img;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("basketrain", 0);
        PlayerPrefs.SetInt("basket", 0);
        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSec");


        //계절체크
        string mon = System.DateTime.Now.ToString("MM");

        int mon_i = int.Parse(mon);

        if (PlayerPrefs.GetInt("seasonCODE", 0) == 10)  //봄 10
        {
            bg_front.GetComponent<Image>().sprite = background_front[0];
            bg_back.GetComponent<Image>().sprite = background_back[0];
            moonbangu_img.GetComponent<Image>().sprite = moonbangu_spr[0];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 20)  //여름 20
        {
            bg_front.GetComponent<Image>().sprite = background_front[1];
            bg_back.GetComponent<Image>().sprite = background_back[1];
            moonbangu_img.GetComponent<Image>().sprite = moonbangu_spr[1];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 30)  //가을 30
        {
            bg_front.GetComponent<Image>().sprite = background_front[2];
            bg_back.GetComponent<Image>().sprite = background_back[2];
            moonbangu_img.GetComponent<Image>().sprite = moonbangu_spr[2];
        }
        else if (PlayerPrefs.GetInt("seasonCODE", 0) == 40)  //겨울 40
        {
            bg_front.GetComponent<Image>().sprite = background_front[3];
            bg_back.GetComponent<Image>().sprite = background_back[3];
            moonbangu_img.GetComponent<Image>().sprite = moonbangu_spr[3];
        }







    }
    IEnumerator updateSec()
    {
        int a = 0;
        while (a == 0)
        {

            if (PlayerPrefs.GetInt("positionleaffirst", 0) == 0)
            {
                randLeaf_i = 1;
            }


            if (PlayerPrefs.GetInt("blad", 0) == 1)
            {
                blackAd_obj.SetActive(false);
                PlayerPrefs.SetInt("blad", 0);
            }
            //나뭇잎
            if (randLeaf_i == 1)
            {
                leaf_obj.SetActive(false);

                if (PlayerPrefs.GetInt("positionleaffirst", 0) == 0)
                {
                    leaf_obj.SetActive(true);
                    Vector3 position = leaf_obj.transform.localPosition;
                    position.x = 74f;
                    position.y = -261f;

                    lx = position.x;
                    ly = position.y;

                    leaf_obj.transform.localPosition = position;
                }
                else
                {
                    leaf_obj.transform.position = new Vector3(lx, ly, leaf_obj.transform.position.z);
                }


                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    leaf_obj.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    randLeaf_i = Random.Range(0, 15);
                    lx = Random.Range(-6, 4);
                    ly = Random.Range(-4, -2);
                }
            }
            //양동이
            bas_i = PlayerPrefs.GetInt("basket", 0);
            bas_i++;
            PlayerPrefs.SetInt("basket", bas_i);
           // Debug.Log(bas_i);
            if (bas_i >= 60)
            {
                rainBas_i = PlayerPrefs.GetInt("basketrain", 0);
                rainBas_i++;
                PlayerPrefs.SetInt("basketrain", rainBas_i);
                PlayerPrefs.SetInt("basket", 0);
            //    Debug.Log(rainBas_i);
            }
            if(PlayerPrefs.GetInt("basketrain", 0) > 0)
            {
                bas_obj.GetComponent<Image>().sprite = bas_spr[1];
            }
            else
            {
                bas_obj.GetComponent<Image>().sprite = bas_spr[0];
            }

            //저장하고 1초동안 대기
            PlayerPrefs.SetString("outLastTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
        }//endOfWhile
    }

    //나뭇잎 눌렀을때
    public void touchLeaf()
    {
        PlayerPrefs.SetInt("positionleaffirst", 999);
        PlayerPrefs.SetFloat("watposy", ly);
        PlayerPrefs.SetFloat("watposx", lx);
        GMP.GetComponent<GetFadeout>().getRainFade();
        leaf_obj.SetActive(false);
        randLeaf_i = 0;
        int c = PlayerPrefs.GetInt(str + "c", 0);
        c = c + 5;
        PlayerPrefs.SetInt(str + "c", c);
        int h = PlayerPrefs.GetInt(str + "h", 0);
        h = h + 3;
        PlayerPrefs.SetInt(str + "h", h);

        if (PlayerPrefs.GetInt("leafget", 0)==0)
        {
            int lf = PlayerPrefs.GetInt("leafcount", 0);
            lf = lf + 1;
            PlayerPrefs.SetInt("leafcount", lf);
        }
        endg();
    }



    /// <summary>
    /// 엔딩나뭇잎
    /// </summary>
    void endg()
    {
        int k = 0;
        if (PlayerPrefs.GetInt("leafending", 0) == 0)
        {
            if (PlayerPrefs.GetInt("leafendcnt", 0) >= 39)
            {
                //수집완료
                PlayerPrefs.SetInt("leafending", 1);
                GMP.GetComponent<EndingBox>().shopNum = 0;
                GMP.GetComponent<EndingBox>().PlayEnd();
                GMP.GetComponent<EndingBox>().end_ani.Play("endleaf1", -1, 0f);
            }
            else
            {
                k = PlayerPrefs.GetInt("leafendcnt", 0);
                k++;
                PlayerPrefs.SetInt("leafendcnt", k);
            }
        }
    }

    public void CloseEnd()
    {
        endWindow_obj.SetActive(false);
        Audio_obj.GetComponent<SoundEvt>().cancleSound();
        //소리
        m_end.clip = sp_original;
        m_end.Play();
    }

    public void endR()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i == 1)
        {
            endR_obj.SetActive(false);
            endClose_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endL_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }
    public void endL()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj.SetActive(false);
        if (end_i == 1)
        {
            endL_obj.SetActive(false);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endR_obj.SetActive(true);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }

}

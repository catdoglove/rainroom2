using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingBox : MonoBehaviour {

    public GameObject endWindow_obj;
    public GameObject[] endItem_obj, endHint_obj;
    public string[] end_str,endHint_str, endNum_str, endCha_str;
    public Sprite[] end_spr, endBox_spr;
    public GameObject audio_obj;
    public GameObject endBoxBtn_obj,endBox_obj;
    public int shopNum;
    public Text end_txt, endNum_txt, endCha_txt;
    public GameObject GM;
    int a;
    public GameObject[] EndAni_obj;
    public GameObject playBtn_obj, endBack_obj, endtxt_obj;


    //엔딩
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;
    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;
    public int page = 0;

    public Animator end_ani;
    public string[] ani_str;

    public int room;

    // Use this for initialization
    void Start () {

        
        
        endString();
        if (room == 5)
        {
            checkEnd();
        }
    }
	
    public void ActEnding()
    {
        playBtn_obj.SetActive(false);
        if (endWindow_obj.activeSelf == true)
        {
            endWindow_obj.SetActive(false);
            endtxt_obj.SetActive(false);
        }
        else
        {
            checkEnd();
            endWindow_obj.SetActive(true);
            
            if (a >= 9)
            {
                if (PlayerPrefs.GetInt("_thank_you_for_play", 0) == 0)
                {
                    PlayerPrefs.SetInt("_thank_you_for_play", 1);
                    GM.GetComponent<AchievementShow>().achievementCheck(24, 0);
                }
            }
        }
    }

    void checkEnd()
    {
        a = 0;
        //대화1
        PlayerPrefs.GetInt("talkending", 0);
        if(PlayerPrefs.GetInt("talkending", 0) == 1)
        {
            endItem_obj[1].SetActive(true);
            a++;
        }
        //첫공원7
        PlayerPrefs.GetInt("parkending", 0);
        if (PlayerPrefs.GetInt("parkending", 0) == 1)
        {
            endItem_obj[5].SetActive(true);
            a++;
        }
        //첫도시3
        PlayerPrefs.GetInt("cityending", 0);
        if (PlayerPrefs.GetInt("cityending", 0) == 1)
        {
            endItem_obj[6].SetActive(true);
            a++;
        }
        //우유10번9
        PlayerPrefs.GetInt("milkending", 0);
        if (PlayerPrefs.GetInt("milkending", 0) == 1)
        {
            endItem_obj[7].SetActive(true);
            a++;
        }
        //바다10번5
        PlayerPrefs.GetInt("seaending", 0);
        if (PlayerPrefs.GetInt("seaending", 0) == 1)
        {
            endItem_obj[3].SetActive(true);
            a++;
        }
        //나뭇잎40번2
        PlayerPrefs.GetInt("leafending", 0);
        if (PlayerPrefs.GetInt("leafending", 0) == 1)
        {
            endItem_obj[0].SetActive(true);
            a++;
        }
        //그림모두8
        PlayerPrefs.GetInt("pictureending", 0);
        if (PlayerPrefs.GetInt("pictureending", 0) == 1)
        {
            endItem_obj[4].SetActive(true);
            a++;
        }
        //모든요리6
        PlayerPrefs.GetInt("cookending", 0);
        if (PlayerPrefs.GetInt("cookending", 0) == 1)
        {
            endItem_obj[2].SetActive(true);
            a++;
        }
        //호감도4
        PlayerPrefs.GetInt("likeending", 0);
        if (PlayerPrefs.GetInt("likeending", 0) == 1)
        {
            endItem_obj[8].SetActive(true);
            a++;
        }
        if (a >= 1)
        {
            endBoxBtn_obj.SetActive(true);
            endBox_obj.GetComponent<Image>().sprite = endBox_spr[1];
        }
        if (a >= 7)
        {
            endHint_obj[0].SetActive(true);
            endHint_obj[1].SetActive(true);
        }
        //그림모두
        if (PlayerPrefs.GetInt("pictureending", 0) == 1)
        {
            endHint_obj[1].SetActive(false);
        }
        //모든요리
        if (PlayerPrefs.GetInt("cookending", 0) == 1)
        {
            endHint_obj[0].SetActive(false);
        }
        if (a >= 9)
        {
            endBox_obj.GetComponent<Image>().sprite = endBox_spr[2];
            endtxt_obj.SetActive(true);
        }


        endNum_str[0] = "2";
        endNum_str[1] = "1";
        endNum_str[2] = "6";
        endNum_str[3] = "5";
        endNum_str[4] = "8";
        endNum_str[5] = "7";
        endNum_str[6] = "3";
        endNum_str[7] = "9";
        endNum_str[8] = "4";

        endCha_str[0] = "D";
        endCha_str[1] = "E";
        endCha_str[2] = "VO";
        endCha_str[3] = "3";
        endCha_str[4] = "m";
        endCha_str[5] = "2";
        endCha_str[6] = "cC";
        endCha_str[7] = "o";
        endCha_str[8] = "5";

    }
    
    void endString()
    {
        end_str[0]= "공원에 있는 여러가지 나뭇잎을 줍다가 발견했지. 방금 떨어진 건지 다른 나뭇잎들보다 색이 선명했어";
        end_str[1] = "어색한 대화들 ..언젠가 편안하게 대화하는 날이 올까?";
        end_str[2] = "다양한 요리를 만들어 보고 싶었어. 직접 만들어서 맛있게 먹었더니 기분이 더 좋았지.";
        end_str[3] = "모래사장을 걸어다니며 조개를 찾다가 푸른빛 소라를 발견했어. 귀에 대면 작게 들려오는 파도소리에 마음이 편안해져.";
        end_str[4] = "그림을 모으다 보니 나도 그려보고 싶어져서 작게 우산을 그렸어 ..잘 그리진 못 했지만 나에겐 소중한 그림이야.";
        end_str[5] = "낯설면서도 익숙한 공원. 어린시절 추억이 고스란히 담겨 있었지.";
        end_str[6] = "높은 건물의 옥상에서 내려다 보던 도시의 풍경이 떠올랐어. 아래에 지나가는 자동차들은 마치 장난감 같았지.";
        end_str[7] = "열심히 우유를 마셔서 이벤트에 응모했는데 당첨됐어. 상품으로 부드러운 쿠션을 받았지.";
        end_str[8] = "어색함은 눈녹듯이 사라지고 어느새 익숙함이 자리를 잡았어 ..너와 내가 가까워 진 걸까?";
        endHint_str[0] = "공원 그림을...";
        endHint_str[1] = "모든 요리를...";

        ani_str[0] ="endleaf";
        ani_str[1] = "endTalk";
        ani_str[2] = "endcook";
        ani_str[3] = "endSea";
        ani_str[4] = "endDraw";
        ani_str[5] = "endPark";
        ani_str[6] = "endCity";
        ani_str[7] = "endMlik";
        ani_str[8] = "endheart";
        ani_str[9] = "endLast";



    }

    public void num0()
    {
        shopNum = 0;
    }
    public void num1()
    {
        shopNum = 1;
    }
    public void num2()
    {
        shopNum = 2;
    }
    public void num3()
    {
        shopNum = 3;
    }
    public void num4()
    {
        shopNum = 4;
    }
    public void num5()
    {
        shopNum = 5;
    }
    public void num6()
    {
        shopNum = 6;
    }
    public void num7()
    {
        shopNum = 7;
    }
    public void num8()
    {
        shopNum = 8;
    }

    public void SetText()
    {
        end_txt.text = end_str[shopNum];
        endNum_txt.text = endNum_str[shopNum];
        endCha_txt.text = endCha_str[shopNum];
        showPlayBtn();
    }

    public void PlayEnd()
    {
        page = 0;
        endBack_obj.SetActive(true);
        //소리
        m_end.clip = sp_end;
        m_end.Play();
        SumPage();
        endL_obj.SetActive(false);
        endClose_obj.SetActive(false);
        endR_obj.SetActive(true);
        end_ani.Play(ani_str[shopNum] + "1", -1, 0f);
    }
    public void PlayEndLast()
    {
        endBack_obj.SetActive(true);
        //소리
        m_end.clip = sp_end;
        m_end.Play();
        endL_obj.SetActive(false);
        endClose_obj.SetActive(false);
        endR_obj.SetActive(true);
        end_ani.Play("endLast1", -1, 0f);
        shopNum = 9;

        page = 13;
    }

    void showPlayBtn()
    {
        playBtn_obj.SetActive(true);
    }

    public void CloseEnd()
    {
        audio_obj.GetComponent<SoundEvt>().cancleSound();
        endBack_obj.SetActive(false);
        //소리
        m_end.clip = sp_original;
        m_end.Play();
        end_i = 0;
        if (room == 5)
        {
            checkEnd();
        }
    }

    public void endR()
    {
        audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i == page)//마지막페이지 -1일때
        {
            endR_obj.SetActive(false);
            endClose_obj.SetActive(true);
            endL_obj.SetActive(true);
            end_i++;
            int l = end_i + 1;
            //다음페이지
            end_ani.Play(ani_str[shopNum] + l, -1, 0f);
        }
        else
        {
            endL_obj.SetActive(true);
            end_i++;
            int l = end_i + 1;
            //다음페이지
            end_ani.Play(ani_str[shopNum] + l, -1, 0f);
        }
    }
    public void endL()
    {
        audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj.SetActive(false);
        if (end_i == 1)
        {
            endL_obj.SetActive(false);
            endR_obj.SetActive(true);
            end_i--;

            int l = end_i + 1;
            //다음페이지
            end_ani.Play(ani_str[shopNum] + l, -1, 0f);
        }
        else
        {
            endR_obj.SetActive(true);
            end_i--;

            int l = end_i + 1;
            //다음페이지
            end_ani.Play(ani_str[shopNum] + l, -1, 0f);
        }
    }

    //페이지수의 값이 다를때
    void SumPage()
    {
        switch (shopNum)
        {
            case 0:
                page = 1;
                break;
            case 1:
                page = 1;
                break;
            case 2:
                page = 1;
                break;
            case 3:
                page = 2;
                break;
            case 4:
                page = 2;
                break;
            case 5:
                page = 0;
                break;
            case 6:
                page = 2;
                break;
            case 7:
                page = 1;
                break;
            case 8:
                page = 2;
                break;
        }
    }
}

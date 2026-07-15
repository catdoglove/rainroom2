using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
//using Unity.Services.LevelPlay;


public class UnityADS : MonoBehaviour
{

    //string appKey = "a1f59a75";
   // private string gameId = "2883785";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)2883785
    public int soundck;
    public GameObject ad_obj, radio_ani, adBtn_obj;

	int sG,mG;
    int sG2, mG2;
   
    Color color;
    public GameObject Toast_obj;


    public GameObject watingAds_obj, watingAdsHelp_obj, watingAdsNoise_obj, watingAdsShow_obj, chAds_obj;
    public Sprite watingAdsNoise_spr1, watingAdsNoise_spr2;
    public Sprite[] watingAdspr;
    int noise_i = 0;
    int rand_i = 0;

    public GameObject GM;
    // public string _adUnitId = "rewardedVideo";
    private Coroutine adTimeRoutine1 = null;
    private Coroutine adAniRoutine1 = null;
    private Coroutine adTimeRoutine2 = null;
    private Coroutine adAniRoutine2 = null;
    private void Awake()
    {
    }
    void Start()
    {
        color = new Color(1f, 1f, 1f);

        // [최적화] 기존에 실행 중인 코루틴이 있다면 안전하게 종료
        StopAllAdCoroutines();

        int place = PlayerPrefs.GetInt("place", 0);
        int outtrip = PlayerPrefs.GetInt("outtrip", 0);

        if (place == 0)
        {
            adTimeRoutine1 = StartCoroutine(adTimeFlow());
            adAniRoutine1 = StartCoroutine(adAniTime());
        }
        else if (outtrip == 0 || outtrip == 2)
        {
            adTimeRoutine2 = StartCoroutine(adTimeFlow2());
            adAniRoutine2 = StartCoroutine(adAniTime2());
        }
        else
        {
            adTimeRoutine1 = StartCoroutine(adTimeFlow());
            adAniRoutine1 = StartCoroutine(adAniTime());
        }
    }
    private void OnDisable()
    {
        // 오브젝트 비활성화 시 코루틴 찌꺼기 완벽 제거
        StopAllAdCoroutines();
    }

    private void StopAllAdCoroutines()
    {
        if (adTimeRoutine1 != null) StopCoroutine(adTimeRoutine1);
        if (adAniRoutine1 != null) StopCoroutine(adAniRoutine1);
        if (adTimeRoutine2 != null) StopCoroutine(adTimeRoutine2);
        if (adAniRoutine2 != null) StopCoroutine(adAniRoutine2);
    }

    public void ShowRewardedAd()
    {
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            GM.GetComponent<AdmobADS>().Toast_obj.SetActive(true);
            GM.GetComponent<AdmobADS>().Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            GM.GetComponent<AdmobADS>().StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);



            //debug.Log("unity-script: ShowRewardedVideoButtonClicked");
           /* if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo("RewardTalk_place");
            }
            else
            {
                PlayerPrefs.SetInt("wait", 2);
                ad_obj.SetActive(true);
                Wating();
            }
           */
        }
            
    }

    public void Wating()
    {
        watingAds_obj.SetActive(true);
        rand_i = Random.Range(0, 15);
        watingAdsShow_obj.GetComponent<Image>().sprite = watingAdspr[rand_i];
        chAds_obj.SetActive(false);
    }

    //광고준비중
    public void WatingAdColse()
    {
        watingAds_obj.SetActive(false);
    }
    public void WatingAdHelp()
    {
        if (watingAdsHelp_obj.activeSelf == true)
        {
            watingAdsHelp_obj.SetActive(false);
        }
        else
        {
            watingAdsHelp_obj.SetActive(true);
        }
    }

    void noise()
    {
        if (noise_i == 0)
        {
            watingAdsNoise_obj.GetComponent<Image>().sprite = watingAdsNoise_spr1;
            noise_i = 1;
        }
        else
        {
            watingAdsNoise_obj.GetComponent<Image>().sprite = watingAdsNoise_spr2;
            noise_i = 0;
        }
    }
    public void WaitAdshow()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            ad_obj.SetActive(true);
        }
    }

    public void adYN()
    {
        PlayerPrefs.SetInt("adrunout", 0);
        ad_obj.SetActive(true);
        watingAds_obj.SetActive(false);
    }
    public void closeAdYN()
    {
        ad_obj.SetActive(false);
    }
    public void adYes()
    {
        ShowRewardedAd();
        ad_obj.SetActive(false);
    }

    public void Admob()
    {
        radio_ani.SetActive(false);
        adBtn_obj.SetActive(false);
        StopCoroutine("adTimeFlow");
        StopCoroutine("adAniTime");
        StartCoroutine("adTimeFlow");
        StartCoroutine("adAniTime");
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            PlayerPrefs.SetInt("secf", 180);
        }
    }


    IEnumerator adTimeFlow()
    {
        // mG 변수는 실제 조건문 외에 큰 역할이 없어 무한루프로 변경 후 조건 이탈 시 break
        while (true)
        {
            sG = PlayerPrefs.GetInt("secf", 180);

            if (sG < 0)
            {
                sG = 0;
            }
            else
            {
                if (radio_ani.activeSelf) radio_ani.SetActive(false);
                if (adBtn_obj.activeSelf) adBtn_obj.SetActive(false);
            }

            sG--; // 1초 차감

            if (sG < 0) sG = -1;

            PlayerPrefs.SetInt("secf", sG);
            noise();

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator adAniTime()
    {
        while (true)
        {
            if (sG < 0)
            {
                bool shouldShow = (PlayerPrefs.GetInt("outtrip", 0) == 1) || (PlayerPrefs.GetInt("front", 0) == 1);

                if (shouldShow)
                {
                    if (!radio_ani.activeSelf) radio_ani.SetActive(true);
                    if (!adBtn_obj.activeSelf) adBtn_obj.SetActive(true);
                }
                else
                {
                    if (radio_ani.activeSelf) radio_ani.SetActive(false);
                    if (adBtn_obj.activeSelf) adBtn_obj.SetActive(false);
                }
            }
            yield return null;
        }
    }

    IEnumerator adTimeFlow2()
    {
        while (true)
        {
            sG2 = PlayerPrefs.GetInt("secf2", 180);

            if (sG2 < 0)
            {
                sG2 = 0;
            }
            else
            {
                if (radio_ani.activeSelf) radio_ani.SetActive(false);
                if (adBtn_obj.activeSelf) adBtn_obj.SetActive(false);
            }

            sG2--; // 1초 차감

            if (sG2 < 0) sG2 = -1;

            PlayerPrefs.SetInt("secf2", sG2);
            noise();

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator adAniTime2()
    {
        while (true)
        {
            if (sG2 < 0)
            {
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                    // 원본 코드에서도 비어있던 조건 (의도된 패스)
                }
                else if (PlayerPrefs.GetInt("front", 0) == 1)
                {
                    if (!radio_ani.activeSelf) radio_ani.SetActive(true);
                    if (!adBtn_obj.activeSelf) adBtn_obj.SetActive(true);
                }
                else
                {
                    if (radio_ani.activeSelf) radio_ani.SetActive(false);
                    if (adBtn_obj.activeSelf) adBtn_obj.SetActive(false);
                }
            }
            yield return null;
        }
    }



    IEnumerator ToastImgFadeOut()
    {
        color.a = 1f;
        Toast_obj.GetComponent<Image>().color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = i; // Lerp 대용으로 더 깔끔하게 대입
            Toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);
    }




}

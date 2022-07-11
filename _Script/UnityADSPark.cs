using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSPark : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{

    private string gameId = "2883785";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)
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
    public string _adUnitId = "rewardedVideo";

    private void Awake()
    {
        Advertisement.Initialize(gameId, false, this);//테스트모드 true
    }

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);
        if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
        }
        else
        {
            StopCoroutine("adTimeFlow2");
            StopCoroutine("adAniTime2");
            StopCoroutine("adTimeFlow");
            StopCoroutine("adAniTime");

            StartCoroutine("adTimeFlow");
            StartCoroutine("adAniTime");
        }

        LoadAd();
      }
      
    // Update is called once per frame

    public void ShowRewardedAd()
    {

        if (PlayerPrefs.GetInt("talk", 5) >= 5 && PlayerPrefs.GetInt("adrunout", 0) == 0)
        {

            if (PlayerPrefs.GetInt("outtrip", 0) == 2)
            {
                GM.GetComponent<AdmobADSCity>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADSCity>().Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
                GM.GetComponent<AdmobADSCity>().StartCoroutine("ToastImgFadeOut");
            }
            else
            {
                GM.GetComponent<AdmobADSPark>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADSPark>().Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
                GM.GetComponent<AdmobADSPark>().StartCoroutine("ToastImgFadeOut");
            }
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);
            Advertisement.Show("rewardedVideo", this);
        }
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        PlayerPrefs.SetInt("wait", 2);
        ad_obj.SetActive(true);
        Wating();
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
        if (PlayerPrefs.GetInt("wait", 0)==2)
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
    

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (PlayerPrefs.GetInt("outtrip", 0) == 2)
            {
                PlayerPrefs.SetInt("talk", 5);
                PlayerPrefs.Save();
                if (PlayerPrefs.GetInt("talk", 5) >= 5)
                {
                    PlayerPrefs.SetInt("secf3", 240);
                }
                GM.GetComponent<AdmobADSCity>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADSCity>().Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
                GM.GetComponent<AdmobADSCity>().StartCoroutine("ToastImgFadeOut");
            }
            else
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
                    PlayerPrefs.SetInt("secf0", 240);
                }
                GM.GetComponent<AdmobADSPark>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADSPark>().Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
                GM.GetComponent<AdmobADSPark>().StartCoroutine("ToastImgFadeOut");

            }
            Advertisement.Load(_adUnitId, this);
        }
    }


    IEnumerator adTimeFlow(){
		while (mG>-1) {
			sG = PlayerPrefs.GetInt("secf0", 0);
			if (sG < 0) {
            } else {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
			sG = PlayerPrefs.GetInt("secf0", 0);
			sG = sG - 1;
			if (sG < 0) {
				sG = -1;
			}
			PlayerPrefs.SetInt("secf0",sG);
            noise();
            yield return new WaitForSeconds(1f);
            //Debug.Log("sg" + sG);
        }
	}
    IEnumerator adAniTime()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG < 0)
            {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
            }
            else
            {

                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            yield return null;
        }

    }


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf2", 240);
            //Debug.Log(sG);
            mG2= (int)(sG2 / 60);
            sG2 = sG2 - (sG2 / 60) * 60;
            if (sG2 < 0)
            {
                sG2 = 0;
                mG2 = 0;
            }
            else
            {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            sG2 = PlayerPrefs.GetInt("secf2", 240);
            sG2 = sG2 - 1;
            if (sG2 < 0)
            {
                sG2 = -1;
            }
            PlayerPrefs.SetInt("secf2", sG2);
            noise();
            //Debug.Log("sg2" + sG2);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator adAniTime2()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG2 < 0)
            {
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                } else if (PlayerPrefs.GetInt("front", 1) == 1)
                    {
                        radio_ani.SetActive(true);
                        adBtn_obj.SetActive(true);
                    }
                    else
                    {
                        radio_ani.SetActive(false);
                        adBtn_obj.SetActive(false);
                    }
                    
            }

            yield return null;
        }

    }



    IEnumerator ToastImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        Toast_obj.GetComponent<Image>().color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            Toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }


    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }
    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }
}

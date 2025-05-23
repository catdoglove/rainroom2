using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Unity.Services.LevelPlay;


public class UnityADS : MonoBehaviour
{

    string appKey = "a1f59a75";
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
    public string _adUnitId = "rewardedVideo";

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {

        //debug.Log("나는UnityADS다");

        //debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        //debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        //debug.Log("unity-script: LevelPlay SDK initialization");

        LevelPlay.Init(appKey, adFormats: new[] { com.unity3d.mediation.LevelPlayAdFormat.REWARDED });

        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;

        color = new Color(1f, 1f, 1f);
        
        StopCoroutine("adTimeFlow2");
        StopCoroutine("adAniTime2");
        StopCoroutine("adTimeFlow");
        StopCoroutine("adAniTime");
        
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            StartCoroutine("adTimeFlow");
            StartCoroutine("adAniTime");
        }
        else if(PlayerPrefs.GetInt("outtrip", 0) == 0)
        {
            StartCoroutine("adTimeFlow2");
            StartCoroutine("adAniTime2");
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            StartCoroutine("adTimeFlow2");
            StartCoroutine("adAniTime2");
        }
        else
        {
            StartCoroutine("adTimeFlow");
            StartCoroutine("adAniTime");
        }

      }

    private void OnDisable()
    {
        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;

        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionDataReadyEvent -= ImpressionDataReadyEvent;

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;
    }

    void EnableAds()
    {
        //debug.Log("나는UnityADS의 리스너다.");

            //Add ImpressionSuccess Event
            IronSourceEvents.onImpressionDataReadyEvent -= ImpressionDataReadyEvent;

            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;

            //Add ImpressionSuccess Event
            IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;

            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }



    void OnApplicationPause(bool isPaused)
    {
        //debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }


    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        //debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }


    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        //debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        //debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        PlayerPrefs.SetInt("wait", 2);
        ad_obj.SetActive(true);
        Wating();
    }

  

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        //debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }



    void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        //debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: " + config);
        EnableAds();
    }

    void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        //debug.Log("unity-script: I got SdkInitializationFailedEvent with error: " + error);
    }

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        //debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
        //debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    }

    public void ShowRewardedAdout()
    {
        PlayerPrefs.SetInt("wait", 1);
        //debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo("RewardOut_place");
        }
        else
        {
        }
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
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo("RewardTalk_place");
            }
            else
            {
                PlayerPrefs.SetInt("wait", 2);
                ad_obj.SetActive(true);
                Wating();
            }
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


    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    { 
        //debug.Log("닫기 이벤트 UnityADS");
    }



    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        //debug.Log("Placement name: " + ironSourcePlacement.getPlacementName());
        if (ironSourcePlacement.getPlacementName() == "RewardTalk_place")
        {
            //debug.Log("대화광고");

            if (PlayerPrefs.GetInt("place", 0) == 0)
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
                    PlayerPrefs.SetInt("secf", 240);
                }


                GM.GetComponent<AdmobADS>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADS>().Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
                
                GM.GetComponent<AdmobADS>().StartCoroutine("ToastImgFadeOut");
            }
            else
            {
                PlayerPrefs.SetInt("talk", 5);
                PlayerPrefs.Save();
                if (PlayerPrefs.GetInt("talk", 5) >= 5)
                {
                    PlayerPrefs.SetInt("secf2", 240);
                }


                GM.GetComponent<AdmobADS>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADS>().Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
                GM.GetComponent<AdmobADS>().StartCoroutine("ToastImgFadeOut");
            }

        }
        else if (ironSourcePlacement.getPlacementName() == "RewardOut_place")
        {
            //debug.Log("외출광고");
            if (PlayerPrefs.GetInt("ForUnityADSnewReward", 0) == 99)
            {
                PlayerPrefs.SetInt("bouttime", 9);

                 GM.GetComponent<AdmobADS>().Toast_obj2.SetActive(true);
            //  Toast_obj3.SetActive(true);  
            }
        }
            //debug.Log("UnityADS광고");
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
            PlayerPrefs.SetInt("secf", 240);
        }
    }

    

	IEnumerator adTimeFlow(){
		while (mG>-1) {
			sG = PlayerPrefs.GetInt("secf", 240);
            ////debug.Log(sG);
            mG = (int)(sG / 60);
			sG = sG-(sG / 60)*60;
			if (sG < 0) {
				sG = 0;
				mG = 0;
            } else {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
			sG = PlayerPrefs.GetInt("secf", 240);
			sG = sG - 1;
			if (sG < 0) {
				sG = -1;
			}
			PlayerPrefs.SetInt("secf",sG);
            noise();

            yield return new WaitForSeconds(1f);
            ////debug.Log("sg" + sG);
        }
	}
    IEnumerator adAniTime()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG < 0)
            {
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
                }
                else
                {
                    if (PlayerPrefs.GetInt("front", 0) == 1)
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
            }
            yield return null;
        }

    }


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf2", 240);
            ////debug.Log(sG);
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
            ////debug.Log("sg2" + sG2);
            noise();

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
                } else if (PlayerPrefs.GetInt("front", 0) == 1)
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



}

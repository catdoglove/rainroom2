using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Unity.Services.LevelPlay;

public class UnityADSnewReward : MonoBehaviour
{

    string appKey = "a1f59a75";


    public GameObject GM;
    public string uid = "rewardedVideo";



    private void Awake()
    {
    }

    void Start()
    {
        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: LevelPlay SDK initialization");
        LevelPlay.Init(appKey, adFormats: new[] { com.unity3d.mediation.LevelPlayAdFormat.REWARDED });

        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
    }


    void EnableAds()
    {

    }


    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }


    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }


    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        PlayerPrefs.SetInt("wait", 2);
        GM.GetComponent<UnityADSPark>().ad_obj.SetActive(true);
        GM.GetComponent<UnityADSPark>().Wating();
    }



    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }



    void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: " + config);
        EnableAds();
    }

    void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("unity-script: I got SdkInitializationFailedEvent with error: " + error);
    }

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
         if (PlayerPrefs.GetInt("ForUnityADSnewReward", 0) == 99)
        {
            PlayerPrefs.SetInt("bouttime", 9);
            GM.GetComponent<AdmobADS>().Toast_obj2.SetActive(true);
        }
    }


    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        if (PlayerPrefs.GetInt("ForUnityADSnewReward", 0) == 77)
        {
            PlayerPrefs.SetInt("foresttime", 4);
            GM.GetComponent<AdmobADSPark>().Toast_obj2.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ForUnityADSnewReward", 0) == 88)
        {
            PlayerPrefs.SetInt("seatime", 4);
            GM.GetComponent<AdmobADSCity>().Toast_obj2.SetActive(true);
        }
    }


    public void ShowRewardedAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Unity.Services.LevelPlay;

public class UnityADSMilk : MonoBehaviour 
{

    string appKey = "a1f59a75";
    //private string gameId = "2883785";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)
    public int soundck;
    public GameObject ad_obj;
    
   
    Color color;
    public GameObject Toast_obj;

    public GameObject GM;
    public string _adUnitId = "rewardedVideo";

    private void Awake()
    {
      //  Advertisement.Initialize(gameId, false, this); //테스트모드 true
    }

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);


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
        GM.GetComponent<AdmobADSMilk>().MilkToast();
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
        /*
        PlayerPrefs.SetInt("milkadc", 1);
        PlayerPrefs.SetInt("setmilkadc", 0);
        GM.GetComponent<WindowMiniGame>().MilkYes();
        GM.GetComponent<AdmobADSMilk>().blackimg.SetActive(false);
        GM.GetComponent<AdmobADSMilk>().Toast_obj2.SetActive(true);
        GM.GetComponent<AdmobADSMilk>().Toast_contain2.SetActive(false);
        GM.GetComponent<AdmobADSMilk>().Toast_contain3.SetActive(true);
        PlayerPrefs.SetInt("adrunout", 0);
        */
        Debug.Log("나는 우유 광고 닫기다");

    }


    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        /*
        PlayerPrefs.SetInt("milkadc", 1);
        PlayerPrefs.SetInt("setmilkadc", 0);
        GM.GetComponent<WindowMiniGame>().MilkYes();
        GM.GetComponent<AdmobADSMilk>().blackimg.SetActive(false);
        GM.GetComponent<AdmobADSMilk>().Toast_obj2.SetActive(true);
        GM.GetComponent<AdmobADSMilk>().Toast_contain2.SetActive(false);
        GM.GetComponent<AdmobADSMilk>().Toast_contain3.SetActive(true);
        PlayerPrefs.SetInt("adrunout", 0);
        */
        Debug.Log("나는 우유 광고다");
        
    }

        public void ShowRewardedAd()
    {

        PlayerPrefs.SetInt("wait", 1);

        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo("Reward2");
        }
        else
        {
        }



    }

    public void adYes()
    {
        ShowRewardedAd();
        ad_obj.SetActive(false);
    }
    


}

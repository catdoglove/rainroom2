using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSnewReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{

    private string gameId = "1486550";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)


    public GameObject GM;
    public string uid = "rewardedVideo";



    private void Awake()
    {
        Advertisement.Initialize(gameId, true, this);//테스트모드 true
    }

    void Start()
    {
        LoadAd();
    }



    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        PlayerPrefs.SetInt("wait", 2);
        GM.GetComponent<UnityADSPark>().ad_obj.SetActive(true);
        GM.GetComponent<UnityADSPark>().Wating();
    }


    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(uid) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
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
            else if (PlayerPrefs.GetInt("ForUnityADSnewReward", 0) == 99)
            {
                PlayerPrefs.SetInt("bouttime", 9);
                GM.GetComponent<AdmobADS>().Toast_obj2.SetActive(true);
            }

            Advertisement.Load(uid, this);
        }
    }

    public void ShowRewardedAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        Advertisement.Show("rewardedVideo", this);
    }


    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + uid);
        Advertisement.Load(uid, this);
    }

    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
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
}

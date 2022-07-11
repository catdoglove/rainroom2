using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSMilk : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{

    private string gameId = "2883785";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)
    public int soundck;
    public GameObject ad_obj;
    
   
    Color color;
    public GameObject Toast_obj;

    public GameObject GM;
    public string _adUnitId = "rewardedVideo";

    private void Awake()
    {
        Advertisement.Initialize(gameId, false, this); //테스트모드 true
    }

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);

        LoadAd();
      }

    // Update is called once per frame

    public void ShowRewardedAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        Advertisement.Show("rewardedVideo", this);


    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        PlayerPrefs.SetInt("wait", 2);
        GM.GetComponent<AdmobADSMilk>().MilkToast();
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
            PlayerPrefs.SetInt("milkadc", 1);
            PlayerPrefs.SetInt("setmilkadc", 0);
            GM.GetComponent<WindowMiniGame>().MilkYes();
            GM.GetComponent<AdmobADSMilk>().blackimg.SetActive(false);
            GM.GetComponent<AdmobADSMilk>().Toast_obj2.SetActive(true);
            GM.GetComponent<AdmobADSMilk>().Toast_contain2.SetActive(false);
            GM.GetComponent<AdmobADSMilk>().Toast_contain3.SetActive(true);
            PlayerPrefs.SetInt("adrunout", 0);
            Advertisement.Load(_adUnitId, this);
        }
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

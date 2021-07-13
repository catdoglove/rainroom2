using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class AdmobADS : MonoBehaviour {

    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;

    //배너
    private BannerView bannerView;
    AdRequest request;

    //영상
    private RewardBasedVideoAd rewardBasedVideo;
    string adUnitIdvideo;

    //전면
    private InterstitialAd interstitial;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg;


    public Button milkad_btn;

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

#if UNITY_ANDROID
        string appId = "ca-app-pub-9179569099191885~5921342761"; //테스트용ca-app-pub-3940256099942544~3347511713
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        //this.RequestBanner();
        

        rewardBasedVideo = RewardBasedVideoAd.Instance;
        
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestRewardedVideo();
        RequestInterstitial();

        //보상형 전면 광고
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/5047087900", request, adLoadCallback);

    }

    //배너
    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }


    
    //동영상
    private void RequestRewardedVideo()
    {

#if UNITY_ANDROID
            adUnitIdvideo = "ca-app-pub-9179569099191885/8650861151"; // 테스트 ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
            adUnitIdvideo = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitIdvideo = "unexpected_platform";
#endif
        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitIdvideo);
    }

    //시청보상
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            PlayerPrefs.SetInt("talk", 5);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("talk", 5) >= 5)
            {
                PlayerPrefs.SetInt("secf", 240);
            }
        }
        else
        {
            PlayerPrefs.SetInt("talk", 5);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("talk", 5) >= 5)
            {
                PlayerPrefs.SetInt("secf2", 240);
            }
        }
        if (PlayerPrefs.GetInt("setmilkadc", 0)==1)
        {
            PlayerPrefs.SetInt("milkadc", 1);
        }
        PlayerPrefs.SetInt("setmilkadc", 0);

    }

    //동영상닫음
    private void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        RequestRewardedVideo();
        blackimg.SetActive(false);
    }

    public void showAdmobVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            blackimg.SetActive(true);
            rewardBasedVideo.Show();
        }
        else
        {
            StartCoroutine("ToastImgFadeOut");
        }
    }
    


    public void callBanner()
    {
        this.RequestBanner();
    }

    
    IEnumerator ToastImgFadeOut()
    {
        if (PlayerPrefs.GetInt("setmilkadc", 0) == 1)
        {
            milkad_btn.interactable = true;
            PlayerPrefs.SetInt("setmilkadc", 0);
        }

        color.a = Mathf.Lerp(0f, 1f, 1f);
        Toast_obj.GetComponent<Image>().color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            Toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);

    }

    //전면광고
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9179569099191885/1598954374"; // 테스트ca-app-pub-3940256099942544/1033173712
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        
    }

    private void OnDisable()
    {
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
    }

    public void ShowAdInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            PlayerPrefs.SetInt("bouttime", 9);
        }
    }

    /*
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }
    */


    //보상형 전면 광고
    private void adLoadCallback(RewardedInterstitialAd ad, string error)
    {
        if (error == null)
        {
            rewardedInterstitialAd = ad;
            rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;

        }
    }
    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            blackimg.SetActive(true);
            rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        PlayerPrefs.SetInt("bouttime", 9);
        blackimg.SetActive(false);
    }

    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        //MonoBehavior.print("Rewarded interstitial ad has failed to present.");
    }

    //방지
    public void closeBlackImg()
    {
        blackimg.SetActive(false);
    }
}

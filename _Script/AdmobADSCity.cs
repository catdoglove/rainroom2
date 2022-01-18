using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class AdmobADSCity : MonoBehaviour {

    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;
    
    AdRequest request;

    //영상
    private RewardedAd rewardedAd;
    string adUnitIdvideo;
    

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg, Toast_obj2;
    public Text Toast_txt;

    public GameObject ad_obj, radio_ani, adBtn_obj;

    int sG, mG;
    int sG2, mG2;

    public GameObject GM;

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

#if UNITY_ANDROID
        string appId = "ca-app-pub-9179569099191885~5921342761 "; //  테스트 ca-app-pub-3940256099942544~3347511713
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        //MobileAds.Initialize(appId);

        //this.RequestBanner();

#if UNITY_ANDROID
        adUnitIdvideo = "ca-app-pub-9179569099191885/8650861151"; // 테스트ca-app-pub-3940256099942544/5224354917 
#elif UNITY_IPHONE
            adUnitIdvideo = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitIdvideo = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitIdvideo);

        // Called when the user should be rewarded for watching a video.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestRewardedVideo();

        //보상형 전면 광고
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/5047087900", request, adLoadCallback);

        StopCoroutine("adTimeFlow2");
        StopCoroutine("adAniTime2");
        StartCoroutine("adTimeFlow2");
        StartCoroutine("adAniTime2");
    }


    private void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
    }


    //동영상
    private void RequestRewardedVideo()
    {


        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    //시청보상
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PlayerPrefs.SetInt("blad", 1);
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            PlayerPrefs.SetInt("secf3", 240);
        }
        ad_obj.SetActive(false);
    }

    //동영상닫음
    private void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        blackimg.SetActive(false);
        RequestRewardedVideo();
        Toast_obj.SetActive(true);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StartCoroutine("ToastImgFadeOut");
    }

    public void showAdmobVideo()
    {
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);
            if (this.rewardedAd.IsLoaded())
            {
                blackimg.SetActive(true);
                this.rewardedAd.Show();
                ad_obj.SetActive(false);
            }
            else
            {
                //StartCoroutine("ToastImgFadeOut");
                GM.GetComponent<UnityADSPark>().Wating();
                PlayerPrefs.SetInt("wait", 2);
            }
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


    public void OpenAd()
    {
        PlayerPrefs.SetInt("adrunout", 0);
        ad_obj.SetActive(true);
        GM.GetComponent<UnityADSPark>().watingAds_obj.SetActive(false);
    }


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf3", 0);
            if (sG2 < 0)
            {
            }
            else
            {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            sG2 = PlayerPrefs.GetInt("secf3", 0);
            sG2 = sG2 - 1;
            if (sG2 < 0)
            {
                sG2 = -1;
            }
            PlayerPrefs.SetInt("secf3", sG2);
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
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
            }

            yield return null;
        }

    }

    public void close()
    {
        ad_obj.SetActive(false);
    }

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
        PlayerPrefs.SetInt("wait", 1);
        if (rewardedInterstitialAd != null)
        {
            blackimg.SetActive(true);
            rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
        else
        {
            GM.GetComponent<UnityADSPark>().Wating();
            PlayerPrefs.SetInt("wait", 2);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        PlayerPrefs.SetInt("seatime", 4);
        blackimg.SetActive(false);
        Toast_obj2.SetActive(true);
    }

    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
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

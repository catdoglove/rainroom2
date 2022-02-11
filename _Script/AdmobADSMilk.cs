using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADSMilk : MonoBehaviour {
    
    
    AdRequest request;

    //영상
    private RewardedAd rewardedAd;
    string adUnitIdvideo;
    

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg, Toast_obj2;
    public Text Toast_txt;

    public Button milkad_btn;

    public GameObject GM;

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
        //MobileAds.Initialize(appId);

        //this.RequestBanner();


#if UNITY_ANDROID
        adUnitIdvideo = "ca-app-pub-9179569099191885/8650861151"; // 테스트 ca-app-pub-3940256099942544/5224354917
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
            PlayerPrefs.SetInt("milkadc", 1);
            PlayerPrefs.SetInt("setmilkadc", 0);
            StartCoroutine("ToastImgFadeOut");

            if (milkad_btn != null)
            {
                milkad_btn.interactable = false;
            }
        PlayerPrefs.SetInt("blad", 1);
    }

    //동영상닫음
    private void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        RequestRewardedVideo();
        blackimg.SetActive(false);
        Toast_obj.SetActive(true);
        Toast_txt.text = "우유 보상 두배 효과가 적용되었다.";
        PlayerPrefs.SetInt("adrunout", 0);
        StartCoroutine("ToastImgFadeOut");
    }

    public void showAdmobVideo()
    {
        PlayerPrefs.SetInt("wait", 1);
        if (this.rewardedAd.IsLoaded())
        {
            blackimg.SetActive(true);
            this.rewardedAd.Show();
        }
        else
        {
            GM.GetComponent<UnityADSMilk>().adYes();
            PlayerPrefs.SetInt("adrunout", 0);

        }
    }

    public void MilkToast()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "아직 볼 수 없다. 나중에 시도하자.";
            StartCoroutine("ToastImgFadeOut");
        }
    }
    

    
    IEnumerator ToastImgFadeOut()
    {
        if (PlayerPrefs.GetInt("setmilkadc", 0) == 1)
        {
            if (milkad_btn != null)
            {
                milkad_btn.interactable = true;
            }
            PlayerPrefs.SetInt("setmilkadc", 0);
        }

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



        

    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
    }
    

    //방지
    public void closeBlackImg()
    {
        blackimg.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADSMilk : MonoBehaviour {
    

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg, Toast_obj2, Toast_contain, Toast_contain2, Toast_contain3;
    public Text Toast_txt;

    public Button milkad_btn;

    public GameObject GM;

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);



        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8650861151";


        LoadRewardedAd();


    }






    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });

        RegisterEventHandlers(rewardedAd); //이벤트 등록
    }


    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log("광고");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            PlayerPrefs.SetInt("adrunout", 0);
            LoadRewardedAd();
            //Debug.Log("광고닫기");
        };
    }







    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null)
        {
            blackimg.SetActive(true);
            rewardedAd.Show((Reward reward) =>
            {
                PlayerPrefs.SetInt("milkadc", 1);
                PlayerPrefs.SetInt("setmilkadc", 0);
                //StartCoroutine("ToastImgFadeOut");
                Toast_obj2.SetActive(true);
                GM.GetComponent<WindowMiniGame>().MilkYes();
                Toast_contain3.SetActive(true);
                Toast_contain2.SetActive(false);
                PlayerPrefs.SetInt("blad", 1);
                blackimg.SetActive(false);
                PlayerPrefs.SetInt("adrunout", 0);
            });
        }
        else
        {
            GM.GetComponent<UnityADSMilk>().adYes();
            PlayerPrefs.SetInt("adrunout", 0);
            LoadRewardedAd();
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

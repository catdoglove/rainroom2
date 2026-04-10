using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api.Mediation.IronSource;
using GoogleMobileAds.Api.Mediation.UnityAds;

public class AdmobADS : MonoBehaviour {

    //보상형 전면 광고
    private RewardedAd rewardedInterstitialAd;
    private string _GoOutADSid;

    AdRequest request;

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg, Toast_obj2;
    public Text Toast_txt;


    public GameObject GM;
    bool rewardEarned = false;

    void Awake()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            GoogleMobileAds.Mediation.IronSource.Api.IronSource.SetMetaData("do_not_sell", "true");
            GoogleMobileAds.Mediation.UnityAds.Api.UnityAds.SetConsentMetaData("gdpr.consent", true);
            GoogleMobileAds.Mediation.UnityAds.Api.UnityAds.SetConsentMetaData("privacy.consent", true);
        }
        else
        {
            // Debug.Log("No Internet, skip init for now 인터넷 연결되지않음");
        }
    }
    // Use this for initialization 앱 ID
    void Start ()
    {/*
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = new List<string> { "016A11309F13D3972AB996CB6F5B25D6" }
        };
        */
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8650861151";
        _GoOutADSid = "ca-app-pub-9179569099191885/2270327348";



        if (Application.internetReachability != NetworkReachability.NotReachable) //인터넷연결된경우?
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                LoadRewardedAd();
                LoadRewardedInterstitialAd();
             /*   // initStatus 안에 어댑터 목록이 있어야 함
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (var keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    Debug.Log($"어댑터: {className}, 상태: {status.InitializationState}");
                }*/
            });
        }
        else
        {
            // Debug.Log("No Internet, skip init for now. 인터넷 연결 불가능");
        }
    }

    public void OnButtonClick()
    {
        MobileAds.OpenAdInspector((AdInspectorError error) =>
        {
            if (error != null)
                Debug.Log($"Ad Inspector 오류: {error.GetMessage()}");
            // Error will be set if there was an issue and the inspector was not displayed.
        });
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
        var adRequest = new AdRequest();

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
                RegisterEventHandlers(ad); //이벤트 등록
            });
        //Debug.Log("광고LoadRewardedAd");
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
           // Debug.Log("광고닫아졌는가");

           // if (rewardEarned)
          //  {
          //      Debug.Log("광고보상이 얻어졌는가");
                giveMeReward();
                rewardEarned = false;
         //   }


        };
    }

    void giveMeReward()
    {
        PlayerPrefs.SetInt("adrunout", 0);
        if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            PlayerPrefs.SetInt("talk", 5);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("talk", 5) >= 5)
            {
                PlayerPrefs.SetInt("secf", 180);
            }
        }
        else
        {
            PlayerPrefs.SetInt("talk", 5);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("talk", 5) >= 5)
            {
                PlayerPrefs.SetInt("secf2", 180);
            }
        }
       //  Debug.Log("광고기브미리워드");
        blackimg.SetActive(false);
        Toast_obj.SetActive(true);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StartCoroutine("ToastImgFadeOut");
        LoadRewardedAd();
    }


    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
               // blackimg.SetActive(true);
                rewardedAd.Show((Reward reward) =>
                {
                //    Debug.Log("광고리워드쇼");
                    rewardEarned = true;
                    PlayerPrefs.SetInt("blad", 1);
                    PlayerPrefs.Save();
                });
            }
            else
            {
                //StartCoroutine("ToastImgFadeOut");
                GM.GetComponent<UnityADS>().Wating();
                PlayerPrefs.SetInt("wait", 2);
                LoadRewardedAd();
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






    public void LoadRewardedInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        //Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_GoOutADSid, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
                RegisterEventHandlers2(ad); //이벤트 등록
            });
    }


    private void RegisterEventHandlers2(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log("광고");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            // TODO: Reward the user.
            PlayerPrefs.SetInt("bouttime", 9);
            Toast_obj2.SetActive(true);
            LoadRewardedInterstitialAd();
        };
    }




    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
         //   blackimg.SetActive(true);
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                blackimg.SetActive(false);
            });
        }
        else
        {
            GM.GetComponent<UnityADS>().Wating();
            PlayerPrefs.SetInt("wait", 2);
            LoadRewardedInterstitialAd();
        }

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

using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmobADStime : MonoBehaviour
{

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;



    Color color;
    public GameObject Toast_obj;
    public Text Toast_txt;

    public GameObject GM, timeWnd_obj, alarm_obj;

    // 중요: 보상 지급 타이밍을 메인 스레드로 넘겨줄 플래그
    private bool isFirstRewardPending = false;

    private bool isReloadPending = false;

    private int loadFailCount = 0;

    void Start()
    {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/3633709000";

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            LoadRewardedAd();
        }
        else
        {
            //Debug.Log("No Internet, skip init for now 인터넷 연결 X");
        }
    }


    // 중요: 메인 스레드에서 플래그를 감지하여 안전하게 보상 지급
    private void Update()
    {
        if (isFirstRewardPending)
        {
            isFirstRewardPending = false;
            giveMeReward();
        }

        if (isReloadPending)
        {
            isReloadPending = false;
            float delay = Mathf.Min(1f * Mathf.Pow(2, loadFailCount), 30f); // 최대 30초
            loadFailCount++;
            Invoke("LoadRewardedAd", delay);
        }
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
                  //  Debug.Log("광고 로드 실패 재시도");
                    isReloadPending = true; // 여기서도 플래그를 세워주면 무한 동력 완성!
                    return;
                }

                loadFailCount = 0;
                rewardedAd = ad;
                RegisterEventHandlers(ad); //이벤트 등록
            });

    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            isReloadPending = true; // 플래그만 세움, 여기서 직접 호출 X
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            isReloadPending = true;
        };
    }


    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                isFirstRewardPending = true;
            });
        }
        else
        {
            PlayerPrefs.SetInt("wait", 2);
            MilkToast();
           // LoadRewardedAd();
        }

    }

    void giveMeReward()
    {
        closeTimeADS();
        Toast_obj.SetActive(true);
        Toast_txt.text = "잠자는 시간이 2시간 감소되었다.";
        StopCoroutine("ToastImgFadeOut");
        StartCoroutine("ToastImgFadeOut");
        PlayerPrefs.SetInt("sleeptimeadsreward", 99);
        alarm_obj.SetActive(false);

        PlayerPrefs.SetInt("blad", 1);
        PlayerPrefs.SetInt("adrunout", 0);
        PlayerPrefs.Save();
    }


    public void openTimeADS()
    {
        timeWnd_obj.SetActive(true);
    }
    public void closeTimeADS()
    {
        timeWnd_obj.SetActive(false);
    }


    public void MilkToast()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "아직 볼 수 없다. 나중에 시도하자.";
            StopCoroutine("ToastImgFadeOut");
            StartCoroutine("ToastImgFadeOut");
        }
    }



    IEnumerator ToastImgFadeOut()
    {
        Image toastImage = Toast_obj.GetComponent<Image>();

        color.a = 1f;
        toastImage.color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(3.5f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            toastImage.color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);
    }




    public void touchToastEvt()
    {
        Toast_obj.SetActive(false);
    }

    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }


}

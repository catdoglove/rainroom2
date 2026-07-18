using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADSPark : MonoBehaviour {

    //보상형 전면 광고
    private RewardedAd rewardedInterstitialAd;
    private string _GoOutADSid;

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj, Toast_obj2;
    public Text Toast_txt;
    public GameObject GM;

    // 중요: 보상 지급 타이밍을 메인 스레드로 넘겨줄 플래그
    private bool isFirstRewardPending = false;
    private bool isSecondRewardPending = false;

    private bool isReloadPending = false;
    private bool isReloadInterstitialPending = false;

    private int loadFailCount = 0;
    private int loadFailCountInterstitial = 0;

    // 기존 플래그들 아래에 추가
    //private bool isFirstAdLoadSuccessPending = false;
   // private bool isSecondAdLoadSuccessPending = false;

    // 애드몹 초기화 상태를 저장할 변수 추가
    private bool isAdmobInitialized = false;
    private bool isInitializing = false;
    private Coroutine networkRoutine = null;
    private Coroutine initTimeoutRoutine = null;
    private bool isInitCompletePending = false;

    private bool isRewardedAdLoading = false;
    private bool isInterstitialAdLoading = false;
    //    public GameObject adsBtn;
    //    private Button adsBtnComponent;
    //    public Button cutTime_btn;

    private void Awake()
    {
      //  adsBtnComponent = adsBtn.GetComponent<Button>();
    }
    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8650861151";
        _GoOutADSid = "ca-app-pub-9179569099191885/2270327348";

        InitializeAds(); // 애드몹 초기화 시도
    }

    // 3초마다 인터넷이 켜졌는지 확인하는 감시자 역할
    private IEnumerator CheckNetworkRoutine()
    {
        // 애드몹이 초기화되지 않은 동안에만 무한 반복
        while (!isAdmobInitialized)
        {
            yield return new WaitForSeconds(3f); // 3초 쉬고 

            if (isInitializing) continue;

            // 인터넷이 켜졌는지 다시 확인
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                //Debug.Log("인터넷 연결 감지! 애드몹 초기화를 시작합니다.");
                InitializeAds(); // 연결되었으니 다시 초기화 시도
            }
        }
        networkRoutine = null;
    }

    public void InitializeAds()
    {
        // 이미 초기화가 끝났거나, 현재 초기화가 진행 중이면 아무것도 안 하고 돌아감
        if (isAdmobInitialized || isInitializing) return;

        if (Application.internetReachability != NetworkReachability.NotReachable) //인터넷연결된경우?
        {
            isInitializing = true; // 잠금장치 ON (초기화 시작)
            initTimeoutRoutine = StartCoroutine(InitTimeoutRoutine());

            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                if (isAdmobInitialized) return; // 이미 다른 시도로 초기화 완료된 경우 무시
                isInitCompletePending = true;
            });


        }
        else
        {
          //  adsBtnComponent.interactable = false; // 인터넷 없으면 비활성화
         //   if (cutTime_btn != null)
         //       cutTime_btn.interactable = false;
            if (networkRoutine == null)
            {
                //Debug.Log("인터넷 없음. 3초마다 재연결을 확인합니다.");
                networkRoutine = StartCoroutine(CheckNetworkRoutine());
            }
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

        if (isSecondRewardPending)
        {
            isSecondRewardPending = false;
            giveMeSecondReward();
        }

        if (isReloadPending)
        {
            isReloadPending = false;
            //   if (adsBtnComponent != null) adsBtnComponent.interactable = false;

            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }

            if (!IsInvoking("LoadRewardedAd")) // ← 이미 예약됐는지 체크
            {
                float delay = Mathf.Min(1f * Mathf.Pow(2, loadFailCount), 30f); // 최대 30초
                loadFailCount++;
                Invoke("LoadRewardedAd", delay);
            }

        }

        if (isReloadInterstitialPending)
        {
            isReloadInterstitialPending = false;
            //    if (cutTime_btn != null) cutTime_btn.interactable = false;

            if (rewardedInterstitialAd != null)
            {
                rewardedInterstitialAd.Destroy();
                rewardedInterstitialAd = null;
            }

            if (!IsInvoking("LoadRewardedInterstitialAd")) // ← 이미 예약됐는지 체크
            {
                float delay = Mathf.Min(1f * Mathf.Pow(2, loadFailCountInterstitial), 30f); // 최대 30초
                loadFailCountInterstitial++;
                Invoke("LoadRewardedInterstitialAd", delay);
            }
        }

        if (isInitCompletePending)
        {
            isInitCompletePending = false;

            isAdmobInitialized = true;
            isInitializing = false;

            if (initTimeoutRoutine != null)
            {
                StopCoroutine(initTimeoutRoutine);
                initTimeoutRoutine = null;
            }

            // 광고 로드 시작
            LoadRewardedAd();
            LoadRewardedInterstitialAd();
        }

        /*
        if (isFirstAdLoadSuccessPending)
        {
            isFirstAdLoadSuccessPending = false;
       //     adsBtnComponent.interactable = true;
        }*/

        /* if (isSecondAdLoadSuccessPending)
         {
             isSecondAdLoadSuccessPending = false;
             if (cutTime_btn != null) // 먼저 버튼이 존재하는지 확인
             {
                 if (PlayerPrefs.GetInt("outtimecut", 0) != 4)
                     cutTime_btn.interactable = true;
             }
         }*/

    }


    public void LoadRewardedAd()
    {
        if (isRewardedAdLoading) return;

        isRewardedAdLoading = true; // 로딩 시작
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
                isRewardedAdLoading = false; // 로드 완료(또는 실패) 시 플래그 해제

                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                 //   Debug.Log("광고 로드 실패 재시도");
                    isReloadPending = true; // 여기서도 플래그를 세워주면 무한 동력 완성!
                    return;
                }

                loadFailCount = 0;
                rewardedAd = ad;
                RegisterEventHandlers(ad); //이벤트 등록
              //  isFirstAdLoadSuccessPending = true;
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

    void giveMeReward()
    {
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.SetInt("secf0", 180);

       // blackimg.SetActive(false);
        Toast_obj.SetActive(true);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StopCoroutine("ToastImgFadeOut");
        StartCoroutine("ToastImgFadeOut");

        PlayerPrefs.SetInt("blad", 1);
        PlayerPrefs.Save();
    }



    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StopCoroutine("ToastImgFadeOut");
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
             //   blackimg.SetActive(true);
                rewardedAd.Show((Reward reward) =>
                {
                    isFirstRewardPending = true;
                });
            }
            else
            {
                //StartCoroutine("ToastImgFadeOut");
                GM.GetComponent<UnityADSPark>().Wating();
                PlayerPrefs.SetInt("wait", 2);
                LoadRewardedAd();
            }
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







    public void LoadRewardedInterstitialAd()
    {
        if (isInterstitialAdLoading) return;

        isInterstitialAdLoading = true; // 로딩 시작
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
                isInterstitialAdLoading = false; // 로드 완료(또는 실패) 시 플래그 해제

                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                 //   Debug.Log("광고 로드 실패, 재시도");
                    isReloadInterstitialPending = true; // 여기서도 플래그를 세워주면 무한 동력 완성!
                    return;
                }

                loadFailCountInterstitial = 0;
                rewardedInterstitialAd = ad;
                RegisterEventHandlers2(ad); //이벤트 등록
             //   isSecondAdLoadSuccessPending = true;
            });
    }


    private void giveMeSecondReward()
    {
        // TODO: Reward the user.
        PlayerPrefs.SetInt("foresttime", 4);
        Toast_obj2.SetActive(true);
        //LoadRewardedInterstitialAd();
    }





    private void RegisterEventHandlers2(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            isReloadInterstitialPending = true;
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            isReloadInterstitialPending = true;
        };
    }



    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
        //    if (cutTime_btn != null)
         //       cutTime_btn.interactable = false;
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                isSecondRewardPending = true;
            });
        }
        else
        {
            GM.GetComponent<UnityADSPark>().Wating();
            PlayerPrefs.SetInt("wait", 2);
            LoadRewardedInterstitialAd();
        }

    }


    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
    }

    private void OnDestroy()
    {
        CancelInvoke();
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }
    }

    private void OnDisable()
    {
        if (networkRoutine != null)
        {
            StopCoroutine(networkRoutine); // 혹시 모를 찌꺼기 실행을 확실히 정지
            networkRoutine = null;         // 변수를 깨끗하게 청소!
        }
        if (initTimeoutRoutine != null)
        {
            StopCoroutine(initTimeoutRoutine);
            initTimeoutRoutine = null;
        }
    }
    // 초기화가 특정 시간 내에 안 끝나면 강제로 잠금을 풀어주는 코루틴
    private IEnumerator InitTimeoutRoutine()
    {
        yield return new WaitForSeconds(15f); // 10초 대기 (네트워크 상태에 따라 15초 등으로 조절 가능)
        if (isAdmobInitialized)
        {
            yield break;
        }
        if (isInitializing)
        {
         //   Debug.Log("애드몹 초기화 타임아웃! 잠금을 해제하여 재시도를 허용합니다.");
            isInitializing = false;
        }

        if (networkRoutine == null)
        {
            networkRoutine = StartCoroutine(CheckNetworkRoutine());
        }
    }
    void OnApplicationPause(bool pause)
    {
        if (!pause && isAdmobInitialized) // 초기화 완료 후에만 체크
        {
            if (rewardedAd == null || !rewardedAd.CanShowAd())
            {
                LoadRewardedAd();
            }
            if (rewardedInterstitialAd == null || !rewardedInterstitialAd.CanShowAd())
            {
                LoadRewardedInterstitialAd();
            }
        }
    }
}

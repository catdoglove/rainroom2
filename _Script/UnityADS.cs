using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityADS : MonoBehaviour {

    private string gameId = "1486550";//★테스트ID , Window > Services 설정 테스트 바꿀것 test 1486550
    public int soundck;
    public GameObject ad_obj, radio_ani, adBtn_obj;

	int sG,mG;

    // Use this for initialization
    void Start () {
        StartCoroutine("adTimeFlow");
        StartCoroutine ("adAniTime");
        if (Advertisement.isSupported)
          {
              Advertisement.Initialize(gameId, true);
          }
      }
      
    // Update is called once per frame

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            PlayerPrefs.SetInt("secf", 30);
        }
        else
        {
        }
    }

    public void adYN()
    {
        ad_obj.SetActive(true);
    }
    public void closeAdYN()
    {
        ad_obj.SetActive(false);
    }
    public void adYes()
    {
        ShowRewardedAd();
        ad_obj.SetActive(false);
    }
    

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            radio_ani.SetActive(false);
            adBtn_obj.SetActive(false);
            PlayerPrefs.SetInt("talk", 5);
        }
    }

    

	IEnumerator adTimeFlow(){
		while (mG>-1) {

            
			sG = PlayerPrefs.GetInt("secf",30);
            Debug.Log(sG);
            mG = (int)(sG / 60);
			sG = sG-(sG / 60)*60;
			if (sG < 0) {
				sG = 0;
				mG = 0;
            } else {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
			sG = PlayerPrefs.GetInt("secf",30);
			sG = sG - 1;
			if (sG < 0) {
				sG = -1;
			}
			PlayerPrefs.SetInt("secf",sG);
			yield return new WaitForSeconds(1f);
		}
	}
    IEnumerator adAniTime()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG < 0)
            {
                if (PlayerPrefs.GetInt("front", 1) == 1)
                {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
                }
                else
                {
                    radio_ani.SetActive(false);
                    adBtn_obj.SetActive(false);
                }
            }
            
            yield return null;
        }

    }
}

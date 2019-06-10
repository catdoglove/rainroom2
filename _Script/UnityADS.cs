using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADS : MonoBehaviour {

    private string gameId = "1486550";//★테스트ID , Window > Services 설정 테스트 바꿀것 test 1486550
    public int soundck;
    public GameObject ad_obj, radio_ani, adBtn_obj;

	int sG,mG;
    int sG2, mG2;
   
    Color color;
    public GameObject Toast_obj;

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);

        if (PlayerPrefs.GetInt("place", 1) == 1)
        {
            StartCoroutine("adTimeFlow2");
            StartCoroutine("adAniTime2");
        }
        else
        {
            StartCoroutine("adTimeFlow");
            StartCoroutine("adAniTime");
        }
        
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
            PlayerPrefs.SetInt("secf", 300);
        }
        else
        {
            StartCoroutine("ToastImgFadeOut");
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
			sG = PlayerPrefs.GetInt("secf",300);
            //Debug.Log(sG);
            mG = (int)(sG / 60);
			sG = sG-(sG / 60)*60;
			if (sG < 0) {
				sG = 0;
				mG = 0;
            } else {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
			sG = PlayerPrefs.GetInt("secf",300);
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


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf2", 300);
            //Debug.Log(sG);
            mG2= (int)(sG2 / 60);
            sG2 = sG2 - (sG2 / 60) * 60;
            if (sG2 < 0)
            {
                sG2 = 0;
                mG2 = 0;
            }
            else
            {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            sG2 = PlayerPrefs.GetInt("secf2", 300);
            sG2 = sG2 - 1;
            if (sG2 < 0)
            {
                sG2 = -1;
            }
            PlayerPrefs.SetInt("secf2", sG2);
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
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
                }
                else
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
                    
            }

            yield return null;
        }

    }



    IEnumerator ToastImgFadeOut()
    {
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
}

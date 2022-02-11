using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSMilk : MonoBehaviour {

    private string gameId = "2883785";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550)
    public int soundck;
    public GameObject ad_obj;
    
   
    Color color;
    public GameObject Toast_obj;

    public GameObject GM;

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);
        

        if (Advertisement.isSupported)
          {
              Advertisement.Initialize(gameId, false);
          }
      }

    // Update is called once per frame

    public void ShowRewardedAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        if (Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            //PlayerPrefs.SetInt("secf", 240);
        }
        else
        {
            GM.GetComponent<UnityADS>().Wating();
            PlayerPrefs.SetInt("wait", 2);
        }


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
                PlayerPrefs.SetInt("milkadc", 1);
                PlayerPrefs.SetInt("setmilkadc", 0);
                if (GM.GetComponent<AdmobADSMilk>().milkad_btn != null)
                {
                    GM.GetComponent<AdmobADSMilk>().milkad_btn.interactable = false;
                }
                GM.GetComponent<AdmobADSMilk>().blackimg.SetActive(false);
                GM.GetComponent<AdmobADSMilk>().Toast_obj.SetActive(true);
                GM.GetComponent<AdmobADSMilk>().Toast_txt.text = "우유 보상 두배 효과가 적용되었다.";
                PlayerPrefs.SetInt("adrunout", 0);
                GM.GetComponent<AdmobADSMilk>().StartCoroutine("ToastImgFadeOut");
        }
    }
    
    
}

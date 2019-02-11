using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {
	public GameObject sceneMove_btn;

	public GameObject MainGM;
	public GameObject GMN;
    public GameObject moreLv_obj;

	AsyncOperation async;

    void Start()
    {
      
    }

	IEnumerator Load()
	{
        async = SceneManager.LoadSceneAsync("SubLoad");
        //async = SceneManager.LoadSceneAsync("Main2");
		while (!async.isDone)
		{
            yield return true;
		}
    }

	IEnumerator Load2()
	{
        async = SceneManager.LoadSceneAsync("SubLoad");
        //async = SceneManager.LoadSceneAsync("Main");
        while (!async.isDone)
		{
            yield return true;
		}
        

    }

	public void moveDown(){

        if (PlayerPrefs.GetInt("lovelv", 0) >= 3)
        {
            PlayerPrefs.SetInt("unlockshop", 10);
            if (GMN == null)
            {
                GMN = GameObject.FindGameObjectWithTag("GMtag");
            }
            GMN.GetComponent<MainBtnEvt>().allClose();


            PlayerPrefs.SetInt("place", 1);
            StartCoroutine(Load());
            PlayerPrefs.Save();
            //아래층으로
        }
        else
        {
            moreLv_obj.SetActive(true);
        }
	}

	public void moveUp(){
        if(GMN == null) {
            GMN = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMN.GetComponent<MainBtnEvt> ().allClose ();


        PlayerPrefs.SetInt("place", 0);
        StartCoroutine(Load2());
        PlayerPrefs.Save();
        //다락방으로
    }


    public void closeMoreLv()
    {
        moreLv_obj.SetActive(false);
    }

}

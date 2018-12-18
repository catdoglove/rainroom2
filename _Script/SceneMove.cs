using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {
	public GameObject sceneMove_btn;

	public GameObject MainGM;
	public GameObject GMN;

	AsyncOperation async;

	IEnumerator Load()
	{
		async = SceneManager.LoadSceneAsync("Main2");
		while (!async.isDone)
		{
			yield return true;
		}

	}

	IEnumerator Load2()
	{
		async = SceneManager.LoadSceneAsync("Main");
		while (!async.isDone)
		{
			yield return true;
		}

	}

	public void moveDown(){
        if (GMN == null) {
            GMN= GameObject.FindGameObjectWithTag("GMtag");
        }
		GMN.GetComponent<MainBtnEvt> ().allClose ();
		
        PlayerPrefs.SetInt("place", 1);
        StartCoroutine(Load());
        PlayerPrefs.Save();
		//아래층으로
	}

	public void moveUp(){
        if(GMN == null) {
            GMN = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMN.GetComponent<MainBtnEvt> ().allClose ();
		for (int i = 0; i < 3; i++) {
        }
        PlayerPrefs.SetInt("place", 0);
        StartCoroutine(Load2());
        PlayerPrefs.Save();
        //다락방으로
    }



}

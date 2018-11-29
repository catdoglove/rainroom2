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
		for (int i = 0; i < 3; i++) {
			GMN.GetComponent<MainBtnEvt> ().MainBtn_obj [i].SetActive (false);
		}
		StartCoroutine(Load());
		PlayerPrefs.SetInt ("place",1);
		//아래층으로
	}

	public void moveUp(){
		GMN = GameObject.FindGameObjectWithTag ("GMtag");
		GMN.GetComponent<MainBtnEvt> ().allClose ();
		for (int i = 0; i < 3; i++) {
			GMN.GetComponent<MainBtnEvt> ().MainBtn_obj [i].SetActive (false);
		}
		StartCoroutine(Load2());
		PlayerPrefs.SetInt ("place",0);
		//다락방으로
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {
	public GameObject sceneMove_btn;

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
		StartCoroutine(Load());
	}

	public void moveUp(){
		StartCoroutine(Load2());
	}



}

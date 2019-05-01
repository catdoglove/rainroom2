using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveOut : MonoBehaviour {
	public GameObject sceneMove_btn;

    public GameObject GMtag;
    public GameObject secondGM;

    public GameObject moreLv_obj;

    //미리 씬을 불러오기
    AsyncOperation async;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    // Use this for initialization
    void Start () {
		
	}

    public void walkOut()
    {
        //StartCoroutine("LoadOut");
    }

    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }
    



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {
    AsyncOperation async;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadCount());
    }



    IEnumerator Load()
    {

        if(PlayerPrefs.GetInt("place", 0) == 1)
        {
            async = SceneManager.LoadSceneAsync("Main2");
        }else if(PlayerPrefs.GetInt("place", 0) == 0)
        {
            async = SceneManager.LoadSceneAsync("Main");
        }
        
        while (!async.isDone)
        {
            yield return true;
        }

    }

    IEnumerator LoadCount()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Load());
    }



}

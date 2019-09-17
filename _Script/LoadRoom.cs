using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {
    AsyncOperation async;

    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    // Use this for initialization
    void Start () {
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = 300f;
        menuBlock_obj.transform.position = menuBlock_vet;
        
        StartCoroutine(LoadCount());
    }



    IEnumerator Load()
    {
        if (PlayerPrefs.GetInt("outtrip", 0)==1)
        {
            async = SceneManager.LoadSceneAsync("park");
            if (PlayerPrefs.GetInt("endbefore", 0)==1)
            {
                PlayerPrefs.SetInt("endafter", 1);
            }
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 3)
        {
            async = SceneManager.LoadSceneAsync("parkMountain");
        }
        else if(PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            async = SceneManager.LoadSceneAsync("city");
            if (PlayerPrefs.GetInt("endbefore", 0) == 1)
            {
                PlayerPrefs.SetInt("endafter", 1);
            }
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 4)
        {
            async = SceneManager.LoadSceneAsync("citySea");
        }
        else
        {
            if (PlayerPrefs.GetInt("place", 0) == 1)
            {
                async = SceneManager.LoadSceneAsync("Main2");
            }
            else  if (PlayerPrefs.GetInt("place", 0) == 0)
            {
                async = SceneManager.LoadSceneAsync("Main");
            }
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

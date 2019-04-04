using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

	AsyncOperation async;
	Color color;
	public GameObject logoImg, prolouge_obj, logocanvas;
    public Animator anim;


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("logofirst", 0) == 99)
        {
            prolouge_obj.SetActive(false);
        }
        else
        {
            logocanvas.SetActive(false);
        }

		StartCoroutine(imgFadeIn());


        float screenNum = (float)Screen.height / (float)Screen.width;
        if (screenNum < 0.57f)
        {

            Screen.SetResolution(Screen.width, Screen.width / 16 * 9, true);

        }
        else if (screenNum >= 0.57f && screenNum < 0.62f)
        {

            Screen.SetResolution(Screen.width, Screen.width / 5 * 3, true);

        }
        else if (screenNum >= 0.62f && screenNum < 0.65f)
        {

            Screen.SetResolution(Screen.width, Screen.width / 16 * 10, true);

        }
        else if (screenNum >= 0.65f && screenNum < 0.7f)
        {

            Screen.SetResolution(Screen.width, Screen.width / 3 * 2, true);

        }
        else if (screenNum >= 0.7f)
        {

            Screen.SetResolution(Screen.width, Screen.width / 4 * 3, true);

        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.width / 3 * 2, true);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f)
        {
            prolouge_obj.SetActive(false);
            StartCoroutine(LoadCount());
        }

        //최초로 프롤로그 실행되는 코드, 로고 애니메이션 false
            
    }

	IEnumerator Load()
	{
		async = SceneManager.LoadSceneAsync("Main");
		while (!async.isDone)
		{
			yield return true;
		}

	}
	IEnumerator LoadCount()
    {
        yield return null;
        StartCoroutine(Load());
	}

	IEnumerator imgFadeIn()
	{
		color = logoImg.GetComponent<Image>().color;
		for (float i = 0f; i < 1f; i += 0.05f)
		{
			color.a = Mathf.Lerp(0f, 1f, i);
			logoImg.GetComponent<Image>().color = color;
			yield return new WaitForSeconds(0.025f);
		}


        //최초로 프롤로그 실행되는 코드, 로고 애니메이션 false
        yield return new WaitForSeconds(2f);
        logocanvas.SetActive(false);
        if (PlayerPrefs.GetInt("logofirst") == 99)
        {
            StartCoroutine(Load());
        }
        PlayerPrefs.SetInt("logofirst", 99);
        PlayerPrefs.Save();
    }


    public void nextScene()
    {
        //StartCoroutine(LoadCount());
    }

}

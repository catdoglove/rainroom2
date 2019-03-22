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
		StartCoroutine(imgFadeIn());
    }
    
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f)
        {
            prolouge_obj.SetActive(false);
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
		yield return new WaitForSeconds(0.5f);
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

    }


    public void nextScene()
    {
        StartCoroutine(LoadCount());
    }

}

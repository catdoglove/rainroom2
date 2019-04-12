using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {
	public GameObject sceneMove_btn;

	public GameObject MainGM;
	public GameObject GMN;
    public GameObject secondGM;
    public GameObject moreLv_obj;

	AsyncOperation async;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    void Start()
    {
        if(PlayerPrefs.GetInt("achievemove", 0) == 1)
        {
            PlayerPrefs.SetInt("achievemove", 0);
            achievementfunc();
        }
      
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

        
            PlayerPrefs.SetInt("unlockshop", 10);
            if (PlayerPrefs.GetInt("waterpurifiershop", 0)==0)
            {
                PlayerPrefs.SetInt("waterpurifiershop", 1);
            }
            if (GMN == null)
            {
                GMN = GameObject.FindGameObjectWithTag("GMtag");
            }
            GMN.GetComponent<MainBtnEvt>().allClose();
            PlayerPrefs.SetInt("achievemove", 1);
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
        PlayerPrefs.SetInt("achievemove", 1);
        PlayerPrefs.SetInt("place", 0);
        StartCoroutine(Load2());
        PlayerPrefs.Save();
        //다락방으로
    }
    
    public void closeMoreLv()
    {
        moreLv_obj.SetActive(false);
    }


    
    //업적
    void achievementfunc()
    {
        int cts = PlayerPrefs.GetInt("countladderst", 0);
        cts++;
        PlayerPrefs.SetInt("countladderst", cts);

        if (cts >= 100 && PlayerPrefs.GetInt("downst", 0) < 3)
        {
            PlayerPrefs.SetInt("downst", 3);
            secondGM.GetComponent<AchievementShow>().achievementCheck(2, 2);
        }
        else if (cts >= 20 && PlayerPrefs.GetInt("downst", 0) < 2)
        {
            PlayerPrefs.SetInt("downst", 2);
            secondGM.GetComponent<AchievementShow>().achievementCheck(2, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("downst", 0) < 1)
        {
            PlayerPrefs.SetInt("downst", 1);
            secondGM.GetComponent<AchievementShow>().achievementCheck(2, 0);
        }
    }

    /*
    void achievement()
    {
        StartCoroutine("achievementIn");
    }

    IEnumerator achievementOut()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 1f; i > -0.2f; i -= 0.05f)
        {
            moveY = moveY + 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }

    }
    IEnumerator achievementIn()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 0f; i < 1.2f; i += 0.05f)
        {
            moveY = moveY - 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine("achievementOut");
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {
	public GameObject sceneMove_btn;

    public GameObject MainGM, loadGM;
    public GameObject GMN;
    public GameObject secondGM;
    public GameObject moreLv_obj;

	AsyncOperation async;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    //가림판
    public GameObject moveBack_obj;

    //타이틀
    public GameObject title_obj;

    void Start()
    {
        if(PlayerPrefs.GetInt("achievemove", 0) == 1)
        {
            achievementfunc();
            PlayerPrefs.SetInt("achievemove", 0);
            if(PlayerPrefs.GetInt("place", 0) == 0)
            {
                title_obj.SetActive(false);
            }
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

    public void moveDown()
    { //메모리해제
        if (loadGM == null)
        {
            loadGM = GameObject.FindGameObjectWithTag("loadGM");
        }

        moveBack_obj.SetActive(true);
        loadGM.GetComponent<LoadingData>().wall_spr[0] = null;
        loadGM.GetComponent<LoadingData>().wall2_spr[0] = null;
        loadGM.GetComponent<LoadingData>().wall_spr[1] = null;
        loadGM.GetComponent<LoadingData>().wall2_spr[1] = null;
        loadGM.GetComponent<LoadingData>().wall_spr[2] = null;
        loadGM.GetComponent<LoadingData>().wall2_spr[2] = null;
        loadGM.GetComponent<LoadingData>().wall_spr[3] = null;
        loadGM.GetComponent<LoadingData>().wall2_spr[3] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall_spr[1] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall2_spr[1] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall_spr[2] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall2_spr[2] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall_spr[3] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall2_spr[3] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall_spr[4] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall2_spr[4] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall_spr[5] = null;
        MainGM.GetComponent<FirstRoomFunction>().reformWall2_spr[5] = null;
        MainGM.GetComponent<FirstRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = null;
        MainGM.GetComponent<FirstRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = null;

        PlayerPrefs.SetInt("storg", 1);
        PlayerPrefs.SetInt("unlockshop", 10);
        if (PlayerPrefs.GetInt("waterpurifiershop", 0) == 0)
        {
            PlayerPrefs.SetInt("waterpurifiershop", 1);
        }
        if (GMN == null)
        {
            GMN = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMN.GetComponent<MainBtnEvt>().allClose();
        PlayerPrefs.SetInt("achievemove", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("place", 1);
        StartCoroutine(Load());
        PlayerPrefs.Save();
        //아래층으로
    }

	public void moveUp(){
        if(GMN == null) {
            GMN = GameObject.FindGameObjectWithTag("GMtag");
        }
        if (secondGM == null)
        {
            secondGM = GameObject.FindGameObjectWithTag("GM2");
        }

        //메모리해제
        moveBack_obj.SetActive(true);
        secondGM.GetComponent<secondRoomFunction>().reformWall_spr[1] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall2_spr[1] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall_spr[2] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall2_spr[2] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall_spr[3] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall2_spr[3] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall_spr[4] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall2_spr[4] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall_spr[5] = null;
        secondGM.GetComponent<secondRoomFunction>().reformWall2_spr[5] = null;
        secondGM.GetComponent<secondRoomFunction>().wallImg_obj.GetComponent<Image>().sprite = null;
        secondGM.GetComponent<secondRoomFunction>().wallImg2_obj.GetComponent<Image>().sprite = null;

        GMN.GetComponent<MainBtnEvt> ().allClose ();
        PlayerPrefs.SetInt("achievemove", 1);
        PlayerPrefs.Save();
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

        if (cts >= 500 && PlayerPrefs.GetInt("downst", 0) < 3)
        {
            PlayerPrefs.SetInt("downst", 3);
            secondGM.GetComponent<AchievementShow>().achievementCheck(2, 2);
        }
        else if (cts >= 100 && PlayerPrefs.GetInt("downst", 0) < 2)
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

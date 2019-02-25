using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMiniGame : MonoBehaviour {

    public GameObject GM;

    public GameObject miniGameWindow_obj, miniBackWindow_obj;

    public GameObject minicat_obj;

    public float cMoveX = 0f;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
    }
	

    public void OpenMiniGame()
    {
        miniGameWindow_obj.SetActive(true);
        PlayerPrefs.SetInt("miniopen",1);
        if(PlayerPrefs.GetInt("windowcatrand", 0) <= 10)
        {
            PlayerPrefs.SetInt("windowcatrand", 999);
            int cat = Random.Range(-2, 2);
            cMoveX = cat;
            minicat_obj.transform.position = new Vector3(cMoveX, minicat_obj.transform.position.y, minicat_obj.transform.position.z);
            minicat_obj.SetActive(true);
        }
        PlayerPrefs.SetInt("windowairplane", 999);
        PlayerPrefs.Save();
    }

    public void CloseMiniGame()
    {
        miniGameWindow_obj.SetActive(false);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        minicat_obj.SetActive(false);
    }

    public void TouchBallon()
    {
        GM.GetComponent<MainTime>().bMoveX = 15.4f;
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 1);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().balloon_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().bMoveX, GM.GetComponent<MainTime>().balloon_obj.transform.position.y, GM.GetComponent<MainTime>().balloon_obj.transform.position.z);
        GM.GetComponent<MainTime>().balloonR_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().bMoveX, GM.GetComponent<MainTime>().balloonR_obj.transform.position.y, GM.GetComponent<MainTime>().balloon_obj.transform.position.z);
        GM.GetComponent<MainTime>().endBMove_i = 0;
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    public void TouchCat()
    {
        achievementfunc2();
        minicat_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    public void TouchAirplane()
    {
        achievementfunc();
        GM.GetComponent<MainTime>().pMoveX = 17.4f;
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowairplane", 999);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().airplane_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().pMoveX, GM.GetComponent<MainTime>().airplane_obj.transform.position.y, GM.GetComponent<MainTime>().airplane_obj.transform.position.z);
        GM.GetComponent<MainTime>().plane_i = 0;
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    //업적
    void achievementfunc()
    {
        
        int cts = PlayerPrefs.GetInt("countairplanest", 0);
        cts++;
        PlayerPrefs.SetInt("countairplanest", cts);
        if (cts >= 50 && PlayerPrefs.GetInt("airplanest", 0) < 3)
        {
            PlayerPrefs.SetInt("airplanest", 3);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 2);
        }
        else if (cts >= 10 && PlayerPrefs.GetInt("airplanest", 0) < 2)
        {
            PlayerPrefs.SetInt("airplanest", 2);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("airplanest", 0) < 1)
        {
            PlayerPrefs.SetInt("airplanest", 1);
            GM.GetComponent<AchievementShow>().achievementCheck(3, 0);
        }
    }

    void achievementfunc2()
    {
        
        int cts = PlayerPrefs.GetInt("countpetcatst", 0);
        cts++;
        PlayerPrefs.SetInt("countpetcatst", cts);
        if (cts >= 50 && PlayerPrefs.GetInt("petcatst", 0) < 3)
        {
            PlayerPrefs.SetInt("petcatst", 3);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 2);
        }
        else if (cts >= 10 && PlayerPrefs.GetInt("petcatst", 0) < 2)
        {
            PlayerPrefs.SetInt("petcatst", 2);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("petcatst", 0) < 1)
        {
            PlayerPrefs.SetInt("petcatst", 1);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 0);
        }
        Debug.Log("cat" + PlayerPrefs.GetInt("petcatst", 0) + "count" + PlayerPrefs.GetInt("countpetcatst", 0));
    }

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

}

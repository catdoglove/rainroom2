using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowMiniGame : MonoBehaviour {

    public GameObject GM;

    public GameObject miniGameWindow_obj, miniBackWindow_obj;

    public GameObject minicat_obj;

    public float cMoveX = 0f;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;

    //낮밤
    public GameObject dayLight_obj,dayRoom_obj,dayWindow_obj;
    public Sprite[] dayLight_spr;

    //우유
    public GameObject milk_obj, milkBtn_obj,milkWindow_obj;
    public Sprite[] milk_spr;
    public Text milk_txt, milkDay_txt;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
    }
	

    public void OpenMiniGame()
    {
        //우유확인
        milk();
        System.DateTime time = System.DateTime.Now;
        if (time.ToString("tt") == "PM")
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k >= 6)
            {
                //밤
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[0];
                dayWindow_obj.SetActive(true);
                dayRoom_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday",1);
            }
            else
            {
                //낮
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[1];
                dayWindow_obj.SetActive(false);
                dayRoom_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int k = int.Parse(time.ToString("hh"));
            if (k == 12)
            {
                k = 0;
            }
            if (k < 6)
            {
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[0];
                dayWindow_obj.SetActive(true);
                dayRoom_obj.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayLight_obj.GetComponent<Image>().sprite = dayLight_spr[1];
                dayRoom_obj.SetActive(false);
                dayWindow_obj.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }

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
        milkWindow_obj.SetActive(false);
        miniGameWindow_obj.SetActive(false);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 0);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        minicat_obj.SetActive(false);
    }

    public void ball1()
    {
        float xx = GM.GetComponent<MainTime>().moveX1;
        float yy = GM.GetComponent<MainTime>().balloon_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        GM.GetComponent<MainTime>().randball1_i = 0;
        GM.GetComponent<MainTime>().moveX1 = 15.4f;
        GM.GetComponent<MainTime>().balloon_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().moveX1, GM.GetComponent<MainTime>().balloon_obj.transform.position.y, GM.GetComponent<MainTime>().balloon_obj.transform.position.z);
    }

    public void ball2()
    {
        float xx = GM.GetComponent<MainTime>().moveX2;
        float yy = GM.GetComponent<MainTime>().balloonR_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        GM.GetComponent<MainTime>().randball2_i = 0;
        GM.GetComponent<MainTime>().moveX2 = 15.4f;
        GM.GetComponent<MainTime>().balloonR_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().moveX2, GM.GetComponent<MainTime>().balloonR_obj.transform.position.y, GM.GetComponent<MainTime>().balloonR_obj.transform.position.z);
    }


    public void TouchBallon()
    {
        
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().endBMove_i = 0;
    }

    public void TouchCat()
    {
        float xx = cMoveX;
        float yy = minicat_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        achievementfunc2();
        minicat_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 10;
        hotRain_i = hotRain_i + 6;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    public void TouchAirplane()
    {
        float xx = GM.GetComponent<MainTime>().pMoveX;
        float yy = GM.GetComponent<MainTime>().airplane_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        achievementfunc();
        GM.GetComponent<MainTime>().pMoveX = 17.4f;
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 50;
        hotRain_i = hotRain_i + 20;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("windowairplane", 999);
        PlayerPrefs.Save();
        GM.GetComponent<MainTime>().airplane_obj.transform.position = new Vector3(GM.GetComponent<MainTime>().pMoveX, GM.GetComponent<MainTime>().airplane_obj.transform.position.y, GM.GetComponent<MainTime>().airplane_obj.transform.position.z);
        GM.GetComponent<MainTime>().plane_i = 0;
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }


    void milk()
    {
        //초기값을가져옵니다
        System.DateTime dateTimenow = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //str로장되어있는과거접속시간을가져옵니다
        string lastTimem = PlayerPrefs.GetString("milktime", dateTimenow.ToString());
        //형변환을해줍니다
        System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);
        //계산
        System.TimeSpan compareTimem = System.DateTime.Now - lastDateTimem;
        int hour;
        int minute;
        hour = 11 - (int)compareTimem.TotalHours;
        minute = 59 - (int)compareTimem.TotalMinutes;
        if (hour < 0)
        {
            milk_obj.GetComponent<Image>().sprite = milk_spr[1];
            milkBtn_obj.SetActive(true);
        }
        else
        {
            milk_obj.GetComponent<Image>().sprite = milk_spr[0];
            milkBtn_obj.SetActive(false);
        }
    }

    public void GetMilk()
    {
        string str = PlayerPrefs.GetString("code", "");
        milkWindow_obj.SetActive(true);
        milkDay_txt.text = "제조일자:"+ System.DateTime.Now.ToString("yyyy년MM월dd일");
        milk_txt.text = "";
        int cm = PlayerPrefs.GetInt(str + "c", 0);
        int hm = PlayerPrefs.GetInt(str + "h", 0);
        int htm = PlayerPrefs.GetInt(str + "ht", 0);
        cm = cm + 100;
        hm = hm + 10;
        htm = htm + 2;
        PlayerPrefs.SetInt(str + "c", cm);
        PlayerPrefs.SetInt(str + "h", hm);
        PlayerPrefs.SetInt(str + "ht", htm);

        //시간초기화
        //PlayerPrefs.SetString("milktime", System.DateTime.Now.ToString());
    }

    public void CloseMlik()
    {
        milkWindow_obj.SetActive(false);
    }

    //업적
    void achievementfunc()
    {
        
        int cts = PlayerPrefs.GetInt("countairplanest", 0);
        cts++;
        PlayerPrefs.SetInt("countairplanest", cts);
        if (cts >= 100 && PlayerPrefs.GetInt("airplanest", 0) < 3)
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
        if (cts >= 500 && PlayerPrefs.GetInt("petcatst", 0) < 3)
        {
            PlayerPrefs.SetInt("petcatst", 3);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 2);
        }
        else if (cts >= 100 && PlayerPrefs.GetInt("petcatst", 0) < 2)
        {
            PlayerPrefs.SetInt("petcatst", 2);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("petcatst", 0) < 1)
        {
            PlayerPrefs.SetInt("petcatst", 1);
            GM.GetComponent<AchievementShow>().achievementCheck(4, 0);
        }
        //Debug.Log("cat" + PlayerPrefs.GetInt("petcatst", 0) + "count" + PlayerPrefs.GetInt("countpetcatst", 0));
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

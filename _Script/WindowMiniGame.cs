using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMiniGame : MonoBehaviour {

    public GameObject GM;

    public GameObject miniGameWindow_obj, miniBackWindow_obj;

    public GameObject minicat_obj;

	// Use this for initialization
	void Start () {
		
	}
	

    public void OpenMiniGame()
    {
        miniGameWindow_obj.SetActive(true);
        PlayerPrefs.SetInt("miniopen",1);
        if(PlayerPrefs.GetInt("windowcatrand", 0) <= 10)
        {
            PlayerPrefs.SetInt("windowcatrand", 999);
            PlayerPrefs.Save();
            minicat_obj.SetActive(true);
        }
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
        minicat_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.SetInt("miniopen", 1);
        PlayerPrefs.SetInt("windowcatrand", 19);
        PlayerPrefs.Save();
    }
}

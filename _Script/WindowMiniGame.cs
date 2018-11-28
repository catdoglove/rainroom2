using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMiniGame : MonoBehaviour {

    public GameObject GM;

    public GameObject miniGameWindow_obj, miniBackWindow_obj;

	// Use this for initialization
	void Start () {
		
	}
	

    public void OpenMiniGame()
    {
        miniGameWindow_obj.SetActive(true);

    }

    public void CloseMiniGame()
    {
        miniGameWindow_obj.SetActive(false);
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.Save();
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
        PlayerPrefs.SetInt("balloon", 10);
        PlayerPrefs.Save();

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpider : MonoBehaviour {

    public GameObject GM;
    public GameObject spider_obj;

    // Use this for initialization
    void Start()
    {

    }

    public void getSpider() {

        GM.GetComponent<MainTime>().randSpider_i = 0;
        spider_obj.transform.position = new Vector3(-11f, spider_obj.transform.position.y, spider_obj.transform.position.z);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }
    
}

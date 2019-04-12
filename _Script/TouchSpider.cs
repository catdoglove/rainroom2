using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpider : MonoBehaviour {

    public GameObject GM;
    public GameObject spider_obj,star_obj;

    // Use this for initialization
    void Start()
    {

    }

    public void getSpider() {

        GM.GetComponent<MainTime>().randSpider_i = 0;

        float xx = spider_obj.transform.position.x;
        float yy = spider_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        spider_obj.transform.position = new Vector3(-11f, spider_obj.transform.position.y, spider_obj.transform.position.z);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();

        //Debug.Log(spider_obj.transform.position.x);

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

    public void getStar()
    {

        GM.GetComponent<MainTime>().randStar_i = 0;

        float xx = star_obj.transform.position.x;
        float yy = star_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        star_obj.transform.position = new Vector3(-11f, star_obj.transform.position.y, star_obj.transform.position.z);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        //테스트할때만 나중에 1로변경할것
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();

        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }

}

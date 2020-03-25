using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchdust : MonoBehaviour {
	public GameObject GM;
    public GameObject audio_obj;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown() {
		string str = gameObject.name;
		if (str.Equals ("먼지1")) {
            float xx = gameObject.transform.position.x;
            float yy = gameObject.transform.position.y;
            PlayerPrefs.SetFloat("watposx", xx);
            PlayerPrefs.SetFloat("watposy", yy);
            GM.GetComponent<SecondRoomTime> ().moveX1 = -14f;
			GM.GetComponent<SecondRoomTime> ().randDust1_i = 0;
            gameObject.transform.position = new Vector3 (-14f, -4f, gameObject.transform.position.z);
		} else {
            float xx = gameObject.transform.position.x;
            float yy = gameObject.transform.position.y;
            PlayerPrefs.SetFloat("watposx", xx);
            PlayerPrefs.SetFloat("watposy", yy);
            GM.GetComponent<SecondRoomTime> ().moveX2 = 14f;
			GM.GetComponent<SecondRoomTime> ().randDust2_i = 0;
			gameObject.transform.position = new Vector3 (14f, -4f, gameObject.transform.position.z);
		}
        audio_obj.GetComponent<SoundEvt>().spiderSound();
        str = PlayerPrefs.GetString ("code", "");
		int coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		int hotRain_i = PlayerPrefs.GetInt (str+"h", 0);
		coldRain_i = coldRain_i + 5;
		hotRain_i = hotRain_i + 3;
		PlayerPrefs.SetInt (str+"c", coldRain_i);
		PlayerPrefs.SetInt (str+"h", hotRain_i);
		PlayerPrefs.Save ();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        
    }
}

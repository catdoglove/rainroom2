using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour {

    public Slider BGM_sld, SE_sld, BGS_sld;
    public AudioSource BGM,BSG,SE;
    float BGMVol_f = 1f;

	// Use this for initialization
	void Start () {
        BGMVol_f = PlayerPrefs.GetFloat("bgm",1f);
        BGM_sld.value = BGMVol_f;
        BGM.volume = BGM_sld.value;
        Debug.Log("1234");
    }
	
	// Update is called once per frame
	void Update () {
        BGMSlider();

    }

    public void BGMSlider()
    {
        BGM.volume = BGM_sld.value;
        BGMVol_f = BGM_sld.value;
        PlayerPrefs.SetFloat("bgm", BGMVol_f);
    }
}

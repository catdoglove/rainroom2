using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour {

    public Slider BGM_sld, SE_sld, BGS_sld;
    public AudioSource BGM,BGS,SE,SE_2;
    float BGMVol_f, BGSVol_f, SEVol_f;

    public GameObject audio_obj; 

	// Use this for initialization
	void Start () {
        

    }
	
    


    // Update is called once per frame
    void Update () {
        if (audio_obj==null) {
            OnLoadSound();
        }
        BGMSlider();
        SESlider();
        BGSSlider();
    }

    public void BGMSlider()
    {
        BGM.volume = BGM_sld.value;
        BGMVol_f = BGM_sld.value;
        PlayerPrefs.SetFloat("bgm", BGMVol_f);
    }
    public void BGSSlider()
    {
        BGS.volume = BGS_sld.value;
        BGSVol_f = BGS_sld.value;
        PlayerPrefs.SetFloat("bgs", BGSVol_f);
    }
    public void SESlider()
    {
        SE.volume = SE_sld.value;
        SEVol_f = SE_sld.value;
        PlayerPrefs.SetFloat("se", SEVol_f);
        SE_2.volume = SE_sld.value;
    }

    public void OnLoadSound()
    {
        audio_obj = GameObject.Find("AudioSound");
        SE = audio_obj.GetComponent<AudioSource>();
        audio_obj = GameObject.Find("BackSound");
        BGS = audio_obj.GetComponent<AudioSource>();
        audio_obj = GameObject.Find("BackMusic");
        BGM = audio_obj.GetComponent<AudioSource>();


        BGMVol_f = PlayerPrefs.GetFloat("bgm", 1f);
        BGM_sld.value = BGMVol_f;
        BGM.volume = BGM_sld.value;

        BGSVol_f = PlayerPrefs.GetFloat("bgs", 1f);
        BGS_sld.value = BGSVol_f;
        BGS.volume = BGS_sld.value;

        SEVol_f = PlayerPrefs.GetFloat("se", 1f);
        SE_sld.value = SEVol_f;
        SE.volume = SE_sld.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandlerRain : MonoBehaviour {
    
    public AudioSource BGS;
    float BGSVol_f;

    public GameObject audio_obj; 

	// Use this for initialization
	void Start () {

        OnLoadSound();
        BGS.time = 5f;
        BGS.Play();
    }
	
    


    // Update is called once per frame
    void Update () {
        OnLoadSound();
    }
    

    public void OnLoadSound()
    {
        BGSVol_f = PlayerPrefs.GetFloat("bgs", 1f);
        BGS.volume = BGSVol_f;
    }
}

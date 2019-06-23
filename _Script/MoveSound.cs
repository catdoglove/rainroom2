using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSound : MonoBehaviour {
    
    public AudioSource BGM, BGS, SE, SE_2;
    float BGMVol_f, BGSVol_f, SEVol_f;

    public GameObject audio_obj;

    // Use this for initialization
    void Start () {
        OnLoadSound();

    }

    public void OnLoadSound()
    {
        SE = audio_obj.GetComponent<AudioSource>();
        
        SEVol_f = PlayerPrefs.GetFloat("se", 1f);
        SE.volume = SEVol_f;
    }
}

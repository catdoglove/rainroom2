using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEvt : MonoBehaviour {

    public AudioSource se_book, se_window,se_cat;
    public AudioClip sp_book, sp_window,sp_cat;

	// Use this for initialization
	void Start () {
		
	}

    public void bookSound()
    {
        se_book = gameObject.GetComponent<AudioSource>();
        se_book.clip = sp_book;
        se_book.loop = false;
        se_book.Play();
    }

    public void windowSound()
    {
        se_window = gameObject.GetComponent<AudioSource>();
        se_window.clip = sp_window;
        se_window.loop = false;
        se_window.Play();
    }

    public void catSound()
    {
        se_cat = gameObject.GetComponent<AudioSource>();
        se_cat.clip = sp_cat;
        se_cat.loop = false;
        se_cat.Play();
    }


}

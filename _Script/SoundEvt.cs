using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEvt : MonoBehaviour {

    public AudioSource se_book, se_window, se_cat, se_cook, se_food, se_light, se_button, se_TV, se_sticker, se_bed, se_star, se_switch;
    public AudioClip sp_book, sp_window, sp_cat, sp_cook, sp_food, sp_light, sp_button, sp_TV, sp_sticker, sp_bed, sp_star, sp_switch;
    
    public AudioSource BGM, BGS;
    float BGMVol_f, BGSVol_f;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("titlesets", 0)==1)
        {
            BGSVol_f = PlayerPrefs.GetFloat("bgs", 1f);
            BGS.volume = BGSVol_f;
            PlayerPrefs.SetInt("titlesets", 0);
        }
    }
   
    //책
    public void bookSound()
    {
        se_book = gameObject.GetComponent<AudioSource>();
        se_book.clip = sp_book;
        se_book.loop = false;
        se_book.Play();
    }

    //창문
    public void windowSound()
    {
        se_window = gameObject.GetComponent<AudioSource>();
        se_window.clip = sp_window;
        se_window.loop = false;
        se_window.Play();
    }
    //고양이
    public void catSound()
    {
        se_cat = gameObject.GetComponent<AudioSource>();
        se_cat.clip = sp_cat;
        se_cat.loop = false;
        se_cat.Play();
    }
    //가스렌지
    public void cookSound()
    {
        se_cook = gameObject.GetComponent<AudioSource>();
        se_cook.clip = sp_cook;
        se_cook.loop = false;
        se_cook.Play();
    }
    //음식
    public void foodSound()
    {
        se_food = gameObject.GetComponent<AudioSource>();
        se_food.clip = sp_food;
        se_food.loop = false;
        se_food.Play();
    }
    //전구
    public void lightSound()
    {
        se_light = gameObject.GetComponent<AudioSource>();
        se_light.clip = sp_light;
        se_light.loop = false;
        se_light.Play();
    }
    //버튼
    public void buttonSound()
    {
        se_button = gameObject.GetComponent<AudioSource>();
        se_button.clip = sp_button;
        se_button.loop = false;
        se_button.Play();
    }
    //티비
    public void TVSound()
    {
        se_TV = gameObject.GetComponent<AudioSource>();
        se_TV.clip = sp_TV;
        se_TV.loop = false;
        se_TV.Play();
    }
    //스티커
    public void stickerSound()
    {
        se_sticker = gameObject.GetComponent<AudioSource>();
        se_sticker.clip = sp_sticker;
        se_sticker.loop = false;
        se_sticker.Play();
    }
    //침대
    public void bedSound()
    {
        se_bed = gameObject.GetComponent<AudioSource>();
        se_bed.clip = sp_bed;
        se_bed.loop = false;
        se_bed.Play();
    }
    //별
    public void starSound()
    {
        se_star = gameObject.GetComponent<AudioSource>();
        se_star.clip = sp_star;
        se_star.loop = false;
        se_star.Play();
    }
    //스위치
    public void switchSound()
    {
        se_switch = gameObject.GetComponent<AudioSource>();
        se_switch.clip = sp_switch;
        se_switch.loop = false;
        se_switch.Play();
    }
}

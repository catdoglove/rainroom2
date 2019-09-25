using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSticker : MonoBehaviour
{

    public string[] sticker_str;
    public GameObject[] sticker_obj;
    public GameObject[] newStickerB_obj, newStickerS_obj, newStickerG_obj;
    
    public Sprite[] stickerS_spr, stickerG_spr;
    public string name_str;

    public bool check;
    Vector2 pos;
    public Vector2 wldObjectPos;

    int posX, posY;
    //float speed = 0.04f;

    public GameObject FGM, GM2,GM;
    public GameObject frame_obj;
    public Sprite frameOpen_spr, frameEnd_spr;

    //소리
    public GameObject audio_obj;

    // Use this for initialization
    void Start()
    {
        
        if (PlayerPrefs.GetInt("place", 0) == 1)
        {
            GM2 = GameObject.FindGameObjectWithTag("GM2");
            GM = GM2;
        }
        else if (PlayerPrefs.GetInt("place", 0) == 0)
        {
            FGM = GameObject.FindGameObjectWithTag("firstroomGM");
            GM = FGM;
        }
        //Debug.Log("talkstb" + PlayerPrefs.GetInt("talkstb", 0));
        //Debug.Log("talkst" + PlayerPrefs.GetInt("talkst", 0));
        //PlayerPrefs.SetInt("gooutst", 3);
        //PlayerPrefs.SetInt("firstcookst", 3);
        //PlayerPrefs.SetInt("airplanest", 3);
        //PlayerPrefs.SetInt("petcatst", 3);
        //PlayerPrefs.SetInt("boxst", 3);
        //PlayerPrefs.SetInt("talkst",3);
        //PlayerPrefs.SetInt("allbook", 1);
        //PlayerPrefs.DeleteKey("talkstb");
        //PlayerPrefs.DeleteKey("talksts");
        //PlayerPrefs.DeleteKey("talkstg");
        //PlayerPrefs.DeleteKey("talkstplus");
        //PlayerPrefs.DeleteKey("petcatstb");
        //PlayerPrefs.DeleteKey("petcatsts");
        //PlayerPrefs.DeleteKey("petcatstg");
        //PlayerPrefs.DeleteKey("petcatstplus");

        if (gameObject.name.Substring(1, 1) == "a")
        {
            showSticker();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {//카드를 드래그 하고있을때
            Vector2 mouseDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            wldObjectPos = Camera.main.ScreenToWorldPoint(mouseDragPos);
            transform.position = Vector2.MoveTowards(transform.position, wldObjectPos, 0.9f);
        }
        else
        {//EndOfIf
            
        }
    }


    void OnMouseDown()
    {
        if (check)
        {

        }
        else
        {

            audio_obj = GameObject.Find("AudioSound");

            audio_obj.GetComponent<SoundEvt>().stickerSound();
        }
        check = true;

    }//EndOfOnMouseDown

    public void OnMouseUp()
    {
        
            audio_obj = GameObject.Find("AudioSound");
        
        audio_obj.GetComponent<SoundEvt>().stickerSound();
        if (wldObjectPos.x > -2.5 && wldObjectPos.x < 2.5)
        {
            if (wldObjectPos.y < 3.64 && wldObjectPos.y > -3.97)
            {
               
                    if (PlayerPrefs.GetInt("place", 0) == 1)
                    {
                        GM2 = GameObject.FindGameObjectWithTag("GM2");
                        GM = GM2;
                    }
                    else if (PlayerPrefs.GetInt("place", 0) == 0)
                    {
                        FGM = GameObject.FindGameObjectWithTag("firstroomGM");
                        GM = FGM;
                    }
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                    GM= GameObject.FindGameObjectWithTag("parkGM");
                }
                if (PlayerPrefs.GetInt("outtrip", 0) == 2)
                {
                    GM = GameObject.FindGameObjectWithTag("cityGM");
                }
                gameObject.SetActive(false);
                name_str = this.gameObject.name;
                if (name_str.Length == 3)
                {
                    allStickerCheck();
                }
                else
                {
                    int name_i = int.Parse(name_str.Substring(1, 1));
                    sticker_obj[name_i].SetActive(true);
                    int plus = PlayerPrefs.GetInt(sticker_str[name_i] + "plus", 0); if (name_i == 7) { name_i = name_i + 2; }
                    GM.GetComponent<FirstRoomSticker>().frame_i = (name_i * 3) + plus;
                    GM.GetComponent<FirstRoomSticker>().ShowFrame(); if (name_i == 9) { name_i = name_i - 2; }
                    if (plus == 1)
                    {
                        sticker_obj[name_i].GetComponent<SpriteRenderer>().sprite = stickerS_spr[name_i];
                    }
                    if (plus == 2)
                    {
                        sticker_obj[name_i].GetComponent<SpriteRenderer>().sprite = stickerG_spr[name_i];
                    }
                    PlayerPrefs.SetInt(sticker_str[name_i] + "plus", plus + 1);
                    PlayerPrefs.SetInt(sticker_str[name_i] + name_str.Substring(0, 1), 2);
                    PlayerPrefs.SetInt("frameopen",1);

                    int v= PlayerPrefs.GetInt("allacvdone", 0);
                    v++;
                    PlayerPrefs.SetInt("allacvdone", v);

                    if (PlayerPrefs.GetInt("place", 0) == 0)
                    {
                        GM.GetComponent<FirstRoomSticker>().frameImg_obj.GetComponent<Image>().sprite = frameOpen_spr;
                        if (v >= 30)
                        {
                            GM.GetComponent<FirstRoomSticker>().frameImg_obj.GetComponent<Image>().sprite = frameEnd_spr;
                        }

                        GM.GetComponent<FirstRoomSticker>().frameImg_obj.GetComponent<Button>().interactable = true;
                    }
                    PlayerPrefs.Save();
                }

            }
        }
        check = false;
    }

    //스티커이미지를자동으로불러와주기
    //뉴스트커그냥스티커들오브젝트에넣기


    public void showSticker()
    {
        
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.GetInt(sticker_str[i], 0) >= 1)
            {
                if (PlayerPrefs.GetInt(sticker_str[i] + "b", 0) == 0)
                {
                    PlayerPrefs.SetInt(sticker_str[i] + "b",1);
                }
                if (PlayerPrefs.GetInt(sticker_str[i], 0) >= 2)
                {
                    if (PlayerPrefs.GetInt(sticker_str[i] + "s", 0) == 0)
                    {
                        PlayerPrefs.SetInt(sticker_str[i] + "s", 1);
                    }
                    if (PlayerPrefs.GetInt(sticker_str[i], 0) == 3)
                    {
                        if (PlayerPrefs.GetInt(sticker_str[i] + "g", 0) == 0)
                        {
                            PlayerPrefs.SetInt(sticker_str[i] + "g", 1);
                        }
                    }
                }
            }
            PlayerPrefs.Save();

            if (PlayerPrefs.GetInt(sticker_str[i] + "b", 0) == 1)
                {
                    newStickerB_obj[i].SetActive(true);
                }
                if (PlayerPrefs.GetInt(sticker_str[i] + "s", 0) == 1)
                {
                    newStickerS_obj[i].SetActive(true);
                }
                if (PlayerPrefs.GetInt(sticker_str[i] + "g", 0) == 1)
                {
                    newStickerG_obj[i].SetActive(true);
                }
            
            if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) >= 1)
            {
                sticker_obj[i].SetActive(true);
                if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) == 2)
                {
                    sticker_obj[i].GetComponent<SpriteRenderer>().sprite = stickerS_spr[i];
                }
                else if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) == 3)
                {
                    sticker_obj[i].GetComponent<SpriteRenderer>().sprite = stickerG_spr[i];
                }
            }
        }
        allsticker();
    }

    void allsticker()
    {
        for (int i = 20; i < 26; i++)
        {
            if (PlayerPrefs.GetInt(sticker_str[i], 0) >= 1)
            {
                if (PlayerPrefs.GetInt(sticker_str[i] + "b", 0) == 0)
                {
                    PlayerPrefs.SetInt(sticker_str[i] + "b", 1);
                }
            }
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt(sticker_str[i] + "b", 0) == 1)
            {
                newStickerB_obj[i].SetActive(true);
            }
            if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) >= 1)
            {
                sticker_obj[i].SetActive(true);
            }
        }//endoffor
    }

    void allStickerCheck()
    {
        if (name_str.Length == 3)
        {
            int ii = int.Parse(name_str.Substring(1, 2));
            if (ii >= 20)
            {
                GM.GetComponent<FirstRoomSticker>().frame_i = ii + 1;
                GM.GetComponent<FirstRoomSticker>().ShowFrame();

                    sticker_obj[ii].SetActive(true);
                PlayerPrefs.SetInt(sticker_str[ii] + "plus", 1);
                PlayerPrefs.SetInt(sticker_str[ii] + name_str.Substring(0, 1), 2);

                int v = PlayerPrefs.GetInt("allacvdone", 0);
                v++;
                PlayerPrefs.SetInt("allacvdone", v);
                if (PlayerPrefs.GetInt("place", 0) == 0)
                {
                    if (v >= 30)
                    {
                        GM.GetComponent<FirstRoomSticker>().frameImg_obj.GetComponent<Image>().sprite = frameEnd_spr;
                    }
                }
                PlayerPrefs.Save();
            }
        }
    }
}

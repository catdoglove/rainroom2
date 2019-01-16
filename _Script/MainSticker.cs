using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSticker : MonoBehaviour
{

    public string[] sticker_str;
    public GameObject[] sticker_obj;
    public GameObject[] newSticker_obj;


    public bool check;
    Vector2 pos;
    public Vector2 wldObjectPos;

    int posX, posY;
    float speed = 0.04f;



    // Use this for initialization
    void Start()
    {

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
        check = true;

    }//EndOfOnMouseDown

    public void OnMouseUp()
    {
        if (wldObjectPos.x > -4 && wldObjectPos.x < 4.4)
        {
            if (wldObjectPos.y < 3.44 && wldObjectPos.y > -2.77)
            {
                gameObject.SetActive(false);
                string name_str = this.gameObject.name;

                //int name_i = int.Parse(name_str.Substring(1, 1));
                int name_i = 0;
                sticker_obj[name_i].SetActive(true);
                //int plus = PlayerPrefs.GetInt(sticker_str[name_i] + "plus", 0);
                //PlayerPrefs.SetInt(sticker_str[name_i]+"plus", plus+1);
                //PlayerPrefs.SetInt(sticker_str[name_i]+name_str.Substring(0, 1), 2);
                //PlayerPrefs.Save();
            }
        }
        check = false;
    }

    //스티커이미지를자동으로불러와주기
    //뉴스트커그냥스티커들오브젝트에넣기


    public void showSticker()
    {
        for (int i = 0; i < 10; i++)
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
                    newSticker_obj[i].SetActive(true);
                }
                if (PlayerPrefs.GetInt(sticker_str[i] + "s", 0) == 1)
                {
                    newSticker_obj[i].SetActive(true);
                }
                if (PlayerPrefs.GetInt(sticker_str[i] + "g", 0) == 1)
                {
                    newSticker_obj[i].SetActive(true);
                }
            
            if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) >= 1)
            {
                sticker_obj[i].SetActive(true);
                if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) == 2)
                {

                }
                else if (PlayerPrefs.GetInt(sticker_str[i] + "plus", 0) == 3)
                {

                }


            }

        }
    }
}

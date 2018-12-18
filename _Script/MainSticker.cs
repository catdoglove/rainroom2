using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSticker : MonoBehaviour {

    public string[] sticker_str;
    public GameObject[] sticker_obj;
    public GameObject[] newSticker_obj;


    public bool check;
    Vector2 pos;
    public Vector2 wldObjectPos;

    int posX, posY;
    float speed = 0.04f;

    

    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {
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
        if (wldObjectPos.x > -4 && wldObjectPos.x < 4.5)
        {
            if (wldObjectPos.y < 3.44 && wldObjectPos.y > -2.77)
            {
                gameObject.SetActive(false);
                int name_i = int.Parse(gameObject.name);
                sticker_obj[name_i].SetActive(true);
                //PlayerPrefs.SetInt(sticker_str[name_i], 2);
            }
        }
        check = false;
    }


    public void showSticker()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetInt(sticker_str[i], 0) == 1)
            {
                newSticker_obj[i].SetActive(true);
            }
            if (PlayerPrefs.GetInt(sticker_str[i], 0) == 2)
            {
                sticker_obj[i].SetActive(true);
            }
        }

    }
}

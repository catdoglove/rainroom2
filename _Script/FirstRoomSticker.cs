using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstRoomSticker : MonoBehaviour {

    public GameObject frame_obj, frameImg_obj;
    //public GameObject 
    public string[] sticker_str;
    public GameObject[] frameBtn_obj;
    public GameObject[] frameABtn_obj;
    public Sprite[] frame_spr;
    public int frame_i;
    int show_i;

    public int[] showNum_i;

    public GameObject frameShow_obj;
    public GameObject GM;

	// Use this for initialization
	void Start () {
        StartFrame();
        frame_spr = Resources.LoadAll<Sprite>("UI/Sticker");
        //LoadFrame();
    }

    public void OpenFrame()
    {
        frame_obj.SetActive(true);
        LoadFrame();
    }

    public void CloseFrame()
    {
        frame_obj.SetActive(false);
    }

    public void ShowFrame()
    {
        if (frame_i == 25)
        {
            GM.GetComponent<EndingBox>().PlayEndLast();
        }
        else
        {
            frameShow_obj.SetActive(true);
            switch (frame_i)
            {
                case 0:
                    show_i = 56;
                    break;
                case 1:
                    show_i = 58;
                    break;
                case 2:
                    show_i = 60;
                    break;
                case 3:
                    show_i = 10;
                    break;
                case 4:
                    show_i = 12;
                    break;
                case 5:
                    show_i = 14;
                    break;
                case 6:
                    show_i = 33;
                    break;
                case 7:
                    show_i = 35;
                    break;
                case 8:
                    show_i = 37;
                    break;
                case 9:
                    show_i = 0;
                    break;
                case 10:
                    show_i = 2;
                    break;
                case 11:
                    show_i = 4;
                    break;
                case 12:
                    show_i = 16;
                    break;
                case 13:
                    show_i = 18;
                    break;
                case 14:
                    show_i = 20;
                    break;
                case 15:
                    show_i = 49;
                    break;
                case 16:
                    show_i = 51;
                    break;
                case 17:
                    show_i = 53;
                    break;
                case 18:
                    show_i = 23;
                    break;
                case 19:
                    show_i = 25;
                    break;
                case 20:
                    show_i = 28;
                    break;
                case 21:
                    show_i = 63;
                    break;
                case 22:
                    show_i = 7;
                    break;
                case 23:
                    show_i = 31;
                    break;
                case 24:
                    show_i = 40;
                    break;
                case 25:
                    break;
                case 26:
                    show_i = 66;
                    break;
                case 27:
                    show_i = 42;
                    break;
                case 28:
                    show_i = 44;
                    break;
                case 29:
                    show_i = 46;
                    break;
            }
            frameShow_obj.GetComponent<Image>().sprite = frame_spr[show_i];
        }
    }

    public void frameNumCheck()
    {
        show_i++;
        int c=0;
        for (int i = 0; i < 29; i++)
        {
            if(show_i == showNum_i[i])
            {
                frameShow_obj.SetActive(false);
                c = 1;
            }
        }
        if (c == 1)
        {
        }
        else
        {
            frameShow_obj.GetComponent<Image>().sprite = frame_spr[show_i];
        }
    }

    void LoadFrame()
    {
        int v = 0;
        for (int i = 0; i < 8; i++)
        {
            int c = PlayerPrefs.GetInt(sticker_str[i], 0);
            if (c >= 1)
            {
                int sum = i * 3;
                frameBtn_obj[sum].SetActive(true);
                v++;
                if (c >= 2)
                {
                    sum = (i * 3) + 1;
                    frameBtn_obj[sum].SetActive(true);
                    v++;
                }
                if (c >= 3)
                {
                    sum = (i * 3) +2;
                    frameBtn_obj[sum].SetActive(true);
                    v++;
                }
            }
        }
        for (int i = 20; i < 26; i++)
        {
            int c = PlayerPrefs.GetInt(sticker_str[i], 0);
            if (c >= 1)
            {
                int sum = i + c - 20 - 1;
                frameABtn_obj[sum].SetActive(true);
                v++;
            }
        }
            PlayerPrefs.SetInt("allacvdone",v);
    }

    /// <summary>
    /// 키값
    /// </summary>
    void StartFrame()
    {
        sticker_str[0] = "talkstplus";
        sticker_str[1] = "boxstplus";
        sticker_str[2] = "downstplus";
        sticker_str[3] = "airplanestplus";
        sticker_str[4] = "petcatstplus";
        sticker_str[5] = "insleepstplus";
        sticker_str[6] = "firstcookstplus";
        sticker_str[7] = "gooutstplus";
        sticker_str[20] = "allwindowplus";
        sticker_str[21] = "allbookplus";
        sticker_str[22] = "allfoodplus";
        sticker_str[23] = "allingredientplus";
        sticker_str[24] = "_thank_you_for_playplus";
        sticker_str[25] = "allflowerplus";
    }

#region
    public void frameNum0()
    {
        frame_i = 0;
    }
    public void frameNum1()
    {
        frame_i = 1;
    }
    public void frameNum2()
    {
        frame_i = 2;
    }
    public void frameNum3()
    {
        frame_i = 3;
    }
    public void frameNum4()
    {
        frame_i = 4;
    }
    public void frameNum5()
    {
        frame_i = 5;
    }
    public void frameNum6()
    {
        frame_i = 6;
    }
    public void frameNum7()
    {
        frame_i = 7;
    }
    public void frameNum8()
    {
        frame_i = 8;
    }
    public void frameNum9()
    {
        frame_i = 9;
    }
    public void frameNum10()
    {
        frame_i = 10;
    }
    public void frameNum11()
    {
        frame_i =11;
    }
    public void frameNum12()
    {
        frame_i = 12;
    }
    public void frameNum13()
    {
        frame_i = 13;
    }
    public void frameNum14()
    {
        frame_i = 14;
    }
    public void frameNum15()
    {
        frame_i = 15;
    }
    public void frameNum16()
    {
        frame_i = 16;
    }
    public void frameNum17()
    {
        frame_i = 17;
    }
    public void frameNum18()
    {
        frame_i = 18;
    }
    public void frameNum19()
    {
        frame_i = 19;
    }
    public void frameNum20()
    {
        frame_i =20;
    }
    public void frameNum21()
    {
        frame_i = 21;
    }
    public void frameNum22()
    {
        frame_i = 22;
    }
    public void frameNum23()
    {
        frame_i = 23;
    }
    public void frameNum24()
    {
        frame_i = 24;
    }
    public void frameNum25()
    {
        frame_i = 25;
    }
    public void frameNum26()
    {
        frame_i = 26;
    }
    public void frameNum27()
    {
        frame_i = 27;
    }
    public void frameNum28()
    {
        frame_i = 28;
    }
    public void frameNum29()
    {
        frame_i = 29;
    }
#endregion
}

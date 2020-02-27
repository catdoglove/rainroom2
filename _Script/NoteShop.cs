using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteShop : MonoBehaviour {

    public GameObject noteShopWindow_obj, noteShopBuyYN_obj, noteShopHelp_obj;
    public GameObject noteShopSign_obj, noteToast_obj;
    Color color;
    string str,note_str;

    public GameObject noteBtn_obj, noteImg_obj;
    public Sprite[] note_spr;

    public int priceShop_i;

    public int noteShopNum_i;

    public Text name_txt,spade_txt;
    // Use this for initialization
    void Start () {

        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
    }
	
    //문방구 열기
    public void ActNoteShop()
    {
        //임시테스트
        GetSpade();
        //노트 몇권가지고 있나?
        CheckNoteNum();
        if (noteShopWindow_obj.activeSelf == true)
        {
            noteShopWindow_obj.SetActive(false);
        }
        else
        {
            noteShopWindow_obj.SetActive(true);
        }
    }
    //노트
    public void BuyNote()
    {
        noteShopNum_i = 0;
        priceShop_i = 10;
        name_txt.text = "공책";
        note_str = "havenotenum";
        noteShopBuyYN_obj.SetActive(true);
    }
    //지우개
    public void BuyEraser()
    {
        noteShopNum_i = 1;
        priceShop_i = 2;
        name_txt.text = "지우개";
        note_str = "erasernum";
        noteShopBuyYN_obj.SetActive(true);
    }

    //연필
    public void BuyPencil()
    {
        noteShopNum_i = 2;
        priceShop_i = 2;
        name_txt.text = "필기구";
        note_str = "pencilnum";
        noteShopBuyYN_obj.SetActive(true);
    }


    //도움말 열기
    public void ShowHelp()
    {
        noteShopHelp_obj.SetActive(true);
    }

    //도움말 닫기
    public void CloseHelp()
    {
        noteShopHelp_obj.SetActive(false);
    }

    //산다
    public void NoteShopBuyY()
    {
        int spade = PlayerPrefs.GetInt(str + "sd", 0);
        //priceShop_i = 0;
        if (spade >= priceShop_i)
        {
            spade = spade - priceShop_i;
            PlayerPrefs.SetInt(str + "sd", spade);
            int k = PlayerPrefs.GetInt(note_str, 0);
            k++;
            PlayerPrefs.SetInt(note_str, k);
            PlayerPrefs.Save();

        }
        else
        {
            StopCoroutine("needSpadeFadeOut");
            StartCoroutine("needSpadeFadeOut");
        }
        // 공책변경
        CheckNoteNum();
        noteShopBuyYN_obj.SetActive(false);
    }

    void CheckNoteNum()
    {
        //공책 이미지 변경 및 버튼 비활성화
        if (PlayerPrefs.GetInt("havenotenum", 0) >= 3)
        {
            noteBtn_obj.SetActive(false);
            noteImg_obj.SetActive(true);
            noteBtn_obj.GetComponent<Image>().sprite = note_spr[3];
        }
        else
        {
            noteBtn_obj.GetComponent<Image>().sprite = note_spr[PlayerPrefs.GetInt("havenotenum", 0)];
        }
        spade_txt.text = "" + PlayerPrefs.GetInt(str + "sd", 0);
    }
    //안산다
    public void NoteShopBuyN()
    {
        noteShopBuyYN_obj.SetActive(false);
    }


    //스페이드없다
    IEnumerator needSpadeFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        noteToast_obj.GetComponent<Image>().color = color;
        noteToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            noteToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        noteToast_obj.SetActive(false);
    }


    //문방구로 오면 스페이드 얻기
    public void GetSpade()
    {
        if (PlayerPrefs.GetInt("outspade", 0) == 1)
        {

            PlayerPrefs.SetInt("outspade", -1);
            GetSpadeNum();
        }
        else if (PlayerPrefs.GetInt("outspade", 0) == 0)
        {
            PlayerPrefs.SetInt("outspade", -3);
            GetSpadeNum();
            GetSpadeNum();
        }
        else if (PlayerPrefs.GetInt("outspade", 0) == -2)
        {
            PlayerPrefs.SetInt("outspade", -3);
            GetSpadeNum();
        }
    }

    void GetSpadeNum()
    {
        int spade = PlayerPrefs.GetInt(str + "sd", 0);
        int sh = 0;
        sh = Random.Range(0, 100);
        if (sh > 4)
        {
            //1~2개획득
            sh = Random.Range(0, 10);
            if (sh >= 7)
            {
                //2개
                spade = spade + 2;
            }
            else
            {
                //1개
                spade = spade + 1;
            }
        }
        else
        {
            spade = spade + 3;
        }
        if (spade >= 999)
        {
            spade = 999;
        }
        PlayerPrefs.SetInt(str + "sd", spade);
        PlayerPrefs.Save();
    }
}

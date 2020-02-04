using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteStoreFunction : MonoBehaviour {

    public int noteBookNum_i, notePageNum_i;
    public GameObject noteWindow_obj, noteWriteYN_obj, noteDeletYN_obj, noteWood_obj, noteWindowImg_obj;
    public Sprite noteImgPage_spr, noteImgCover_spr;
    public Sprite[] noteBtn_spr;
    public GameObject noteToast_obj,noteRBtn_obj, noteLBtn_obj, noteWriteToastBtn_obj, noteRBtnImg_obj;
    public GameObject showPage_obj, showCover_obj, noteWriteOKBtn_obj;
    Color color;
    string str;

    public InputField inputfieldNote;

    public Text page_txt,writePage_txt,input_txt;
    public GameObject input_obj;
    // Use this for initialization
    void Start () {

        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //책을 구매해서 선반이 나와있나?
    public void NoteWoodCheck()
    {
        noteWood_obj.SetActive(true);
    }

    //열기닫기 노트창
    public void ActNote()
    {
        CheckPencle();
        if (noteWindow_obj.activeSelf == true)
        {
            noteWindow_obj.SetActive(false);
        }
        else
        {
            noteWindow_obj.SetActive(true);
        }

    }

    public void checkNoteLine()
    {
        string ipstr = input_txt.text;

        int k = input_txt.cachedTextGenerator.lineCount;
        int kn = ipstr.Length;
        kn--;
        if (k > 10)
        {
            StartCoroutine("noteLine");
        }
    }

    //연필을 가지고 있나?
    void CheckPencle()
    {
        if (PlayerPrefs.GetInt("penclenum", 0) >= 1)
        {
            noteWriteToastBtn_obj.SetActive(false);
        }
        else
        {
            noteWriteToastBtn_obj.SetActive(true);
        }
    }

    //토스트버튼
    public void WritePage()
    {
        if(PlayerPrefs.GetInt("penclenum", 0) >= 1)
        {

        }
        else
        {
            StartCoroutine("toastPencleImgFadeOut");
        }
    }
    
    //쓰여진 페이지
    void WritedPage()
    {
        string notestr= PlayerPrefs.GetString("notewrite1p"+ notePageNum_i, "");
        int note_i = PlayerPrefs.GetInt("checkwrite1p" + notePageNum_i, 0);
        if (note_i == 1)
        {
            writePage_txt.text = notestr;
            input_obj.SetActive(false);
        }
        else
        {
            writePage_txt.text = "";
            input_obj.SetActive(true);
        }
    }
    //저장하기 버튼 띄우기
    public void showOKBtn()
    {
        noteWriteOKBtn_obj.SetActive(true);
    }

    //저장하기 버튼 지우기
    public void CloseOKBtn()
    {
        noteWriteOKBtn_obj.SetActive(true);
    }

    //저장할까요?
    public void WriteYN()
    {
        noteWriteYN_obj.SetActive(true);
    }

    //쓰기저장Y
    public void saveWriteY()
    {
        PlayerPrefs.SetString("notewrite1p" + notePageNum_i, input_txt.text);
        PlayerPrefs.SetInt("checkwrite1p" + notePageNum_i, 1);
        noteWriteYN_obj.SetActive(false);
    }

    //쓰기저장N
    public void saveWriteN()
    {
        noteWriteYN_obj.SetActive(false);
    }

    //커버열기
    public void OpenCover()
    {
        noteWindowImg_obj.GetComponent<Image>().sprite = noteImgPage_spr;
        showPage_obj.SetActive(true);
        showCover_obj.SetActive(false);
        notePageNum_i = 1;
        noteLBtn_obj.SetActive(true);
        WritedPage();
    }

    //다음장넘기기
    public void NextPage()
    {
        if (notePageNum_i == 0)
        {
            noteWindowImg_obj.GetComponent<Image>().sprite = noteImgPage_spr;
            showPage_obj.SetActive(true);
            showCover_obj.SetActive(false);
            notePageNum_i++;
            noteLBtn_obj.SetActive(true);

            noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        else if (notePageNum_i >= 29)
        {
            noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[0];
            notePageNum_i = 30;
        }
        else
        {
            noteWindowImg_obj.GetComponent<Image>().sprite = noteImgPage_spr;
            showPage_obj.SetActive(true);
            showCover_obj.SetActive(false);
            notePageNum_i++;
            noteLBtn_obj.SetActive(true);
            noteRBtn_obj.SetActive(true);
            noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        page_txt.text = "" + notePageNum_i + "/30";
        WritedPage();
    }

    //뒷장넘기기
    public void BackPage()
    {
        noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        if (notePageNum_i == 1)
        {
            noteWindowImg_obj.GetComponent<Image>().sprite = noteImgCover_spr;
            showPage_obj.SetActive(false);
            showCover_obj.SetActive(true);
            notePageNum_i--;
            noteLBtn_obj.SetActive(false);
            noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        else if (notePageNum_i <= 1)
        {
            noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[0];
            notePageNum_i = 0;
        }
        else
        {
            notePageNum_i--;
            noteRBtn_obj.SetActive(true);
            noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        page_txt.text = "" + notePageNum_i + "/30";
        WritedPage();
    }


    //연필없다
    IEnumerator toastPencleImgFadeOut()
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

    //줄정리
    IEnumerator noteLine()
    {
        int k = input_txt.cachedTextGenerator.lineCount;
        while (k > 10)
        {
            string ipstr = input_txt.text;
            k = input_txt.cachedTextGenerator.lineCount;
            int kn = ipstr.Length;
            ipstr = input_txt.text;
            inputfieldNote.Select();
            kn = ipstr.Length;
            kn--;
            inputfieldNote.text = "" + ipstr.Substring(0, kn);
            k = input_txt.cachedTextGenerator.lineCount;

            yield return new WaitForSeconds(0.2f);
        }
    }

    //나갔다 오면 스페이드 얻기
    public void GetSpade()
    {
        int spade = PlayerPrefs.GetInt(str + "sd", 0);
        int sh = 0;
        if (PlayerPrefs.GetInt("outspade", 0) == 1)
        {
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
                    spade = spade + 2;
                }
            }
            else
            {
                //3~5개획득
                sh = Random.Range(0, 11);
                if (sh == 1)
                {
                    //5개
                    spade = spade + 5;
                }
                else
                {
                    //3,4개
                    spade = spade + Random.Range(3, 5);
                }
            }
            PlayerPrefs.SetInt(str + "sd", spade);
        }//endofif
    }

}

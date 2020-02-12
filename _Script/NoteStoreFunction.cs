using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteStoreFunction : MonoBehaviour {

    public int noteBookNum_i, notePageNum_i;
    public GameObject noteWindow_obj, noteWriteYN_obj, noteDeletYN_obj, noteWood_obj, noteWindowImg_obj,noteC_obj, eraser_obj, pencil_obj, noteOn_obj;
    public Sprite noteImgPage_spr, noteImgCover_spr;
    public Sprite[] noteBtn_spr, noteCImg_spr,noteImg_spr;
    public GameObject noteToast_obj,noteRBtn_obj, noteLBtn_obj, noteWriteToastBtn_obj, noteRBtnImg_obj;
    public GameObject showPage_obj, showCover_obj, noteWriteOKBtn_obj, startWriteBtn_obj, titleWriteOKBtn_obj, startBtn_obj;
    Color color;
    
    string str;

    public InputField inputfieldNote, inputfieldTitle;

    public Text page_txt, writePage_txt, input_txt, lineTest_txt, eraseNum_txt, pencilNum_txt, need_txt, title_txt, titleInput_txt;
    public GameObject input_obj, writePage_obj,page_obj,title_obj, titleTxt_obj;
    public GameObject noteHelp_obj;

    //노트권선택
    public GameObject[] noteBooks_obj;
    public Sprite[] noteColor_spr, notePageColor_spr;

    public Text charNum_txt;

    // Use this for initialization
    void Start () {
        
        color = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
        NoteWoodCheck();
    }
	

    //책을 구매해서 선반이 나와있나?
    public void NoteWoodCheck()
    {
        if (PlayerPrefs.GetInt("havenotenum", 0) >= 1)
        {
            noteOn_obj.GetComponent<Image>().sprite = noteImg_spr[PlayerPrefs.GetInt("havenotenum", 0)];
            if (PlayerPrefs.GetInt("putnote", 1) == 1)
            {
                noteWood_obj.SetActive(true);
            }
        }
    }

    //열기닫기 노트창
    public void ActNote()
    {
        StopCoroutine("moveC");
        StartCoroutine("moveC");
        //연필체크
        CheckPencle();
        //열때 초기화
        noteBookNum_i = 1;
        noteWindowImg_obj.SetActive(false);
        CheckHaveNote();
        //페이지수
        notePageNum_i = 0;
        //입력필드텍스트
        inputfieldNote.text = "";
        lineTest_txt.text = "";
        //커버이미지
        showCover();
        //페이지 닫기
        showPage_obj.SetActive(false);
        //커버 열기
        showCover_obj.SetActive(true);
        //페이지 넘기기 버튼
        noteLBtn_obj.SetActive(false);
        //noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        //제목 저장불러오기
        WritedTitle();
        //노트키기
        if (noteWindow_obj.activeSelf == true)
        {
            noteWindow_obj.SetActive(false);
            StopCoroutine("moveC");
        }
        else
        {
            noteWindow_obj.SetActive(true);

            //연필 지우개 이미지 켜기
            showpen();
        }

    }

    void showpen()
    {
        //연필 지우개 이미지 켜기
        if (PlayerPrefs.GetInt("erasernum", 0) >= 1)
        {
            eraser_obj.SetActive(true);
        }
        else
        {
            eraser_obj.SetActive(false);
        }

        if (PlayerPrefs.GetInt("pencilnum", 0) >= 1)
        {
            pencil_obj.SetActive(true);
        }
        else
        {
            pencil_obj.SetActive(false);
        }
    }

    //공책을 몇개 샀나?
    void CheckHaveNote()
    {
        if (PlayerPrefs.GetInt("havenotenum", 0) >= 1)
        {
            noteBooks_obj[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("havenotenum", 0) >= 2)
        {
            noteBooks_obj[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("havenotenum", 0) >= 3)
        {
            noteBooks_obj[3].SetActive(true);
        }
    }

    //줄수체크
    public void checkNoteLine()
    {
        string ipstr = input_txt.text;
        int k = input_txt.cachedTextGenerator.lineCount;
        int kn = ipstr.Length;
        kn--;
        if (k > 10)
        {

            StopCoroutine("noteLine");
            StartCoroutine("noteLine");
        }
        else
        {
            StopCoroutine("noteLine");
        }
    }

    //연필을 가지고 있나?
    void CheckPencle()
    {
        if (PlayerPrefs.GetInt("pencilnum", 0) >= 1)
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
        if(PlayerPrefs.GetInt("pencilnum", 0) >= 1)
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
        string notestr= PlayerPrefs.GetString("notewrite" + noteBookNum_i + "p" + notePageNum_i, "");
        
        int note_i = PlayerPrefs.GetInt("checkwrite" + noteBookNum_i + "p" + notePageNum_i, 0);
        if (note_i == 1)
        {
            writePage_obj.SetActive(true);
            writePage_txt.text = notestr;
            input_obj.SetActive(false);
        }
        else
        {
            writePage_obj.SetActive(false);
            writePage_txt.text = "";
            input_obj.SetActive(true);
        }
    }

    //작성된 제목
    void WritedTitle()
    {
        string titlestr = PlayerPrefs.GetString("notewrite" + noteBookNum_i + "t", "");
        int title_i = PlayerPrefs.GetInt("checkwrite" + noteBookNum_i + "t", 0);
        if (title_i == 1)
        {
            titleTxt_obj.SetActive(true);
            title_txt.text = titlestr;
            title_obj.SetActive(false);
        }
        else
        {
            titleTxt_obj.SetActive(false);
            title_txt.text = "";
            title_obj.SetActive(true);
        }
    }

    //저장하기 버튼 띄우기
    public void showOKBtn()
    {
        inputfieldNote.Select();
        startWriteBtn_obj.SetActive(false);
        noteWriteOKBtn_obj.SetActive(true);
        page_obj.SetActive(false);
    }

    //저장하기 버튼 지우기
    public void CloseOKBtn()
    {
        page_obj.SetActive(true);
        startWriteBtn_obj.SetActive(true);
        noteWriteOKBtn_obj.SetActive(false);
        inputfieldNote.text = "";
    }


    //제목저장하기 버튼 띄우기
    public void showTitleOKBtn()
    {
        inputfieldTitle.Select();
        startBtn_obj.SetActive(false);
        titleWriteOKBtn_obj.SetActive(true);
    }

    //제목저장하기 버튼 지우기
    public void CloseTitleOKBtn()
    {
        startBtn_obj.SetActive(true);
        titleWriteOKBtn_obj.SetActive(false);
        inputfieldTitle.text = "";

        string titlestr = PlayerPrefs.GetString("notewrite" + noteBookNum_i + "t", "");
        int title_i = PlayerPrefs.GetInt("checkwrite" + noteBookNum_i + "t", 0);
        if (title_i == 1)
        {
            titleTxt_obj.SetActive(true);
            title_txt.text = titlestr;
            title_obj.SetActive(false);
        }
    }

    //제목저장
    public void SaveTitle()
    {
        title_obj.SetActive(false);
        PlayerPrefs.SetString("notewrite" + noteBookNum_i + "t", titleInput_txt.text);
        PlayerPrefs.SetInt("checkwrite" + noteBookNum_i + "t", 1);
        titleTxt_obj.SetActive(true);
        titleWriteOKBtn_obj.SetActive(false);
        title_txt.text = titleInput_txt.text;
    }

    //제목 다시쓰기
    public void Title()
    {
        title_obj.SetActive(true);
        titleWriteOKBtn_obj.SetActive(true);
        titleTxt_obj.SetActive(false);
        inputfieldTitle.Select();
    }



    //저장할까요?
    public void WriteYN()
    {
        noteWriteYN_obj.SetActive(true);
    }

    //쓰기저장Y
    public void saveWriteY()
    {
        if (PlayerPrefs.GetInt("pencilnum", 0) >= 1)
        {

        }
        //PlayerPrefs.SetString("notewrite1p" + notePageNum_i, input_txt.text);

        PlayerPrefs.SetString("notewrite" + noteBookNum_i + "p" + notePageNum_i, input_txt.text);
        PlayerPrefs.SetInt("checkwrite" + noteBookNum_i + "p" + notePageNum_i, 1);
        noteWriteYN_obj.SetActive(false);
        noteWriteOKBtn_obj.SetActive(false);
        writePage_txt.text = input_txt.text;
        writePage_obj.SetActive(true);
        input_obj.SetActive(false);
        StopCoroutine("noteLine");
        inputfieldNote.text = "";
        page_obj.SetActive(true);

        startWriteBtn_obj.SetActive(true);

        int e = PlayerPrefs.GetInt("pencilnum", 0);
        e--;
        PlayerPrefs.SetInt("pencilnum", e);
        PlayerPrefs.Save();
        //쓴후 연필체크
        CheckPencle();

        //연필 지우개 이미지 켜기
        showpen();
    }
    //쓰기저장N
    public void saveWriteN()
    {
        noteWriteYN_obj.SetActive(false);
    }

    //1권선택
    public void note1()
    {
        noteBookNum_i = 1;
        ClearNote();
        noteWindowImg_obj.SetActive(true);
    }
    //2권선택
    public void note2()
    {
        noteBookNum_i = 2;
        ClearNote();
        noteWindowImg_obj.SetActive(true);
    }
    //3권선택
    public void note3()
    {
        noteBookNum_i = 3;
        ClearNote();
        noteWindowImg_obj.SetActive(true);
    }

    //다른 노트를 선택했을때 초기화 클리어
    void ClearNote()
    {
        CheckHaveNote();
        noteBooks_obj[noteBookNum_i].SetActive(false);
        //페이지수
        notePageNum_i = 0;
        //입력필드텍스트
        inputfieldNote.text = "";
        lineTest_txt.text = "";
        //커버이미지
        showCover();
        //페이지 닫기
        showPage_obj.SetActive(false);
        //커버 열기
        showCover_obj.SetActive(true);
        //페이지 넘기기 버튼
        noteLBtn_obj.SetActive(false);
        noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        //제목 저장불러오기
        WritedTitle();
    }

    //도움말 열기
    public void ShowHelp()
    {
        noteHelp_obj.SetActive(true);
    }

    //도움말 닫기
    public void CloseHelp()
    {
        noteHelp_obj.SetActive(false);
    }

    //지울까요?
    public void DeletWriteYN()
    {

        if (PlayerPrefs.GetInt("erasernum", 0) >= 1)
        {
            noteDeletYN_obj.SetActive(true);
        }
        else
        {
            StopCoroutine("toastEraserImgFadeOut");
            StartCoroutine("toastEraserImgFadeOut");
        }

    }


    //지울까Y
    public void DeletWriteY()
    {
        PlayerPrefs.SetString("notewrite" + noteBookNum_i + "p" + + notePageNum_i, "");
        PlayerPrefs.SetInt("checkwrite" + noteBookNum_i + "p" + + notePageNum_i, 0);
        noteDeletYN_obj.SetActive(false);
        writePage_txt.text = "";
        writePage_obj.SetActive(false);
        input_obj.SetActive(true);
        startWriteBtn_obj.SetActive(true);
        inputfieldNote.text = "";

        int e = PlayerPrefs.GetInt("erasernum", 0);
        e--;
        PlayerPrefs.SetInt("erasernum", e);
        PlayerPrefs.Save();

        //연필 지우개 이미지 켜기
        showpen();
    }

    //지울까N
    public void DeletWriteN()
    {
        noteDeletYN_obj.SetActive(false);
    }

    //커버열기
    public void OpenCover()
    {
        noteWindowImg_obj.GetComponent<Image>().sprite = notePageColor_spr[noteBookNum_i];
        showPage_obj.SetActive(true);
        showCover_obj.SetActive(false);
        notePageNum_i = 1;
        noteLBtn_obj.SetActive(true);
        WritedPage();
        page_txt.text = "" + notePageNum_i + "/30";
    }

    //다음장넘기기
    public void NextPage()
    {
        if (notePageNum_i == 0)
        {
            noteWindowImg_obj.GetComponent<Image>().sprite = notePageColor_spr[noteBookNum_i];
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
            noteWindowImg_obj.GetComponent<Image>().sprite = notePageColor_spr[noteBookNum_i];
            showPage_obj.SetActive(true);
            showCover_obj.SetActive(false);
            notePageNum_i++;
            noteLBtn_obj.SetActive(true);
            noteRBtn_obj.SetActive(true);
            noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        page_txt.text = "" + notePageNum_i + "/30";
        inputfieldNote.text = "";
        lineTest_txt.text = "";
        WritedPage();
    }
    //커버정보초기화 이미지 변경
    void showCover()
    {
        noteWindowImg_obj.GetComponent<Image>().sprite = noteColor_spr[noteBookNum_i];
        eraseNum_txt.text = ""+ PlayerPrefs.GetInt("erasernum", 0);
        pencilNum_txt.text = "" + PlayerPrefs.GetInt("pencilnum", 0);
        //제목
        title_txt.text ="";
    }

    //뒷장넘기기
    public void BackPage()
    {
        noteRBtnImg_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        if (notePageNum_i == 1)
        {
            showCover();
            showPage_obj.SetActive(false);
            showCover_obj.SetActive(true);
            notePageNum_i--;
            noteLBtn_obj.SetActive(false);
            //noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        else if (notePageNum_i <= 1)
        {
            //noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[0];
            notePageNum_i = 0;
        }
        else
        {
            notePageNum_i--;
            noteRBtn_obj.SetActive(true);
            //noteLBtn_obj.GetComponent<Image>().sprite = noteBtn_spr[1];
        }
        page_txt.text = "" + notePageNum_i + "/30";
        inputfieldNote.text = "";
        lineTest_txt.text = "";
        WritedPage();
        CloseTitleOKBtn();
        //제목 저장불러오기
        WritedTitle();
        //연필 지우개 이미지 켜기
        showpen();
    }


    //캐릭터움직임
    IEnumerator moveC()
    {
        int s=0;

        int inp = input_txt.cachedTextGenerator.characterCount;
        while (s==0)
        {
            //
            inp = input_txt.cachedTextGenerator.characterCount-1;
            charNum_txt.text = inp + "/130";

            noteC_obj.GetComponent<Image>().sprite = noteCImg_spr[0];
            yield return new WaitForSeconds(0.4f);

            inp = input_txt.cachedTextGenerator.characterCount - 1;
            charNum_txt.text = inp + "/130";
            yield return new WaitForSeconds(0.4f);

            inp = input_txt.cachedTextGenerator.characterCount - 1;
            charNum_txt.text = inp + "/130";

            noteC_obj.GetComponent<Image>().sprite = noteCImg_spr[1];
            yield return new WaitForSeconds(0.4f);

            inp = input_txt.cachedTextGenerator.characterCount - 1;
            charNum_txt.text = inp + "/130";

            yield return new WaitForSeconds(0.4f);

            inp = input_txt.cachedTextGenerator.characterCount - 1;
            charNum_txt.text = inp + "/130";
        }
    }


    //연필없다
    IEnumerator toastPencleImgFadeOut()
    {
        need_txt.text = "필기구가 없다.";
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

    //지우개없다
    IEnumerator toastEraserImgFadeOut()
    {
        need_txt.text = "지우개가 없다.";
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
            ipstr = input_txt.text;
            inputfieldNote.Select();
            int kn = ipstr.Length;
            kn--;
            kn--;
            if (kn < 0)
            {
                kn = 0;
            }
            inputfieldNote.text = "" + ipstr.Substring(0, kn);
            lineTest_txt.text = "" + ipstr.Substring(0, kn);
            k = lineTest_txt.cachedTextGenerator.lineCount;

            //Debug.Log("" + lineTest_txt.text);
            //Debug.Log("" + kn);
            //Debug.Log("" + k);
            if (k<=10)
            {
            }

            yield return new WaitForSeconds(0.2f);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class TalkEvt : MonoBehaviour {

    List<Dictionary<string, object>> data, data_book, data_light, data_seed, data_wall, data_window; //csv파일
    public Text Text_obj; //선언 및 보여질
    string[] testText_cut; //대사 끊기
    string text_str; //실질적 대사출력

    public GameObject talkbtn,itembtn, talkballoon, closeTB, talkCursor; //대화버튼 및 영역
    bool ihaveitem;

    int[] allArr = new int[10]; //총 호감단계
    int loveLv = 1; //호감도 단계라고 생각하면 됨
    int countTalkNum;//대화횟수

    float speedF = 0.05f;
    int nowArr =0; //현재 줄
    int[] randArr;//난수 필
    int loveExp;//호감도
    int loveMax = 10;//필요 호감도

    //질문만들기
    string quesStr; //질문용대화
    public Text btnTxt1, btnTxt2; //질문버튼 텍스트
    public GameObject quesBtmArea, quesBack; //질문버튼, 뒤
    int choiceNum; //예스or노
    
    //아이템 관련- 0책, 1벽지, 2전등, 3창문, 4씨앗
    int[] itemLv = new int[5]; // 등급
    int itemAllArr; //총 줄수 
    int itemNowArr; //현재 줄
    //하트얻기
    public int talkHeart_i;
    
    //캐릭터 변환
    public Animator charAni;

    //레벨업
    public GameObject leveUpToast_obj;
    Color color;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;
    public GameObject firstGM;

    //나가기
    public GameObject exitTalkBalln, closeTB_exit;
    public Sprite[] ballnSpr;
    public Text exitText;
    int exCk = 0;
    //종료
    int exit_int, exitTalk;
    int cnt_exit;

    //소리
    public GameObject Audio_obj;


    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;


    //엔딩
    public GameObject endWindow_obj2;
    public Sprite[] end_spr2;
    public int end_i2 = 0;
    public GameObject endR_obj2, endL_obj2, endClose_obj2;

    // Use this for initialization
    void Start ()
    {
        color = new Color(1f, 1f, 1f);
        countTalkNum = PlayerPrefs.GetInt("talk", 5);
        callTalkBook();
        callTalkItem();
        //test
        //PlayerPrefs.SetInt("talkending", 0);
        data = CSVReader.Read("Talk/talk_room"); //대사 불러오기   
        data_book = CSVReader.Read("Talk/talk_book"); 
        data_light = CSVReader.Read("Talk/talk_light"); 
        data_seed = CSVReader.Read("Talk/talk_seed"); 
        data_wall = CSVReader.Read("Talk/talk_wall"); 
        data_window = CSVReader.Read("Talk/talk_window");
        setCharAni();
    }

    //게임종료-------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeTalkBoon();
            talkCursor.SetActive(true);
            if (!exitTalkBalln.activeSelf)
            {
                exit_int = 1;
                exitTalkBalln.SetActive(true); //대화창 새로만들기
                closeTB_exit.SetActive(true);
                exitTalkBalln.GetComponent<Image>().sprite = ballnSpr[0];
                if (PlayerPrefs.GetInt("sleepTxt", 0) == 1)
                {
                    talkCursor.SetActive(false);
                    exitTalkBalln.GetComponent<Image>().sprite = ballnSpr[1];
                    exitText.text = "(자고있다. 조용히 나갈까?)\n(뒤로두번 종료)";
                }
                else
                {
                    exitText.text = "가는거니?\n(뒤로두번 종료)";
                }
            }
            else
            {
                StartCoroutine("quitGame");
            }
        }


        if (exit_int == 1)
        {
            cnt_exit++;
        }
        if (cnt_exit == 150)
        {
            if (PlayerPrefs.GetInt("sleepTxt", 0) == 1)
            {
                talkCursor.SetActive(false);
                exitTalkBalln.GetComponent<Image>().sprite = ballnSpr[1];
                exitText.text = "(..흠)";
            }
            else
            {
                exitText.text = "..음";
            }

        }
        else if (cnt_exit == 250)
        {
            exitTalkBalln.SetActive(false);
            closeTB_exit.SetActive(false);
            exit_int = 0;
            cnt_exit = 0;
        }


    }

    void OnApplicationQuit()
    {
        /* 앱이 종료 될 때 처리 */
    }

    IEnumerator quitGame()
    {
        float fl;
        for (fl = 0; fl <= 300; fl++)
        {
            if (fl >= 200)
            {
                if (PlayerPrefs.GetInt("sleepTxt", 0) == 1)
                {
                    talkCursor.SetActive(false);
                    exitTalkBalln.GetComponent<Image>().sprite = ballnSpr[1];
                    exitText.text = "(..잘자)";
                }
                else
                {
                    loveLv = PlayerPrefs.GetInt("lovelv", 0);

                    if (loveLv >= 6) { charAni.Play("bye"); }

                    if (loveLv < 2)
                    {
                        exitText.text = "..잘가";
                    }
                    else if (loveLv < 4)
                    {
                        exitText.text = "잘가렴..";
                    }
                    else if (loveLv < 6)
                    {
                        exitText.text = "안녕 잘가..";
                    }
                    else if (loveLv < 9)
                    {
                        exitText.text = "다음에 봐. 잘가렴";
                    }
                    else if (loveLv < 11)
                    {
                        exitText.text = "안녕 다음에 보자";
                    }
                    else if (loveLv < 13)
                    {
                        exitText.text = "반가웠어 다음에 보자. 친구";
                    }
                    else if (loveLv > 12)
                    {
                        exitText.text = "반가웠어 다음에 보자. 친구";
                    }
                }

            }
            if (fl == 245)
            {
                Application.Quit();
            }
        }
        yield return new WaitForSeconds(fl);
    }


    void lovetalk() { //호감도에 또는 사물에 따른 대화
        int lol;
        if (loveLv >= 9)
        {
            lol = 9;
        }
        else
        {
            lol = loveLv;
        }
        lineReload();
        text_str = "" + data[randArr[nowArr - 1]]["대화" + lol]; //문장넣기 0~9
    }

    void cleantalk() //대화 초기화
    {
        Text_obj.text = "";
        text_str = "";
    }


    public void talkA() //대사치기
    {

        //저장값을 가져온다
        countTalkNum = PlayerPrefs.GetInt("talk", 5);
        loveMax = PlayerPrefs.GetInt("lovemax", 40);
        loveExp = PlayerPrefs.GetInt("lovepoint", 0);
        loveLv = PlayerPrefs.GetInt("lovelv", 0);


        

        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        //Debug.Log(countTalkNum);



        if (countTalkNum == 0)
        {
            //Debug.Log("대화횟수마감");
        }
        else
        {
            //하트를 5번째마다 더해주는 함수
            getTalkHeart();

            //엔딩 대화
            endg();

            //온수를 레벨에 알맞게 더해주기
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            int hRain = PlayerPrefs.GetInt(str1 + "h", 0);
            int sum = loveLv;
            if (sum < 2)
            {
                sum = 1;
            }
            else if (sum < 4)
            {
                sum = 2;
            }
            else if (sum < 6)
            {
                sum = 3;
            }
            else if (sum < 9)
            {
                sum = 4;
            }
            else if (sum < 11)
            {
                sum = 5;
            }
            else if (sum < 13)
            {
                sum = 6;
            }
            else if (sum > 12)
            {
                sum = 6;
            }
            hRain = hRain + sum;
            PlayerPrefs.SetInt(str1 + "h", hRain);
            
            //소리
            Audio_obj.GetComponent<SoundEvt>().talkSound();

            checkach();//업적체크
            lovetalk();
            testText_cut = text_str.Split('/'); //끊기
            cleantalk();

            if (testText_cut[0] == "q")
            { //질문이 있는경우
                StartCoroutine("questionTalkRun");
            }
            else
            {
                StartCoroutine("talkRun");
            }
            countTalk();
            //경험치
            ExpCk_talk();
        }
    }

    /// <summary>
    /// 엔딩대화
    /// </summary>
    void endg()
    {
        int k = 0;
        if (PlayerPrefs.GetInt("talkending", 0) == 0)
        {
            if (PlayerPrefs.GetInt("talkendcnt", 0) >= 9)
            {
                //수집완료
                PlayerPrefs.SetInt("talkending", 1);
                endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
                endWindow_obj.SetActive(true);
            }
            else
            {
                k = PlayerPrefs.GetInt("talkendcnt", 0);
                k++;
                PlayerPrefs.SetInt("talkendcnt", k);
            }
        }
    }

    public void CloseEnd()
    {
        endWindow_obj.SetActive(false);
        Audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

    public void endR()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i == 1)
        {
            endR_obj.SetActive(false);
            endClose_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endL_obj.SetActive(true);
            end_i++;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }
    public void endL()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj.SetActive(false);
        if (end_i == 1)
        {
            endL_obj.SetActive(false);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
        else
        {
            endR_obj.SetActive(true);
            end_i--;
            endWindow_obj.GetComponent<Image>().sprite = end_spr[end_i];
        }
    }



    //대사 출력
    IEnumerator talkRun()
    {
        falseObject();
        for (int i = 0; i < testText_cut.Length; i++)
        {
                text_str = text_str.Insert(text_str.Length, testText_cut[i]);
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
        }
        trueObject();
    }

    //질문 출력
    IEnumerator questionTalkRun()
    {
        falseObject();
        closeTB.SetActive(false);
        quesBack.SetActive(true);
        quesStr = " ";
        for (int i = 0; i < testText_cut.Length; i++)
        {
            quesStr = quesStr.Insert(quesStr.Length, testText_cut[i]);
        }

        for (int i = 1; i < testText_cut.Length; i++)
        {
            text_str = text_str.Insert(text_str.Length, testText_cut[i]);

            if (text_str.Contains("y"))
            { 
                string str, str2;
                str = quesStr.Substring(quesStr.IndexOf("y") + 1, 4);
                btnTxt1.text = str;
                str2 = quesStr.Substring(quesStr.IndexOf("n") + 1, 4);
                btnTxt2.text = str2;                
            }
            else
            {
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        quesBtmArea.SetActive(true);
    }

    //선택한 질문 출력
    IEnumerator choiceTextRun()
    {
        falseObject();

        quesStr = " ";
        for (int i = 0; i < testText_cut.Length; i++)
        {
            quesStr = quesStr.Insert(quesStr.Length, testText_cut[i]);            
        }        

        if (choiceNum == 1)
        {
            for (int i = quesStr.IndexOf("a"); i < quesStr.IndexOf("b")-1; i++)
            {
                text_str = text_str.Insert(text_str.Length, testText_cut[i]);
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        else if (choiceNum == 2)
        {
            for (int i = quesStr.IndexOf("c"); i < quesStr.IndexOf("d")-1; i++)
            { 
                text_str = text_str.Insert(text_str.Length, testText_cut[i]);
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        trueObject();
    }


    //질문버튼
    public void choiceBtn1()
    {
        cleantalk();
        choiceNum = 1;
        StartCoroutine("choiceTextRun");

    }

    public void choiceBtn2()
    {
        cleantalk();
        choiceNum = 2;
        StartCoroutine("choiceTextRun");

    }







    /*
     생각할 것
     진행 중일 때 호감도가 오른다면 어디에 넣어야 할까? > loveLv
     상점에서 업그레이드를 한다면 어디에 넣어야 할까? > loveLv
     loveLv 수치 조절하면 lovetalk()에서 값을 받아 알아서 출력해준다
     랜덤 대화만 해결하면 될 듯? 10.04 완료
     호감도가 올라서 새로운 대화를 해야할 때 allArr(length)과 nowArr 둘 다 초기화 해준다(줄 수 다시 불러오기 + 1로 만들기

    실시간 호감도 업그레이드에 따른 정보 출력 = 정보창이 열릴 때
     */


    public int[] GetRandomInt(int length) //중복없는 난수생성기
    {
        randArr = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {            
            while (true)
            {
                randArr[i] = Random.Range(0, length); //0~(line_txt-1)
                isSame = false;

                for (int j = 0; j < i; ++j)
                {
                    if (randArr[j] == randArr[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArr;
    }


    void lineReload() // 대화 차례대로 보여주기 및 대화줄 초기화
    {
        int lol;
        if (loveLv >= 9)
        {
            lol = 9;
        }
        else
        {
            lol = loveLv;
        }
        if (nowArr == 0) // 난수 돌리기
        {
            GetRandomInt(allArr[lol]); //9>allArr[loveLv] ★꼭 바꾸기 테스트용
            nowArr++;
        }
        else if (nowArr < allArr[lol]) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= allArr[lol]) //대화 줄 초기화
        {
            GetRandomInt(allArr[lol]);
            nowArr = 0;
            nowArr++;
        }

    }

    //대화에 따른 호감도 증가
    void ExpCk_talk() 
    {
        
        if (loveExp < loveMax) // 필요량 - 1
        {
            if (ihaveitem)
            {
                //아이템 효과는 이렇게 if문으로 추가하기
                loveExp++;
            }
            loveExp= loveExp + 1;
            PlayerPrefs.SetInt("lovepoint", loveExp);
            // 이 변수는 나중에 GetInt되어서 공유됨, 또한 조건문을 이용하여 호감단계에 따른 경험치 획득 및 아이템 장착효과도 넣을 수 있다.  

            
        }
        else if (loveExp >= loveMax)
        {
            if (loveLv > 12)
            {
                //최대 레벨
                //대화집 또는 해당 레벨에 언락되어서 뭔가 살 수 있는 무언가를 생각해보자
                //Debug.Log("레벨멈춤");
                checkList();
            }
            else {
                checkList();
                
            }

        }
        
        PlayerPrefs.Save();
    }

    public void itemEff()
    {
        ihaveitem = true;
        itembtn.SetActive(false);
    }

    void callTalkBook()
    {
        allArr[0] = 55; 
        allArr[1] = 60; 
        allArr[2] = 70;
        allArr[3] = 70; 
        allArr[4] = 75; 
        allArr[5] = 75; 
        allArr[6] = 90; 
        allArr[7] = 95;
        allArr[8] = 95; 
        allArr[9] = 165;
        
    }






    public void closeTalkBoon()
    {
        talkballoon.SetActive(false);
        closeTB.SetActive(false);
        closeTB.GetComponent<Button>().interactable = false;
    }

    public void closeExitBoon()
    {
        exitTalkBalln.SetActive(false);
        closeTB_exit.SetActive(false);
    }


    


    //대화횟수차감
    void countTalk()
    {        
        if (countTalkNum <= 0)
        {
            countTalkNum = 0;
        }
        else
        {
            countTalkNum--;
        }
        PlayerPrefs.SetInt("talk", countTalkNum);
        PlayerPrefs.Save();
    }


    //버튼 가리기(대화할 때 안 눌리기 위해서)
    void falseObject()
    {
        //closetalk_btn.SetActive(true);
        int aninum = loveLv;
        if (aninum < 13)
        {
            charAni.Play("talk1");
        }
        else if (aninum > 12)
        {
            charAni.Play("talk2");
        }

        talkballoon.SetActive(true);
        closeTB.GetComponent<Button>().interactable = false;
        closeTB.SetActive(true);

        talkbtn.SetActive(false);
        quesBtmArea.SetActive(false);
        quesBack.SetActive(false);

    }

    void trueObject()
    {
        closeTB.GetComponent<Button>().interactable = true;
        //closeTB.SetActive(true);
        talkbtn.SetActive(true);

        setCharAni();

    }
    
    void callTalkItem()
    {
        // 나중에 0을 아이템 등급이라 생각하면 됨 각각 bookitemlv windowlv 등등 정하면 끝
        itemLv[0] = PlayerPrefs.GetInt("booklv",0)-1;
        itemLv[1] = PlayerPrefs.GetInt("walllv", 0);
        itemLv[2] = PlayerPrefs.GetInt("lightlv", 0);
        itemLv[3] = PlayerPrefs.GetInt("windowlv", 0);
        itemLv[4] = PlayerPrefs.GetInt("seedlv", 0) - 1;
    }

    public void talkBook()
    {
        callTalkItem();
        itemLineReload(299); 
        text_str = "" + data_book[itemNowArr]["book" + itemLv[0]];
        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }

    public void talkWall()
    {
        callTalkItem();
        itemLineReload(304);
        text_str = "" + data_wall[itemNowArr]["wall" + itemLv[1]];
        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }
    
    public void talkLight()
    {
        callTalkItem();
        itemLineReload(376);
        text_str = "" + data_light[itemNowArr]["light" + itemLv[2]];
        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }

    public void talkWindow()
    {
        callTalkItem();
        itemLineReload(472);
        text_str = "" + data_window[itemNowArr]["window" + itemLv[3]];
        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }

    public void talkSeed()
    {
        if (PlayerPrefs.GetInt("seedlv", 0) >= 10)
        {
            achvcheck();
        }
        callTalkItem();
        itemLineReload(289);
        text_str = "" + data_seed[itemNowArr]["seed" + itemLv[4]];
        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }
    void achvcheck()
    {
        //업적
        if(PlayerPrefs.GetInt("allflower", 0) == 0)
        {
            PlayerPrefs.SetInt("allflower", 1);
            firstGM.GetComponent<AchievementShow>().achievementCheck(25, 0);
        }
    }


        //아이템대사 출력
        IEnumerator itemTalkRun()
    {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        falseObject();
        for (int i = 0; i < testText_cut.Length; i++)
        {
            text_str = text_str.Insert(text_str.Length, testText_cut[i]);
            Text_obj.text = text_str;
            yield return new WaitForSeconds(speedF);
        }

        trueObject();
    }


    //범위 설정 다시하기
    public void bookAllArr()
    {
        if (itemLv[0] == 12 || itemLv[0] == 13)
            {
            itemAllArr = 7;
        }else if (itemLv[0] == 7 || itemLv[0] == 8 || itemLv[0] == 9 || itemLv[0] == 10 || itemLv[0] == 11)
        {
            itemAllArr = 5;
        }
        else if (itemLv[0] <= 6)
        {
            itemAllArr = 3;
        }

    }
    
    void wallAllArr()
    {
        if (itemLv[1] == 3)
        {
            itemAllArr = 7;
        }
        else if (itemLv[1] <= 2)
        {
            itemAllArr = 3;
        }
    }

    void lightAllArr()
    {
        itemAllArr = 3;
    }

    void windowAllArr()
    {
        if (itemLv[3] == 8)
        {
            itemAllArr = 9;
        }
        else if (itemLv[3] == 7 || itemLv[3] == 6)
        {
            itemAllArr = 5;
        }
        else if (itemLv[3] <= 5)
        {
            itemAllArr = 3;
        }
    }

    void seedAllArr()
    {
        if (itemLv[4] == 9)
        {
            itemAllArr = 7;
        }
        else if (itemLv[4] == 6 || itemLv[4] == 7 || itemLv[4] == 8)
        {
            itemAllArr = 5;
        }
        else if (itemLv[4] <= 5)
        {
            itemAllArr = 3;
        }
    }


    void itemLineReload(int lv)
    {
        switch (lv)
        {
            case 299:
                //Debug.Log("책이다");
                bookAllArr();
                break;

            case 304:
               // Debug.Log("벽이다");
                wallAllArr();
                break;

            case 376:
                //Debug.Log("빛이다");
                lightAllArr();
                break;

            case 472:
                //Debug.Log("창이다");
                windowAllArr();
                break;

            case 289:
                //Debug.Log("풀이다");
                seedAllArr();
                break;
        }

        if (itemNowArr < itemAllArr) //대화 차례대로 보이기
        {
            itemNowArr++;
            //Debug.Log(itemNowArr);
        }
        else if (itemNowArr >= itemAllArr) //대화 줄 초기화
        {
            itemNowArr = 0;
            //Debug.Log("리셋");
        }
    }

    void checkList()
    {
        int a = 0;
        switch (loveLv)
        {
            case 0:
                if(PlayerPrefs.GetInt("booklv", 0) >= 2)
                {
                    if(PlayerPrefs.GetInt("desklv", 0) >= 1)
                    {
                        a = 1;
                        PlayerPrefs.SetInt("lovemax", 100);
                    }
                }
                    break;
            case 1:
                if (PlayerPrefs.GetInt("booklv", 0) >= 4)
                {
                    if (PlayerPrefs.GetInt("cabinetlv", 0) >= 1)
                    {
                        a = 1;
                        PlayerPrefs.SetInt("lovemax", 150);
                    }
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("booklv", 0) >= 6)
                {
                    if (PlayerPrefs.GetInt("bedlv", 0) >= 1)
                    {
                        if (PlayerPrefs.GetInt("walllv", 0) >= 1)
                        {
                            a = 1;
                            PlayerPrefs.SetInt("lovemax", 220);
                        }
                    }
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("booklv", 0) >= 7)
                {
                    if (PlayerPrefs.GetInt("windowlv", 0) >= 1)
                    {
                        if (PlayerPrefs.GetInt("ladderlv", 0) >= 1)
                        {
                            a = 1;
                            PlayerPrefs.SetInt("lovemax", 250);
                        }
                    }
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("booklv", 0) >= 8)
                {
                    if (PlayerPrefs.GetInt("windowlv", 0) >= 3)
                    {
                        if (PlayerPrefs.GetInt("gasrangelv", 0) >= 1)
                        {
                            a = 1;
                            PlayerPrefs.SetInt("lovemax", 250);
                        }
                    }
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("windowlv", 0) >= 5)
                {
                    
                        if (PlayerPrefs.GetInt("iceboxlv") >= 1)
                        {
                            a = 1;
                            PlayerPrefs.SetInt("lovemax", 290);
                        }
                }
                break;
            case 6:
                if (PlayerPrefs.GetInt("lightlv", 0) >= 1)
                {
                    if (PlayerPrefs.GetInt("drawerlv", 0) >= 1)
                    {
                        if (PlayerPrefs.GetInt("doorbox", 0) == 1)
                        {
                            a = 1;
                            PlayerPrefs.SetInt("lovemax", 350);
                        }
                    }
                }
                break;
            case 7:
                if (PlayerPrefs.GetInt("booklv", 0) >= 10)
                {
                    if (PlayerPrefs.GetInt("windowlv", 0) >= 6)
                    {
                        a = 1;
                        PlayerPrefs.SetInt("lovemax", 370);
                    }
                }
                break;
            case 8:
                if (PlayerPrefs.GetInt("booklv", 0) >= 11)
                {
                    if (PlayerPrefs.GetInt("windowlv", 0) >= 7)
                    {
                        a = 1;
                        PlayerPrefs.SetInt("lovemax", 400);
                    }
                }
                break;
            case 9:
                if (PlayerPrefs.GetInt("booklv", 0) >= 12)
                {
                    if (PlayerPrefs.GetInt("windowlv", 0) >= 8)
                    {
                        a = 1;
                        PlayerPrefs.SetInt("lovemax", 400);
                    }
                }
                break;
            case 10:
                if (PlayerPrefs.GetInt("lightlv", 0) >= 2)
                {
                    a = 1;
                    PlayerPrefs.SetInt("lovemax", 400);
                }
                break;
            case 11:
                if (PlayerPrefs.GetInt("booklv", 0) >= 14)
                {
                    a = 1;
                    PlayerPrefs.SetInt("lovemax", 400);
                }
                break;
            case 12:
                    a = 1;
                    PlayerPrefs.SetInt("lovemax", 400);
                endg2();
                break;
            case 13:
                loveExp = 0;
                PlayerPrefs.SetInt("lovemax", 200);
                PlayerPrefs.SetInt("lovepoint", loveExp);
                int add = PlayerPrefs.GetInt("addtxt",0);
                add++;
                if (add == 50)
                {
                    add = 0;
                }
                PlayerPrefs.SetInt("addtxt",add);
                StopCoroutine("leveUpToastImgFadeOut");
                StartCoroutine("leveUpToastImgFadeOut");
                leveUpToast_obj.SetActive(true);
                countTalkNum = 5;
                PlayerPrefs.SetInt("talk", countTalkNum);
                break;
        }
        //조금 친해진 것 같다 호감레벨 상승
        if (a == 1)
        {
            loveLv++;
            loveExp = 0;
            nowArr = 1;
            if (loveLv >= 9)
            {
                    GetRandomInt(allArr[9]);
            }
            else
            {
                GetRandomInt(allArr[loveLv]);
            }
            
            PlayerPrefs.SetInt("lovepoint", loveExp);
            PlayerPrefs.SetInt("lovelv", loveLv);

            countTalkNum = 5;
            PlayerPrefs.SetInt("talk", countTalkNum);
            
            PlayerPrefs.Save();
            StopCoroutine("leveUpToastImgFadeOut");
            StartCoroutine("leveUpToastImgFadeOut");
            leveUpToast_obj.SetActive(true);
        }
        
    }


    /// <summary>
    /// 엔딩레벨
    /// </summary>
    void endg2()
    {
        if (PlayerPrefs.GetInt("likeending", 0) == 0)
        {
                PlayerPrefs.SetInt("likeending", 1);
                endWindow_obj2.GetComponent<Image>().sprite = end_spr2[end_i2];
                endWindow_obj2.SetActive(true);
        }
    }

    public void CloseEnd2()
    {
        endWindow_obj2.SetActive(false);
        Audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

    public void endR2()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i2 == 1)
        {
            endR_obj2.SetActive(false);
            endClose_obj2.SetActive(true);
            end_i2++;
            endWindow_obj2.GetComponent<Image>().sprite = end_spr2[end_i2];
        }
        else
        {
            endL_obj2.SetActive(true);
            end_i2++;
            endWindow_obj2.GetComponent<Image>().sprite = end_spr2[end_i2];
        }
    }
    public void endL2()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj2.SetActive(false);
        if (end_i2 == 1)
        {
            endL_obj2.SetActive(false);
            end_i2--;
            endWindow_obj2.GetComponent<Image>().sprite = end_spr2[end_i2];
        }
        else
        {
            endR_obj2.SetActive(true);
            end_i2--;
            endWindow_obj2.GetComponent<Image>().sprite = end_spr2[end_i2];
        }
    }

    //하트를 5번째대화마다 준다.
    void getTalkHeart()
    {
        talkHeart_i = PlayerPrefs.GetInt("talkheartcount", 0);
        talkHeart_i++;
        if (talkHeart_i >= 5)
        {
            talkHeart_i = 0;
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            int ht;
            if (PlayerPrefs.GetInt("outtrip", 0) == 1)
            {
                ht = PlayerPrefs.GetInt(str1 + "cv", 0);
                ht++;
                PlayerPrefs.SetInt(str1 + "cv", ht);
            }
            else
            {
                ht = PlayerPrefs.GetInt(str1 + "ht", 0);
                ht++;
                PlayerPrefs.SetInt(str1 + "ht", ht);
            }
            
            
        }
        PlayerPrefs.SetInt("talkheartcount", talkHeart_i);

    }

    void setCharAni()
    {
        int aninum = loveLv;
        if (aninum < 2)
        {
            charAni.Play("char_12");
        }
        else if (aninum < 4)
        {
            charAni.Play("char_34");
        }
        else if (aninum < 6)
        {
            charAni.Play("char_56");
        }
        else if (aninum < 9)
        {
            charAni.Play("char_78");
        }
        else if (aninum < 11)
        {
            charAni.Play("char_101");
        }
        else if (aninum < 13)
        {
            charAni.Play("123");
        }
        else if (aninum > 12)
        {
            charAni.Play("144");
        }
    }
    
    public void closeLeveUP()
    {
        leveUpToast_obj.SetActive(false);
    }

    //업적
    void checkach()
    {
        int cts = PlayerPrefs.GetInt("counttalkst", 0);
        cts++;
        PlayerPrefs.SetInt("counttalkst", cts);
        //Debug.Log("tal" + PlayerPrefs.GetInt("talkst", 0) + "cts" + cts);
        if (cts >= 1500 && PlayerPrefs.GetInt("talkst", 0) < 3)
        {
            PlayerPrefs.SetInt("talkst", 3);
            firstGM.GetComponent<AchievementShow>().achievementCheck(0, 2);
        }
        else if (cts >= 700 && PlayerPrefs.GetInt("talkst", 0) < 2)
        {
            PlayerPrefs.SetInt("talkst", 2);
            firstGM.GetComponent<AchievementShow>().achievementCheck(0, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("talkst", 0) < 1)
        {
            PlayerPrefs.SetInt("talkst", 1);
            firstGM.GetComponent<AchievementShow>().achievementCheck(0, 0);
        }
        PlayerPrefs.Save();
    }

 

    IEnumerator achievementOut()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 1f; i > -0.2f; i -= 0.05f)
        {
            moveY = moveY + 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        
    }
    IEnumerator achievementIn()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 0f; i < 1.2f; i += 0.05f)
        {
            moveY = moveY - 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine("achievementOut");
    }

    //친해졌다
    IEnumerator leveUpToastImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        leveUpToast_obj.GetComponent<Image>().color = color;
        leveUpToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            leveUpToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        leveUpToast_obj.SetActive(false);
    }
    
}

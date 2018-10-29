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

    public GameObject talkbtn,itembtn; //대화버튼
    bool ihaveitem;

    int[] allArr = new int[10]; //총 줄수
    int loveLv = 0; //호감도 단계라고 생각하면 됨
    

    int nowArr =0; //현재 줄
    int[] randArr;//난수 필
    int loveExp;//호감도
    int loveMax = 10;//필요 호감도
    public Text loveLv_obj, loveExp_obj, loveTalk_obj;

    //질문만들기
    string quesStr; //질문용대화
    public Text btnTxt1, btnTxt2; //질문버튼 텍스트
    public GameObject quesBtn1, quesBtn2; //질문버튼
    int choiceNum; //예스or노

    float speedF = 0.03f;

    //아이템 관련- 0책, 1빛, 2씨앗, 3벽, 4창문
    int[] itemLv = new int[5]; // 등급
    int[] itemAllArr = new int[5]; //총 줄수 
    int[] itemNowArr = new int[5]; //현재 줄
    public GameObject book_obj, light_obj, seed_obj, wall_obj, window_obj; //대화버튼



    // Use this for initialization
    void Start () {
        callTalkBook();
        callTalkItem();
        
        data = CSVReader.Read("Talk/talk_room"); //대사 불러오기   
        data_book = CSVReader.Read("Talk/talk_book"); 
        data_light = CSVReader.Read("Talk/talk_light"); 
        data_seed = CSVReader.Read("Talk/talk_seed"); 
        data_wall = CSVReader.Read("Talk/talk_wall"); 
        data_window = CSVReader.Read("Talk/talk_window");
                
    }

    void lovetalk() { //호감도에 또는 사물에 따른 대화

        lineReload();
        text_str = "" + data[randArr[nowArr - 1]]["대화" + loveLv]; //문장넣기 0~9
    }

    void cleantalk() //대화 초기화
    {
        Text_obj.text = "";
        text_str = "";
    }


    public void talkA() //대사치기
    {
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

        //경험치
        ExpCk_talk();

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
        quesBtn1.SetActive(true);
        quesBtn2.SetActive(true);
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
        if (nowArr == 0) // 난수 돌리기
        {
            GetRandomInt(allArr[loveLv]); //9>allArr[loveLv] ★꼭 바꾸기 테스트용
            nowArr++;
        }
        else if (nowArr < allArr[loveLv]) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= allArr[loveLv]) //대화 줄 초기화
        {
            GetRandomInt(allArr[loveLv]);
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
            loveExp++;
            // 이 변수는 나중에 GetInt되어서 공유됨, 또한 조건문을 이용하여 호감단계에 따른 경험치 획득 및 아이템 장착효과도 넣을 수 있다.            
        }
        else if (loveExp >= loveMax)
        {
            if (loveLv > 8)
            {
                //최대 레벨
                //대화집 또는 해당 레벨에 언락되어서 뭔가 살 수 있는 무언가를 생각해보자
                Debug.Log("레벨멈춤");
            }
            else {
                loveLv++;
                loveExp = 0;
                nowArr = 1;
                GetRandomInt(allArr[loveLv]);
            }

        }

        loveLv_obj.text = loveLv + "- 레벨 및 대화셀";
        loveExp_obj.text = loveExp + "- 경험치";


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
    
    public void TalkSpeedFast()
    {
        speedF = 0.01f;
    }

    public void TalkSpeedNor()
    {
        speedF = 0.03f;
    }

    public void TalkSpeedSlpw()
    {
        speedF = 0.05f;
    }


   

    //버튼 가리기(대화할 때 안 눌리기 위해서)
    void falseObject()
    {
        talkbtn.SetActive(false);
        quesBtn1.SetActive(false);
        quesBtn2.SetActive(false);
        book_obj.SetActive(false);
       // light_obj.SetActive(false);
       // seed_obj.SetActive(false);
       // wall_obj.SetActive(false);
       // window_obj.SetActive(false);
    }

    void trueObject()
    {
        talkbtn.SetActive(true);
        book_obj.SetActive(true);
    }


    void callTalkItem()
    {
        // 나중에 0을 아이템 등급이라 생각하면 됨 각각 bookitemlv windowlv 등등 정하면 끝
        itemLv[0] = 0;
        itemLv[1] = 0;
        itemLv[2] = 0;
        itemLv[3] = 0;
        itemLv[4] = 0;

    }

    public void talkBook()
    {
        
        itemLineReload();
        text_str = "" + data_book[itemNowArr[0]]["book" + itemLv[0]];
        itemNowArr[0]++;

        testText_cut = text_str.Split('/');
        cleantalk();

        StartCoroutine("itemTalkRun");
    }

    //아이템대사 출력
    IEnumerator itemTalkRun()
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


    //범위 설정 다시하기
    public void testItemLvUP()
    {
        if (itemLv[0] > 12)
        {
            //최대 레벨
        }
        else
        {
            itemLv[0]++;
            Debug.Log("현재레벨" + itemLv[0]);
            itemNowArr[0] = 0; //레벨업하면 현재 초기화
        }

        if (itemLv[0] == 12 || itemLv[0] == 13)
            {
            itemAllArr[0] = 8;
            Debug.Log("88");
        }else if (itemLv[0] == 7 || itemLv[0] == 8 || itemLv[0] == 9 || itemLv[0] == 10 || itemLv[0] == 11)
        {
            itemAllArr[0] = 6;
            Debug.Log("66");
        }else if (itemLv[0] == 6 || itemLv[0] == 5 || itemLv[0] == 4 || itemLv[0] == 3)
        {
            itemAllArr[0] = 4;
            Debug.Log("44");
        }
        else if (itemLv[0] < 3)
        {
            itemAllArr[0] = 3;
        }

    }

    void itemLineReload()
    {
        if (itemNowArr[0] < itemAllArr[0]-1) //대화 차례대로 보이기
        { 

        }
        else if (itemNowArr[0] > itemAllArr[0]-1) //대화 줄 초기화
        {
            itemNowArr[0] = 0;
            Debug.Log("리셋");
        }
        

    }

}

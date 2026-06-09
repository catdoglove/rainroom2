using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class TalkEvtOut : MonoBehaviour
{

    List<Dictionary<string, object>> data_out,data_park; //csv파일
    int etcNum = 0;
    public Text Text_obj; //선언 및 보여질
    string[] testText_cut; //대사 끊기
    string text_str; //실질적 대사출력

    public GameObject talkbtn, talkballoon, closeTB, talkCursor; //대화버튼 및 영역
    bool ihaveitem;

    int allArr = 150; //총 줄수
    int loveLv = 1; //호감도 단계라고 생각하면 됨
    int countTalkNum;//대화횟수

    float speedF = 0.08f;
    int nowArr = 0; //현재 줄
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
    public int talkClover_i;

    //캐릭터 변환
    public Animator charAni;

    //레벨업
    Color color;

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;
    public GameObject firstGM,GM;

    //나가기
    public GameObject exitTalkBalln, closeTB_exit, exitBbg;
    public Text exitText;
    int exCk = 0;
    //종료
    int exit_int, exitTalk;
    int cnt_exit;
    public Sprite[] ballnSpr;

    //소리
    public GameObject Audio_obj;
    //외물물건
    public GameObject putToast_obj;
    private int cnt;
    private string[] lineStr;


    // Use this for initialization
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GMtag");
        color = new Color(1f, 1f, 1f);
        countTalkNum = PlayerPrefs.GetInt("talk", 5);
        charAni.Play("char_park");


        data_out = CSVReader.Read("Talk/talk_out"); //대사 불러오기   
        data_park = CSVReader.Read("Talk/etc_park");



    }

    // 종료 함수
    void Update()
    {
        //클립초기화
        if (PlayerPrefs.GetInt("frontpark", 0) == 1)
        {
            PlayerPrefs.SetInt("frontpark", 99);
            charAni.Play("char_park");
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GM.GetComponent<MainBtnEvt>().goHome_obj.SetActive(false);
            GM.GetComponent<MainBtnEvt>().helpfrist_obj.SetActive(false);
            if((PlayerPrefs.GetInt("outtrip", 0) == 1)){
                firstGM.GetComponent<ParkShop>().allClose();
                firstGM.GetComponent <Parkfunction>().mountainWindow_obj.SetActive(false);
                firstGM.GetComponent<Parkfunction>().mountainAD_obj.SetActive(false);
            }
            if ((PlayerPrefs.GetInt("outtrip", 0) == 2))
            {
                firstGM.GetComponent<CityShop>().allClose();
                firstGM.GetComponent<CityFood>().allClose();
            }
            // 여기에러
            closeTalkBoon();
            if (!exitTalkBalln.activeSelf)
            {
                exit_int = 1;
                exitTalkBalln.SetActive(true); //대화창 새로만들기
                closeTB_exit.SetActive(true);
                exitBbg.GetComponent<Image>().sprite = ballnSpr[0];

                if (PlayerPrefs.GetInt("frontpark", 0) == 99 || (PlayerPrefs.GetInt("frontcity", 0) == 1))
                {
                    talkCursor.SetActive(true);
                    exitText.text = "집으로 돌아갈까..\n(종료는 집에서 가능)";
                }
                else if (PlayerPrefs.GetInt("frontpark", 0) == 2 || PlayerPrefs.GetInt("frontcity", 0) == 2)
                {
                    talkCursor.SetActive(false);
                    exitBbg.GetComponent<Image>().sprite = ballnSpr[1];
                    exitText.text = "(가려면 집으로 돌아가야 할 거 같다.)";
                }
            }
        }


        if (exit_int == 1)
        {
            cnt_exit++;
        }
        if (cnt_exit == 150)
        {
            if (PlayerPrefs.GetInt("frontpark", 0) == 99 || (PlayerPrefs.GetInt("frontcity", 0) == 1))
            {
                exitText.text = "..음";
            }
            else if (PlayerPrefs.GetInt("frontpark", 0) == 2 || (PlayerPrefs.GetInt("frontcity", 0) == 2))
            {
                exitText.text = "(..흠)";
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



        if (countTalkNum == 0)
        {
            //대화못함
        }
        else
        {
            //하트를 5번째마다 더해주는 함수
            getTalkClover();

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

            checkach();
            lineReload();

            if (PlayerPrefs.GetInt("outtrip", 0) == 1)
            {
                text_str = "" + data_out[randArr[nowArr - 1]]["공원"];
                feed();
            }
            else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
            {
                text_str = "" + data_out[randArr[nowArr - 1]]["도시"];
                feedCity();
            }



            Text_obj.text = "";
            lineStr = text_str.ToString().Split('|'); // 0:질문 1:대답버튼 2:1번의 대답 3:대답버튼 4:3번의 대답          
            if (text_str.Contains("Z"))
            { //질문이 있는경우
                StartCoroutine("questionTalkRun");
            }
            else
            {
                StartCoroutine(talkRun(speedF));
            }

            //경험치
            ExpCk_talk();
        }
    }

    //500번 했을때
    void feed()
    {
        if (PlayerPrefs.GetInt("ptfin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("ptcount", 0);
            if (c >= 499)
            {
                PlayerPrefs.SetInt("ptfin", 1);
                PlayerPrefs.SetInt("setoutgoods", 1);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("ptcount", c);
            }
        }
    }

    //500번 했을때
    void feedCity()
    {
        if (PlayerPrefs.GetInt("ctfin", 0) == 0)
        {
            int c = PlayerPrefs.GetInt("ctcount", 0);
            if (c >= 499)
            {
                PlayerPrefs.SetInt("ctfin", 1);
                PlayerPrefs.SetInt("setoutgoods", 2);
                putToast_obj.SetActive(true);
            }
            else
            {
                c++;
                PlayerPrefs.SetInt("ctcount", c);
            }
        }
    }


    //대사 출력
    IEnumerator talkRun(float f)
    {
        falseObject();
        cnt = 0;
        while (cnt != text_str.Length)
        {
            if (cnt < text_str.Length)
            {
                Text_obj.text += text_str[cnt].ToString();
                cnt++;
            }
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
        btnTxt1.text = "";
        btnTxt2.text = "";
        cnt = 1;
        while (cnt != lineStr[0].Length)
        {
            if (cnt < lineStr[0].Length)
            {
                Text_obj.text += lineStr[0][cnt].ToString();
                cnt++;
            }
            yield return new WaitForSeconds(speedF);
        }

        btnTxt1.text += lineStr[1].ToString();
        btnTxt2.text += lineStr[3].ToString();

        quesBtmArea.SetActive(true);
    }

    //선택한 질문 출력
    IEnumerator choiceTextRun()
    {
        falseObject();

        quesStr = " ";
        cnt = 0;

        if (choiceNum == 1)
        {
            while (cnt != lineStr[2].Length)
            {
                if (cnt < lineStr[2].Length)
                {
                    Text_obj.text += lineStr[2][cnt].ToString();
                    cnt++;
                }
                yield return new WaitForSeconds(speedF);
            }
        }
        else if (choiceNum == 2)
        {
            while (cnt != lineStr[4].Length)
            {
                if (cnt < lineStr[4].Length)
                {
                    Text_obj.text += lineStr[4][cnt].ToString();
                    cnt++;
                }
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
            GetRandomInt(allArr); 
            nowArr++;
        }
        else if (nowArr < allArr) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= allArr) //대화 줄 초기화
        {
            GetRandomInt(allArr);
            nowArr = 0;
            nowArr++;
        }

    }


    //대화횟수차감
    public void countTalk()
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
        charAni.Play("char_park_talk");

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


        charAni.Play("char_park");

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
            loveExp = loveExp + 1;
            PlayerPrefs.SetInt("lovepoint", loveExp);
            // 이 변수는 나중에 GetInt되어서 공유됨, 또한 조건문을 이용하여 호감단계에 따른 경험치 획득 및 아이템 장착효과도 넣을 수 있다.  


        }
        else if (loveExp >= loveMax)
        {
            if (loveLv > 11)
            {
                //최대 레벨
                //대화집 또는 해당 레벨에 언락되어서 뭔가 살 수 있는 무언가를 생각해보자
                //Debug.Log("레벨멈춤");
            }

        }

        PlayerPrefs.Save();
    }
    
    void getTalkClover()
    {
        
        if (PlayerPrefs.GetInt("outtrip", 0) == 1)
        {
            talkClover_i = PlayerPrefs.GetInt("talkclovercount", 0);
            talkClover_i++;
            if (talkClover_i >= 5)
            {
                talkClover_i = 0;
                string str1;
                str1 = PlayerPrefs.GetString("code", "");
                int cv;
                cv = PlayerPrefs.GetInt(str1 + "cv", 0);
                cv++;
                PlayerPrefs.SetInt(str1 + "cv", cv);

            }
            PlayerPrefs.SetInt("talkclovercount", talkClover_i);
        }
        else if (PlayerPrefs.GetInt("outtrip", 0) == 2)
        {
            talkClover_i = PlayerPrefs.GetInt("talkdiacount", 0);
            talkClover_i++;
            if (talkClover_i >= 5)
            {
                talkClover_i = 0;
                string str1;
                str1 = PlayerPrefs.GetString("code", "");
                int cv;
                cv = PlayerPrefs.GetInt(str1 + "dm", 0);
                cv++;
                PlayerPrefs.SetInt(str1 + "dm", cv);

            }
            PlayerPrefs.SetInt("talkdiacount", talkClover_i);
        }
    }


    public void itemEff()
    {
        ihaveitem = true;
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


    public void talk1()
    {
        cleantalk();
        text_str = "" + data_park[etcNum]["벤치"];
        StopCoroutine("itemTalkRun");
        StartCoroutine("itemTalkRun");

        if (etcNum >= 2)
        {
            etcNum = 0;
        }
        else
        {
            etcNum++;
        }
    }

    public void talk2()
    {
        cleantalk();
        text_str = "" + data_park[etcNum]["모래"];
        StopCoroutine("itemTalkRun");
        StartCoroutine("itemTalkRun");

        if (etcNum >= 2)
        {
            etcNum = 0;
        }
        else
        {
            etcNum++;
        }
    }

    public void talk3()
    {
        cleantalk();
        text_str = "" + data_park[etcNum]["시소"];
        StopCoroutine("itemTalkRun");
        StartCoroutine("itemTalkRun");

        if (etcNum >= 2)
        {
            etcNum = 0;
        }
        else
        {
            etcNum++;
        }
    }

    public void talk4()
    {
        cleantalk();
        text_str = "" + data_park[etcNum]["그네"];
        StopCoroutine("itemTalkRun");
        StartCoroutine("itemTalkRun");

        if (etcNum >= 2)
        {
            etcNum = 0;
        }
        else
        {
            etcNum++;
        }
    }

    public void talk5()
    {
        cleantalk();
        text_str = "" + data_park[etcNum]["미끄럼틀"];
        StopCoroutine("itemTalkRun");
        StartCoroutine("itemTalkRun");


        if (etcNum >= 2)
        {
            etcNum = 0;
        }
        else
        {
            etcNum++;
        }
    }

    IEnumerator itemTalkRun()
    {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        falseObject();
        cnt = 0;
        while (cnt != text_str.Length)
        {
            if (cnt < text_str.Length)
            {
                Text_obj.text += text_str[cnt].ToString();
                cnt++;
            }
            yield return new WaitForSeconds(speedF);
        }
        trueObject();
    }

}

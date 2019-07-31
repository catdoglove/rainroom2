using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkShop : MonoBehaviour {

    //이밴트와 상점창
    public GameObject eventPaint_obj, eventPet_obj, foodShop_obj, inShop_obj, basicShop_obj;
    public GameObject blackClose_obj;
    public int shopNum, helpNum;
    public string str;
    public Text coldRain_txt, hotRain_txt;
    //거리의화가
    public int[] eventPaint_i;
    public GameObject[] eventPaintImg_obj,soldout_obj;
    public GameObject buyPaintYN_obj;
    public Sprite[] moviePaint_spr, specialPaint_spr, storyPaint_spr;
    public string[] moviePaint_str, specialPaint_str, storyPaint_str;
    public int special_i,movie_i,story_i,paint_i;
    public string paint_str;
    public Text paint_txt;
    public Text paintPrice_txt,paintP_txt;
    public Sprite[] soldOut_spr;
    //야시장
    public GameObject foodBuy_obj,selectFood_obj,foodIlust_obj;
    public Sprite[] selectFood_spr;
    public int point_i;
    public Color colorP;
    public GameObject needToast_obj;
    public Text clover_txt;
    //공원상점
    public GameObject shopReform_obj, shopIng_obj,shopSelect_obj, inSelect_obj;
    public GameObject shopNY_obj,met_obj,met2_obj,icebox_obj,shelf_obj;
    public GameObject inYN_obj, inYNImg_obj;
    public Sprite[] shop_spr,in_spr;
    public GameObject shopImg_obj, shopText_obj, ingblock_obj;
    int c_i,h_i;
    int p_i,cv_i;
    public GameObject[] shopSoldout_obj,shopBtn_obj, inSoldout_obj, inBtn_obj, shopGet_obj;
    public GameObject inIceboxTxt_obj;
    //애완동물
    public GameObject buyPetYN_obj;
    public GameObject petMarimo_obj,petRabbit_obj,petTutle_obj,petFish_obj;
    public GameObject[] adopt_obj;
    public Sprite[] pet_spr;
    public Sprite adopt_spr;
    public Text pet_txt,petP_txt;
    public Text petClover_txt;
    //소리
    public GameObject audio_obj;
    //도움말
    public GameObject help_obj;
    public Sprite[] help_spr;

    //밤식물
    public GameObject flowerColor_obj,flowerBuy_obj;
    public int[] flowerPriceH_i, flowerPriceCv_i;
    public Text flowerCv_txt,flowerH_txt;
    int mc = 0;
    // Use this for initialization
    void Start () {
        colorP = new Color(1f, 1f, 1f);
        str = PlayerPrefs.GetString("code", "");
        paintImg();
    }

    public void allClose()
    {
        eventPaint_obj.SetActive(false);
        eventPet_obj.SetActive(false);
        foodShop_obj.SetActive(false);
        inShop_obj.SetActive(false);
        basicShop_obj.SetActive(false);
        blackClose_obj.SetActive(false);
        help_obj.SetActive(false);
        buyPetYN_obj.SetActive(false);
        buyPaintYN_obj.SetActive(false);
        inYN_obj.SetActive(false);
        buyPaintN();
    }

    void OpenClose()
    {
        blackClose_obj.SetActive(true);
    }

    /// <summary>
    /// 거리의화가
    /// </summary>
    public void OpenPaintShop()
    {
        OpenClose();
        eventPaint_obj.SetActive(true);
        paintPrice_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
    }
    //그림 랜덤 바꿔주기
    void paintImg()
    {
        //기본값이9이고 9일경우 다시랜덤을돌린다
        //아직수집못한것만 값을 넣어준다

        //명화
        special_i = 9;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("paintp"+i, 0) == 0)
            {
                eventPaint_i[i] = i;
            }
        }
        special_i = eventPaint_i[Random.Range(0, 4)];
        for (int i = 0; i < 4; i++)
        {
            if (special_i == 9)
            {
                special_i = eventPaint_i[Random.Range(i, 4)];
            }
        }
        if (special_i == 9)
        {
            for (int i = 0; i < 4; i++)
            {
                if (eventPaint_i[i] != 9)
                {
                    special_i = eventPaint_i[i];
                }
            }
        }
        if (special_i != 9)
        {
            eventPaintImg_obj[0].GetComponent<Image>().sprite = specialPaint_spr[special_i];
        }
        else
        {
            eventPaintImg_obj[0].GetComponent<Image>().sprite = soldOut_spr[1];
            eventPaintImg_obj[0].GetComponent<Button>().interactable = false;
        }

        //영화
        for (int i = 0; i < 4; i++)
        {
            eventPaint_i[i] = 9;
            if (PlayerPrefs.GetInt("paintm" + i, 0) == 0)
            {
                eventPaint_i[i] = i;
            }
        }
        movie_i = eventPaint_i[Random.Range(0, 4)];
        for (int i = 0; i < 4; i++)
        {
            if (movie_i == 9)
            {
                movie_i = eventPaint_i[Random.Range(i, 4)];
            }
        }
        if (movie_i == 9)
        {
            for (int i = 0; i < 4; i++)
            {
                if (eventPaint_i[i] != 9)
                {
                    movie_i = eventPaint_i[i];
                }
            }
        }
            

        if (movie_i != 9)
        {
            eventPaintImg_obj[1].GetComponent<Image>().sprite = moviePaint_spr[movie_i];
        }
        else
        {
            eventPaintImg_obj[1].GetComponent<Image>().sprite = soldOut_spr[1];
            eventPaintImg_obj[1].GetComponent<Button>().interactable = false;
        }

        //스토리
        story_i =PlayerPrefs.GetInt("paints", 0);
        if(PlayerPrefs.GetInt("paints", 0) == 10)
        {
            eventPaintImg_obj[2].GetComponent<Image>().sprite = soldOut_spr[1];
            eventPaintImg_obj[2].GetComponent<Button>().interactable = false;
        }
        else
        {
            eventPaintImg_obj[2].GetComponent<Image>().sprite = storyPaint_spr[story_i];
        }

    }
    public void buyMovie()
    {
        paint_str = "m";
        paint_i = movie_i;
        if (paint_i != 9)
        {
            buyPaintYN_obj.SetActive(true);
            paint_txt.text = specialPaint_str[movie_i];
            paintP_txt.text = "15";
        }
    }
    public void buySpecial()
    {
        paint_str = "p";
        paint_i = special_i;
        if (paint_i != 9)
        {
            buyPaintYN_obj.SetActive(true);
            paint_txt.text = moviePaint_str[special_i];
            paintP_txt.text = "15";
        }
    }
    public void buyStory()
    {
        paint_str = "s";
        paint_i = story_i;
        if (paint_i != 10)
        {
            buyPaintYN_obj.SetActive(true);
            paint_txt.text = storyPaint_str[story_i];
            paintP_txt.text = ""+PlayerPrefs.GetInt("pra", 4);
        }
    }


    public void buyPaintY()
    {
        p_i = PlayerPrefs.GetInt(str + "cv", 0);
        int pra = 15;
        if (paint_str == "s")
        {
            pra = PlayerPrefs.GetInt("pra", 4);
        }
        if (p_i >= pra)
        {
            p_i = p_i - pra;
            PlayerPrefs.SetInt(str + "cv", p_i);
            if (paint_str == "s")
            {
                PlayerPrefs.SetInt("paints", story_i + 1);
            }
            else
            {
                PlayerPrefs.SetInt("paint" + paint_str + paint_i, 1);
            }
            
            if (paint_str == "p")
            {
                eventPaintImg_obj[0].GetComponent<Image>().sprite = soldOut_spr[0];
                eventPaintImg_obj[0].GetComponent<Button>().interactable = false;
            }
            else if (paint_str == "m")
            {
                eventPaintImg_obj[1].GetComponent<Image>().sprite = soldOut_spr[0];
                eventPaintImg_obj[1].GetComponent<Button>().interactable = false;
            }
            else if (paint_str == "s")
            {
                eventPaintImg_obj[2].GetComponent<Image>().sprite = soldOut_spr[0];
                eventPaintImg_obj[2].GetComponent<Button>().interactable = false;
                pra = PlayerPrefs.GetInt("pra", 4) + 4;
                PlayerPrefs.SetInt("pra", pra);
            }
            PlayerPrefs.SetInt("paintinroom", 1);
            buyPaintYN_obj.SetActive(false);
            paintPrice_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
            PlayerPrefs.Save();
        }
        else
        {
            needMoney();
        }
    }
    public void buyPaintN()
    {
        buyPaintYN_obj.SetActive(false);
    }

    public void OpenPetShop()
    {
        //조건에따라 비활성화
        //마리모 책상 금붕어 정수기 토끼 침대 거북이 조건없음
        OpenClose();
        buyPetYN_obj.SetActive(false);
        eventPet_obj.SetActive(true);
        if (PlayerPrefs.GetInt("marimo", 0) == 1)
        {
            adopt_obj[0].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[0].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("rabbit", 0) == 1)
        {
            adopt_obj[1].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[1].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("tutle", 0) == 1)
        {
            adopt_obj[2].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[2].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("goldfish", 0) == 1)
        {
            adopt_obj[3].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[3].GetComponent<Button>().interactable = false;
        }
        petClover_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
    }
    public void OpenFoodShop()
    {
        clover_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
        OpenClose();
        foodShop_obj.SetActive(true);
    }
    public void OpenInShop()
    {
        inShop_obj.SetActive(true);
    }
    public void OpenBasicShop()
    {
        shopLoad();
        OpenClose();
        basicShop_obj.SetActive(true);
        
    }
    /// <summary>
    /// 상점 불러오기
    /// </summary>
    void shopLoad()
    {
        coldRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
        hotRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
        //아직못사는 물건에 테이프
        if (PlayerPrefs.GetInt("mat1lv", 0) < 4)
        {
            met_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("mat2lv", 0) < 4)
        {
            met2_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("shelflv", 0) < 3)
        {
            shelf_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("iceboxlv", 0) < 1)
        {
            icebox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 2)
        {
            shopBtn_obj[3].GetComponent<Button>().interactable = false;
            shopSoldout_obj[3].SetActive(true);
            shopGet_obj[3].SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("shoppalette"+i, 0) == 1)
            {
                shopBtn_obj[i].GetComponent<Button>().interactable = false;
                shopSoldout_obj[i].SetActive(true);
                shopGet_obj[i].SetActive(false);
            }
        }
    }
    
    //상점
    public void BuyShop()
    {
        shopSelect_obj.GetComponent<Image>().sprite = shop_spr[shopNum];
        shopNY_obj.SetActive(true);
        shopImg_obj.SetActive(true);
        shopText_obj.SetActive(false);
        if (shopNum == 3)
        {
            shopImg_obj.SetActive(false);
            shopText_obj.SetActive(true);
        }
    }
    public void BuyShopY()
    {
        p_i = PlayerPrefs.GetInt(str + "h", 0);
        c_i = PlayerPrefs.GetInt(str + "c", 0);
        //도어,부엌,선반,아이스박스0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 100&& c_i>=900)
                {
                    p_i = p_i - 100;
                    c_i = c_i - 900;
                    //PlayerPrefs.GetInt("mat1lv", 5);
                    PlayerPrefs.SetInt("shoppalette"+shopNum,1);
                    PlayerPrefs.SetInt("shoppalette"+shopNum + "0", 1);
                    PlayerPrefs.SetInt("reformshop", 1);
                    PlayerPrefs.SetInt("setmatpalette", 1);
                    shopBtn_obj[0].GetComponent<Button>().interactable = false;
                    shopSoldout_obj[0].SetActive(true);
                    shopGet_obj[0].SetActive(false);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 100 && c_i >= 900)
                {
                    p_i = p_i - 100;
                    c_i = c_i - 900;
                    //PlayerPrefs.GetInt("mat2lv", 5);
                    PlayerPrefs.SetInt("shoppalette" + shopNum, 1);
                    PlayerPrefs.SetInt("shoppalette" + shopNum + "0", 1);
                    PlayerPrefs.SetInt("reformshop", 1);
                    PlayerPrefs.SetInt("setmat2palette", 1);
                    shopBtn_obj[1].GetComponent<Button>().interactable = false;
                    shopSoldout_obj[1].SetActive(true);
                    shopGet_obj[1].SetActive(false);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 200 && c_i >= 1000)
                {
                    p_i = p_i - 200;
                    c_i = c_i - 1000;
                    //PlayerPrefs.GetInt("shelflv", 4);
                    PlayerPrefs.SetInt("shoppalette" + shopNum, 1);
                    PlayerPrefs.SetInt("shoppalette" + shopNum + "0", 1);
                    PlayerPrefs.SetInt("reformshop", 1);
                    PlayerPrefs.SetInt("setshelfpalette", 1);
                    shopBtn_obj[2].GetComponent<Button>().interactable = false;
                    shopSoldout_obj[2].SetActive(true);
                    shopGet_obj[2].SetActive(false);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 250 && c_i >= 2000)
                {
                    p_i = p_i - 250;
                    c_i = c_i - 2000;
                    PlayerPrefs.SetInt("iceboxlv", 2);
                    shopBtn_obj[3].GetComponent<Button>().interactable = false;
                    shopSoldout_obj[3].SetActive(true);
                    shopGet_obj[3].SetActive(false);
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
        }
        shopNY_obj.SetActive(false);
    }
    void shopOk()
    {
        PlayerPrefs.SetInt(str + "h", p_i);
        PlayerPrefs.SetInt(str + "c", c_i);
        coldRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
        hotRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
    }
    public void OpenRe()
    {
        shopReform_obj.SetActive(true);
        shopIng_obj.SetActive(false);
    }
    public void OpenIn()
    {
        if(PlayerPrefs.GetInt("iceboxlv", 0) >= 2)
        {
            ingblock_obj.SetActive(false);
        }
        shopReform_obj.SetActive(false);
        shopIng_obj.SetActive(true);
        if (PlayerPrefs.GetInt("bread", 0) == 1)
        {
            inSoldout_obj[0].SetActive(true);
            inBtn_obj[0].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("ham", 0) == 1)
        {
            inSoldout_obj[1].SetActive(true);
            inBtn_obj[1].GetComponent<Button>().interactable = false;
        }
    }
    public void buyShopN()
    {
        shopNY_obj.SetActive(false);
    }

    //재료
    public void buyIn()
    {
        inSelect_obj.GetComponent<Image>().sprite = in_spr[shopNum];
        inYN_obj.SetActive(true);
    }
    public void buyInY()
    {
        p_i = PlayerPrefs.GetInt(str + "h", 0);
        c_i = PlayerPrefs.GetInt(str + "c", 0);
        //빵,햄0~1
        switch (shopNum)
        {
            case 0:
                if (p_i >= 100 && c_i >= 1000)
                {
                    p_i = p_i - 100;
                    c_i = c_i - 1000;
                    PlayerPrefs.SetInt("bread", 1);
                    inSoldout_obj[0].SetActive(true);
                    inBtn_obj[0].GetComponent<Button>().interactable = false;
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 120 && c_i >= 800)
                {
                    p_i = p_i - 120;
                    c_i = c_i - 800;
                    PlayerPrefs.SetInt("ham", 1);
                    inSoldout_obj[1].SetActive(true);
                    inBtn_obj[1].GetComponent<Button>().interactable = false;
                    shopOk();
                }
                else
                {
                    needMoney();
                }
                break;
        }
        inYN_obj.SetActive(false);
    }
    public void buyInN()
    {
        inYN_obj.SetActive(false);
    }

    //야시장
    public void BuyFoodShop()
    {
        foodBuy_obj.SetActive(true);
        selectFood_obj.GetComponent<Image>().sprite = selectFood_spr[shopNum];
    }
    public void BuyFoodShopY()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        p_i = PlayerPrefs.GetInt(str1 + "cv", 0);
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        //붕어빵,핫도그,닭강정,문어빵0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 6)
                {
                    p_i = p_i - 6;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 7;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 8)
                {
                    p_i = p_i - 8;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 10;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 12)
                {
                    p_i = p_i - 12;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 14;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 10)
                {
                    p_i = p_i - 10;
                    PlayerPrefs.SetInt(str1 + "cv", p_i);
                    point_i = point_i + 12;
                    BuyFoodOk();
                }
                else
                {
                    needMoney();
                }
                break;
        }
        foodBuy_obj.SetActive(false);
    }
    void BuyFoodOk()
    {
        clover_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
        PlayerPrefs.SetInt("lovepoint", point_i);
        allClose();
        foodIlust_obj.SetActive(true);
        audio_obj.GetComponent<SoundEvt>().foodSound();
        PlayerPrefs.Save();
    }
    public void CloseFoodIlust()
    {
        foodIlust_obj.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().buttonSound();
    }
    public void BuyFoodShopN()
    {
        foodBuy_obj.SetActive(false);
    }

    //애완동물
    public void pet0()
    {
        //마리모
        eventPet_obj.GetComponent<Image>().sprite = pet_spr[0];
        petMarimo_obj.SetActive(true);
        petRabbit_obj.SetActive(false);
        petTutle_obj.SetActive(false);
        petFish_obj.SetActive(false);
        shopNum = 0;
    }
    public void pet1()
    {
        //토끼
        eventPet_obj.GetComponent<Image>().sprite = pet_spr[1];
        petMarimo_obj.SetActive(false);
        petRabbit_obj.SetActive(true);
        petTutle_obj.SetActive(false);
        petFish_obj.SetActive(false);
        shopNum = 1;
    }
    public void pet2()
    {
        //거북이
        eventPet_obj.GetComponent<Image>().sprite = pet_spr[2];
        petMarimo_obj.SetActive(false);
        petRabbit_obj.SetActive(false);
        petTutle_obj.SetActive(true);
        petFish_obj.SetActive(false);
        shopNum = 2;
    }
    public void pet3()
    {
        //금붕어
        eventPet_obj.GetComponent<Image>().sprite = pet_spr[3];
        petMarimo_obj.SetActive(false);
        petRabbit_obj.SetActive(false);
        petTutle_obj.SetActive(false);
        petFish_obj.SetActive(true);
        shopNum = 3;
    }
    public void buyPetShop()
    {
        buyPetYN_obj.SetActive(true);
        switch (shopNum)
        {
            case 0:
                pet_txt.text = "큰 책상이 필요해";
                petP_txt.text = "10";
                break;
            case 1:
                pet_txt.text = "깨끗한 침구가 필요해";
                petP_txt.text = "20";
                break;
            case 2:
                pet_txt.text = "";
                petP_txt.text = "10";
                break;
            case 3:
                pet_txt.text = "정수기가 필요해";
                petP_txt.text = "15";
                break;
        }
    }
    public void buyPetN()
    {
        buyPetYN_obj.SetActive(false);
    }
    public void buyPetY()
    {
        p_i = PlayerPrefs.GetInt(str + "cv", 0);
        //마리모,토끼,거북이,금붕어0~3
        switch (shopNum)
        {
            case 0:
                if (p_i >= 10)
                {
                    if (PlayerPrefs.GetInt("desklv") >= 5)
                    {
                        p_i = p_i - 10;
                        PlayerPrefs.SetInt(str + "cv", p_i);
                        PlayerPrefs.SetInt("marimo", 1);
                        PlayerPrefs.SetInt("setmarimo", 1);
                        petOk();
                    }
                    else
                    {
                        needMoney();
                    }
                }
                else
                {
                    needMoney();
                }
                break;
            case 1:
                if (p_i >= 20)
                {
                    if (PlayerPrefs.GetInt("bedlv") >= 5)
                    {
                        p_i = p_i - 20;
                        PlayerPrefs.SetInt(str + "cv", p_i);
                        PlayerPrefs.SetInt("rabbit", 1);
                        PlayerPrefs.SetInt("setrabbit", 1);
                        petOk();
                    }
                    else
                    {
                        needMoney();
                    }
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (p_i >= 10)
                {
                    p_i = p_i - 10;
                    PlayerPrefs.SetInt(str + "cv", p_i);
                    PlayerPrefs.SetInt("tutle", 1);
                    PlayerPrefs.SetInt("settutle", 1);
                    petOk();
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (p_i >= 15)
                {
                    if (PlayerPrefs.GetInt("waterpurifiershop") >= 2)
                    {
                        p_i = p_i - 15;
                        PlayerPrefs.SetInt(str + "cv", p_i);
                        PlayerPrefs.SetInt("goldfish", 1);
                        PlayerPrefs.SetInt("setgoldfish", 1);
                        petOk();
                    }
                    else
                    {
                        needMoney();
                    }
                }
                else
                {
                    needMoney();
                }
                break;
        }
        buyPetYN_obj.SetActive(false);
    }
    /// <summary>
    /// 펫 구매성공 공통처리
    /// </summary>
    void petOk()
    {
        audio_obj.GetComponent<SoundEvt>().buttonSound();
        if (PlayerPrefs.GetInt("marimo", 0) == 1)
        {
            adopt_obj[0].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[0].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("rabbit", 0) == 1)
        {
            adopt_obj[1].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[1].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("tutle", 0) == 1)
        {
            adopt_obj[2].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[2].GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("goldfish", 0) == 1)
        {
            adopt_obj[3].GetComponent<Image>().sprite = adopt_spr;
            adopt_obj[3].GetComponent<Button>().interactable = false;
        }
        petClover_txt.text = ""+PlayerPrefs.GetInt(str + "cv", p_i);
        PlayerPrefs.SetInt("shopfpethotel",1);
    }
    

    public void OpenHelp()
    {
        help_obj.SetActive(true);
        help_obj.GetComponent<Image>().sprite = help_spr[helpNum];
    }

    public void CloseHelp()
    {
        help_obj.SetActive(false);
    }


    //밤에식물구매 꽃 팔레트 바꾸기
    public void OpenActFlowerColor()
    {
        if (flowerColor_obj.activeSelf == true)
        {
            flowerColor_obj.SetActive(false);
        }
        else
        {
            flowerColor_obj.SetActive(true);
        }
    }
    
    public void BuyActFlowerColor()
    {
        if (flowerBuy_obj.activeSelf == true)
        {
            flowerBuy_obj.SetActive(false);
        }
        else
        {
            flowerBuy_obj.SetActive(true);
        }
    }
    public void BuyActFlowerPotColor()
    {
        if (flowerBuy_obj.activeSelf == true)
        {
            flowerBuy_obj.SetActive(false);
        }
        else
        {
            flowerBuy_obj.SetActive(true);
        }
    }

    public void BuyFlowerY()
    {
        mc = 0;
        flowerOk();
        if (mc == 1)
        {
            switch (shopNum)
            {
                //파랑 진노랑 핑크 보라
                case 0://파랑
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflower", shopNum);
                    break;
                case 1://분홍
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflower", shopNum);
                    break;
                case 2://보라
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflower", shopNum);
                    break;
                case 3://노랑
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflower", shopNum);
                    break;
            }
            PlayerPrefs.SetInt("getflowerpalette", 1);
            PlayerPrefs.Save();
        }
    }

    public void BuyFlowerPotY()
    {
        mc = 0;
        flowerOk();
        if (mc == 1)
        {
            switch (shopNum)
            {
                case 0://파랑
                    PlayerPrefs.SetInt("flowerpotpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflowerpot", shopNum);
                    break;
                case 1://분홍
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflowerpot", shopNum);
                    break;
                case 2://보라
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflowerpot", shopNum);
                    break;
                case 3://힌색
                    PlayerPrefs.SetInt("flowerpalette" + shopNum, 1);
                    PlayerPrefs.SetInt("setflowerpot", shopNum);
                    break;
            }
            PlayerPrefs.SetInt("getflowerpalette", 1);
            PlayerPrefs.Save();
        }
    }

    void flowerOk()
    {
        cv_i = PlayerPrefs.GetInt(str + "cv", 0);
        h_i = PlayerPrefs.GetInt(str + "h", 0);
        if (h_i >= 200&& cv_i >= 8)
        {
            h_i = h_i - 200;
            cv_i = cv_i - 8;
            flowerCv_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
            flowerH_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);

            PlayerPrefs.SetInt(str + "cv", cv_i);
            PlayerPrefs.SetInt(str + "h", h_i);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
            mc = 1;
        }
        else
        {
            needMoney();
            mc = 0;
        }
    }

    void flowerPotOk()
    {
        cv_i = PlayerPrefs.GetInt(str + "cv", 0);
        h_i = PlayerPrefs.GetInt(str + "h", 0);
        if (h_i >= 150 && cv_i >= 5)
        {
            h_i = h_i - 150;
            cv_i = cv_i - 5;
            flowerCv_txt.text = "" + PlayerPrefs.GetInt(str + "cv", 0);
            flowerH_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);

            PlayerPrefs.SetInt(str + "cv", cv_i);
            PlayerPrefs.SetInt(str + "h", h_i);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
            mc = 1;
        }
        else
        {
            needMoney();
            mc = 0;
        }
    }


    //꽃팔레트가격
    void price()
    {
        flowerPriceH_i[0] = 0;
        flowerPriceCv_i[0] = 0;
        flowerPriceH_i[1] = 1;
        flowerPriceCv_i[1] = 1;
        flowerPriceH_i[2] = 2;
        flowerPriceCv_i[2] = 2;
        flowerPriceH_i[3] = 3;
        flowerPriceCv_i[3] = 3;
        flowerPriceH_i[4] = 4;
        flowerPriceCv_i[4] = 4;
        flowerPriceH_i[5] = 5;
        flowerPriceCv_i[5] = 5;
    }


    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorP.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorP;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorP.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorP;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }
    //꽃
    IEnumerator toastFlowerFadeOut()
    {
        colorP.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorP;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorP.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorP;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }


    /// <summary>
    /// 상점넘버
    /// </summary>
    #region
    public void num0()
    {
        shopNum = 0;
    }
    public void num1()
    {
        shopNum = 1;
    }
    public void num2()
    {
        shopNum = 2;
    }
    public void num3()
    {
        shopNum = 3;
    }
    public void num4()
    {
        shopNum = 4;
    }
    void num5()
    {
        shopNum = 5;
    }
    public void numH0()
    {
        helpNum = 0;
    }
    public void numH1()
    {
        helpNum = 1;
    }
    public void numH2()
    {
        helpNum = 2;
    }
    public void numH3()
    {
        helpNum = 3;
    }
    public void numH4()
    {
        helpNum = 4;
    }
    #endregion
}

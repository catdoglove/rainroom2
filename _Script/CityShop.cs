using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour {
    public GameObject reformShopWin_obj,paint_obj, fabric_obj, paintBtn_obj, fabricBtn_obj, fabricBuy_obj, paintBuy_obj;
    public GameObject interiorWin_obj,interiorBuy_obj;
    public GameObject[] interiorTape_obj, paintTape_obj, fabricTape_obj;
    public Sprite[] paintBtn_spr, fabricBtn_spr;

    public GameObject audio_obj, needToast_obj;
    Color colorP;

    //가격
    public int itemIndex_i, price_i,chip_i;
    public int[] fabricH_i, fabricD_i;
    public int interiorC_i, interiorH_i;
    public string interior_str;
    string str;
    int coldRain_i, hotRain_i,diamond_i;
    public Text cRain_txt,hRain_txt,diamond_txt, cIRain_txt, hIRain_txt;
    public int[] paintPriceC_i, paintPriceD_i;

    public int[] bedPriceH_i, bedPriceC_i, deskPriceH_i, deskPriceC_i, lightPriceH_i, lightPriceC_i, icePriceH_i, icePriceC_i,gasPriceH_i, gasPriceC_i;
    public Text paintBuyC_txt, paintBuyD_txt, fabricBuyH_txt, fabricBuyD_txt, interiorBuyC_txt, interiorBuyH_txt;
    public Sprite[] reformItem_spr;
    public GameObject paintItem_obj, fabricItem_obj;
    public Text InteriorItem_txt;
    //솔드아웃
    public GameObject[] soldInterior_obj, soldPaint_obj, soldFabric_obj;
    public Sprite soldOut_spr;
    //비활성화
    public Button[] paint0_btn, paint1_btn, paint2_btn, paint3_btn, paint4_btn, paint5_btn, paint6_btn, paint7_btn, paint8_btn, paint9_btn, paint10_btn, paint11_btn, paint12_btn;
    // Use this for initialization
    void Start () {
        str = PlayerPrefs.GetString("code", "");
        colorP= new Color(1f, 1f, 1f);
        Price();
    }
    //리폼상점창 열기
    public void OpenActReform()
    {
        paintBuy_obj.SetActive(false);
        fabricBuy_obj.SetActive(false);
        if (reformShopWin_obj.activeSelf == true)
        {
            reformShopWin_obj.SetActive(false);
        }
        else
        {
            for(int i = 0; i < 6; i++)
            {
                fabricTape_obj[i].SetActive(false);
                paintTape_obj[i].SetActive(false);
            }
            cRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            diamond_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);
            reformShopWin_obj.SetActive(true);
            SetPaint();
            SetFabric();
            CheckMaxPaint();
        }
    }

    //페인트샵
    public void SwitchPaint()
    {
        paintBtn_obj.GetComponent<Image>().sprite = paintBtn_spr[1];
        fabricBtn_obj.GetComponent<Image>().sprite = fabricBtn_spr[0];
        paint_obj.SetActive(true);
        fabric_obj.SetActive(false);
    }

    //원단샵
    public void SwitchFabric()
    {
        paintBtn_obj.GetComponent<Image>().sprite = paintBtn_spr[0];
        fabricBtn_obj.GetComponent<Image>().sprite = fabricBtn_spr[1];
        paint_obj.SetActive(false);
        fabric_obj.SetActive(true);
    }
    
    //인테리어 열기
    public void OpenActInterior()
    {
        interiorBuy_obj.SetActive(false);
        if (interiorWin_obj.activeSelf == true)
        {
            interiorWin_obj.SetActive(false);
        }
        else
        {
            cIRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hIRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);

            SetInterior();
            interiorWin_obj.SetActive(true);
        }
    }

    void SetInterior()
    {
        //물건 업그래이드 단계확인 부족하면 테이프로 가리기
        //침대 5레벨 일때 가능
        if (PlayerPrefs.GetInt("bedlv", 0) < 5)
        {
            interiorTape_obj[0].SetActive(true);
        }
        //냉장고 2레벨일때 가능
        if (PlayerPrefs.GetInt("iceboxlv", 0) < 2)
        {
            interiorTape_obj[1].SetActive(true);
        }
        //테이블 3레벨일때 가능
        if (PlayerPrefs.GetInt("desklv", 0) < 3)
        {
            interiorTape_obj[2].SetActive(true);
        }
        //전구? 4레벨일때 가능
        if (PlayerPrefs.GetInt("lightlv", 0) < 4)
        {
            interiorTape_obj[3].SetActive(true);
        }
        //가스렌지 1레벨
        interiorTape_obj[4].SetActive(false);
        if (PlayerPrefs.GetInt("gasrangelv", 0) < 1 || PlayerPrefs.GetInt("iceboxlv", 0) < 3)
        {
            interiorTape_obj[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("gasrangelv", 0) > 2 && PlayerPrefs.GetInt("iceboxlv", 0) < 4)
        {
            interiorTape_obj[4].SetActive(true);
        }

        //다 팔렸을때 솔드아웃
        CheckMaxInterior();
    }

    public void icebox()
    {
        itemIndex_i = 0;
        interior_str = "iceboxlv";
        int inm = PlayerPrefs.GetInt(interior_str, 0);
        inm = inm - 2;
        interiorC_i = icePriceC_i[inm];
        interiorH_i = icePriceH_i[inm];
        int hm = PlayerPrefs.GetInt(interior_str, 0);

        InteriorItem_txt.text = "";
        if (hm == 2)
        {
            InteriorItem_txt.text = "미니냉장고";
        }

        if (hm == 3)
        {
            InteriorItem_txt.text = "2단냉장고";
        }
        
    }
    public void bed()
    {
        itemIndex_i = 1;
        interior_str = "bedmaxlv";
        int inm = PlayerPrefs.GetInt(interior_str, 0);
        interiorC_i = bedPriceC_i[inm];
        interiorH_i = bedPriceH_i[inm];
        int hm = PlayerPrefs.GetInt(interior_str, 0);
        InteriorItem_txt.text = "";
        if (hm == 0)
        {
            InteriorItem_txt.text = "낡은 침대";
        }

        if (hm == 1)
        {
            InteriorItem_txt.text = "완전한 침대";
        }

    }
    public void desk()
    {

        itemIndex_i = 2;
        interior_str = "desklv";
        int inm = PlayerPrefs.GetInt(interior_str, 0);
        inm = inm - 3;
        interiorC_i = deskPriceC_i[inm];
        interiorH_i = deskPriceH_i[inm];
        int hm = PlayerPrefs.GetInt(interior_str, 0);

        InteriorItem_txt.text = "";
        if (hm == 3)
        {
            InteriorItem_txt.text = "플라스틱 탁자";
        }
        if (hm == 4)
        {
            InteriorItem_txt.text = "작은 책상";
        }
        if (hm == 5)
        {
            InteriorItem_txt.text = "완전한 책상";
        }
    }
    public void lights()
    {

        itemIndex_i = 3;
        interior_str = "lightmaxlv";
        int inm = PlayerPrefs.GetInt(interior_str, 0);
        interiorC_i = lightPriceC_i[inm];
        interiorH_i = lightPriceH_i[inm];
        int hm = PlayerPrefs.GetInt(interior_str, 0);

        InteriorItem_txt.text = "";
        if (hm == 0)
        {
            InteriorItem_txt.text = "좋은 전등";
        }
        if (hm == 1)
        {
            InteriorItem_txt.text = "완전한 전등";
        }
    }
    public void gas()
    {
        itemIndex_i = 4;
        interior_str = "gasrangelv";
        int inm = PlayerPrefs.GetInt(interior_str, 0);
        inm = inm - 1;
        interiorC_i = gasPriceC_i[inm];
        interiorH_i = gasPriceH_i[inm];
        int hm = PlayerPrefs.GetInt(interior_str, 0);
        InteriorItem_txt.text = "";
        if (hm == 1)
        {
            InteriorItem_txt.text = "낡은 렌지";
        }
        if (hm == 2)
        {
            InteriorItem_txt.text = "평범한 렌지";
        }
        if (hm == 3)
        {
            InteriorItem_txt.text = "투박한 가스렌지";
        }
        if (hm == 4)
        {
            InteriorItem_txt.text = "좋은 가스렌지";
        }
        Debug.Log(InteriorItem_txt + "," + hm);
    }

    public void InteriorY()
    {

        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);

        if (coldRain_i >= interiorC_i && hotRain_i >= interiorH_i)
        {
            coldRain_i = coldRain_i - interiorC_i;
            hotRain_i = hotRain_i - interiorH_i;

            PlayerPrefs.SetInt(str + "h", hotRain_i);
            PlayerPrefs.SetInt(str + "c", coldRain_i);

            cIRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hIRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            int inum = PlayerPrefs.GetInt(interior_str, 0);
            inum++;
            PlayerPrefs.SetInt(interior_str, inum);
            BuyInterior();
            CheckMaxInterior();
            SetInterior();
        }
        else
        {
            needMoney();
        }
    }
    void CheckMaxInterior()
    {

        //침대 완전한 2레벨
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 2)
        {
            //soldInterior_obj[1].SetActive(true);
            interiorTape_obj[0].SetActive(true);
            interiorTape_obj[0].GetComponent<Image>().sprite = soldOut_spr;
        }
        //냉장고 4
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 4)
        {
            //soldInterior_obj[0].SetActive(true);
            interiorTape_obj[1].SetActive(true);
            interiorTape_obj[1].GetComponent<Image>().sprite = soldOut_spr;
        }
        //테이블 6레벨
        if (PlayerPrefs.GetInt("desklv", 0) >= 6)
        {
            //soldInterior_obj[2].SetActive(true);
            interiorTape_obj[2].SetActive(true);
            interiorTape_obj[2].GetComponent<Image>().sprite = soldOut_spr;
        }
        //전구 완전한 2레벨
        if (PlayerPrefs.GetInt("lightmaxlv", 0) >= 2)
        {
            //soldInterior_obj[3].SetActive(true);
            interiorTape_obj[3].SetActive(true);
            interiorTape_obj[3].GetComponent<Image>().sprite = soldOut_spr;
        }
        //가스렌지 2레벨
        if (PlayerPrefs.GetInt("gasrangelv", 0) >= 5)
        {
            //soldInterior_obj[3].SetActive(true);
            interiorTape_obj[4].SetActive(true);
            interiorTape_obj[4].GetComponent<Image>().sprite = soldOut_spr;
        }
    }
    void CheckMaxPaint()
    {
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 0 + j, 0) == 1)
            {
                paint0_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 1 + j, 0) == 1)
            {
                paint1_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 2 + j, 0) == 1)
            {
                paint2_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 3 + j, 0) == 1)
            {
                paint3_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 4 + j, 0) == 1)
            {
                paint4_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 5 + j, 0) == 1)
            {
                paint5_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 6 + j, 0) == 1)
            {
                paint6_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 7 + j, 0) == 1)
            {
                paint7_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 8 + j, 0) == 1)
            {
                paint8_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 9 + j, 0) == 1)
            {
                paint9_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 10 + j, 0) == 1)
            {
                paint10_btn[j].GetComponent<Button>().interactable = false;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.GetInt("shoppalette" + 11 + j, 0) == 1)
            {
                paint11_btn[j].GetComponent<Button>().interactable = false;
            }
        }
    }

    //살까? 창 열어주기
    public void BuyInterior()
    {
        if (interiorBuy_obj.activeSelf == true)
        {
            interiorBuy_obj.SetActive(false);
        }
        else
        {
            interiorBuyC_txt.text = "" + interiorC_i;
            interiorBuyH_txt.text = "" + interiorH_i;
            interiorBuy_obj.SetActive(true);
            //interiorItem_obj.GetComponent<Image>().sprite = InteriorItem_spr[itemIndex_i];
        }
    }
    public void BuyPaint()
    {
        if (paintBuy_obj.activeSelf == true)
        {
            paintBuy_obj.SetActive(false);
        }
        else
        {
            paintBuyC_txt.text = "" + fabricH_i[itemIndex_i];
            paintBuyD_txt.text = "" + fabricD_i[itemIndex_i];
            paintBuy_obj.SetActive(true);
            paintItem_obj.GetComponent<Image>().sprite = reformItem_spr[itemIndex_i];
        }
    }
    public void BuyFabric()
    {

        if (fabricBuy_obj.activeSelf == true)
        {
            fabricBuy_obj.SetActive(false);
        }
        else
        {
            fabricBuyH_txt.text = "" + fabricH_i[itemIndex_i];
            fabricBuyD_txt.text = "" + fabricD_i[itemIndex_i];
            fabricBuy_obj.SetActive(true);
            fabricItem_obj.GetComponent<Image>().sprite = reformItem_spr[itemIndex_i];
        }
    }

    void SetPaint()
    {
        //matImg2_obj.GetComponent<Image>().color = new Color(99, 99, 99);
        //물건 업그래이드 단계확인 부족하면 테이프로 가리기
        //장식장 4레벨 일때 가능
        if (PlayerPrefs.GetInt("drawerlv", 0) < 4)
        {
            paintTape_obj[0].SetActive(true);
        }
        //선반 2레벨일때 가능
        if (PlayerPrefs.GetInt("shoppalette2", 0) == 0)
        {
            paintTape_obj[1].SetActive(true);
        }
        //테이블 3레벨일때 가능
        if (PlayerPrefs.GetInt("desklv", 0) < 6)
        {
            paintTape_obj[2].SetActive(true);
        }
        //서랍장 6레벨일때 가능
        if (PlayerPrefs.GetInt("cabinetlv", 0) < 6)
        {
            paintTape_obj[3].SetActive(true);
        }
        //책장 15레벨일때 가능
        if (PlayerPrefs.GetInt("booklv", 0) < 15)
        {
            paintTape_obj[4].SetActive(true);
        }
        //전구 완전샵에서 2일때가능
        if (PlayerPrefs.GetInt("lightmaxlv", 0) < 2)
        {
            paintTape_obj[5].SetActive(true);
        }
    }

    void SetFabric()
    {
        //도어,부엌,선반,전구0~3  창문,장식장,책장,침대,테이블 4,5,6,7,8 벽지,러그,서랍장,가스렌지 9,10,11,12
        //물건 업그래이드 단계확인 부족하면 테이프로 가리기
        //부엌매트 4레벨 일때 가능
        if (PlayerPrefs.GetInt("shoppalette" + 1, 0) != 1)
        {
            fabricTape_obj[0].SetActive(true);
        }
        //도어매트 4레벨일때 가능
        if (PlayerPrefs.GetInt("shoppalette" + 0, 0) != 1)
        {
            fabricTape_obj[1].SetActive(true);
        }
        //창문 8레벨일때 가능
        if (PlayerPrefs.GetInt("windowlv", 0) < 8)
        {
            fabricTape_obj[2].SetActive(true);
        }
        //러그 3레벨일때 가능
        if (PlayerPrefs.GetInt("ruglv", 0) < 3)
        {
            fabricTape_obj[3].SetActive(true);
        }
        //침대 완전샵에서 구매후 2단계일때 가능
        if (PlayerPrefs.GetInt("bedmaxlv", 0) < 2)
        {
            fabricTape_obj[4].SetActive(true);
        }
        //벽지 3레벨일때 가능
        if (PlayerPrefs.GetInt("walllv", 0) < 3)
        {
            fabricTape_obj[5].SetActive(true);
        }
    }

    //원단샵 온수,다이아
    public void FabricY()
    {
        hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        diamond_i = PlayerPrefs.GetInt(str + "dm", 0);

        

        if (hotRain_i >= fabricH_i[itemIndex_i]&& diamond_i>= fabricD_i[itemIndex_i])
        {

            hotRain_i = hotRain_i - fabricH_i[itemIndex_i];
            diamond_i = diamond_i - fabricD_i[itemIndex_i];

            PlayerPrefs.SetInt(str + "h", hotRain_i);
            PlayerPrefs.SetInt(str + "dm", diamond_i);


            PlayerPrefs.SetInt("shoppalette" + itemIndex_i, 1);
            PlayerPrefs.SetInt("shoppalette" + itemIndex_i + chip_i, 1);
            PlayerPrefs.SetInt("reformshop", 1);
            Setpale();
            CheckMaxPaint();
            BuyFabric();
            cRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            diamond_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);

        }
        else
        {
            needMoney();
        }


    }


    //페인트샵 빗물과 다이아
    public void PaintY()
    {

        coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        diamond_i = PlayerPrefs.GetInt(str + "dm", 0);


        if (coldRain_i >= fabricH_i[itemIndex_i] && diamond_i >= fabricD_i[itemIndex_i])
        {

            coldRain_i = coldRain_i - fabricH_i[itemIndex_i];
            diamond_i = diamond_i - fabricD_i[itemIndex_i];

            PlayerPrefs.SetInt(str + "c", coldRain_i);
            PlayerPrefs.SetInt(str + "dm", diamond_i);

            PlayerPrefs.SetInt("shoppalette" + itemIndex_i, 1);
            PlayerPrefs.SetInt("shoppalette" + itemIndex_i + chip_i, 1);
            PlayerPrefs.SetInt("reformshop", 1);
            Setpale();
            CheckMaxPaint();
            BuyPaint();
            cRain_txt.text = "" + PlayerPrefs.GetInt(str + "c", 0);
            hRain_txt.text = "" + PlayerPrefs.GetInt(str + "h", 0);
            diamond_txt.text = "" + PlayerPrefs.GetInt(str + "dm", 0);
        }
        else
        {
            needMoney();
        }
    }
    


        void Setpale()
    {
        //도어,부엌,선반,전구0~3  창문,장식장,책장,침대,테이블 4,5,6,7,8 벽지,러그,서랍장,가스렌지 9,10,11,12
        chip_i++;
        switch (itemIndex_i)
        {
            case 0:
                PlayerPrefs.SetInt("setmatpalette", chip_i);
                break;
            case 1:
                PlayerPrefs.SetInt("setmat2palette", chip_i);
                break;
            case 2:
                PlayerPrefs.SetInt("setshelfpalette", chip_i);
                break;
            case 3:
                PlayerPrefs.SetInt("setlightpalette", chip_i);
                break;
            case 4:
                PlayerPrefs.SetInt("setwindowpalette", chip_i);
                break;
            case 5:
                PlayerPrefs.SetInt("setdrawerpalette", chip_i);
                break;
            case 6:
                PlayerPrefs.SetInt("setbookpalette", chip_i);
                break;
            case 7:
                PlayerPrefs.SetInt("setbedpalette", chip_i);
                break;
            case 8:
                PlayerPrefs.SetInt("setdeskpalette", chip_i);
                break;
            case 9:
                PlayerPrefs.SetInt("setwallpalette", chip_i);
                break;

            case 10:
                PlayerPrefs.SetInt("setrugpalette", chip_i);
                break;
            case 11:
                PlayerPrefs.SetInt("setcabinetpalette", chip_i);
                break;
            case 12:
                //PlayerPrefs.SetInt("setgaspalette", chip_i);
                break;
        }
        chip_i--;
    }




    public void ChipStr0()
    {
        chip_i = 0;
    }
    public void ChipStr1()
    {
        chip_i = 1;
    }
    public void ChipStr2()
    {
        chip_i = 2;
    }

    //도어,부엌,선반,전구0~3  창문,장식장,책장,침대,테이블 4,5,6,7,8 벽지,러그,서랍장,가스렌지 9,10,11,12

       
    void Price()
    {
        //매트
        fabricH_i[0] = 40;
        fabricD_i[0] = 2;
        fabricH_i[1] = 40;
        fabricD_i[1] = 2;
        //선반전구
        fabricH_i[2] = 200;
        fabricD_i[2] = 3;
        fabricH_i[3] = 1000;
        fabricD_i[3] = 8;
        //창문
        fabricH_i[4] = 180;
        fabricD_i[4] = 10;
        //장식장책장
        fabricH_i[5] = 400;
        fabricD_i[5] = 4;
        fabricH_i[6] = 1400;
        fabricD_i[6] = 15;
        //침대
        fabricH_i[7] = 200;
        fabricD_i[7] = 15;
        //테이블벽지
        fabricH_i[8] = 800;
        fabricD_i[8] = 7;
        fabricH_i[9] = 150;
        fabricD_i[9] = 10;
        //러그
        fabricH_i[10] = 60;
        fabricD_i[10] = 3;
        //서랍장
        fabricH_i[11] = 200;
        fabricD_i[11] = 5;

        //책상
        deskPriceC_i[0] = 2500;
        deskPriceH_i[0] = 220;
        deskPriceC_i[1] = 3000;
        deskPriceH_i[1] = 240;
        deskPriceC_i[2] = 4000;
        deskPriceH_i[2] = 270;

        //전구
        lightPriceC_i[0] = 4200;
        lightPriceH_i[0] = 320;
        lightPriceC_i[1] = 4500;
        lightPriceH_i[1] = 340;

        //냉장고
        icePriceC_i[0] = 3500;
        icePriceH_i[0] = 300;
        icePriceC_i[1] = 4000;
        icePriceH_i[1] = 350;

        //침대
        bedPriceC_i[0] = 6000;
        bedPriceH_i[0] = 450;
        bedPriceC_i[1] = 8000;
        bedPriceH_i[1] = 500;

        //가스렌지
        gasPriceC_i[0] = 2300;
        gasPriceH_i[0] = 210;
        gasPriceC_i[1] = 2700;
        gasPriceH_i[1] = 250;
        gasPriceC_i[2] = 3300;
        gasPriceH_i[2] = 310;
        gasPriceC_i[3] = 4400;
        gasPriceH_i[3] = 360;

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
    

    public void setIndex0()
    {
        itemIndex_i = 0;
    }
    public void setIndex1()
    {
        itemIndex_i = 1;
    }
    public void setIndex2()
    {
        itemIndex_i = 2;
    }
    public void setIndex3()
    {
        itemIndex_i = 3;
    }
    public void setIndex4()
    {
        itemIndex_i = 4;
    }
    public void setIndex5()
    {
        itemIndex_i = 5;
    }
    public void setIndex6()
    {
        itemIndex_i = 6;
    }
    public void setIndex7()
    {
        itemIndex_i = 7;
    }
    public void setIndex8()
    {
        itemIndex_i = 8;
    }

    public void setIndex9()
    {
        itemIndex_i = 9;
    }

    public void setIndex10()
    {
        itemIndex_i = 10;
    }

    public void setIndex11()
    {
        itemIndex_i = 11;
    }

    public void setIndex12()
    {
        itemIndex_i = 12;
    }

    public void setIndex13()
    {
        itemIndex_i = 13;
    }
    public void setIndex14()
    {
        itemIndex_i = 14;
    }
    public void setIndex15()
    {
        itemIndex_i = 15;
    }
}

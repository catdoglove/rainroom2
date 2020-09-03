using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasrangeEvt : MonoBehaviour {

    public GameObject gasrange_obj, iceBox_obj;
    public int egg_i, milk_i, tofu_i, bread_i, mushroom_i, carot_i, ham_i, seeweed_i, cucumber_i, paprika_i, shrimp_i, bam_i;
    public int iceLv_i;

    public GameObject[] cookPage_obj, ingredientPage_obj;
    public GameObject[] cookFood_obj, ingredient_obj;
    public int page_i=0, pageIce_i=0,indexNumber_i;

    public Text ingredient_txt;
    public GameObject text_obj;
    List<Dictionary<string, object>> data;
    

    //요리
    public GameObject cookYN_obj,cookImg_obj,GM2;
    public Sprite[] cook_spr;
    public string[] cook_str;
    public Text cooks_txt;

    public GameObject pen_obj, illust_obj, block_obj;
    public Sprite pen1_spr, pen2_spr;
    public int[] cookPrice_i;
    public int gasLv_i;
    public Text price_txt, allHeart_txt;
    
    public GameObject[] gasrangeLv_obj;

    public Color colorB;
    public GameObject beadalYetToast_obj, beadalTime_obj;
    int point_i;
    public GameObject dishBtn_obj;

    Color colorC, color;
    public GameObject cookHToast_obj;
    public Text cookPage_txt;

    //냉장고
    public GameObject icePage1, icePage2;
    public GameObject[] iceBoxLv_obj;

    public GameObject audio_obj;

    public int a,v = 0;
    int gaspage = 1;

    //시즌 이벤트
    public GameObject fsticker_obj,sToast_obj, wScarf_obj,sWood_obj,sHat_obj;

    //엔딩
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;

    // Use this for initialization
    void Start()
    {

        //요리이름
        CookStrSet();
        colorB = new Color(1f, 1f, 1f);
        colorC = new Color(1f, 1f, 1f);
        color = new Color(1f, 1f, 1f);
        CheckIng();
        data = CSVReader.Read("material");
        if (PlayerPrefs.GetInt("putfallleaf", 0) == 1)
        {
            fsticker_obj.SetActive(true);
        }

        if (PlayerPrefs.GetInt("putwinterc", 0) == 1)
        {
            wScarf_obj.SetActive(true);
        }

        if (PlayerPrefs.GetInt("putwoodflower", 0) == 1)
        {
            sWood_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("putsummerhat", 0) == 1)
        {
            sHat_obj.SetActive(true);
        }
    }

    #region
    public void indexNumber0()
    {
        indexNumber_i = 0;
        point_i = 5;
    }
    public void indexNumber1()
    {
        indexNumber_i = 1;
        point_i = 11;
    }
    public void indexNumber2()
    {
        indexNumber_i = 2;
        point_i = 13;
    }
    public void indexNumber3()
    {
        indexNumber_i = 3;
        point_i = 11;
    }
    public void indexNumber4()
    {
        indexNumber_i = 4;
        point_i = 6;
    }
    public void indexNumber5()
    {
        indexNumber_i = 5;
        point_i = 13;
    }
    public void indexNumber6()
    {
        indexNumber_i = 6;
        point_i = 7;
    }
    public void indexNumber7()
    {
        indexNumber_i = 7;
        point_i = 7;
    }
    public void indexNumber8()
    {
        indexNumber_i = 8;
        point_i = 13;
    }
    public void indexNumber9()
    {
        indexNumber_i = 9;
        point_i = 13;
    }
    public void indexNumber10()
    {
        indexNumber_i = 10;
        point_i = 5;
    }
    public void indexNumber11()
    {
        indexNumber_i = 11;
        point_i = 5;
    }
    #endregion

    void CookStrSet()
    {
        cook_str[0] = "계란프라이";
        cook_str[1] = "계란볶음밥";
        cook_str[2] = "야채볶음";
        cook_str[3] = "샌드위치";
        cook_str[4] = "두부부침";
        cook_str[5] = "소세지빵";
        cook_str[6] = "계란찜";
        cook_str[7] = "미역국";
        cook_str[8] = "오이냉채";
        cook_str[9] = "버섯볶음밥";
        cook_str[10] = "군밤";
        cook_str[11] = "대하구이";
    }
    public void OpenGasrange()
    {

        if (PlayerPrefs.GetInt("gasrangelv", 0) >= 5)
        {
            gaspage = 2;
        }
        if (PlayerPrefs.GetInt("cooked", 0) == 0)
        {
            gasrange_obj.SetActive(true);
            CheckIng();
            if (PlayerPrefs.GetInt("gasrangelv", 0) >= 3)
            {
                gasrangeLv_obj[0].SetActive(true);
                gasrangeLv_obj[1].SetActive(true);
            }

            if (PlayerPrefs.GetInt("gasrangelv", 0) >= 5)
            {
                gasrangeLv_obj[0].SetActive(true);
                gasrangeLv_obj[1].SetActive(true);
                gasrangeLv_obj[2].SetActive(true);//버섯
                gasrangeLv_obj[3].SetActive(true);//미역국
                gasrangeLv_obj[4].SetActive(true);//오이냉채
                gasrangeLv_obj[5].SetActive(true);//두부부침
            }
            a = 0;
            //계란
            if (egg_i == 1)
            {
                cookFood_obj[0].SetActive(true);
                a++;
            }
            //볶음밥
            if (egg_i + ham_i + carot_i == 3)
            {
                cookFood_obj[1].SetActive(true);
                a++;
            }
            //야채복음
            if (carot_i + paprika_i + cucumber_i == 3)
            {
                cookFood_obj[2].SetActive(true);
                a++;
            }
            //샌드위치
            if (ham_i + egg_i + bread_i == 3)
            {
                cookFood_obj[3].SetActive(true);
                a++;
            }
            //두부
            if (tofu_i == 1)
            {
                cookFood_obj[4].SetActive(true);
                a++;
            }
            //소시지빵
            if (bread_i + ham_i == 2)
            {
                cookFood_obj[5].SetActive(true);
                a++;
            }
            //계란찜
            if (milk_i + egg_i == 2)
            {
                cookFood_obj[6].SetActive(true);
                a++;
            }
            //미역국
            if (seeweed_i == 1)
            {
                cookFood_obj[7].SetActive(true);
                a++;
            }
            //오이냉채
            if (cucumber_i + seeweed_i == 2)
            {
                cookFood_obj[8].SetActive(true);
                a++;
            }
            //버섯볶음밥
            if (mushroom_i + carot_i == 2)
            {
                cookFood_obj[9].SetActive(true);
                a++;
            }
            //밤
            if (bam_i == 1)
            {
                cookFood_obj[10].SetActive(true);
            }
            //대하
            if (shrimp_i == 1)
            {
                cookFood_obj[11].SetActive(true);
            }
            if (a == 10)
            {
                achv();
            }
        }
        else
        {
            StopCoroutine("toastBImgFadeOut");
            beadalYetToast_obj.SetActive(true);
            StartCoroutine("toastBImgFadeOut");
            //아직배부름
        }

    }

    public void CloseIceBox()
    {

        cookYN_obj.SetActive(false);
        iceBox_obj.SetActive(false);
        gasrange_obj.SetActive(false);
    }
    public void Closefood()
    {
        cookYN_obj.SetActive(false);
    }

    public void foodShow()
    {
        string str1 = PlayerPrefs.GetString("code", "");
        int ht = PlayerPrefs.GetInt(str1 + "ht", 0);
        cookYN_obj.SetActive(true);
        //cookImg_obj.GetComponent<Image>().sprite = cook_spr[indexNumber_i];
        cooks_txt.text = cook_str[indexNumber_i];
        price_txt.text = "" + cookPrice_i[indexNumber_i];
        allHeart_txt.text = "" + ht;
    }

    public void foodY()
    {
        string str1 = PlayerPrefs.GetString("code", "");
        int ht = PlayerPrefs.GetInt(str1 + "ht", 0);

        if (ht >= cookPrice_i[indexNumber_i])
        {
            checkach();
            StartCoroutine("penMove");
            ht = ht - cookPrice_i[indexNumber_i];
            allHeart_txt.text = "" + ht;
            PlayerPrefs.SetInt(str1 + "ht", ht);
            point_i = PlayerPrefs.GetInt("lovepoint", 0) + point_i;
            PlayerPrefs.SetInt("cooked", 1);
            PlayerPrefs.SetInt("lovepoint", point_i);
            PlayerPrefs.SetString("cookLastTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
            audio_obj.GetComponent<SoundEvt>().cookSound();
            if (indexNumber_i>=10)
            {
                int help = PlayerPrefs.GetInt("fallsCount", 0);
                if (help >= 9 && PlayerPrefs.GetInt("windowfall", 0) == 0)
                {
                    PlayerPrefs.SetInt("windowfall", 1);
                    //GM2.GetComponent<FirstRoomFunction>().window_season_obj.SetActive(true);
                    sToast_obj.SetActive(true);
                    StopCoroutine("toastHotImgFadeOut");
                    StartCoroutine("toastHotImgFadeOut");
                    help++;
                    PlayerPrefs.SetInt("fallsCount", help);
                }
                else
                {
                    help++;
                    PlayerPrefs.SetInt("fallsCount", help);
                }
            }
        }
        else
        {
            audio_obj.GetComponent<SoundEvt>().cancleSound();
            StopCoroutine("cookToastFadeOut");
            StartCoroutine("cookToastFadeOut");
        }
    }
    

    IEnumerator penMove()
    {
        block_obj.SetActive(true);
        cookYN_obj.SetActive(false);
        pen_obj.SetActive(true);
        int a = 1;
        for (int i = 0; i < 5; i += 1)
        {
            if (a == 1)
            {
                pen_obj.GetComponent<Image>().sprite = pen1_spr;
                a = 0;
            }
            else
            {
                pen_obj.GetComponent<Image>().sprite = pen2_spr;
                a = 1;
            }
            yield return new WaitForSeconds(0.6f);
        }
        pen_obj.SetActive(false);
        illust_obj.SetActive(true);
        gasrange_obj.SetActive(false);
        block_obj.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().foodSound();
    }

    public void closeIllust()
    {
        illust_obj.SetActive(false);
        dishBtn_obj.SetActive(true);
        PlayerPrefs.SetInt("cookendfoods"+indexNumber_i, 1);
        endg();
    }

    public void cleanDish()
    {
        float xx = dishBtn_obj.transform.position.x;
        float yy = dishBtn_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        PlayerPrefs.SetInt("dishw", 1);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        coldRain_i = coldRain_i + 20;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.Save();
        dishBtn_obj.SetActive(false);
        GM2.GetComponent<GetFadeout>().getRainFade();
    }

    public void infoShow()
    {
        text_obj.SetActive(false);
        ingredient_txt.text = "" + data[indexNumber_i]["재료"];
    }

    IEnumerator toastBImgFadeOut()
    {
        colorB.a = Mathf.Lerp(0f, 1f, 1f);
        beadalYetToast_obj.GetComponent<Image>().color = colorB;
        beadalTime_obj.GetComponent<Image>().color = colorB;
        beadalYetToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorB.a = Mathf.Lerp(0f, 1f, i);
            beadalYetToast_obj.GetComponent<Image>().color = colorB;
            beadalTime_obj.GetComponent<Image>().color = colorB;
            yield return null;
        }
        beadalYetToast_obj.SetActive(false);
    }

    public void OpenIceBox()
    {
        ingredient_txt.text = "";
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 2)
        {
            iceBoxLv_obj[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 3)
        {
            iceBoxLv_obj[1].SetActive(true);
            iceBoxLv_obj[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 4)
        {
            iceBoxLv_obj[2].SetActive(true);
        }
        CheckIng();
        iceBox_obj.SetActive(true);
        v = 0;
        if (egg_i == 1)
        {
            ingredient_obj[0].SetActive(true);
            v++;
        }
        if (carot_i == 1)
        {
            ingredient_obj[1].SetActive(true);
            v++;
        }
        if (milk_i == 1)
        {
            ingredient_obj[2].SetActive(true);
            v++;
        }
        if (cucumber_i == 1)
        {
            ingredient_obj[3].SetActive(true);
            v++;
        }
        if (bread_i == 1)
        {
            ingredient_obj[4].SetActive(true);
            v++;
        }
        if (paprika_i == 1)
        {
            ingredient_obj[5].SetActive(true);
            v++;
        }
        if (seeweed_i == 1)
        {
            ingredient_obj[6].SetActive(true);
            v++;
        }
        if (ham_i == 1)
        {
            ingredient_obj[7].SetActive(true);
            v++;
        }
        if (mushroom_i == 1)
        {
            ingredient_obj[9].SetActive(true);
            v++;
        }
        if (tofu_i == 1)
        {
            ingredient_obj[8].SetActive(true);
            v++;
        }
        if (bam_i == 1)
        {
            ingredient_obj[10].SetActive(true);
        }
        if (shrimp_i == 1)
        {
            ingredient_obj[11].SetActive(true);
        }
        if (v == 10)
        {
            achv2();
        }
    }

    void CheckIng()
    {
        if (PlayerPrefs.GetInt("iceboxlv", 0) >= 3)
        {
            PlayerPrefs.SetInt("milk", 1);
        }
        egg_i = PlayerPrefs.GetInt("egg", 0);
        milk_i = PlayerPrefs.GetInt("milk", 0);
        tofu_i = PlayerPrefs.GetInt("tofu", 0);
        bread_i = PlayerPrefs.GetInt("bread", 0);
        mushroom_i = PlayerPrefs.GetInt("mushroom", 0);
        carot_i = PlayerPrefs.GetInt("carot", 0);
        ham_i = PlayerPrefs.GetInt("ham", 0);
        seeweed_i = PlayerPrefs.GetInt("seaweed", 0);
        cucumber_i = PlayerPrefs.GetInt("cucumber", 0);
        paprika_i = PlayerPrefs.GetInt("paprika", 0);
        //bam_i = PlayerPrefs.GetInt("pat", 0);
        //shrimp_i = PlayerPrefs.GetInt("ggomak", 0);
        //bam_i = PlayerPrefs.GetInt("ssuck", 0);
        //shrimp_i = PlayerPrefs.GetInt("juggume", 0);
        //bam_i = PlayerPrefs.GetInt("subaks", 0);
        //shrimp_i = PlayerPrefs.GetInt("icebars", 0);
        bam_i = PlayerPrefs.GetInt("bam2", 0);
        shrimp_i = PlayerPrefs.GetInt("shrimp2", 0);
    }

    public void RightIce()
    {
        icePage2.SetActive(true);
        icePage1.SetActive(false);
        audio_obj.GetComponent<SoundEvt>().buttonSound();
    }

    public void LeftIce()
    {
        icePage2.SetActive(false);
        icePage1.SetActive(true);
        audio_obj.GetComponent<SoundEvt>().buttonSound();
    }


    public void RightButtonG()
    {
        if (page_i < 2)
        {
            page_i++;
            cookPage_obj[0].SetActive(false);
            cookPage_obj[1].SetActive(false);
            cookPage_obj[2].SetActive(false);
            cookPage_obj[page_i].SetActive(true);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
            cookPage_txt.text = "" + (1 + page_i) + "/3";
        }
    }

    public void LeftButtonG()
    {
        if (page_i > 0)
        {
            page_i--;
            cookPage_obj[0].SetActive(false);
            cookPage_obj[1].SetActive(false);
            cookPage_obj[2].SetActive(false);
            cookPage_obj[page_i].SetActive(true);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
            cookPage_txt.text = "" + (1 + page_i) + "/3";
        }
    }

    public void RightButtonI()
    {
        if (PlayerPrefs.GetInt("iceboxlv", 0) <= 2)
        {

        }
        else if (pageIce_i < 1)
        {
            pageIce_i++;
            ingredientPage_obj[0].SetActive(false);
            ingredientPage_obj[1].SetActive(true);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
    }

    public void LeftButtonI()
    {
        if (PlayerPrefs.GetInt("iceboxlv", 0) <= 2)
        {

        }
        else if (pageIce_i > 0)
        {
            pageIce_i--;
            ingredientPage_obj[0].SetActive(true);
            ingredientPage_obj[1].SetActive(false);
            audio_obj.GetComponent<SoundEvt>().buttonSound();
        }
    }

    //업적
    void checkach()
    {
        
        int cts = PlayerPrefs.GetInt("countfirstcookst", 0);
        cts++;
        PlayerPrefs.SetInt("countfirstcookst", cts);
        if (cts >= 100 && PlayerPrefs.GetInt("firstcookst", 0) < 3)
        {
            PlayerPrefs.SetInt("firstcookst", 3);
            GM2.GetComponent<AchievementShow>().achievementCheck(6, 2);
        }
        else if (cts >= 50 && PlayerPrefs.GetInt("firstcookst", 0) < 2)
        {
            PlayerPrefs.SetInt("firstcookst", 2);
            GM2.GetComponent<AchievementShow>().achievementCheck(6, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("firstcookst", 0) < 1)
        {
            PlayerPrefs.SetInt("firstcookst", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(6, 0);
        }
        PlayerPrefs.Save();
    }


    //요리마음부족토스트페이드아웃
    IEnumerator cookToastFadeOut()
    {
        colorC.a = Mathf.Lerp(0f, 1f, 1f);
        cookHToast_obj.GetComponent<Image>().color = colorC;
        cookHToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorC.a = Mathf.Lerp(0f, 1f, i);
            cookHToast_obj.GetComponent<Image>().color = colorC;
            yield return null;
        }
        cookHToast_obj.SetActive(false);
    }

    //요리레시피업적
    void achv()
    {

        if (PlayerPrefs.GetInt("allfood", 0) == 0)
        {
            PlayerPrefs.SetInt("allfood", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(22, 0);
        }
    }

    //업적
    //요리재료업적
    void achv2()
    {
        if (PlayerPrefs.GetInt("allingredient", 0) == 0)
        {
            PlayerPrefs.SetInt("allingredient", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(23, 0);
        }
    }
    
    //온수가 부족하다
    IEnumerator toastHotImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        sToast_obj.GetComponent<Image>().color = color;
        sToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            sToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        sToast_obj.SetActive(false);
    }



    /// <summary>
    /// 엔딩요리
    /// </summary>
    void endg()
    {
        int k = 0;
        if (PlayerPrefs.GetInt("cookending", 0) == 0)
        {
            int fo = 0;
            for(int i=0;i<10; i++)
            {
                fo = fo + PlayerPrefs.GetInt("cookendfoods" + i, 0);
            }

            if (fo >= 10)
            {
                //수집완료
                PlayerPrefs.SetInt("cookending", 1);
                GM2.GetComponent<EndingBox>().shopNum = 2;
                GM2.GetComponent<EndingBox>().PlayEnd();
                GM2.GetComponent<EndingBox>().end_ani.Play("endcook1", -1, 0f);
            }
        }
    }
    

}

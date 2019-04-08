using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasrangeEvt : MonoBehaviour {

    public GameObject gasrange_obj, iceBox_obj;
    public int egg_i, milk_i, tofu_i, bread_i, mushroom_i, carot_i, ham_i, seeweed_i, cucumber_i, paprika_i;
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

    public GameObject pen_obj, illust_obj,block_obj;
    public Sprite pen1_spr, pen2_spr;
    public int[] cookPrice_i;
    public int gasLv_i;
    public Text price_txt, allHeart_txt;

    public Color colorB;
    public GameObject beadalYetToast_obj, beadalTime_obj;
    int point_i;


    // Use this for initialization
    void Start () {
        colorB = new Color(1f, 1f, 1f);
        CheckIng();
        data = CSVReader.Read("material");
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
        point_i = 9;
    }
    public void indexNumber2()
    {
        indexNumber_i = 2;
        point_i = 7;
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
#endregion
    
    public void OpenGasrange()
    {
        if(PlayerPrefs.GetInt("beadal", 0) == 0)
        {
            gasrange_obj.SetActive(true);
            CheckIng();
            switch (gasLv_i)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

            if (egg_i == 1)
            {
                cookFood_obj[0].SetActive(true);
            }
            if (egg_i + ham_i + carot_i == 3)
            {
                cookFood_obj[1].SetActive(true);
            }
            if (carot_i + paprika_i + cucumber_i == 3)
            {
                cookFood_obj[2].SetActive(true);
            }
            if (ham_i + egg_i + bread_i == 3)
            {
                cookFood_obj[3].SetActive(true);
            }
            if (tofu_i == 1)
            {
                cookFood_obj[4].SetActive(true);
            }
            if (bread_i + ham_i == 2)
            {
                cookFood_obj[5].SetActive(true);
            }
            if (milk_i + egg_i == 2)
            {
                cookFood_obj[6].SetActive(true);
            }
            if (seeweed_i == 1)
            {
                cookFood_obj[7].SetActive(true);
            }
            if (cucumber_i + seeweed_i == 2)
            {
                cookFood_obj[8].SetActive(true);
            }
            if (mushroom_i + carot_i == 1)
            {
                cookFood_obj[9].SetActive(true);
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
        cookImg_obj.GetComponent<Image>().sprite = cook_spr[indexNumber_i];
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
            PlayerPrefs.SetInt("beadal", 1);
            PlayerPrefs.SetInt("lovepoint", point_i);
            PlayerPrefs.SetString("foodLastTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        else
        {

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
    }

    public void closeIllust()
    {
        illust_obj.SetActive(false);
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
        switch (iceLv_i)
        {
            //계란, 
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
        CheckIng();
        iceBox_obj.SetActive(true);
        if (egg_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (carot_i == 1)
        {
            ingredient_obj[1].SetActive(true);
        }
        if (milk_i == 1)
        {
            ingredient_obj[2].SetActive(true);
        }
        if (cucumber_i == 1)
        {
            ingredient_obj[3].SetActive(true);
        }
        if (bread_i == 1)
        {
            ingredient_obj[4].SetActive(true);
        }
        if (paprika_i == 1)
        {
            ingredient_obj[5].SetActive(true);
        }
        if (seeweed_i == 1)
        {
            ingredient_obj[6].SetActive(true);
        }
        if (ham_i == 1)
        {
            ingredient_obj[7].SetActive(true);
        }
        if (mushroom_i == 1)
        {
            ingredient_obj[9].SetActive(true);
        }
        if (tofu_i == 1)
        {
            ingredient_obj[8].SetActive(true);
        }
    }

    void CheckIng()
    {
        egg_i = PlayerPrefs.GetInt("egg", 0);
        milk_i = PlayerPrefs.GetInt("milk", 0);
        tofu_i = PlayerPrefs.GetInt("tofu", 0);
        bread_i = PlayerPrefs.GetInt("bread", 0);
        mushroom_i = PlayerPrefs.GetInt("mushroom", 0);
        carot_i = PlayerPrefs.GetInt("carot", 0);
        ham_i = PlayerPrefs.GetInt("ham", 0);
        seeweed_i = PlayerPrefs.GetInt("seeweed", 0);
        cucumber_i = PlayerPrefs.GetInt("cucumber", 0);
        paprika_i = PlayerPrefs.GetInt("paprika", 0);
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
        }
    }

    public void RightButtonI()
    {
        if (pageIce_i < 1)
        {
            pageIce_i++;
            ingredientPage_obj[0].SetActive(false);
            ingredientPage_obj[1].SetActive(true);
        }
    }

    public void LeftButtonI()
    {
        if (pageIce_i > 0)
        {
            pageIce_i--;
            ingredientPage_obj[0].SetActive(true);
            ingredientPage_obj[1].SetActive(false);
        }
    }

    //업적
    void checkach()
    {
        
        int cts = PlayerPrefs.GetInt("countfirstcookst", 0);
        cts++;
        PlayerPrefs.SetInt("countfirstcookst", cts);
        if (cts >= 5 && PlayerPrefs.GetInt("firstcookst", 0) < 3)
        {
            PlayerPrefs.SetInt("firstcookst", 3);
            GM2.GetComponent<AchievementShow>().achievementCheck(6, 2);
        }
        else if (cts >= 3 && PlayerPrefs.GetInt("firstcookst", 0) < 2)
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

}

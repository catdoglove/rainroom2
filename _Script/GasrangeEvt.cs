using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasrangeEvt : MonoBehaviour {

    public GameObject gasrange_obj, iceBox_obj;
    public int egg_i, milk_i, tofu_i, bread_i, mushroom_i, carot_i, ham_i, seeweed_i, cucumber_i, paprika_i;

    public GameObject[] cookPage_obj, ingredientPage_obj;
    public GameObject[] cookFood_obj, ingredient_obj;
    public int page_i=0, pageIce_i=0,indexNumber_i;


	// Use this for initialization
	void Start () {
        CheckIng();
    }

#region
    public void indexNumber0()
    {
        indexNumber_i = 0;
    }
    public void indexNumber1()
    {
        indexNumber_i = 1;
    }
    public void indexNumber2()
    {
        indexNumber_i = 2;
    }
    public void indexNumber3()
    {
        indexNumber_i = 3;
    }
    public void indexNumber4()
    {
        indexNumber_i = 4;
    }
    public void indexNumber5()
    {
        indexNumber_i = 5;
    }
    public void indexNumber6()
    {
        indexNumber_i = 6;
    }
    public void indexNumber7()
    {
        indexNumber_i = 7;
    }
    public void indexNumber8()
    {
        indexNumber_i = 8;
    }
    public void indexNumber9()
    {
        indexNumber_i = 9;
    }
#endregion
    
    public void OpenGasrange()
    {
        gasrange_obj.SetActive(true);

        
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

    public void OpenIceBox()
    {
        iceBox_obj.SetActive(true);
        if (egg_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (carot_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (cucumber_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (milk_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (tofu_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (bread_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (paprika_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (seeweed_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (ham_i == 1)
        {
            ingredient_obj[0].SetActive(true);
        }
        if (mushroom_i == 1)
        {
            ingredient_obj[0].SetActive(true);
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
            ingredientPage_obj[1].SetActive(false);
            ingredientPage_obj[page_i].SetActive(true);
        }
    }

    public void LeftButtonI()
    {
        if (pageIce_i > 0)
        {
            pageIce_i--;
            ingredientPage_obj[0].SetActive(false);
            ingredientPage_obj[1].SetActive(false);
            ingredientPage_obj[pageIce_i].SetActive(true);
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkShop : MonoBehaviour {

    //이밴트와 상점창
    public GameObject eventPaint_obj, eventPet_obj, foodShop_obj, inShop_obj, basicShop_obj;
    public GameObject blackClose_obj;

    //거리의화가
    public int[] eventPaint_i;
    public GameObject[] eventPaintImg_obj;
    public GameObject allPaint_obj;
    public Sprite[] moviePaint_spr, specialPaint_spr, storyPaint_spr;
    // Use this for initialization
    void Start () {
		
	}

    public void allClose()
    {
        eventPaint_obj.SetActive(false);
        eventPet_obj.SetActive(false);
        foodShop_obj.SetActive(false);
        inShop_obj.SetActive(false);
        basicShop_obj.SetActive(false);
        blackClose_obj.SetActive(false);
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
        //paintImg();
        OpenClose();
        eventPaint_obj.SetActive(true);
    }
    //그림 랜덤 바꿔주기
    void paintImg()
    {
        for (int i = 0; i < 3; i++)
        {
            eventPaint_i[i] = Random.Range(0, 3);
            switch (i)
            {
                case 0:
                    eventPaintImg_obj[0].GetComponent<Image>().sprite = moviePaint_spr[eventPaint_i[0]];
                    break;
                case 1:
                    eventPaintImg_obj[1].GetComponent<Image>().sprite = specialPaint_spr[eventPaint_i[1]];
                    break;
                case 2:
                    eventPaintImg_obj[2].GetComponent<Image>().sprite = storyPaint_spr[eventPaint_i[2]];
                    break;
            }
        }
    }
    public void OpenPetShop()
    {
        OpenClose();
        eventPet_obj.SetActive(true);
    }
    public void OpenFoodShop()
    {
        OpenClose();
        foodShop_obj.SetActive(true);
    }
    public void OpenInShop()
    {
        OpenClose();
        inShop_obj.SetActive(true);
    }
    public void OpenBasicShop()
    {
        OpenClose();
        basicShop_obj.SetActive(true);
    }

}

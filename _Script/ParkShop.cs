using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkShop : MonoBehaviour {

    //이밴트와 상점창
    public GameObject eventPaint_obj, eventPet_obj, foodShop_obj, inShop_obj, basicShop_obj;
    public GameObject blackClose_obj;
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

    public void OpenPaintShop()
    {
        OpenClose();
        eventPaint_obj.SetActive(true);
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

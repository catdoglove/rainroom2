using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAdShow : MonoBehaviour {

    public GameObject adWindow_obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActAd()
    {
        PlayerPrefs.SetInt("adrunout", 0);
        if (adWindow_obj.activeSelf == true)
        {
            adWindow_obj.SetActive(false);
        }
        else
        {
            adWindow_obj.SetActive(true);
        }
    }


    public void closeAd()
    {
        adWindow_obj.SetActive(false);
    }
}

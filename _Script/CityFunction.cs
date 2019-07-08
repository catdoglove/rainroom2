using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFunction : MonoBehaviour {

    public GameObject buildToast_obj;

    // Use this for initialization
    void Start () {
		
	}


    public void building()
    {
        if (buildToast_obj.activeSelf == true)
        {
            buildToast_obj.SetActive(false);
        }
        else
        {
            buildToast_obj.SetActive(true);
        }

    }
}

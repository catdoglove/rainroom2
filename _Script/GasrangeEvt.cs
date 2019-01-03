using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasrangeEvt : MonoBehaviour {

    public GameObject gasrange_obj;

	// Use this for initialization
	void Start () {
		
	}
    
    public void OpenGasrange()
    {
        gasrange_obj.SetActive(true);
    }


}

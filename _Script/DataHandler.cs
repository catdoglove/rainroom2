using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {

	public Camera Main_camera;
	public GameObject canvas_obj, memoCanvas_obj;

	void Awake(){
		canvas_obj = GameObject.Find ("MainCanvas");
        memoCanvas_obj = GameObject.FindWithTag("쪽지Canvas");
    }
	// Use this for initialization
	void Start () {
		
		canvas_obj.GetComponent<Canvas>().worldCamera = Main_camera;

        memoCanvas_obj.GetComponent<Canvas>().worldCamera = Main_camera;
    }
	

}

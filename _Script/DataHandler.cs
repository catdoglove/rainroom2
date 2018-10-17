using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {

	public Camera Main_camera;
	public GameObject canvas_obj;

	void Awake(){
		canvas_obj = GameObject.Find ("MainCanvas");
	}
	// Use this for initialization
	void Start () {
		
		canvas_obj.GetComponent<Canvas>().worldCamera = Main_camera;
	}
	

}

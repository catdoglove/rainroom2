using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingData : MonoBehaviour {


	public Camera camera_c;
	public GameObject GM;
	public int progress_i=0;


	// Use this for initialization
	void Start () {
		progress_i = 1;
		DontDestroyOnLoad (this.gameObject);
	}


	public void setCam(){
		camera_c = GM.GetComponent<DataHandler> ().Main_camera;

	}
}

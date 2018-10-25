using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInfo : MonoBehaviour {

	public GameObject infoWindow_obj,infoBackWindow_obj;
	Vector3 rect_scl,backRect_scl;

	int turnCk_i;
	// Use this for initialization
	void Start () {
		
	}

	public void infoWindowTurn(){
		StopCoroutine ("backTurning2");
		StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StartCoroutine ("turning");
	}

	public void infoBackWindowTurn(){
		StopCoroutine ("backTurning");
		StopCoroutine ("turning2");
		StopCoroutine ("turning");
		StartCoroutine ("backTurning2");
	}


	IEnumerator turning(){
		rect_scl = infoWindow_obj.transform.localScale;

			while (rect_scl.x >= 0f) {
				rect_scl.x = rect_scl.x - 0.1f;
				infoWindow_obj.transform.localScale = rect_scl;
				yield return null;
			}
		StartCoroutine ("backTurning");

	}


	IEnumerator backTurning(){
		backRect_scl = infoBackWindow_obj.transform.localScale;

			while (backRect_scl.x <= 1f) {
				backRect_scl.x=backRect_scl.x+0.1f;
				infoBackWindow_obj.transform.localScale = backRect_scl;
				yield return null;
			}
	}



	IEnumerator turning2(){
		rect_scl = infoWindow_obj.transform.localScale;

		while (rect_scl.x <= 1f) {
			rect_scl.x = rect_scl.x + 0.1f;
			infoWindow_obj.transform.localScale = rect_scl;
			yield return null;
		}

		rect_scl.x = 1f;
		infoWindow_obj.transform.localScale = rect_scl;

	}


	IEnumerator backTurning2(){
		backRect_scl = infoBackWindow_obj.transform.localScale;

		while (backRect_scl.x >= 0f) {
			backRect_scl.x=backRect_scl.x-0.1f;
			infoBackWindow_obj.transform.localScale = backRect_scl;
			yield return null;
		}
		backRect_scl.x=0f;
		infoBackWindow_obj.transform.localScale = backRect_scl;
		StartCoroutine ("turning2");
	}
}

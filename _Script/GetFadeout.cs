using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFadeout : MonoBehaviour {

	Color color;

	// Use this for initialization
	void Start () {


		
	}


	//거미랑먼지터치할때 터치한위치에빗물이나옴 상점에서 소모될때돈에서빠져나가는게보임


	public void getRainFade(){
		//StopCoroutine ("imgFadeOut");
		StartCoroutine ("imgFadeOut");

	}

	IEnumerator imgFadeOut(){
		//color = showCard.GetComponent<Image>().color;	
		for (float i = 1f; i > 0f; i -= 0.05f) {
			Debug.Log (i);
			color.a = Mathf.Lerp (0f, 1f, i);
//			showCard.GetComponent<Image>().color = color;
			yield return null;
		}

	}


}

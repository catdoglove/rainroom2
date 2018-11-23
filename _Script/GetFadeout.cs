using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFadeout : MonoBehaviour {

	Color color;

    Vector2 pos;
    public Vector2 wldObjectPos;

    public GameObject fade_obj;


    // Use this for initialization
    void Start () {


		
	}
	//거미랑먼지터치할때 터치한위치에빗물이나옴 상점에서 소모될때돈에서빠져나가는게보임

        /// <summary>
        /// 터치좌표를 가져와 그부분에 +되는걸 표시
        /// </summary>
	public void getRainFade(){
        Vector2 mouseDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        wldObjectPos = Camera.main.ScreenToWorldPoint(mouseDragPos);
        fade_obj.transform.position = wldObjectPos;
        color.a = Mathf.Lerp(0f, 1f, 1f);
        fade_obj.GetComponent<Image>().color = color;
        //StopCoroutine ("imgFadeOut");
        StartCoroutine ("imgFadeOut");
        
    }

	IEnumerator imgFadeOut(){
		//color = showCard.GetComponent<Image>().color;	
		for (float i = 1f; i > 0f; i -= 0.05f) {
			color.a = Mathf.Lerp (0f, 1f, i);
            fade_obj.GetComponent<Image>().color = color;
            yield return null;
		}

	}


}

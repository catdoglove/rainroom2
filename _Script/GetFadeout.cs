using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFadeout : MonoBehaviour {

	Color color;

    Vector2 pos;
    public Vector2 wldObjectPos;

    public GameObject fade_obj;

    public float moveY,moveX;

    Vector2 mouseDragPos;

    public Sprite[] rain_spr;


    // Use this for initialization
    void Start () {
        color = fade_obj.GetComponent<Image>().color;


    }
	//거미랑먼지터치할때 터치한위치에빗물이나옴 상점에서 소모될때돈에서빠져나가는게보임

        /// <summary>
        /// 터치좌표를 가져와 그부분에 +되는걸 표시
        /// </summary>
	/*public void getRainFade(){
        mouseDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        wldObjectPos = Camera.main.ScreenToWorldPoint(mouseDragPos);
        moveY = wldObjectPos.y;
        moveX = wldObjectPos.x;
        fade_obj.transform.position = wldObjectPos;
        color.a = Mathf.Lerp(0f, 1f, 1f);
        fade_obj.GetComponent<Image>().color = color;
        //StopCoroutine ("imgFadeOut");
        StartCoroutine ("imgFadeOut");
        
    }*/

    public void getRainFade()
    {
        mouseDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        wldObjectPos = Camera.main.ScreenToWorldPoint(mouseDragPos);

        if (PlayerPrefs.GetInt("dishw", 0) == 1)
        {
            fade_obj.GetComponent<Image>().sprite= rain_spr[1];
        }
        else
        {
            fade_obj.GetComponent<Image>().sprite = rain_spr[0];
        }
        PlayerPrefs.SetInt("dishw", 0);
        moveY = PlayerPrefs.GetFloat("watposy", 11);
        moveX = PlayerPrefs.GetFloat("watposx", 11);
        fade_obj.transform.position = wldObjectPos;
        color.a = Mathf.Lerp(0f, 1f, 1f);
        fade_obj.GetComponent<Image>().color = color;
        StartCoroutine("imgFadeOut");

    }

    IEnumerator imgFadeOut(){
        for (float i = 1f; i > 0f; i -= 0.05f) {
			color.a = Mathf.Lerp (0f, 1f, i);
            fade_obj.GetComponent<Image>().color = color;
            moveY = moveY + 0.02f;
            fade_obj.transform.position = new Vector2(moveX, moveY);
            yield return null;
		}
        fade_obj.transform.position = new Vector2(15f, 15f);
        if (PlayerPrefs.GetInt("dishw", 0) == 1)
        {
            PlayerPrefs.SetInt("dishw", 0);
            fade_obj.GetComponent<Image>().sprite = rain_spr[0];
        }
    }


    
}

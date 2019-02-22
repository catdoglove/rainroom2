using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementShow : MonoBehaviour {

    //업적
    public GameObject achievement_obj;
    public float moveX, moveY;
    //public sprite _spr;
    public Sprite[] achievementImg_spr;

    // Use this for initialization
    void Start () {
		
	}

    //업적
    void achievement()
    {
        StartCoroutine("achievementIn");
    }

    IEnumerator achievementOut()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 1f; i > -0.2f; i -= 0.05f)
        {
            moveY = moveY + 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }

    }
    IEnumerator achievementIn()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 0f; i < 1.2f; i += 0.05f)
        {
            moveY = moveY - 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine("achievementOut");
    }

}

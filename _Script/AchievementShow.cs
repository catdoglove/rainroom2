using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementShow : MonoBehaviour {

    //업적
    public GameObject achievement_obj,achSticker_obj;
    public float moveX, moveY;
    //public sprite _spr;
    public Sprite[] achievementImg_spr;
    public Sprite[] achievementImg2_spr;
    public Text title_txt,info_txt;

    List<Dictionary<string, object>> data;

    // Use this for initialization
    void Start () {
        data = CSVReader.Read("rewardname");
    }

    //업적
    public void achievementCheck(int achv_i,int tier_i)
    {
        if (achv_i >= 20)
        {

        }
        else
        {
            achSticker_obj.GetComponent<Image>().sprite = achievementImg_spr[(achv_i * 3) + tier_i];
            tier_i++;
            string str = "lv" + tier_i;
            //Debug.Log("achv" + achv_i + "lv" + tier_i+data[0]["lv1"]);
            tier_i--;
            title_txt.text = "" + data[achv_i][str];
        }
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

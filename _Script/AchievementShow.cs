using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementShow : MonoBehaviour {

    //업적
    public GameObject achievement_obj,achSticker_obj;
    public float moveX, moveY, saveY;
    //public sprite _spr;
    public Sprite[] achievementImg_spr;
    public Sprite[] achievementImg2_spr;
    public Text title_txt,info_txt;

    List<Dictionary<string, object>> data;

    // Use this for initialization
    void Start () {
        saveY = achievement_obj.transform.position.y;
        data = CSVReader.Read("rewardname");
    }

    //업적
    public void achievementCheck(int achv_i,int tier_i)
    {
        achievement_obj.SetActive(true);
        if (achv_i >= 20)
        {
            achSticker_obj.GetComponent<Image>().sprite = achievementImg2_spr[achv_i-20];
            tier_i++;
            string str = "lv" + tier_i;
            Debug.Log("achv" + achv_i + "lv" + tier_i );
            //Debug.Log(data[20]["lv1"]);
            tier_i--;
            title_txt.text = "" + data[achv_i][str];
        }
        else
        {
            achSticker_obj.GetComponent<Image>().sprite = achievementImg_spr[(achv_i * 3) + tier_i];
            tier_i++;
            string str = "lv" + tier_i;
            //Debug.Log("achv" + achv_i + "lv" + tier_i+data[0]["lv1"]);
            tier_i--;
            data = CSVReader.Read("rewardname");
            title_txt.text = "" + data[achv_i][str];
        }
        StartCoroutine("achievementIn");
    }
    IEnumerator achievementOut()
    {
        moveY = achievement_obj.transform.position.y;
        for (float i = 1f; i > 0f; i -= 0.04f)
        {
            moveY = moveY + 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        achievement_obj.SetActive(false);
    }
    IEnumerator achievementIn()
    {
        moveY = saveY;
        //moveY = achievement_obj.transform.position.y;
        for (float i = 0f; i < 1f; i += 0.04f)
        {
            moveY = moveY - 0.08f;
            achievement_obj.transform.position = new Vector2(achievement_obj.transform.position.x, moveY);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine("achievementOut");
    }

}

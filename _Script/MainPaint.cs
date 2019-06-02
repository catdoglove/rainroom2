using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainPaint : MonoBehaviour {

    public GameObject[] lightPaint_obj;
    public GameObject[] paintP_obj, paintM_obj, paintS_obj;
    public Sprite[] light_spr;
    public Text paint_txt;
    public GameObject paintFrame_obj, paintFrameWindow_obj;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetInt("paintinroom", 0)==1)
        {
            paintFrame_obj.SetActive(true);
        }
        
    }
	
    public void OpenPaint()
    {
        paintFrameWindow_obj.SetActive(true);
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[PlayerPrefs.GetInt("setPaint", 0)].GetComponent<Image>().sprite = light_spr[1];

        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("paintp" + i, 0) == 1)
            {
                paintP_obj[i].SetActive(true);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("paintm" + i, 0) == 1)
            {
                paintM_obj[i].SetActive(true);
            }
        }
        /*
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("paints" + i, 0) == 1)
            {
                paintS_obj[i] = i;
            }
        }
        */
    }

    public void paintSet0()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[0].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text= "<키스>\n사랑을 한다는 것은 어떤 느낌일까";
    }
    public void paintSet1()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[1].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<절규>\n괴롭다는 것은 정말 힘든 일이야";
    }

    public void paintSet2()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[2].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<진주 귀걸이>\n살면서 진짜 진주를 본 적은 없어";
    }

    public void ClosePaint()
    {
        paintFrameWindow_obj.SetActive(false);
    }
}

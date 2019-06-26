using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainPaint : MonoBehaviour {

    public GameObject[] lightPaint_obj;
    public GameObject[] paintP_obj, paintM_obj, paintS_obj;
    public Sprite[] light_spr,paint_spr;
    public Text paint_txt;
    public GameObject paintFrame_obj, paintFrameWindow_obj,roomPaint_obj;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetInt("paintinroom", 0)==1)
        {
            paintFrame_obj.SetActive(true);
            if (PlayerPrefs.GetInt("setPaint", -1) == -1)
            {

            }
            else
            {
                roomPaint_obj.GetComponent<Image>().sprite = paint_spr[PlayerPrefs.GetInt("setPaint", 0)];
                roomPaint_obj.SetActive(true);
            }
        }
    }
	
    public void OpenPaint()
    {
        paintFrameWindow_obj.SetActive(true);
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        if (PlayerPrefs.GetInt("setPaint", -1) == -1)
        {

        }
        else
        {
            lightPaint_obj[PlayerPrefs.GetInt("setPaint", 0)].GetComponent<Image>().sprite = light_spr[1];
        }

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
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("paints", 0) > i)
            {
                paintS_obj[i].SetActive(true);
            }
        }
    }

    public void paintSet0()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[0].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text= "<키스>\n사랑을 한다는 것은 어떤 느낌일까";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[0];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 0);
    }
    public void paintSet1()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[1].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<절규>\n괴롭다는 것은 정말 힘든 일이야";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[1];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 1);
    }

    public void paintSet2()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[2].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<진주 귀걸이>\n살면서 진짜 진주를 본 적은 없어";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[2];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 2);
    }
    public void paintSet3()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[3].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<모나리자>\n눈썹이 없어도 괜찮다고 생각해";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[3];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 3);
    }
    public void paintSet4()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[4].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<방바타>\n또 다른 신체를 가지고 있는건 어떤 기분일까";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[4];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 4);
    }
    public void paintSet5()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[5].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<위>\n나도 풍선을 타고 날아가고 싶어";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[5];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 5);
    }
    public void paintSet6()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[6].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<미옹>\n아무리 생각해도 화분이 매력적이야";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[6];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 6);
    }
    public void paintSet7()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[7].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<다락의 움직이는 방>\n내 방이 산을 오르면 재밌을 거 같아";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[7];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 7);
    }
    public void paintSet8()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[8].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<어둠 속에>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[8];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 8);
    }
    public void paintSet9()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[9].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<일어나서>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[9];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 9);
    }
    public void paintSet10()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[10].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<헤매는 중>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[10];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 10);
    }
    public void paintSet11()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[11].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<저 멀리>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[11];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 11);
    }
    public void paintSet12()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[12].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<발견>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[12];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 12);
    }
    public void paintSet13()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[13].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<거기 있는 것>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[13];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 13);
    }
    public void paintSet14()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[14].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<나아가>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[14];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 14);
    }
    public void paintSet15()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[15].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<밖으로>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[15];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 15);
    }
    public void paintSet16()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[16].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<온몸으로>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[16];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 16);
    }
    public void paintSet17()
    {
        for (int i = 0; i < 18; i++)
        {
            lightPaint_obj[i].GetComponent<Image>().sprite = light_spr[0];
        }
        lightPaint_obj[17].GetComponent<Image>().sprite = light_spr[1];
        paint_txt.text = "<느끼다>";
        roomPaint_obj.GetComponent<Image>().sprite = paint_spr[17];
        roomPaint_obj.SetActive(true);
        PlayerPrefs.SetInt("setPaint", 17);
    }

    public void ClosePaint()
    {
        paintFrameWindow_obj.SetActive(false);
    }
}

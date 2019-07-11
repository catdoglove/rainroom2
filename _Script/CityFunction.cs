using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFunction : MonoBehaviour {
    public GameObject GMtag;
    public GameObject buildToast_obj;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    // Use this for initialization
    void Start () {

        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;
        if (GMtag == null)
        {
            GMtag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMtag.GetComponent<MainBtnEvt>().comeHome_obj.SetActive(true);
        GMtag.GetComponent<MainBtnEvt>().shop_obj.SetActive(false);
        GMtag.GetComponent<MainBtnEvt>().SetDiamond();

    }


    public void building()
    {
        if (buildToast_obj.activeSelf == true)
        {
            buildToast_obj.SetActive(false);
        }
        else
        {
            buildToast_obj.SetActive(true);
        }

    }
}

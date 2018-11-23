using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {


    private void Awake()
    {
        string str = gameObject.name;
        GameObject[] des= GameObject.FindGameObjectsWithTag(str);
        int len = des.Length;
        if (len == 2)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        
    }







    /*
    //정적변수
    private static Singleton instance = null;
    //인스턴스 접근 프로퍼티
    public static Singleton Instances
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    */
}

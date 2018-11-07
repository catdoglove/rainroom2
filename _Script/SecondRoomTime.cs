using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomTime : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //업데이트대신쓴다
        StartCoroutine("UpdateSec");
    }

    /// <summary>
    /// 1초당변경하는것들
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateSec()
    {
        int a = 0;
        while (a == 0)
        {
            
            yield return new WaitForSeconds(1f);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingData : LodingDataBase {

    void Start()
    {
        //setSprite();
    }


    public void setSprite(){
        if (window_spr[0] == null)
        {
            window_spr = Resources.LoadAll<Sprite>("window_test");
            book_spr = Resources.LoadAll<Sprite>("");
            bed_spr = Resources.LoadAll<Sprite>("");
            desk_spr = Resources.LoadAll<Sprite>("");
            flower_spr = Resources.LoadAll<Sprite>("");
            icebox_spr = Resources.LoadAll<Sprite>("");
            wall_spr = Resources.LoadAll<Sprite>("");
            shelf_spr = Resources.LoadAll<Sprite>("");
            drawing_spr = Resources.LoadAll<Sprite>("");
            flowerpot_spr = Resources.LoadAll<Sprite>("");
            gasrange_spr = Resources.LoadAll<Sprite>("");
            waterPurifiler_spr = Resources.LoadAll<Sprite>("");
            rug_spr = Resources.LoadAll<Sprite>("");
            clock_spr = Resources.LoadAll<Sprite>("");
            stand_spr = Resources.LoadAll<Sprite>("");
            tapestry_spr = Resources.LoadAll<Sprite>("");
            umbrella_spr = Resources.LoadAll<Sprite>("");
            mat_spr = Resources.LoadAll<Sprite>("");
            cabinet_spr = Resources.LoadAll<Sprite>("");
            poster_spr = Resources.LoadAll<Sprite>("");
            drawer_spr = Resources.LoadAll<Sprite>("");
        }
		
	}
}

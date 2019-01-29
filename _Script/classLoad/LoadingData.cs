using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingData : LodingDataBase {

    void Awake()
    {
        setSprite();
    }


    public void setSprite(){
        if (window_spr[0] == null)
        {
            window_spr = Resources.LoadAll<Sprite>("UI/Roomdown/head_window(280x210)");
            window2_spr = Resources.LoadAll<Sprite>("UI/Roomdown/back_window(220x220)");
            book_spr = Resources.LoadAll<Sprite>("UI/Roomup/back_book(210x150)");
            bed_spr = Resources.LoadAll<Sprite>("UI/Roomup/head_bed(400x260)");
            desk_spr = Resources.LoadAll<Sprite>("UI/Roomup/back_desk(240x240)");
            flower_spr = Resources.LoadAll<Sprite>("UI/Roomdown/head_flowerseed(100x170)");
            icebox_spr = Resources.LoadAll<Sprite>("UI/Roomdown/back_ice(190x230)");
            light_spr = Resources.LoadAll<Sprite>("UI/Roomdown/light(150x130)");
            //wall_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            shelf_spr = Resources.LoadAll<Sprite>("UI/Roomdown/back_shelf(240x130)");
            //drawing_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            flowerpot_spr = Resources.LoadAll<Sprite>("UI/Roomdown/head_flowerseed(100x170)");
            gasrange_spr = Resources.LoadAll<Sprite>("UI/Roomdown/back_gasrange(210x200)");
            //waterPurifiler_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            //rug_spr = Resources.LoadAll<Sprite>("UI/Roomup/rug(280x100)");
            //clock_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            //stand_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            //tapestry_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            //umbrella_spr = Resources.LoadAll<Sprite>("UI/Roomdown/");
            mat_spr = Resources.LoadAll<Sprite>("UI/Roomdown/head_carpet(230x200)");
            mat2_spr = Resources.LoadAll<Sprite>("UI/Roomdown/back_carpet(200x80)");
            cabinet_spr = Resources.LoadAll<Sprite>("UI/Roomup/head_shelf(230x230)");
            //poster_spr = Resources.LoadAll<Sprite>("UI/Roomup/");
            drawer_spr = Resources.LoadAll<Sprite>("UI/Roomdown/head_tvdown(350x150)");
        }
	}
}

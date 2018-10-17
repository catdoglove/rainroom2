using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : CommonDate {

	public GameObject[] shopItems_btn;
	public int itemIndex_i, itemLevel_i;
	public string itemName_str;


	void setIndex0(){
		itemIndex_i = 0;
	}
	void setIndex1(){
		itemIndex_i = 1;
	}
	void setIndex2(){
		itemIndex_i = 2;
	}
	void setIndex3(){
		itemIndex_i = 3;
	}
	void setIndex4(){
		itemIndex_i = 4;
	}
	void setIndex5(){
		itemIndex_i = 5;
	}
	void setIndex6(){
		itemIndex_i = 6;
	}
	void setIndex7(){
		itemIndex_i = 7;
	}
	void setIndex8(){
		itemIndex_i = 8;
	}
	void setIndex9(){
		itemIndex_i = 9;
	}
	void setIndex10(){
		itemIndex_i = 10;
	}
	void setIndex11(){
		itemIndex_i = 11;
	}

	void callShopButtonName(){

		itemName_str = shopItems_btn [itemIndex_i].name;
		itemLevel_i = PlayerPrefs.GetInt (itemName_str, 0);

		//딕셔너리로2차열하기
	}
	

}

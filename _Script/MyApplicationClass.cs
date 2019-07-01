using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class MyApplicationClass : MonoBehaviour {

    //public Text _txt;
    public string str;
    string[] split_text;



// Use this for initialization
void Start () {
		
	}
	public void delCache()
    {
        string help="";
        string ss = Application.persistentDataPath;
        ss = ss+"/cache";

        split_text = ss.Split('/');
        
        for (int i = 0; i < split_text.Length-2; i++)
        {
            help = help + "/"+ split_text[i];
        }
        ss = help;
        ss = ss + "/cache/UnityAdsCache";
        ss = ss.Substring(1);
        //_txt.text = ss;
        //UnityAdsCache
        if (System.IO.Directory.Exists(ss))
        {
            string[] files = System.IO.Directory.GetFiles(ss);
            foreach (string s in files)

            {

                //_txt.text = ss;
                string fileName = System.IO.Path.GetFileName(s);
                string deletefile = ss +"/"+ fileName;
                //_txt.text = deletefile;
                System.IO.File.SetAttributes(deletefile, FileAttributes.Normal);
                System.IO.File.Delete(deletefile);
                str = str + deletefile;
                //_txt.text = str;
            }
        }
        

    }

    private void OnDestroy()
    {
        Debug.Log("살려줘");
        delCache();
    }

}

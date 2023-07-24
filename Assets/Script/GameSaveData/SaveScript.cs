using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public TimeCountDown Dtime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < MainUI._instance.transform.childCount; i++)
        {
            if (MainUI._instance.transform.GetChild(i).childCount > 0)
            {
                SubUI picture = MainUI._instance.transform.GetChild(i).transform.GetChild(0).GetComponent<SubUI>();
                sb.Append(picture.targetGraphic.name + "," + (int)picture.transform.localEulerAngles.z + "-");
            }
        }
        ES3.Save<string>("QuickSave", sb.ToString());
        print("SaveFinish");
    }
    public void LoadInventory()
    {
        //StartCoroutine("Load");
        if (ES3.KeyExists("QuickSave") == false)
        {
           return;
        }
        Dtime.isLoading = true;
        string str = ES3.Load<string>("QuickSave");
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length - 1; i++)
        {
            string itemStr = itemArray[i];
            string[] temp = itemStr.Split(',');
            string id = temp[0];
            int RotAngle = int.Parse(temp[1]);
            MainUI._instance.transform.GetChild(i).transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, RotAngle);
            MainUI._instance.transform.GetChild(i).transform.GetChild(0).name = id;
            MainUI._instance.transform.GetChild(i).transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<RenderTexture>($"RenderTexture/YUAN_{id}");
        }
        
        print("LoadFinish");

    }

    //IEnumerator Load()
    //{
    //    if (ES3.KeyExists("QuickSave") == false)
    //    {
    //        yield break;
    //    }

    //    ES3.Load<int>("QuickSaveGameTime");

    //    string str = ES3.Load<string>("QuickSave");
    //    string[] itemArray = str.Split('-');
    //    for (int i = 0; i < itemArray.Length - 1; i++)
    //    {
    //        string itemStr = itemArray[i];
    //        string[] temp = itemStr.Split(',');
    //        string id = temp[0];
    //        int RotAngle = int.Parse(temp[1]);
    //        yield return null;
    //        MainUI._instance.transform.GetChild(i).transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, RotAngle);
    //        MainUI._instance.transform.GetChild(i).transform.GetChild(0).name = id;
    //        MainUI._instance.transform.GetChild(i).transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<RenderTexture>($"RenderTexture/YUAN_{id}");
    //    }
    //    TimeCountDown._instance.GameTime = ES3.Load<int>("QuickSaveGameTime");
    //    print("LoadFinish");
    //}
}

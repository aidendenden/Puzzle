using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class GameManager:MonoBehaviour
{
    public static GameManager _instance;

    static public bool win1;
    static public bool win2;

    public bool isPaused = true;
    public VideoPlayer video;

   

    private void Awake()
    {
        _instance = this;
        //UnPause();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Pause();
    //        UnPause();
    //    }
    //}

    public void Pause()
    {
        if (isPaused == false)
        {
            isPaused = true;
            //menuGo.SetActive(true);
            Time.timeScale = 0;
            video.Pause();
            //Cursor.visible = true;
        }
    }

    public void UnPause()
    {
        if (isPaused == true)
        {
            isPaused = false;
            //menuGo.SetActive(false);
            Time.timeScale = 1;
            video.Play();
            //Cursor.visible = false;
        }
    }

 


    //public void QuitGame()
    //{
    //    Application.Quit();
    //}





    //static public void RandomArray(Sprite[] sprites)
    //{
    //    for (int i = 0; i < sprites.Length; i++)
    //    {
    //        //随机抽取数字中的一个位置，并将这张图片与第i张图片交换.
    //        int index = Random.Range(i, sprites.Length);
    //        Sprite temp = sprites[i];
    //        sprites[i] = sprites[index];
    //        sprites[index] = temp;
    //    }
    //}



    //static public void SetParent(Transform mine, Transform target, Transform oldParent)
    //{
    //    //如果检测到图片，则交换父物体并重置位置.
    //    switch (target.tag)
    //    {
    //        case "Cell":
    //            mine.SetParent(target.parent);
    //            target.SetParent(oldParent);
    //            mine.localPosition = Vector3.zero;
    //            target.localPosition = Vector3.zero;
    //            break;
    //        default:
    //            mine.SetParent(oldParent);
    //            mine.localPosition = Vector3.zero;
    //            break;
    //    }
    //}

    static public bool CheckWin1()
    {
        for (int i = 0; i < ImageCreater._instance.transform.childCount; i++)
        {
            if (ImageCreater._instance.transform.GetChild(i).childCount > 0)
            {       
                if( (ImageCreater._instance.transform.GetChild(i).name != ImageCreater._instance.transform.GetChild(i).transform.GetChild(0).name))
                {
                    Debug.Log("T_ok2");
                    return false;
                }
            }
        }
        return true;
    }
    static public bool CheckWin2()
    {     
        for (int i = 0; i < KKK._instance.transform.childCount; i++)
        {
            if (KKK._instance.transform.GetChild(i).childCount > 0)
            {
                if (KKK._instance.transform.GetChild(i).name != KKK._instance.transform.GetChild(i).transform.GetChild(0).name)
                {
                    return false;
                }       
            }
        }
        return true;
    }





    static public bool CheckWin_001()
    {
        for (int i = 0; i < MainUI._instance.transform.childCount; i++)
        {
            if (MainUI._instance.transform.GetChild(i).childCount > 0)
            { 
                if (MainUI._instance.transform.GetChild(i).name != MainUI._instance.transform.GetChild(i).transform.GetChild(0).name)
                {
                    Debug.Log("Puzzle_Complete");
                    return false;
                }
                if ((int)MainUI._instance.transform.GetChild(i).localEulerAngles.z != (int)MainUI._instance.transform.GetChild(i).transform.GetChild(0).localEulerAngles.z)
                {
                    Debug.Log("Puzzle_Rotation_Complete");
                    return false;
                }
            }
        }
        return true;
    }

  




}



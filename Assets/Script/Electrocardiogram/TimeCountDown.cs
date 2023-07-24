using UnityEngine.UI;
using UnityEngine;

public class TimeCountDown : MonoBehaviour
{

    //填写你需要的时间，按秒计算，如120秒，就是2：00;
    public int CountDownTime;
    public int GameTime;
    private float times = 0;
    public Text GameCountTimeText;
    public GameObject gameOverImage;
    public GameObject gamePass;

    public bool isLoading;
        
    void Start() 
    {
        GameTime = CountDownTime;
        int m = (int)(GameTime / 60);
        float s = GameTime % 60;
        GameCountTimeText.text = m + "：" + string.Format("{0:00}",s);
    }
    void Update()
    { 
        timeDown();
    }

    public void timeDown()
    {
        if (GameManager.win1)
        {
            GameCountTimeText.text = "";
            gamePass.SetActive(true);
            return;
        }
        if (isLoading)
        {
            GameTime = ES3.Load<int>("QuickSaveGameTime");
            isLoading = false;
        }
        int M = (int)(GameTime / 60);
        float S = GameTime % 60;
        times += Time.deltaTime;
        if (times >= 1f)
        {
            times = 0;
            GameTime--;
            ES3.Save<int>("QuickSaveGameTime", GameTime);
            GameCountTimeText.text = M + ":" + string.Format("{0:00}", S);
            if (M <= 0 & S <= 0)
            {
                //结束游戏操作
                GameCountTimeText.gameObject.SetActive(false);
                if (!GameManager.win1)
                {
                    GameCountTimeText.text = "";
                    gameOverImage.SetActive(true);
                }
                else
                {
                    GameCountTimeText.text = "";
                    gamePass.SetActive(true);
                }
            }
        }
    }
}

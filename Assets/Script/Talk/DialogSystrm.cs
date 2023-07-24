using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystrm : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    public GameObject selectButton;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite face01, face02;

    bool textFinished;
    bool cancelTyping;
    bool oneFinish;


    public static bool isTalk;

    List<string> textList = new List<string>();



    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);

    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine("SetTextUI");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && index == textList.Count && !oneFinish)
        {
            isTalk = false;
            oneFinish = true;
            selectButton.SetActive(true);
            index = 0;
            return;

        }
        if (Input.anyKeyDown&&!oneFinish)
        {

            if (textFinished && !cancelTyping)
            {
                StartCoroutine("SetTextUI");
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var Line in lineData)
        {
            textList.Add(Line);
        }
    }



    IEnumerator SetTextUI()
    {

        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {

            case "A":

                faceImage.sprite = face01;
                index++;
                break;
            case "B":

                faceImage.sprite = face02;
                index++;
                break;
        }

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {

            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBottomPlate : MonoBehaviour
{
    public UISheet[] uiPiece;

    public float speed=5;

    public string mapTip;
    public Text mapUiTip;

    public int PieceNum;
    public int startNum;

    public GameObject selObj;

    public Image UIselPiece;


    public Color curColor = Color.blue;
    public Color nextColor = Color.green;

    public Text switchNumber;
    public int switchNum = 10000;
    public int switchStartNum;
    string sNum;

    public UISheet selectPiece;


    public int width;
    public int height;

    public KeyCode selectkey = KeyCode.Return;



    // Start is called before the first frame update
    void Start()
    {
        startNum = PieceNum;
        uiPiece = GetComponentsInChildren<UISheet>();

        switchNumber.GetComponent<Text>().text = switchStartNum.ToString();

        UIselPiece = selObj.GetComponent<Image>();
        selectPiece = uiPiece[PieceNum];

        selObj.transform.position = selectPiece.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        mapUiTip.text = mapTip;
        switchNumber.text = sNum;
        UIMapMove();
    }



    private void UIMapMove()
    {
        if (selectPiece.isSelect)
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && switchNum > 0)
            {
                switchNum -= 1;
                sNum = switchNum.ToString();
                SelectedMapMove(-width, uiPiece);
            }
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && switchNum > 0)
            {
                switchNum -= 1;
                sNum = switchNum.ToString();
                SelectedMapMove(width, uiPiece);
            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && switchNum > 0)
            {
                switchNum -= 1;
                sNum = switchNum.ToString();
                SelectedMapMove(1, uiPiece);
            }
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && switchNum > 0)
            {
                switchNum -= 1;
                sNum = switchNum.ToString();
                SelectedMapMove(-1, uiPiece);
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(selectkey))
            {
                UIselPiece.color = curColor;
                selectPiece.isSelect = false;
                mapTip = ("");
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectingMapMove(-width, uiPiece);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectingMapMove(width, uiPiece);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectingMapMove(1, uiPiece);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectingMapMove(-1, uiPiece);
            }
            if (Input.GetKeyDown(selectkey))
            {
                if (selectPiece.isCanSelect)
                {
                    UIselPiece.color = nextColor;
                    selectPiece.isSelect = true;
                }
            }

        }
    }


    public void SelectedMapMove(int nextindex, UISheet[] uiPiece)
    {
        if (PieceNum + nextindex >= 0 && PieceNum + nextindex < uiPiece.Length
            && !((PieceNum % width == 0 && nextindex == -1)
            ||
            (PieceNum % width == (width - 1) && nextindex == 1))
            && uiPiece[PieceNum + nextindex].isCanMove)
        {

            UISheet tempUI = selectPiece;

            selectPiece = uiPiece[PieceNum + nextindex];

            uiPiece[PieceNum + nextindex] = tempUI;

            uiPiece[PieceNum] = selectPiece;


            Vector3 uiV3temp = tempUI.transform.position;

            tempUI.transform.position = selectPiece.transform.position;

            selectPiece.transform.position = uiV3temp;

            selectPiece = uiPiece[PieceNum + nextindex];

            selObj.transform.position = selectPiece.transform.position;
            PieceNum += nextindex;

        }
        else
        {
            mapTip = ("无法移动此区域");
        }
    }

    public void SelectingMapMove(int a, UISheet[] uiPiece)
    {
        if (PieceNum + a >= 0 && PieceNum + a < uiPiece.Length
            && !((PieceNum % width == 0 && a == -1) || (PieceNum % width == (width - 1) && a == 1)))
        {
            PieceNum += a;
            selectPiece = uiPiece[PieceNum];

            selObj.transform.position = selectPiece.transform.position;
        }

        else
        {
            mapTip = ("没有更多地图块了");

        }
    }

    public void UIMapReset()
    {
        switchNum = switchStartNum;
        sNum = switchNum.ToString();

        uiPiece = GetComponentsInChildren<UISheet>();


        selectPiece.isSelect = false;
        selectPiece = uiPiece[startNum];

        UIselPiece.color = curColor;
        PieceNum = startNum;

        foreach (var item in uiPiece)
        {
            item.transform.position = item.initialPos;
        }

        selObj.transform.position = selectPiece.transform.position;
    }

    
}
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PicDrag : Button, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool finishWork = true;

    private Transform beginParentTransform; //记录开始拖动时的父级对象        

    private Transform topOfUiT;

  

    protected override void Start()
    {
        base.Start();
        topOfUiT = GameObject.Find("Canvas").transform;
    }


    public void OnBeginDrag(PointerEventData _)
    {
        if (finishWork)
        {
            if (transform.parent == topOfUiT) return;
            beginParentTransform = transform.parent;
            transform.SetParent(topOfUiT);
        }
    }

    public void OnDrag(PointerEventData _)
    {
        if (finishWork)
        {
            transform.position = Input.mousePosition;
            if (transform.GetComponent<Image>().raycastTarget) transform.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData _)
    {

        GameObject go = _.pointerCurrentRaycast.gameObject;
        if (go == null)
        {
            finishWork = false;
            SetPosAndParent(transform, beginParentTransform);
            transform.GetComponent<Image>().raycastTarget = true;
            finishWork = true;
            return;
        }
        if (go.tag == "ASS")
        {
            finishWork = false;
            SetPosAndParent(transform, go.transform.parent);                              //将当前拖动物品设置到目标位置
            go.transform.SetParent(topOfUiT);                                             //目标物品设置到 UI 顶层
            if (Math.Abs(go.transform.position.x - beginParentTransform.position.x) <= 0) //以下 执行置换动画，完成位置互换 
            {
                go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                {
                    go.transform.SetParent(beginParentTransform);
                    transform.GetComponent<Image>().raycastTarget = true;
                }).SetEase(Ease.InOutQuint);
            }
            else
            {
                go.transform.DOMoveX(beginParentTransform.position.x, 0.2f).OnComplete(() =>
                {
                    go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                    {
                        go.transform.SetParent(beginParentTransform);
                        transform.GetComponent<Image>().raycastTarget = true;
                    }).SetEase(Ease.InOutQuint);
                });
            }
            finishWork = true;
        }
        else //其他任何情况，物体回归原始位置
        {
            finishWork = false;
            SetPosAndParent(transform, beginParentTransform);
            transform.GetComponent<Image>().raycastTarget = true;
            finishWork = true;
        }


        //检测是否完成拼图.
        if (GameManager.CheckWin2())
        {
            GameManager.win2 = true;
            Debug.Log("Win2!!!");
            KKK._instance.gamePass.SetActive(true);
        }

    }

    private void SetPosAndParent(Transform t, Transform parent)
    {
        t.SetParent(parent);
        t.position = parent.position;
    }





}

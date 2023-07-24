using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubUI : Button, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private int RotateAngle = 90;
    public bool finishWork = true;
    public bool inImage;

    public double t1;
    public double t2;

    public int origionZ;

    private Transform beginParentTransform; //记录开始拖动时的父级对象        

    private Transform topOfUiT;

    protected override void Start()
    {
        base.Start();
        topOfUiT = GameObject.Find("Canvas").transform;
    }


    private void Update()
    {
        PicRotate();
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckCheck();
        }
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Highlighted:

                inImage = true;
                break;

            case SelectionState.Normal:

                inImage = false;
                break;

            default:
                break;
        }
    }



    public void OnBeginDrag(PointerEventData _)
    {
        if (finishWork && !GameManager.win1)
        {
            if (transform.parent == topOfUiT) return;
            beginParentTransform = transform.parent;
            transform.SetParent(topOfUiT);

        }
    }

    public void OnDrag(PointerEventData _)
    {
        if (finishWork && !GameManager.win1)
        {
            transform.position = Input.mousePosition;
            if (transform.GetComponent<RawImage>().raycastTarget) transform.GetComponent<RawImage>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData _)
    {
        if (!GameManager.win1)
        {
            GameObject go = _.pointerCurrentRaycast.gameObject;
            if (go == null)
            {
                finishWork = false;
                SetPosAndParent(transform, beginParentTransform);
                transform.GetComponent<RawImage>().raycastTarget = true;
                finishWork = true;
                return;
            }
            if (go.tag == "Puzzle")
            {
                finishWork = false;
                SetPosAndParent(transform, go.transform.parent);                              //将当前拖动物品设置到目标位置
                go.transform.SetParent(topOfUiT);                                             //目标物品设置到 UI 顶层
                if (Math.Abs(go.transform.position.x - beginParentTransform.position.x) <= 0) //以下 执行置换动画，完成位置互换 
                {
                    go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                    {
                        go.transform.SetParent(beginParentTransform);
                        transform.GetComponent<RawImage>().raycastTarget = true;
                        finishWork = true;
                    }).SetEase(Ease.InOutQuint);
                }
                else
                {
                    go.transform.DOMoveX(beginParentTransform.position.x, 0.2f).OnComplete(() =>
                    {
                        go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                        {
                            go.transform.SetParent(beginParentTransform);
                            transform.GetComponent<RawImage>().raycastTarget = true;
                            finishWork = true;
                        }).SetEase(Ease.InOutQuint);
                    });
                }

            }
            else //其他任何情况，物体回归原始位置
            {
                finishWork = false;
                SetPosAndParent(transform, beginParentTransform);
                transform.GetComponent<RawImage>().raycastTarget = true;
                finishWork = true;
            }
            //检测是否完成拼图.
            CheckCheck();
        }

    }

    private void CheckCheck()
    {
        if (GameManager.CheckWin_001())
        {
            GameManager.win1 = true;
            Debug.Log("Win1!!!");
            return;
        }
    }


    private void PicRotate()
    {
        if (inImage)
        {
            if (Input.GetMouseButtonDown(0))
            {
                t2 = Time.time;
                if (t2 - t1 < 0.3f)
                {
                    origionZ = (int)transform.localEulerAngles.z;
                    float a = RotateAngle + origionZ;
                    transform.DORotate(new Vector3(0, 0, (int)a), 0.4f, RotateMode.Fast).SetEase(Ease.InSine).OnComplete(() => { CheckCheck(); });
                    CheckCheck();
                    print("double click");
                }
                //else if (t2 - t1 > 0.5f)
                //{
                //    print("endSelect");
                //    endSelect = true;
                //}
                t1 = t2;
            }
        }
    }



    private void SetPosAndParent(Transform t, Transform parent)
    {
        t.SetParent(parent);
        t.position = parent.position;
    }

}

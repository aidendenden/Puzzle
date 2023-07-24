using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragOnPic : Button, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    ////记录下自己的父物体.
    //Transform myParent;
    ////Panel，使拖拽是显示在最上方.
    //Transform tempParent;
    //CanvasGroup cg;
    //RectTransform rt;
    ////记录鼠标位置.
    //Vector3 newPosition;
    //void Awake()
    //{
    //	//添加CanvasGroup组件用于在拖拽是忽略自己，从而检测到被交换的图片.
    //	cg = this.gameObject.AddComponent<CanvasGroup>();
    //	rt = this.GetComponent<RectTransform>();
    //	tempParent = GameObject.Find("Canvas").transform;
    //}
    ///// <summary>
    ///// Raises the begin drag event.
    ///// </summary>
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //	//拖拽开始时记下自己的父物体.
    //	myParent = transform.parent;
    //	//拖拽开始时禁用检测.
    //	cg.blocksRaycasts = false;
    //	this.transform.SetParent(tempParent);
    //}
    ///// <summary>
    ///// Raises the drag event.
    ///// </summary>
    //void IDragHandler.OnDrag(PointerEventData eventData)
    //{
    //	//推拽是图片跟随鼠标移动.
    //	RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, Input.mousePosition, eventData.enterEventCamera, out newPosition);
    //	transform.position = newPosition;
    //}
    ///// <summary>
    ///// Raises the end drag event.
    ///// </summary>

    ///public void OnEndDrag(PointerEventData eventData)
    //{
    ////获取鼠标下面的物体.
    //GameObject target = eventData.pointerEnter;
    ////如果能检测到物体.
    //if (target)
    //{
    //	GameManager.SetParent(this.transform, target.transform, myParent);
    //}
    //else
    //{
    //	this.transform.SetParent(myParent);
    //	this.transform.localPosition = Vector3.zero;
    //}
    ////拖拽结束时启用检测.
    //cg.blocksRaycasts = true;
    ////检测是否完成拼图.
    //if (GameManager.CheckWin())
    //{
    //	Debug.Log("Win!!!");
    //}
    //	}
    //}

    public float RotateAngle = 90;
    public bool finishWork = true;
    public bool inImage;
    public double t1;
    public double t2;
    //public bool myRotation;
    private int count;

    public float origionZ;

    private Transform beginParentTransform; //记录开始拖动时的父级对象        

    private Transform topOfUiT;




    protected override void Start()
    {
        base.Start();
        origionZ = transform.rotation.z;
        topOfUiT = GameObject.Find("Canvas").transform;
    }


    private void Update()
    {
        PicRotate();

        //if (transform.rotation == Quaternion.Euler(0, 0, 0))
        //{
        //    Debug.Log("RRR");
        //    myRotation = true;
        //}
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Highlighted:

                inImage = true;
                //this.GetComponent<Image>().color = Color.green;
                Debug.Log("鼠标进入Button！");
                break;

            case SelectionState.Normal:

                inImage = false;
                //this.GetComponent<Image>().color = Color.white;
                Debug.Log("鼠标离开Button！");
                break;

            default:
                break;
        }
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
        CheckCheck();

    }

    private void CheckCheck()
    {
        //if (myRotation)
        //{
            if (GameManager.CheckWin1())
            {
                GameManager.win1 = true;
                ImageCreater._instance.xinZang.SetActive(true);
                Debug.Log("Win111!!!");
                return;
            }
        //}
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
                    count++;
                    float a = RotateAngle * count + origionZ;
                    transform.DORotate(new Vector3(0, 0, (int)a), 0.4f, RotateMode.Fast).SetEase(Ease.InSine).OnComplete(() => { CheckCheck(); });
                    print("double click");
                }
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

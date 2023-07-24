using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISheet : MonoBehaviour
{
    [Header("状态")]
    public bool isSelect;
    public bool isCanSelect;
    public bool isCanMove;

    [Header("初始位置")]
    public Vector2 initialPos;

    public float speed= 9;
    public float RotateAngle = 90;

    private float origionZ;
    private Quaternion targetRotation;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        //initialIndex = transform.GetSiblingIndex()-1;//找到所在父物体的子问题下标位置

        origionZ = transform.rotation.z;    
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelect)
        {
            RotatingUI(); 
        }
    }

    public void RotatingUI()
    {
        if (Input.GetKeyDown(KeyCode.F) && transform.rotation == targetRotation)
        {
            count++;
            targetRotation = Quaternion.Euler(0, 0, RotateAngle * count + origionZ) * Quaternion.identity;
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
            {
                transform.rotation = targetRotation;
            }
        }
    }


}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRotate : MonoBehaviour
{
    public float speed = 2f;
    public float RotateAngle = 90;       //定义每次旋转的角度
    
    private float origionZ;                //声明初始的Z轴旋转值
    private Quaternion targetRotation;    //声明旋转目标角度
    private int count;                  //声明一个量记录到目标角度需要进行旋转RotateAngle度的个数
                                       //由于每次旋转转  90(RotateAngle）度，所以从（0，0，0）到（0，0，180）需要旋转两个 90(RotateAngle) 度
    private void Start()
    {
        origionZ = transform.rotation.z;    //获取当前Y轴旋转值赋给origionZ
        targetRotation = transform.rotation;
    }

    void Update()
    {
        //RotatingUI();
    }

    public void RotatingUI()
    {   
        if (transform.rotation == targetRotation)  //按下赋值
        {
            count++;                                     //当按下F键记录将count+1,一次转90,再按一次就应该从初始的Z变为Z+180,所以每按一次count+1
            targetRotation = Quaternion.Euler(0, 0, RotateAngle * count + origionZ) * Quaternion.identity;  //给旋转目标值赋值，由于只有Z轴动，所以目标值应是  (旋转角(RotateAngle)*需要旋转的个数(count)+origionZ(物体初始Z轴旋转角))*Quarternion.identity(四元数的初始值)
        }
        else  //当不按下就进行旋转
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);//利用Slerp插值让物体进行旋转  2是旋转速度 越大旋转越快

            if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
            {
                transform.rotation = targetRotation;                        //当物体当前角度与目标角度差值小于1度直接将目标角度赋予物体 让旋转角度精确到我们想要的度数
            }
        }
    }

}

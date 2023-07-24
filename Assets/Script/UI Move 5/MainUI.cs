using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI _instance;
 
    void Start()
    {
        _instance = this;
    }

}

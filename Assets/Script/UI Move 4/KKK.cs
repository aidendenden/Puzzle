using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KKK : MonoBehaviour
{
    public static KKK _instance;
    void Start()
    {
        _instance = this;
    }

    public GameObject gamePass;
}
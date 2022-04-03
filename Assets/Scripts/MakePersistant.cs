using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePersistant : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

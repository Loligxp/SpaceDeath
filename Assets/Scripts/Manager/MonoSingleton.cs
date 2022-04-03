using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Singelton Missing");
            }

            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
            _instance = (T)this;
        else
        {
            Debug.Log("Multiple instances of MonoSingleton");
            Destroy(this.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject destructionEffect;

    public void Start()
    {
        Invoke(nameof(SelfDestruct),countdown);
    }

    public void SelfDestruct()
    {
        if(destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        
        GameObject.Destroy(this.gameObject);
    }
}

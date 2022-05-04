using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var audioComp = GetComponent<AudioSource>();
        audioComp.pitch = Random.Range(0.8f, 1.2f);
        audioComp.Play();
    }


}

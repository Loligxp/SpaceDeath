using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoSingleton<MusicPlayer>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}

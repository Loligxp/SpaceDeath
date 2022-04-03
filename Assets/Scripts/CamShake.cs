using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamShake : MonoSingleton<CamShake>
{
    public Transform target;
    public CinemachineVirtualCamera cine;
    public void Shake(float intensity)
    {
        if(GameManager.Instance.screenShake)
            StartCoroutine(_Shake(intensity));
    }

    IEnumerator _Shake(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cineShake = cine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        for (int i = 0; i < 20; i++)
        {
            cineShake.m_AmplitudeGain = intensity;
            yield return new WaitForFixedUpdate();
        }

        cineShake.m_AmplitudeGain = 0;
    }
}

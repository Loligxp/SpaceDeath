using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [SerializeField] private float explosionRange;
    [SerializeField] private float damage;
    [SerializeField] private DamageEffects damageEffect;
    [SerializeField] private LayerMask explosionMask;

    private void Start()
    {
        var explosionHits = Physics2D.OverlapCircleAll(transform.position, explosionRange, explosionMask);
        CamShake.Instance.Shake(1);
        
        for (int i = 0; i < explosionHits.Length; i++)
        {
            if (explosionHits[i].CompareTag("Enemy"))
            {
                explosionHits[i].GetComponent<Enemy>().TakeDamage(damage,damageEffect);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,explosionRange);
    }
}
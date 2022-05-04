using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")] 
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private DamageEffects damageEffect;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int piercing = 0;
    
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(damage,damageEffect);
            Instantiate(deathEffect, transform.position, Quaternion.Euler(90,0,0));
            
            if(piercing <= 0)
                GameObject.Destroy(this.gameObject);

            piercing--;
        }
    }

    public void SetValues(float _damage, float _speed, int _piercing)
    {
        speed = _speed;
        damage = _damage;
        piercing = _piercing;

    }
}

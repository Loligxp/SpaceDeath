using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform _target;

    [SerializeField] private Transform graphicTransform;
    [Header("Stats")] 
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float XP_gained;
    [SerializeField] private SpriteRenderer SPR;
    public float damage;
    
    private void Start()
    {
        _target = GameManager.Instance.playerObject.transform;
    }

    private void Update()
    {
        var walkDir = new Vector2(transform.position.x - _target.position.x, transform.position.y - _target.position.y);
        walkDir.Normalize();
        transform.Translate(-walkDir * speed * Time.deltaTime);
        graphicTransform.Rotate(0,0,90 * Time.deltaTime);

        if (Vector2.Distance(_target.position, transform.position) > 50)
        {
            var playerPos = GameManager.Instance.playerObject.transform.position;
            var spawnDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
            spawnDir.Normalize();
            transform.position = playerPos + (spawnDir * 30);
        }
    }

    public void TakeDamage(float damageAmount, DamageEffects damageType)
    {
        switch (damageType)
        {
            case DamageEffects.Normal:
                health -= damageAmount;
                break;
            case DamageEffects.Frost:
                break;
            case DamageEffects.Fire:
                break;
            case DamageEffects.Electric:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
        }

        if (health <= 0)
        {
            //Enemy died
            GameManager.Instance.GainXP(XP_gained);
            GameObject.Destroy(this.gameObject);
        }

        StartCoroutine(damageFlash());
    }

    IEnumerator damageFlash()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        SPR.color = Color.red;
        for (float i = 0; i < 25; i += 1)
        {
            transform.localScale = new Vector3(0.9f + (i) / 250, 0.9f + (i) / 250, 0.9f + (i) / 250);
            SPR.color = new Color(1f, i / 25, i / 25);

            yield return new WaitForFixedUpdate();
        }
    }
    
}

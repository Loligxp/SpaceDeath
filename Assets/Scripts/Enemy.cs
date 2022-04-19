using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform _target;
    private Vector3 _targetPositionOffset;
    
    [SerializeField] private Transform graphicTransform;
    
    [Header("Stats")] 
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float XP_gained;
    [SerializeField] private SpriteRenderer SPR;
    [SerializeField] private SpriteRenderer shadowSPR;
    [SerializeField] private TrailRenderer[] lineRends;
    [SerializeField] private float circlingRadius;
    public float damage;

    [Header("Paralaxing")] 
    [SerializeField] private float z_Level;

    private void Start()
    {
        _target = GameManager.Instance.playerObject.transform;
        EnemyManager.Instance.AddEnemy(transform);

        z_Level = Random.Range(0, 100f);
        z_Level = Mathf.RoundToInt(z_Level);

        SPR.sortingOrder = (int)z_Level * 2;
        shadowSPR.sortingOrder = (int)z_Level * 2 - 1;

        
        var difficulty = EnemyManager.Instance.difficultyLevel;

        if (difficulty > 200)
        {
            difficulty -= 200;

            speed += (speed / 100) * difficulty / 200;

            health += (speed / 100) * difficulty / 100;
            XP_gained += (XP_gained / 100) * difficulty / 100;

        }

        StartCoroutine(ReLocate(Random.Range(30, 60)));

        _targetPositionOffset = new Vector3( Random.Range(-circlingRadius, +circlingRadius), Random.Range(-circlingRadius, +circlingRadius));
        
        graphicTransform.localScale = Vector3.one + Vector3.one * (z_Level/500);
        SPR.color = new Color(0.8f + z_Level/500, 0.8f + z_Level/500, 0.8f + z_Level/500);
    }

    private void Update()
    {
        var walkDir = new Vector2(transform.position.x - _target.position.x + _targetPositionOffset.x, transform.position.y - _target.position.y + _targetPositionOffset.y);
        walkDir.Normalize();
        transform.Translate(-walkDir * speed * Time.deltaTime);
        graphicTransform.Rotate(0,0,90 * Time.deltaTime);

        if (Vector2.Distance(_target.position, transform.position) > 50)
        {
            var playerPos = GameManager.Instance.playerObject.transform.position;
            var spawnDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
            spawnDir.Normalize();
            transform.position = playerPos + (spawnDir * 30);

            foreach (var lineRenderer in lineRends)
            {
                lineRenderer.Clear();
            }
        }

        
        if (Vector2.Distance(transform.position, _target.position - _targetPositionOffset) < 0.2f)
        {
            _targetPositionOffset = new Vector3( Random.Range(-circlingRadius, +circlingRadius), Random.Range(-circlingRadius, +circlingRadius));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, _target.position - _targetPositionOffset);
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

    private void OnDestroy()
    {
        EnemyManager.Instance.RemoveEnemy(this.transform);
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

    IEnumerator ReLocate(float timeToRelocate)
    {
        yield return new WaitForSeconds(timeToRelocate);
        
        var playerPos = GameManager.Instance.playerObject.transform.position;
        var spawnDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
        spawnDir.Normalize();
        transform.position = playerPos + (spawnDir * 30);

        StartCoroutine(ReLocate(Random.Range(30, 60)));

    }
    
}

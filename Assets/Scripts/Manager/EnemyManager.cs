using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float difficultyLevel;
    [SerializeField] private float spawnDistance;
    
    private float timer;
    
    private void Update()
    {
        timer -= Time.deltaTime;
        difficultyLevel += Time.deltaTime;
        
        if (timer < 0)
        {
            timer = 0;
            var playerPos = GameManager.Instance.playerObject.transform.position;
            var spawnDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
            spawnDir.Normalize();

            var enemyChosen = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyChosen], playerPos + (spawnDir * spawnDistance), quaternion.identity);

            timer = Mathf.Clamp(enemyChosen + 0.4f - (difficultyLevel) / 200,0.02f,5f);
        }
    }
}

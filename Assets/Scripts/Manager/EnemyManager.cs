using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] public float difficultyLevel;
    [SerializeField] private float spawnDistance;
    
    private int maxEnemies = 300;
    private float timer;
    
    private List<Transform> enemiesAlive = new List<Transform>();
    private int currentEnemyCount = 0;
    
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
            if (difficultyLevel < 50)
                enemyChosen = 0;
            else if (difficultyLevel < 100)
                enemyChosen = Random.Range(0, 2);
            
            
            Instantiate(enemies[enemyChosen], playerPos + (spawnDir * spawnDistance), quaternion.identity);
            timer = Mathf.Clamp(enemyChosen == 0? 1f : 2f  + 0.4f - (difficultyLevel) / 200,0.05f,5f);
        }
    }
    
    public void AddEnemy(Transform newEnemy)
    {
        currentEnemyCount++;
        enemiesAlive.Add(newEnemy);


        if (currentEnemyCount > maxEnemies)
        {
            ForceRemove(enemiesAlive[0]);
        }
    }
    
    public void RemoveEnemy(Transform destroyedEnemy)
    {
        currentEnemyCount--;

        enemiesAlive.Remove(destroyedEnemy);
    }

    public void ForceRemove(Transform destroyedEnemy)
    {
        currentEnemyCount--;
        
        Destroy(destroyedEnemy.gameObject);
        enemiesAlive.Remove(destroyedEnemy);
    }
}

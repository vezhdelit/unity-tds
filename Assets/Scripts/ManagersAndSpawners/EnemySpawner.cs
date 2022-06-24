using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    public int poolCount = 10;
    public float spawnInterval = 5;
    [SerializeField] private bool autoExpand = false;
    
    private Pool<EnemyController> pool;

    void Start()
    {
        pool = new Pool<EnemyController>(this.enemyPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
        
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);
        CreateEnemy();
        
        StartCoroutine(spawnEnemy());
    }
    public void CreateEnemy()
    {
        Vector3[] spawnAreas = new Vector3[]
        {
            new Vector3(Random.Range(-60f, 60f), 60),
            new Vector3(Random.Range(-60f, 60f), -60),
            new Vector3(60, Random.Range(-60f, 60f)),
            new Vector3(-60, Random.Range(-60f, 60f))
        };
        
        var enemy = this.pool.GetFreeElement();
        if (enemy != null)
        {
            enemy.transform.position = spawnAreas[Random.Range(0, 3)];
        }
    }

    public void SetNewPoolCount(int newPoolCount)
    {
        poolCount = newPoolCount;
        pool.ExpandPool(newPoolCount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private AddSquadMember coinPrefab;
    [SerializeField] private int poolCount = 10;
    [SerializeField] private float spawnInterval = 10;
    [SerializeField] private bool autoExpand = false;


    
    private Pool<AddSquadMember> pool;

    void Start()
    {
        pool = new Pool<AddSquadMember>(this.coinPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
        
        StartCoroutine(spawnCoin());
    }

    private IEnumerator spawnCoin()
    {
        yield return new WaitForSeconds(spawnInterval);
        CreateCoin();

        StartCoroutine(spawnCoin());
    }
    public void CreateCoin()
    {
        var coin = this.pool.GetFreeElement();
        if (coin != null)
        {
            coin.transform.position = new Vector3(Random.Range(-40f, 40f), Random.Range(-40f, 40f));
        }
    }
}

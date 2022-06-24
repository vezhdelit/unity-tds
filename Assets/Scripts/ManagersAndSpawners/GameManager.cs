using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float time = 0;
    public float upLevelInterval = 20;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;

    private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(upLevel());
    }

    void Update()
    {
        scoreDisplay.text = score.ToString();
        timerDisplay.text = (Math.Round(time)) + "s";
        time += Time.deltaTime;
        
    }
    
    private IEnumerator upLevel()
    {
        yield return new WaitForSeconds(upLevelInterval);

        enemySpawner.SetNewPoolCount(enemySpawner.poolCount + 10);
        enemySpawner.spawnInterval -= 0.01f;
        StartCoroutine(upLevel());
    }
    
}

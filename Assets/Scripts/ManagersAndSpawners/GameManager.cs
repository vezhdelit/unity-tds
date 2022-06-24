using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float time = 0;
    public float upLevelInterval = 20;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private TextMeshProUGUI gameOverDisplay;
    [SerializeField] private GameObject panel;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Time.timeScale = 1;
    }
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
    public void GameOver()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        
        gameOverDisplay.text = $"Zombies killed: {score.ToString()}\nSurvival time: {(Math.Round(time)) + "s"}";
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}

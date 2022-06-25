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
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    
    private EnemySpawner enemySpawner;
    private SquadManager sm;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        sm = FindObjectOfType<SquadManager>();
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
        enemySpawner.spawnInterval -= 0.02f;

        if (enemySpawner.poolCount <= 120 && enemySpawner.spawnInterval>= 0.05f)
        {
            StartCoroutine(upLevel());
        }
    }
    public void GameOver()
    {
        sm.gameObject.SetActive(false);
        enemySpawner.gameObject.SetActive(false);
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        
        gameOverDisplay.text = $"Zombies killed: {score.ToString()}\nSurvival time: {(Math.Round(time)) + "s"}";
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (pausePanel.activeSelf)
        {
            sm.gameObject.SetActive(true);
            enemySpawner.gameObject.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = 1; 
        }
        else
        {
            sm.gameObject.SetActive(false);
            enemySpawner.gameObject.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0; 
        }

    }
    
}

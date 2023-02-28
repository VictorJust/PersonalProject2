using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    private float fixedDeltaTime;
    private float spawnRate = 1;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public GameObject titleScreen;
    public GameObject pauseScreen;

    private SpawnManager spawnManager;
    
    public bool isPaused;
    public bool isGameActive;

    public void Awake()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        this.fixedDeltaTime = Time.fixedDeltaTime;

        livesText.SetText("Lives: " + lives);
        scoreText.SetText("Score: " + score);
    }

    void Update()
    {
        //Check if the user has pressed the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }

        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    IEnumerator SpawnTarget() 
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnManager.gameObject.SetActive(true);
        }
    }

    public void UpdateLives(int livesToDecrease)
    {
        lives += livesToDecrease;
        if (lives == 0)
        {
            GameOver();
        }
        livesText.SetText("Lives: " + lives);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.SetText("Score: " + score);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;

        score = 0;
        lives = 3;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
    }

    public void ChangePaused()
    {
        // Activate / disactivate pause panel
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

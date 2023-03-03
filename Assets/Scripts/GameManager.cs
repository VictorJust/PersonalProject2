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

    //Fields for display the player info
    public Text CurrentPlayerName;
    public Text BestPlayerNameAndScore;

    //Static variables for holding the best player data
    private static int BestScore;
    private static string BestPlayer;

    public void Awake()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        this.fixedDeltaTime = Time.fixedDeltaTime;

        UpdateLives(0);
        UpdateScore(0);

        LoadGameRankScript.LoadGameRank();
    }

    private void Start()
    {
        CurrentPlayerName.text = PlayerDataHandle.Instance.PlayerName;

        SetBestPlayer();
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
        livesText.SetText($"Lives: {lives}");
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.SetText($"Score: {score}");

        PlayerDataHandle.Instance.Score = score;
    }

    public void GameOver()
    {
        isGameActive = false;
        CheckBestPlayer();
        gameOverText.gameObject.SetActive(true);
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CheckBestPlayer()
    {
        int CurrentScore = PlayerDataHandle.Instance.Score;

        if (CurrentScore > BestScore)
        {
            BestPlayer = PlayerDataHandle.Instance.PlayerName;
            BestScore = CurrentScore;

            LoadGameRankScript.SaveGameRank(BestPlayer, BestScore);
        }

        BestPlayerNameAndScore.text = $"Best Score - {BestPlayer}: {BestScore}";
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerNameAndScore.text = "";
        }
        else
        {
            BestPlayerNameAndScore.text = $"Best Score - {BestPlayer}: {BestScore}";
        }
    }
}

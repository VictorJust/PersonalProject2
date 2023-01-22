using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    public void AddLives(int value)
    {
        lives += value;
        if (lives <= 0)
        {
            lives = 0;
            Debug.Log("Game Over!");
        }
        Debug.Log("Lives = " + lives);
    }
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
    }
}

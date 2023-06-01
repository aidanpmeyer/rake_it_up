using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Score tracking
    private int score = 0;
    public int winThresh = 40;

    // Timer variables
    public float levelTime = 30f;
    private float timeRemaining; // Total time for the scene in seconds
    private bool isTimerRunning = false;

    // UI elements
    public TextMeshProUGUI timeText;

    // Win/Lose screens
    public string winScreen;
    public string loseScreen;

    private void Awake()
    {
        timeRemaining = levelTime;
        isTimerRunning = true;
        //singleton nonsense
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //once per frame
    private void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            // Update the UI text for the remaining time
            timeText.text = "Time Left: " + Mathf.Max(0, Mathf.FloorToInt(timeRemaining)).ToString("00");

            // Check if time has run out
            if (timeRemaining <= 0f)
            {
                isTimerRunning = false;
                CheckScore();
            }
        }
    }

    // Public method to update the score by a specified amount
    public void UpdateScore(int amount)
    {
        score += amount;
        CheckScore();
    }

    public int GetScore()
    {
        return score;
    }

    // Start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Method to check the score and load appropriate screens
    private void CheckScore()
    {
        if (score >= winThresh)
        {
            // Load win screen
            SceneManager.LoadScene(winScreen);
        }
        else if (timeRemaining <= 0f)
        {
            // Load lose screen
            SceneManager.LoadScene(loseScreen);
        }
    }

}


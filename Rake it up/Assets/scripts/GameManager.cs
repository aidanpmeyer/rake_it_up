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

    public Transform Nikki;
    public PlayerController Player;

    public Camera NikkiCam;
    public Camera ThirdPerson;
    public Camera FirstPerson;

    public float NikkiTime = 2;
    public float ThirdTime = 4;
    public float NikkiSpeed = 50;
    public bool autoFall = false;
    public bool roundStarted = false;

    public GameObject Title;
    public GameObject UI;

    public Camera rotatingCam;

    private void Awake()
    {
       
        timeRemaining = levelTime;
        isTimerRunning = true;

        Title.SetActive(true);
        UI.SetActive(false);
        NikkiCam.gameObject.SetActive(false);
        ThirdPerson.gameObject.SetActive(false);
        FirstPerson.gameObject.SetActive(false);
        rotatingCam.gameObject.SetActive(true);

        Nikki.gameObject.SetActive(false);

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
        if (!roundStarted && Input.GetKeyDown(KeyCode.Space))
        {
            roundStarted = true;
            FirstPerson.gameObject.SetActive(true);
            rotatingCam.gameObject.SetActive(false);
            Title.SetActive(false);
            UI.SetActive(true);

        }
        if (roundStarted && isTimerRunning)
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
            StartCoroutine(LoseSequence());
        }
    }

    private System.Collections.IEnumerator LoseSequence() {

        Player.DisableController();
        Player.gameObject.AddComponent<Rigidbody>();
        Nikki.gameObject.SetActive(true);

        Nikki.position = new Vector3(Player.GetPosition().x - 150f, 0f, Player.GetPosition().z);

        Nikki.GetComponent<Rigidbody>().velocity = new Vector3(NikkiSpeed, 0, 0);

        FirstPerson.gameObject.SetActive(false);
        ThirdPerson.gameObject.SetActive(false);
        NikkiCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(NikkiTime);

        FirstPerson.gameObject.SetActive(false);
        NikkiCam.gameObject.SetActive(false);
        ThirdPerson.gameObject.SetActive(true);

        yield return new WaitForSeconds(ThirdTime);

        if(loseScreen == SceneManager.GetActiveScene().name)
        {
            Awake();
        }
        SceneManager.LoadScene(loseScreen);
    }
}


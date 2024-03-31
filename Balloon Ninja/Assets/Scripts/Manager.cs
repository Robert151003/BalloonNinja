using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Manager : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1.0f;
        LoadPlayer();
    }

    [Header("PauseMenu")]
    public GameObject pauseMenuCanvas;
    public GameObject mainCanvas;
    public GameObject endCanvas;

    [Header("PlayerInfo")]
    //MainGain
    public int health = 3;
    public Slider healthSlider;
    public int score;
    public TMP_Text endScoreCounter;
    public TMP_Text scoreCounter;
    public int highscore;
    public TMP_Text highScoreCounter;

    //MiniGame
    public int miniHighScore;
    public TMP_Text miniHighScoreCounter;

    public float timer = 2f;
    public float miniGameTimer = 60f;
    public TMP_Text gameTime;

    public bool bomb = false;

    public float startTimer;
    public GameObject startScreen;

    public bool newDart;
    public Transform spawnPoint;
    public GameObject dartPrefab;

    public bool mainGame;
    public bool miniGame;

    //PauseMenu
    public void PauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    public void EndScreen()
    {
        endCanvas.SetActive(true);
        mainCanvas.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            highScoreCounter.text = highscore.ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            miniHighScoreCounter.text = miniHighScore.ToString();
        }
        endScoreCounter.text = score.ToString();

        Time.timeScale = 0f;
        SavePlayer();
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name);
        score = 0;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        score= 0;
    }

    //ScoreCounter
    void Update()
    {

        scoreCounter.text = score.ToString();

        //MainGame
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthSlider.value = health;

            if (score > highscore)
            {
                highscore = score;
            }
            if (bomb)
            {
                health -= 1;
                bomb = false;
            }
            if(health == 0)
            {
                EndScreen();
            }
        }

        //MiniGame
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            miniGameTimer -= Time.deltaTime;
            gameTime.text = Mathf.FloorToInt(miniGameTimer).ToString();
            if (miniGameTimer < 0f)
            {
                miniGameTimer = 0f;
                EndScreen();
            }

            if (miniHighScore < score)
            {
                miniHighScore = score;
            }

            if (bomb)
            {
                EndScreen();
            }
        }

        if (newDart)
        {
            Instantiate(dartPrefab, spawnPoint.position, dartPrefab.transform.rotation);

            newDart = false;
        }
        
        

        //remove, just to test save system        
        //timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SavePlayer();
            timer = 2f;
        }
    }


    //Save system
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        
        try
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data != null)
            {
                highscore = data.highscore;
                miniHighScore = data.miniHighScore;

            }
            else
            {
                Debug.LogWarning("Failed to load data");
            }
        
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message);
        }
        
    }
}

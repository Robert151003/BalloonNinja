using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject achievementsMenu;

    public GameObject startButton;
    public GameObject startMinigameButton;
    public GameObject sendingBalloon;
    public Animator fadeSwipe;
    public bool startPress = false;
    public int buildIndex;
    public float moveSpeed;
    public float timer;

    public void Update()
    {
        if (startPress)
        {
            Vector2 endPoint = new Vector2(sendingBalloon.transform.position.x, sendingBalloon.transform.position.y + 25f);
            fadeSwipe.SetBool("Fade", true);
            sendingBalloon.transform.position = Vector2.Lerp(sendingBalloon.transform.position, endPoint, moveSpeed*Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(buildIndex);
            }
        }
    }
    public void startGame()
    {
        timer = 2f;
        sendingBalloon = startButton;
        startPress = true;
        buildIndex = 1;
        
    }

    public void startMiniGame()
    {
        timer = 2f;
        sendingBalloon = startMinigameButton;
        startPress = true;
        buildIndex = 2;
    }

    public void backButton()
    {
        MainMenu.SetActive(true);
        achievementsMenu.SetActive(false);
    }

    public void openAchievements()
    {
        MainMenu.SetActive(false);
        achievementsMenu.SetActive(true);
    }
}

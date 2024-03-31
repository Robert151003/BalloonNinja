using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera cam { get; private set; }
    int score;


    void Awake()
    {
        Instance = this;

        cam = Camera.main;
    }


    void SetScore(int newScore)
    {
        score = newScore;
        EventManager.ScoreChanged(score);
    }

    void EventManager_BalloonPopped(GameObject go, BalloonType type)
    {
        switch (type)
        {
            case BalloonType.Normal:
                SetScore(score + 1);
                break;

            case BalloonType.Bomb:
                /// Lose
                break;
        }

        // Handle destroying here so we can more easily switch to object pooling later if we want to
        Destroy(go);
    }

    void EventManager_OnTookDamage()
    {

    }


    void OnEnable()
    {
        EventManager.OnBalloonPopped += EventManager_BalloonPopped;
        EventManager.OnTookDamage += EventManager_OnTookDamage;
    }

    void OnDisable()
    {
        EventManager.OnBalloonPopped -= EventManager_BalloonPopped;
        EventManager.OnTookDamage -= EventManager_OnTookDamage;
    }
}
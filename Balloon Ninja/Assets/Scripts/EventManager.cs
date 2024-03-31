using UnityEngine;
using System;

public static class EventManager
{
    public static event Action<GameObject, BalloonType> OnBalloonPopped;
    public static void BalloonPopped(GameObject go, BalloonType type) => OnBalloonPopped?.Invoke(go, type);


    public static event Action<int> OnScoreChanged;
    public static void ScoreChanged(int score) => OnScoreChanged?.Invoke(score);


    public static event Action OnTookDamage;
    public static void TookDamage() => OnTookDamage?.Invoke();
}

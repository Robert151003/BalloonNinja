using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highscore;
    public int miniHighScore;

    public PlayerData(Manager manager)
    {
        highscore = manager.highscore;
        miniHighScore = manager.miniHighScore;
    }
}

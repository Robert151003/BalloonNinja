using UnityEngine;

public class BombBalloon : Balloon
{
    public override BalloonType Type => BalloonType.Bomb;

    [SerializeField] Color balloonColor;

    public override Color GetColor() => balloonColor;
}
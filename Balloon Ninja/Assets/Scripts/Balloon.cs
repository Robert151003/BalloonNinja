using UnityEngine;

public class Balloon : BaseBalloon
{
    public override BalloonType Type => BalloonType.Normal;

    public override Color GetColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }
}

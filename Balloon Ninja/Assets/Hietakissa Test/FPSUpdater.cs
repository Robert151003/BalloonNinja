using UnityEngine;
using TMPro;
using HietakissaUtils;

public class FPSUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;
    float updateTime;

    void Update()
    {
        updateTime -= Time.deltaTime;

        if (updateTime <= 0f)
        {
            updateTime = 0.5f;
            UpdateText();
        }
    }


    void UpdateText()
    {
        fpsText.text = $"FPS: {(1f / Time.deltaTime).RoundToNearest()}";
    }
}
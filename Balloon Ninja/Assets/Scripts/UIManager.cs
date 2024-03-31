using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Menu[] menus;
    [SerializeField] TextMeshProUGUI[] scoreTexts; //I got lazy with the ui implementation


    public void OpenMenu(string menuName)
    {
        foreach (Menu menu in menus)
        {
            menu.gameObject.SetActive(menu.menuName == menuName);
        }
    }


    void EventManager_OnScoreChanged(int score)
    {
        foreach (TextMeshProUGUI text in scoreTexts)
        {
            text.text = $"Score:\n{score}";
        }
    }


    void OnEnable()
    {
        EventManager.OnScoreChanged += EventManager_OnScoreChanged;
    }

    void OnDisable()
    {
        EventManager.OnScoreChanged -= EventManager_OnScoreChanged;
    }
}
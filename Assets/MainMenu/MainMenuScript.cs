using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Button goBackButton;

    [NonSerialized] public bool mainMenuEnabled;

    private void Start()
    {
        mainMenuEnabled = true;
        gameObject.SetActive(true);
        generalPanel.SetActive(true);
    }

    public void PlayGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
        mainMenuEnabled = false;
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        generalPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnGoBackButtonClicked()
    {
        settingsPanel.SetActive(false);
        generalPanel.SetActive(true);
    }
}

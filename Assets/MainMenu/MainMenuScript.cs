using System;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public bool mainMenuEnabled;

    void Start()
    {
        mainMenuEnabled = true;
    }

    public void PlayGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
        mainMenuEnabled = false;
    }

    public void ShowSettings()
    {
        print("Not implemented yet");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

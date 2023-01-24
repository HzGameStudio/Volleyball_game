using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool SettingsMenuActive = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (SettingsMenuActive)
                    UnloadMenu();
                else
                    Resume();
            }
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        SettingsMenuActive = true;
    }

    public void UnloadMenu()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        SettingsMenuActive = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
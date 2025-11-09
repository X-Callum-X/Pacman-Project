using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public bool isPaused;
    public bool canPause = true;

    void Start()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        canPause = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && canPause)
        {
            if (isPaused && !SettingsMenu.activeSelf)
            {
                ResumeGame();
            }
            else if (!isPaused && !SettingsMenu.activeSelf)
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        SFXManager.instance.sfxSource.Stop();
        SFXManager.PlaySFX(SFX.POWER_PELLET);

        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        SFXManager.PlaySFX(SFX.POWER_PELLET);

        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}

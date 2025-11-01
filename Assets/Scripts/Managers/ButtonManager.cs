using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public PauseManager pauseManager {  get; private set; }

    private void Awake()
    {
        pauseManager = FindFirstObjectByType<PauseManager>();
    }

    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseManager.ResumeGame();
    }

    private IEnumerator SwitchDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
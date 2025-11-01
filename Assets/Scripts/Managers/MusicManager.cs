using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameManager gameManager;
    public PauseManager pauseManager;

    public AudioSource musicSource;
    public AudioClip blockTown;
    public AudioClip pacmansPark;
    public AudioClip sandboxLand;
    public AudioClip junglyStreets;

    public bool isMusicPaused = false;

    private void Start()
    {
        this.gameManager = FindFirstObjectByType<GameManager>();
        this.pauseManager = FindFirstObjectByType<PauseManager>();
    }
    private void Update()
    {
        MusicController();
    }

    private void MusicController()
    {
        if (gameManager.isMusicPlaying)
        {
            if (!musicSource.isPlaying && !isMusicPaused )
            {
                musicSource.PlayOneShot(pacmansPark);
            }
            if (!pauseManager.isPaused)
            {
                musicSource.UnPause();
                isMusicPaused = false;
            }
            else if (pauseManager.isPaused)
            {
                musicSource.Pause();
                isMusicPaused = true;
            }
        }
        else
        {
            musicSource.Stop();
        }
    }
}
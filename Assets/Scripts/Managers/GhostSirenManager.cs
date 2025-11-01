using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GhostSirenManager : MonoBehaviour
{
    public GameManager gameManager {  get; private set; }
    public PauseManager pauseManager { get; private set; }

    public Ghost ghost { get; private set; }
    public AudioSource ghostSiren;
    public AudioClip siren0;
    public AudioClip siren1;
    public AudioClip siren2;
    public AudioClip siren3;
    public AudioClip siren4;
    public AudioClip fright;

    private void Start()
    {
        this.gameManager = FindFirstObjectByType<GameManager>();
        this.pauseManager = FindFirstObjectByType<PauseManager>();
        this.ghost = FindFirstObjectByType<Ghost>();

        if (!this.ghost.frightened.enabled && !ghostSiren.isPlaying)
        {
            ghostSiren.PlayOneShot(siren0);
        }
    }

    private void Update()
    {
        if (!gameManager.isGhostSirenPlaying)
        {
            ghostSiren.Stop();
        }
        else if (pauseManager.isPaused)
        {
            ghostSiren.Pause();
        }
        else
        {
            if (this.enabled && !this.ghost.frightened.enabled && !ghostSiren.isPlaying)
            {
                if (this.ghost.gameManager.pelletsEaten < 100)
                {
                    ghostSiren.PlayOneShot(siren0);
                }
                else if (this.ghost.gameManager.pelletsEaten < 150)
                {
                    ghostSiren.PlayOneShot(siren1);
                }
                else if (this.ghost.gameManager.pelletsEaten < 180)
                {
                    ghostSiren.PlayOneShot(siren2);
                }
                else if (this.ghost.gameManager.pelletsEaten <= 210)
                {
                    ghostSiren.PlayOneShot(siren3);
                }
                else if (this.ghost.gameManager.pelletsEaten > 210)
                {
                    ghostSiren.PlayOneShot(siren4);
                }
            }
            else
            {
                if (!ghostSiren.isPlaying)
                {
                    ghostSiren.PlayOneShot(fright);
                }
            }
        }
    }
}
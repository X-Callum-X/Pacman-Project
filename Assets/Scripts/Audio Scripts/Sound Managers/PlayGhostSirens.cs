using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayGhostSirens : MonoBehaviour
{
    public GameManager gameManager {  get; private set; }
    public PauseManager pauseManager { get; private set; }

    public Ghost ghost { get; private set; }

    private void Start()
    {
        this.gameManager = FindFirstObjectByType<GameManager>();
        this.pauseManager = FindFirstObjectByType<PauseManager>();
        this.ghost = FindFirstObjectByType<Ghost>();

        if (!this.ghost.frightened.enabled && !GhostSirenManager.instance.sirenSource.isPlaying)
        {
            GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_0);
        }
    }

    private void Update()
    {
        if (!gameManager.isSFXSourcePlaying)
        {
            GhostSirenManager.instance.sirenSource.Stop();
        }
        else if (pauseManager.isPaused)
        {
            GhostSirenManager.instance.sirenSource.Pause();
        }
        else
        {
            if (this.enabled && !this.ghost.frightened.enabled && !GhostSirenManager.instance.sirenSource.isPlaying)
            {
                if (this.ghost.gameManager.pelletsEaten < 100)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_0);
                }
                else if (this.ghost.gameManager.pelletsEaten < 150)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_1);
                }
                else if (this.ghost.gameManager.pelletsEaten < 180)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_2);
                }
                else if (this.ghost.gameManager.pelletsEaten <= 210)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_3);
                }
                else if (this.ghost.gameManager.pelletsEaten > 210)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_SIREN_4);
                }
            }
            else
            {
                if (!GhostSirenManager.instance.sirenSource.isPlaying)
                {
                    GhostSirenManager.PlaySiren(GhostSirens.GHOST_FRIGHT);
                }
            }
        }
    }
}
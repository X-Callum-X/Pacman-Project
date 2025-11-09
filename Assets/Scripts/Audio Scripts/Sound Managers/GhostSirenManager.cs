using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GhostSirens
{
    GHOST_SIREN_0,
    GHOST_SIREN_1,
    GHOST_SIREN_2,
    GHOST_SIREN_3,
    GHOST_SIREN_4,
    GHOST_FRIGHT,
    GHOST_EYES
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class GhostSirenManager : MonoBehaviour
{
    [SerializeField] private SirenList[] sirenList;
    [HideInInspector] public static GhostSirenManager instance;
    [HideInInspector] public AudioSource sirenSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sirenSource = GetComponent<AudioSource>();
    }

    public static void PlaySiren(GhostSirens siren, float volume = 1)
    {
        AudioClip[] clips = instance.sirenList[(int)siren].GhostSirens;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.sirenSource.PlayOneShot(randomClip);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] sirenNames = Enum.GetNames(typeof(GhostSirens));

        Array.Resize(ref sirenList, sirenNames.Length);

        for (int i = 0; i < sirenNames.Length; i++)
        {
            sirenList[i].name = sirenNames[i];
        }
    }
#endif
}

[Serializable]
public struct SirenList
{
    public AudioClip[] GhostSirens { get => sirens; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sirens;
}

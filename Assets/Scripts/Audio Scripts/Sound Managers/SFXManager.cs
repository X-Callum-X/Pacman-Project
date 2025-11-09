using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SFX
{
    WAKA_1,
    WAKA_2,
    WAKA_FULL,
    PAC_DEATH,
    GHOST_EATEN,
    FRUIT_EATEN,
    EXTRA_LIFE,
    AUDIENCE_APPLAUSE,
    POWER_PELLET,
    LEVEL_START,
    INSERT_CREDIT
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SFXManager : MonoBehaviour
{
    [SerializeField] private SFXList[] sfxList;
    [HideInInspector] public static SFXManager instance;
    [HideInInspector] public AudioSource sfxSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public static void PlaySFX(SFX sfx, float volume = 1)
    {
        AudioClip[] clips = instance.sfxList[(int)sfx].SFX;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.sfxSource.PlayOneShot(randomClip);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] sfxNames = Enum.GetNames(typeof(SFX));

        Array.Resize(ref sfxList, sfxNames.Length);

        for (int i = 0; i < sfxNames.Length; i++)
        {
            sfxList[i].name = sfxNames[i];
        }
    }
#endif
}

[Serializable]
public struct SFXList
{
    public AudioClip[] SFX { get => sfx; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sfx;
}
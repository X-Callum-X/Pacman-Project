using System;
using UnityEngine;

public enum SFX
{
    WAKA_1,
    WAKA_2,
    WAKA_FULL,
    PAC_DEATH,
    GHOST_EATEN,
    FRUIT_EATEN,
    EXTRA_LIFE,
    GHOST_SIREN_0,
    GHOST_SIREN_1,
    GHOST_SIREN_2,
    GHOST_SIREN_3,
    GHOST_SIREN_4,
    GHOST_FRIGHT,
    GHOST_EYES,
    AUDIENCE_APPLAUSE,
    POWER_PELLET,
    LEVEL_START,
    INSERT_CREDIT
}

public enum Music
{
    BLOCK_TOWN,
    PACMANS_PARK,
    SANDBOX_LAND,
    JUNGLY_STEPS,
    GAME_OVER
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SFXList[] sfxList;
    [SerializeField] private MusicList[] musicList;
    private static SoundManager instance;
    private AudioSource sfxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        musicSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SFX sfx, float volume = 1)
    {
        AudioClip[] clips = instance.sfxList[(int)sfx].SFX;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.sfxSource.PlayOneShot(randomClip);
    }

    public static void PlayMusic(Music music, float volume = 1)
    {
        AudioClip[] clips = instance.musicList[(int)music].Music;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.musicSource.PlayOneShot(randomClip);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] sfxNames = Enum.GetNames(typeof(SFX));
        string[] musicNames = Enum.GetNames(typeof(Music));

        Array.Resize(ref sfxList, sfxNames.Length);
        Array.Resize(ref musicList, musicNames.Length);

        for (int i = 0; i < sfxNames.Length;)
        {
            sfxList[i].name = sfxNames[i];
        }

        for (int i = 0; i < musicNames.Length;)
        {
            musicList[i].name = musicNames[i];
        }

    }
#endif
}

[Serializable]
public struct SFXList
{
    public AudioClip[] SFX { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}

[Serializable]
public struct MusicList
{
    public AudioClip[] Music { get => music; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] music;
}
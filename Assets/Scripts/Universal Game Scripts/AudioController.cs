using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);
    }
}

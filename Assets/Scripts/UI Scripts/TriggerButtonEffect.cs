using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EasyTextEffects;

public class TriggerButtonEffect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public AudioSource source;
    public AudioClip waka;

    private TextEffect buttonText;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextEffect>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(buttonText + " was selected");
        source.PlayOneShot(waka);

        if (buttonText != null)
        {
            buttonText.StartManualEffects();
        }
    }
    public void OnDeselect(BaseEventData eventData)
    {
        buttonText.StopManualEffects();
    }
}
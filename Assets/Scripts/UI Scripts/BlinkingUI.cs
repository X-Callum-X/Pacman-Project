using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    private Text playerText;

    private void Start()
    {
        playerText = GetComponent<Text>();
        Invoke(nameof(StartBlinking), 0.5f);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (playerText.color.a.ToString())
            {
                case "0":
                    playerText.color = new Color(playerText.color.r, playerText.color.g, playerText.color.b, 1);
                    yield return new WaitForSeconds(0.25f);
                    break;
                case "1":
                    playerText.color = new Color(playerText.color.r, playerText.color.g, playerText.color.b, 0);
                    yield return new WaitForSeconds(0.25f);
                    break;
            }
        }
    }

    private void StartBlinking()
    {
        StopCoroutine(Blink());
        StartCoroutine(Blink());
    }

    private void StopBlinking()
    {
        StopCoroutine(Blink());
    }
}
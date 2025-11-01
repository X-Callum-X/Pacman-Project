using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyFruit : MonoBehaviour
{
    public int points = 100;
    public TMP_Text fruitScoreText;

    protected virtual void Eat()
    {
        FindFirstObjectByType<GameManager>().FruitEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

    public IEnumerator DisplayScoreText(float duration)
    {
        float timer = 0f;

        fruitScoreText.gameObject.SetActive(true);

        while (timer < duration)
        {
            // Use Time.unscaledDeltaTime because deltaTime is affected by timeScale
            timer += Time.unscaledDeltaTime;

            yield return null; // Wait until the next frame
        }

        fruitScoreText.gameObject.SetActive(false);
    }
}
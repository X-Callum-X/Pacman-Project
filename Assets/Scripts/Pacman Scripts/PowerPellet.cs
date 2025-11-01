using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public GameManager gameManager;
    private int s = 0;

    protected override void Eat()
    {
        FindFirstObjectByType<GameManager>().PowerPelletEaten(this);
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Invoke(nameof(StartBlinking), 0.175f);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (s.ToString())
            {
                case "0":
                    spriteRenderer.enabled = false;
                    yield return new WaitForSeconds(0.175f);
                    s = 1;
                    break;
                case "1":
                    spriteRenderer.enabled = true;
                    yield return new WaitForSeconds(0.175f);
                    s = 0;
                    break;
            }
        }
    }

    private void StartBlinking()
    {
        StopCoroutine(Blink());

        if (this.gameObject.activeSelf)
        {
            StartCoroutine(Blink());
        }
    }

    private void StopBlinking()
    {
        StopCoroutine(Blink());
    }
}
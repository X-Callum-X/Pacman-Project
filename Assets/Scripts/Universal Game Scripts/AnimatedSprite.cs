using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animTime = 0.075f;
    public int animFrame {  get; private set; }
    private bool isLooping = true;

    private void OnDisable()
    {
        CancelInvoke("Advance");
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Advance), this.animTime, this.animTime);
    }

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        this.animFrame++;

        if (this.animFrame >= this.sprites.Length && this.isLooping)
        {
            this.animFrame = 0;
        }

        if (this.animFrame >= 0 && this.animFrame < this.sprites.Length)
        { 
            this.spriteRenderer.sprite = this.sprites[this.animFrame];
        }

    }

    public void Restart()
    {
        this.animFrame = -1;

        Advance();
    }
}
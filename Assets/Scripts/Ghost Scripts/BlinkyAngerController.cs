using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkyAngerControllwe: MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Movement movement { get; private set; }
    public GameManager gameManager { get; private set; }

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public Sprite angryUp;
    public Sprite angryDown;
    public Sprite angryLeft;
    public Sprite angryRight;

    public Sprite reallyAngryUp;
    public Sprite reallyAngryDown;
    public Sprite reallyAngryLeft;
    public Sprite reallyAngryRight;

    public int howManyPelletsToTrigger;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (this.gameManager.pelletsEaten >= howManyPelletsToTrigger)
        {
            if (this.movement.direction == Vector2.up)
            {
                this.spriteRenderer.sprite = this.reallyAngryUp;
            }
            if (this.movement.direction == Vector2.down)
            {
                this.spriteRenderer.sprite = this.reallyAngryDown;
            }
            if (this.movement.direction == Vector2.left)
            {
                this.spriteRenderer.sprite = this.reallyAngryLeft;
            }
            if (this.movement.direction == Vector2.right)
            {
                this.spriteRenderer.sprite = this.reallyAngryRight;
            }

            this.movement.speed = 12.5f;
        }
        else if (this.gameManager.pelletsEaten > 210)
        {
            if (this.movement.direction == Vector2.up)
            {
                this.spriteRenderer.sprite = this.angryUp;
            }
            if (this.movement.direction == Vector2.down)
            {
                this.spriteRenderer.sprite = this.angryDown;
            }
            if (this.movement.direction == Vector2.left)
            {
                this.spriteRenderer.sprite = this.angryLeft;
            }
            if (this.movement.direction == Vector2.right)
            {
                this.spriteRenderer.sprite = this.angryRight;
            }

            this.movement.speed = 8.25f;
        }
        else
        {
            if (this.movement.direction == Vector2.up)
            {
                this.spriteRenderer.sprite = this.up;
            }
            if (this.movement.direction == Vector2.down)
            {
                this.spriteRenderer.sprite = this.down;
            }
            if (this.movement.direction == Vector2.left)
            {
                this.spriteRenderer.sprite = this.left;
            }
            if (this.movement.direction == Vector2.right)
            {
                this.spriteRenderer.sprite = this.right;
            }
        }
    }
}
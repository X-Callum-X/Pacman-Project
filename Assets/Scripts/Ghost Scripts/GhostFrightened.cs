using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostFrightened : GhostBehaviour
{
    public GhostSirenManager sirenManager;
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public TMP_Text ghostScoreText;

    private GameManager gameManager;
    private SpriteRenderer pacmanSprite;
    private PauseManager pauseManager;

    public float delayDuration = 1.25f;

    public bool eaten { get; private set; }

    private void Start()
    {
        pacmanSprite = GameObject.Find("Pacman").GetComponent<SpriteRenderer>();
        pauseManager = FindFirstObjectByType<PauseManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public override void Enable(float duration)
    {
        this.sirenManager.ghostSiren.Stop();
        this.eaten = false;
        base.Enable(duration);

        this.ghost.movement.SetDirection(-this.ghost.movement.direction);

        if (!this.eaten)
        {
            this.body.enabled = false;
            this.eyes.enabled = false;
            this.blue.enabled = true;
            this.white.enabled = false;
        }

        Invoke(nameof(Flash), duration - 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        this.eaten = true;

        StopAllCoroutines();

        StartCoroutine(DelayAfterEaten(0f, 1f, delayDuration));

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Makes the ghosts move in random directions
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count >= 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }

        //// This commented code makes the ghosts move directly away from Pacman

        //Node node = other.GetComponent<Node>();

        //if (node != null && this.enabled)
        //{
        //    Vector2 direction = Vector2.zero;
        //    float maxDistance = float.MinValue;

        //    foreach (Vector2 availableDirection in node.availableDirections)
        //    {
        //        Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
        //        float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

        //        if (distance > maxDistance)
        //        {
        //            direction = availableDirection;
        //            maxDistance = distance;
        //        }
        //    }

        //    this.ghost.movement.SetDirection(direction);
        //}
    }

    private IEnumerator DelayAfterEaten(float startScale, float endScale, float duration)
    {
        float timer = 0f;
        Time.timeScale = startScale; // Ensure it starts at 0
        pacmanSprite.enabled = false;
        ghostScoreText.text = (this.ghost.points * (this.gameManager.ghostMultiplier / 2)).ToString();
        ghostScoreText.gameObject.SetActive(true);
        pauseManager.canPause = false;

        while (timer < duration)
        {
            // Use Time.unscaledDeltaTime because deltaTime is affected by timeScale
            timer += Time.unscaledDeltaTime;

            //// Calculate the new timescale using Lerp
            //Time.timeScale = Mathf.Lerp(startScale, endScale, timer / duration);

            yield return null; // Wait until the next frame
        }

        // Ensure the final value is exactly 1.0f
        Time.timeScale = endScale;
        pacmanSprite.enabled = true;
        ghostScoreText.gameObject.SetActive(false);
        this.eyes.enabled = true;
        pauseManager.canPause = true;

        Vector3 position = this.ghost.house.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.house.Enable(this.duration);
    }
}
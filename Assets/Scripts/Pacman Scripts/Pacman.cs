using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public AnimatedSprite pacmanAnim;
    public Movement movement {  get; private set; }
    public PauseManager pauseManager { get; private set; }

    public void Awake()
    {
        this.pacmanAnim = GetComponent<AnimatedSprite>();
        this.movement = GetComponent<Movement>();
        this.pauseManager = FindFirstObjectByType<PauseManager>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, this.movement.direction, 0.5f, this.movement.obstacleLayer);

        if (hit)
        {
            pacmanAnim.enabled = false;
        }
        else
        {
            pacmanAnim.enabled = true;
        }

        if (!pauseManager.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.movement.SetDirection(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.movement.SetDirection(Vector2.down);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.movement.SetDirection(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.movement.SetDirection(Vector2.right);
            }
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}

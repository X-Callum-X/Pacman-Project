using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHouse house { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    public GhostBehaviour initialBehaviour;

    public GameManager gameManager;

    public Transform scatterTarget;
    public Transform chaseTarget;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.house = GetComponent<GhostHouse>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.movement.speed = 7.0f;
        this.gameObject.SetActive(true);

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.house != initialBehaviour)
        {
            this.house.Disable();
        }

        if (this.initialBehaviour != null) 
        {
            this.initialBehaviour.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindFirstObjectByType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindFirstObjectByType<GameManager>().PacmanEaten();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "SlowTriggerLeft" || other.gameObject.name == "SlowTriggerRight")
        {
            this.movement.speed /= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "SlowTriggerLeft" || other.gameObject.name == "SlowTriggerRight")
        {
            this.movement.speed *= 2;
        }
    }
}
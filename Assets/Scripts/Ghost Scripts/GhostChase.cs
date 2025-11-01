using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
        this.ghost.movement.SetDirection(-this.ghost.movement.direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Makes the ghost move directly towards Pacman, picking the fastest route
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                if (availableDirection != -this.ghost.movement.direction)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                    if (this.ghost.chaseTarget != null)
                    {
                        float distance = (this.ghost.chaseTarget.position - newPosition).sqrMagnitude;

                        if (distance < minDistance)
                        {
                            direction = availableDirection;
                            minDistance = distance;
                        }
                    }
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (this.enabled)
        {
            Gizmos.DrawLine(this.transform.position, this.ghost.chaseTarget.position);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
        this.ghost.movement.SetDirection(-this.ghost.movement.direction);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //// This commented code makes the ghosts move in random directions

        //Node node = other.GetComponent<Node>();

        //if (node != null && this.enabled && !this.ghost.frightened.enabled)
        //{
        //    int index = Random.Range(0, node.availableDirections.Count);

        //    if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
        //    {
        //        index++;

        //        if (index >= node.availableDirections.Count)
        //        {
        //            index = 0;
        //        }
        //    }

        //    this.ghost.movement.SetDirection(node.availableDirections[index]);
        //}

        // Makes the ghosts move to their scatter targets
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

                    if (this.ghost.scatterTarget != null)
                    {
                        float distance = (this.ghost.scatterTarget.position - newPosition).sqrMagnitude;

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
            Gizmos.DrawLine(this.transform.position, this.ghost.scatterTarget.position);
        }
    }
}
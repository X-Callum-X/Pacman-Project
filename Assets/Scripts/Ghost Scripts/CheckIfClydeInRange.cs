using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfClydeInRange : GhostBehaviour
{
    public Transform pacman;
    public Transform clydeScatterTarget;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ClydeTarget"))
        {
            this.ghost.chaseTarget = clydeScatterTarget;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ClydeTarget"))
        {
            this.ghost.chaseTarget = clydeScatterTarget;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ClydeTarget"))
        {
            this.ghost.chaseTarget = pacman;
        }
    }
}

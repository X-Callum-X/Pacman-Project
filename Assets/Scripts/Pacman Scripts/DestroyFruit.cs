using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFruit : MonoBehaviour
{
    public int points = 100;

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
}
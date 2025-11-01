using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class GetInkyTarget : MonoBehaviour
{
    public GameObject blinky;
    public GameObject pinkyChaseTarget;
    public GameObject target;

    private void Update()
    {
        GetTarget();
    }

    private void GetTarget()
    {
        float xDistance = pinkyChaseTarget.transform.position.x - blinky.transform.position.x;
        float yDistance = pinkyChaseTarget.transform.position.y - blinky.transform.position.y;

        target.transform.position = new Vector2((pinkyChaseTarget.transform.position.x * 2) + xDistance, (pinkyChaseTarget.transform.position.y * 2) + yDistance);
    }
}

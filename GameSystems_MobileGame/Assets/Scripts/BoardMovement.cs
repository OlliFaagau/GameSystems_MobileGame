using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour
{
    public Transform[] waypoints;

    [HideInInspector] private float moveSpeed = 1f;
    [HideInInspector] public int waypointIndex = 0;

    public bool moveAllowed = false;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 4);
    }

    void Update()
    {
        if (moveAllowed)
        {
            Move();
        }
    }

    private void Move()
    {
        if(waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                waypoints[waypointIndex].transform.position, 
                moveSpeed * 0.01f);

            if(transform.position.x == waypoints[waypointIndex].transform.position.x 
                && transform.position.y == waypoints[waypointIndex].transform.position.y)
            {
                Debug.Log("Adding to Index");
                waypointIndex += 1;
            }
        }
    }
}

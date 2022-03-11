using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Track : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       transform.position =  Vector2.MoveTowards(transform.position, Player.transform.position, Time.deltaTime);
    }
}

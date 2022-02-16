using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnBoard : MonoBehaviour
{
    public GameObject[] players;
    private int numOfPlayers;

    void Start()
    {
        numOfPlayers = GameManager.playerNum;
        SetBoard();
    }

    void Update()
    {
        //if (players[GameManager.pM].GetComponent<BoardMovement>().waypointIndex < 1 && GameManager.canMove)
        //{
        //    players[GameManager.pM].GetComponent<BoardMovement>().moveAllowed = false;
        //}
    }

    void SetBoard()
    {
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }

        for (int i = 0; i < numOfPlayers; i++)
        {
            players[i].SetActive(true);
            players[i].GetComponent<SpriteRenderer>().sprite = GameManager.sprites[i];
            players[i].GetComponent<SpriteRenderer>().color = GameManager.colors[i];
        }
    }

}

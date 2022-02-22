using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnBoard : MonoBehaviour
{
    public GameObject[] players;
    public Text roundText;
    private int numOfPlayers;

    void Start()
    {
        Time.timeScale = 1;
        numOfPlayers = GameManager.playerNum;
        roundText.text = "Round " + GameManager.roundNum;
        SetBoard();
    }

    void Update()
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            if(GameManager.movePlayers[i].y > 0)
                players[i].transform.position = Vector2.Lerp(players[i].transform.position, GameManager.movePlayers[i], Time.deltaTime);
        }
       
    }

    void SetBoard()
    {
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }

        for (int i = 0; i < numOfPlayers; i++)
        {
            if(GameManager.movePlayers[i].y == players[i].transform.position.y)
                GameManager.movePlayers[i] = players[i].transform.position;
            players[i].SetActive(true);
            players[i].GetComponent<SpriteRenderer>().sprite = GameManager.sprites[i];
            players[i].GetComponent<SpriteRenderer>().color = GameManager.colors[i];
        }
    }

    public void ResetBoard()
    {
        GameManager.roundNum = 1;

        for (int i = 0; i < numOfPlayers; i++)
        {
            players[i].transform.position = new Vector2(players[i].transform.position.x, 0);
            GameManager.movePlayers[i] = players[i].transform.position;
        }
    }

}

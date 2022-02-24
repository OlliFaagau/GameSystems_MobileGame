using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnBoard : MonoBehaviour
{
    public GameObject[] players;
    public LineRenderer[] lines;
    public GameObject winnerPanel;
    public GameObject realBoard;
    public GameObject canvasBoard;
    public Image winner;
    public Button playButton;
    public Text roundText;
    public Text winningText;

    private int numOfPlayers;
    private int victor;

    void Start()
    {
        Time.timeScale = 1;
        victor = 0;
        playButton.enabled = true;
        winnerPanel.SetActive(false);
        numOfPlayers = GameManager.playerNum;
        roundText.text = "Round " + GameManager.roundNum;
        realBoard.SetActive(true);
        canvasBoard.SetActive(true);
        SetBoard();
    }

    void Update()
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            if(GameManager.movePlayers[i].y > 0)
                players[i].transform.position = Vector2.Lerp(players[i].transform.position, GameManager.movePlayers[i], Time.deltaTime);

            lines[i].SetPosition(i, new Vector2(players[i].transform.position.x, players[i].transform.position.y));
            lines[i].startColor = players[i].GetComponent<SpriteRenderer>().color;
            lines[i].endColor = players[i].GetComponent<SpriteRenderer>().color;

            if (players[i].transform.position.y >= 9.9 && victor == 0)
            {
                SetWinnerPanel(i);
            }
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

    void SetWinnerPanel(int playerIndex)
    {
        winnerPanel.SetActive(true);
        winningText.text = $"Player {playerIndex + 1} wins!";
        winner.sprite = players[playerIndex].GetComponent<SpriteRenderer>().sprite;
        winner.color = players[playerIndex].GetComponent<SpriteRenderer>().color;
        realBoard.SetActive(false);
        canvasBoard.SetActive(false);
        playButton.enabled = false;
        victor = 1;
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

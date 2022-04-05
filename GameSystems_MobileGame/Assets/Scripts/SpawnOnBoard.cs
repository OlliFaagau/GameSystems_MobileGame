using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnBoard : MonoBehaviour
{
    public GameObject[] players;
    public LineRenderer[] lines;//used for the trails behind the player sprites
    public GameObject winnerPanel;
    public GameObject realBoard;
    public GameObject canvasBoard;
    public Image winner;
    public Button playButton;
    public Button pauseButton;
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
            if(GameManager.movePlayers[i].y > 0)//if new position's y axis is greater than 0 move players
                players[i].transform.position = Vector2.Lerp(players[i].transform.position, GameManager.movePlayers[i], Time.deltaTime);

            //set player trails
            lines[i].SetPosition(i, new Vector2(players[i].transform.position.x, players[i].transform.position.y));//have trail follow player
            lines[i].startColor = players[i].GetComponent<SpriteRenderer>().color;
            lines[i].endColor = players[i].GetComponent<SpriteRenderer>().color;

            if(GameManager.roundNum > 3)
            {
                playButton.enabled = false;
                pauseButton.enabled = false;
            }

            if (players[i].transform.position.y >= 9.9 && victor == 0)//if player reaches this position they win
            {
                SetWinnerPanel(i);
            }
        }
    }

    void SetBoard()
    {
        foreach (GameObject player in players)//start by hiding all players
        {
            player.SetActive(false);
        }

        for (int i = 0; i < numOfPlayers; i++)//go through the number of player selected
        {
            if(GameManager.movePlayers[i].y == players[i].transform.position.y)//get latest player position on board (used mainly for re-entering the board)
                GameManager.movePlayers[i] = players[i].transform.position;

            players[i].SetActive(true);//make player visiible
            players[i].GetComponent<SpriteRenderer>().sprite = GameManager.sprites[i];//set player's sprite
            players[i].GetComponent<SpriteRenderer>().color = GameManager.colors[i];//set player's color
        }
    }

    void SetWinnerPanel(int playerIndex)//set winner information onto winner panel
    {
        winnerPanel.SetActive(true);
        winningText.text = $"Player {playerIndex + 1} wins!";
        winner.sprite = players[playerIndex].GetComponent<SpriteRenderer>().sprite;
        winner.color = players[playerIndex].GetComponent<SpriteRenderer>().color;
        realBoard.SetActive(false);
        canvasBoard.SetActive(false);
        playButton.enabled = false;
        victor = 1;//ensures that the first player to cross wins
    }

    public void ResetBoard()
    {
        GameManager.roundNum = 1;//reset round number

        for (int i = 0; i < numOfPlayers; i++)//reset player positions on board
        {
            players[i].transform.position = new Vector2(players[i].transform.position.x, 0);
            GameManager.movePlayers[i] = players[i].transform.position;
        }
    }

}

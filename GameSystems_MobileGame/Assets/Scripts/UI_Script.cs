using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    public float timeLeft = 60f;
    public float time = 0;
    public Text timeText;
    public Text scoreText;
    public Text playingPlayer;
    public Text[] placements;
    public DodgeMovement player;
    public GameObject GameOverPanel;
    public GameObject leaderBoard;
    public Image[] playerIcons;

    static public int numOfPlays = GameManager.playerNum;

    void Start()
    {
        UnPause();
        timeText.text = $"{(int)timeLeft}";
        GameOverPanel.SetActive(false);
        leaderBoard.SetActive(false);
        playingPlayer.text = $"Player {(GameManager.playerNum - numOfPlays) + 1}";
        playingPlayer.color = GameManager.colors[GameManager.playerNum - numOfPlays];
   
        foreach(Image image in playerIcons)
        {
            image.enabled = false;
        }
    }

    void Update()
    {
        if (numOfPlays > 0)
        {
            GetGameOver();
        }

        timeLeft -= Time.deltaTime;
        time += Time.deltaTime;
        timeText.text = $"{(int)timeLeft}";
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    void GetGameOver()
    {
        if (GameOverPanel != null)
        {
            if (player.health < 0)
            {
                GameOverPanel.SetActive(true);
                scoreText.text = "Your time: " + (int)time;
                Pause();
            }
        }
    }

    void GetLeaderBoard()
    {
        if (leaderBoard != null)
        {
            if (player.health < 0)
            {
                DisplayScores();
                DisplayOrders();
                GameOverPanel.SetActive(false);
                leaderBoard.SetActive(true);
                Pause();
            }
        }
    }
    void DisplayScores()
    {
        int i = 0;
        if(GameManager.scores.Count >= 0)
        {
            List<KeyValuePair<string, int>> newList = new List<KeyValuePair<string, int>>(GameManager.scores);
            newList.Sort(delegate(KeyValuePair<string, int> place1, KeyValuePair<string, int>place2) {
                return place2.Value.CompareTo(place1.Value);
            });
        
            GameManager.scores.Clear();

            foreach (KeyValuePair<string, int> place in newList)
            {
                placements[i].text =  "" + place.Value;
                i++;
            }
        }
    }

    void DisplayOrders()
    {
        int i = 0;
        if (GameManager.scores.Count >= 0)
        {
            List<KeyValuePair<int, int>> newList = new List<KeyValuePair<int, int>>(GameManager.orders);
            newList.Sort(delegate (KeyValuePair<int, int> place1, KeyValuePair<int, int> place2) {
                return place2.Value.CompareTo(place1.Value);
            });

            GameManager.orders.Clear();

            foreach (KeyValuePair<int, int> place in newList)
            {
                playerIcons[i].enabled = true;
                playerIcons[i].sprite = GameManager.sprites[place.Key];
                playerIcons[i].color = GameManager.colors[place.Key];

                switch (i)
                {
                    case 0:
                        //player moves 3 spaces
                        break;
                    case 1:
                        //player moves 2 spaces
                        break;
                    case 2:
                        //player moves 1 space
                        break;
                }

                print("Player Index: " + place.Key + " - " + place.Value);
                i++;
            }
        }
    }
    public void Replay()
    {
        GameManager.scores.Add($"Player {(GameManager.playerNum - numOfPlays) + 1}", (int)time);
        GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)time);

        numOfPlays--;

        if (numOfPlays > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            GetLeaderBoard();
            numOfPlays = GameManager.playerNum;
        }
    }
}

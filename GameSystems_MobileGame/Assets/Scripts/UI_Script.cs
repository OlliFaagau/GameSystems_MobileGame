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
    public Text bonusText;
    public Text playingPlayer;
    public Text[] placements;
    public DodgeMovement player;
    public GameObject GameOverPanel;
    public GameObject leaderBoard;
    public Image[] playerIcons;

    static public int numOfPlays = GameManager.playerNum;
    static public int clickCounter = 0;

    void Start()
    {
        UnPause();
        if(numOfPlays != GameManager.playerNum && clickCounter == 0)
        {
            numOfPlays = GameManager.playerNum;
        }

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
                bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
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
                DisplayOrders();
                GameOverPanel.SetActive(false);
                leaderBoard.SetActive(true);
                Pause();
            }
        }
    }

    void DisplayOrders()
    {
        int i = 0;
        if (GameManager.orders.Count >= 0)
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

                placements[i].text = "" + place.Value;

                switch (i)
                {
                    case 0:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 4;
                        break;
                    case 1:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 3;
                        break;
                    case 2:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 2;
                        break;
                    case 3:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 1;
                        break;
                }

                print("Player Index: " + place.Key + " - " + place.Value);
                i++;
            }
        }
    }
    public void Replay()
    {
        GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)time + GameManager.bonusPoints[GameManager.playerNum - numOfPlays]);

        numOfPlays--;
        clickCounter++;

        if (numOfPlays > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            GetLeaderBoard();
            numOfPlays = GameManager.playerNum;
            clickCounter = 0;
            GameManager.roundNum++;
        }
    }
}

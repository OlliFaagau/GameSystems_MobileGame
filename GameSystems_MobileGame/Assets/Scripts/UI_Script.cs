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
    public GameObject GameOverPanel;
    public GameObject leaderBoard;
    public Image[] playerIcons;

    static public int numOfPlays = GameManager.playerNum;
    static public int clickCounter = 0;

    bool healthDeath = false;

    void Start()
    {
        Pause();
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
        timeLeft -= Time.deltaTime;
        time += Time.deltaTime;
        timeText.text = $"{(int)timeLeft}";

        GetGameOver();
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
            if (GameManager.health < 0 || timeLeft <= 0 || MatchingController.matches == 8) 
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
        if (SceneManager.GetActiveScene().name != "MatchingGame")
        {
            scoreText.text = "Your Score: " + (int)time;
            bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
            Pause();
        }
        else
        {
            if (GameManager.health <= 0)
            {
                scoreText.text = "Your Score: " + MatchingController.matches;
                bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
                healthDeath = true;
                Pause();
            }
            else
            {
                scoreText.text = "Your Score: " + ((int)MatchingController.points + MatchingController.matches);
                bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
                Pause();
            }
        }
    }

    void GetLeaderBoard()
    {
        if (leaderBoard != null)
        {
            if (GameManager.health < 0 || timeLeft <= 0 || MatchingController.matches == 8)
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
        if (SceneManager.GetActiveScene().name != "MatchingGame")
        {
            GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)time + GameManager.bonusPoints[GameManager.playerNum - numOfPlays]);
        }
        else
        {
            if (healthDeath)
            {
                GameManager.orders.Add(GameManager.playerNum - numOfPlays,  GameManager.bonusPoints[GameManager.playerNum - numOfPlays] + MatchingController.matches);
                healthDeath = false;
            }
            else
            {
                GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)MatchingController.points + GameManager.bonusPoints[GameManager.playerNum - numOfPlays] + MatchingController.matches);
            }
        }

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

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
    public Image damageImage;
    public AudioSource damageAudio;

    static public int numOfPlays = GameManager.playerNum;//keep track of the number of times the game needs to be replyed
    static public int clickCounter = 0;//used to keep track of times the continue button has been clicked

    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private Button pauseButton;

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
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

        foreach(Image image in playerIcons)//hide the player icons on the leaderboard 
        {
            image.enabled = false;
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        time += Time.deltaTime;
        timeText.text = $"{(int)timeLeft}";

        if(Time.timeScale != 1)//disable pause button at the beginning of minigames
        {
            pauseButton.enabled = false;
        }
        else
        {
            pauseButton.enabled = true;
        }

        if (GameManager.damaged)
        {
            damageImage.color = flashColour;//flash red
            damageAudio.Play();//play damage sound
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);//make damaged panel invisible
        }
        GameManager.damaged = false;

        GetGameOver();//check game over conditions
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
            if (GameManager.health <= 0 || timeLeft <= 0 || MatchingController.matches == 8) 
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);

        //death conditions for different minigames
        if (SceneManager.GetActiveScene().name != "MatchingGame") //For dodge and swipe minigames
        {
            scoreText.text = "Your Score: " + (int)time;
            bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
            Pause();
        }
        else//for matching minigame
        {
            //sets the score and bonus text for losses due to health reaching 0
            if (GameManager.health <= 0)
            {
                MatchingController.points = 0;
            }

            scoreText.text = "Your Score: " + ((int)MatchingController.points + MatchingController.matches);
            bonusText.text = "Bonus: " + GameManager.bonusPoints[GameManager.playerNum - numOfPlays];
            Pause();;
        }
    }

    void GetLeaderBoard()
    {
        if (leaderBoard != null)
        {
            if (GameManager.health < 0 || timeLeft <= 0 || MatchingController.matches == 8)
            {
                DisplayOrders();//initialize leaderboard
                Destroy(GameOverPanel);//remove game ove rpanel
                leaderBoard.SetActive(true);//make leaderboard visible
                Pause();
            }
        }
    }

    void DisplayOrders()
    {
        int i = 0;//index used to access player assets
        if (GameManager.orders.Count >= 0)
        {
            List<KeyValuePair<int, int>> newList = new List<KeyValuePair<int, int>>(GameManager.orders);//convert dictionary into a List of KeyValuePair

            //sorting the new list values from greatest to least
            newList.Sort(delegate (KeyValuePair<int, int> place1, KeyValuePair<int, int> place2) {
                return place2.Value.CompareTo(place1.Value);
            });

            GameManager.orders.Clear();//clear orders dictionary for next minigame

            foreach (KeyValuePair<int, int> place in newList)//display the rankings
            {
                playerIcons[i].enabled = true;//make sprite visible
                playerIcons[i].sprite = GameManager.sprites[place.Key];//change sprite to appropriate character
                playerIcons[i].color = GameManager.colors[place.Key];//give sprite appropriate color

                placements[i].text = "" + place.Value;//display score

                switch (i)//used to move players up the board by changing their y axis
                {
                    case 0:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 4;//first place moves 4 spaces up
                        break;
                    case 1:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 3;//second place moves 3 spaces  up
                        break;
                    case 2:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 2;//third place moves 2 spaces up
                        break;
                    case 3:
                        GameManager.movePlayers[place.Key].y = GameManager.movePlayers[place.Key].y + 1;//fourth place moves 1 space up
                        break;
                }

                print("Player Index: " + place.Key + " - " + place.Value);
                i++;//increasing index
            }
        }
    }
    public void Replay()
    {
        if (SceneManager.GetActiveScene().name != "MatchingGame") //save scores for dodge and swipe minigames
        {
            //add the time survived with the player bonus
            GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)time + GameManager.bonusPoints[GameManager.playerNum - numOfPlays]);
        }
        else//save scores for matching minigame
        {
            //add the time left over(points) with the player bonus and the amount of matches made
            Debug.Log("" + (int)MatchingController.points + GameManager.bonusPoints[GameManager.playerNum - numOfPlays] + MatchingController.matches);
            GameManager.orders.Add(GameManager.playerNum - numOfPlays, (int)MatchingController.points + GameManager.bonusPoints[GameManager.playerNum - numOfPlays] + MatchingController.matches);
        }

        numOfPlays--;//decrease the number of times left to replay the minigame
        clickCounter++;//add a click

        if (numOfPlays > 0)//if there are still players that need their turn replay minigame
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else//if there are no more players in que get the ranking leaderboard
        {
            GetLeaderBoard();
            numOfPlays = GameManager.playerNum;//reset the number of plays for the next minigame
            clickCounter = 0;
            GameManager.roundNum++;//used to change the round text in the board scene
        }
    }
}

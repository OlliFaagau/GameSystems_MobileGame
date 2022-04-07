using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    static public void Board()
    { 
        SceneManager.LoadScene("Board");
        MatchingController.matches = 0;
    }

    static public void Minigame()
    {
        SceneManager.LoadScene("MiniGame");
    }

    static  public void SwipeGame()
    {
        SceneManager.LoadScene("SwipingMiniGame");
    }

    static public void MatchGame()
    {
        SceneManager.LoadScene("MatchingGame");
    }

    static public void MainMenu()
    {
        GameManager.playerNum = 0;
        GameManager.orders.Clear();
        UI_Script.clickCounter = 0;
        GameManager.movePlayers = new Vector2[4];
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AlternateGames()//manage minigame order
    {
        if(GameManager.altGamesNum == 0)
        {
            Minigame();
            GameManager.altGamesNum++;
        }
        else if(GameManager.altGamesNum == 1)
        {
            SwipeGame();
            GameManager.altGamesNum++;
        }
        else
        {
            MatchGame();
            GameManager.altGamesNum = 0;
        }
    }
}

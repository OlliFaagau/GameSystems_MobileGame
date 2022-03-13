﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    static public void Board()
    { 
        SceneManager.LoadScene("Board");
    }

    static public void Minigame()
    {
        SceneManager.LoadScene("MiniGame");
    }

    static  public void SwipeGame()
    {
        SceneManager.LoadScene("SwipingMiniGame");
    }

    static public void MainMenu()
    {
        GameManager.playerNum = 0;
        GameManager.movePlayers = new Vector2[4];
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AlternateGames()
    {
        if(GameManager.altGamesNum == 0)
        {
            Minigame();
            GameManager.altGamesNum++;
        }
        else
        {
            SwipeGame();
            GameManager.altGamesNum = 0;
        }
    }
}

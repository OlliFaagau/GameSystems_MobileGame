using System.Collections;
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

}

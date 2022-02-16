using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.playerNum = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void Board()
    {
        SceneManager.LoadScene("Board");
    }

    public void Minigame()
    {
        SceneManager.LoadScene("MiniGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

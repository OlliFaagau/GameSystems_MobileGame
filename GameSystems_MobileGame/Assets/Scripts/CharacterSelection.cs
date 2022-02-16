using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public enum Characters { Square, Circle, Triangle, Diamond};
    public Sprite[] characterPrefabs;
    public Button[] charButtons;
    public Button startButton;
    public Slider armorSlider;
    public Slider healthSlider;
    public Slider bonusSlider;
    public Text playerText;

    static public int numOfPressableButtons;

    private int playerTracker;

    Scene currentScene;
    Characters pick;
    Color playerColor;
    int buttonIndex;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        playerText.text = $"Player {GameManager.playerNum + 1}";
        startButton.interactable = false;
        playerTracker = 0;
    }

    void Update()
    {
        CheckButtons();
    }

    void CheckButtons()
    {
        if (currentScene.name == "MainMenu")
        {
            if (numOfPressableButtons == 0 && playerTracker > 0)
            {
                foreach (Button b in charButtons)
                {
                    b.interactable = false;
                }
                playerText.text = "Let's get started!";
                ManageScenes.Board();
            }
            else
            {
                foreach (Button b in charButtons)
                {
                    b.interactable = true;
                }
                playerText.text = $"Player {GameManager.playerNum + 1}";
            }
        }
    }

    void Pressed()
    {
        numOfPressableButtons--;

        if (numOfPressableButtons >= 0)
        {
           GameManager.playerNum++;
        }
    }

    public void ResetButtons()
    {
        GameManager.playerNum = 0;
        foreach (Button b in charButtons)
        {
            b.enabled = true;
        }
        startButton.interactable = false;
    }

    public void CheckForOrder(Characters sprite, Color color)
    {
        switch (playerTracker)
        {
            case 0:
                GameManager.sprites[playerTracker]= characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                break;
            case 1:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                break;
            case 2:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                break;
            case 3:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                break;

        }
    }

    void DisableButtons(int button)
    {
        switch (button)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                charButtons[button].enabled = false;
                break;
        }
    }
    public void Square()
    {
        pick = Characters.Square;
        armorSlider.value = 5;
        healthSlider.value = 5;
        bonusSlider.value = 5;

        armorSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 0, 0, 255);
        healthSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 0, 0, 255);
        bonusSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 0, 0, 255);
        buttonIndex = 0;
        playerColor = new Color32(255, 0, 0, 255);
    }

    public void Circle()
    {
       pick = Characters.Circle;
        armorSlider.value = 3;
        healthSlider.value = 9;
        bonusSlider.value = 3;

        armorSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 255, 0, 255);
        healthSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 255, 0, 255);
        bonusSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 255, 0, 255);
        buttonIndex = 1;
        playerColor = new Color32(0, 255, 0, 255); ;
    }

    public void Triangle()
    {
        pick = Characters.Triangle;
        armorSlider.value = 7;
        healthSlider.value = 7;
        bonusSlider.value = 1;

        armorSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 255, 0, 255);
        healthSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 255, 0, 255);
        bonusSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(255, 255, 0, 255);
        buttonIndex = 2;
        playerColor = new Color32(255, 255, 0, 255);
    }

    public void Diamond()
    {
        pick = Characters.Diamond;
        armorSlider.value = 10;
        healthSlider.value = 3;
        bonusSlider.value = 2;

        armorSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 0, 255, 255);
        healthSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 0, 255, 255);
        bonusSlider.GetComponent<RectTransform>().GetComponentInChildren<Image>().color = new Color32(0, 0, 255, 255);
        buttonIndex = 3;
        playerColor = new Color32(0, 0, 255, 255); ;
    }

    public void ConfirmPick()
    {
        DisableButtons(buttonIndex);
        Pressed();
        CheckForOrder(pick, playerColor);
        playerTracker++;
    }
  
}

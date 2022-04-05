using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public enum Characters { Square, Circle, Triangle, Diamond};
    public Sprite[] characterPrefabs;//character sprites
    public Button[] charButtons;//the character buttons
    public Button startButton;
    public Slider armorSlider;
    public Slider healthSlider;
    public Slider bonusSlider;
    public Text playerText;

    static public int numOfPressableButtons;

    private int playerTracker;//used as an index to keep track of the order in which the characters were chosen

    Scene currentScene;
    Characters pick;
    Color playerColor;
    int buttonIndex;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();//get current scene
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
            if (numOfPressableButtons == 0 && playerTracker > 0)//check for pressable buttons 
            {
                foreach (Button b in charButtons)
                {
                    b.interactable = false;
                }
                playerText.text = "Let's get started!";
                ManageScenes.Board();//transition to game board
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
        numOfPressableButtons--;//decrease the amount of buttons left to be pressed

        if (numOfPressableButtons >= 0)
        {
           GameManager.playerNum++;//increase the number of players
        }
    }

    public void ResetButtons()//resets character selcetion
    {
        GameManager.playerNum = 0;
        playerTracker = 0;
        foreach (Button b in charButtons)
        {
            b.enabled = true;
        }
        startButton.interactable = false;
    }

    public void CheckForOrder(Characters sprite, Color color)
    {
        switch (playerTracker)//setting sprites and colors arrays that will be used for the characters
        {
            case 0:
                GameManager.sprites[playerTracker]= characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                SetStats(sprite, playerTracker);
                break;
            case 1:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                SetStats(sprite, playerTracker);
                break;
            case 2:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                SetStats(sprite, playerTracker);
                break;
            case 3:
                GameManager.sprites[playerTracker] = characterPrefabs[(int)sprite];
                GameManager.colors[playerTracker] = color;
                SetStats(sprite, playerTracker);
                break;

        }
    }

    public void SetStats(Characters characters ,int tracker)//get character and the order it was chosen
    {
        switch ((int)characters)
        {
            case 0:
                GameManager.armorPoints[tracker] = 50;
                GameManager.healthPoints[tracker] = 50;
                GameManager.bonusPoints[tracker] = 5;
                break;                                        
            case 1:                                           
                GameManager.armorPoints[tracker] = 30;
                GameManager.healthPoints[tracker] = 90;
                GameManager.bonusPoints[tracker] = 3;
                break;                                       
            case 2:                                          
                GameManager.armorPoints[tracker] = 70;
                GameManager.healthPoints[tracker] = 70;
                GameManager.bonusPoints[tracker] = 1;
                break;                                        
            case 3:                                           
                GameManager.armorPoints[tracker] = 100;
                GameManager.healthPoints[tracker] = 30;
                GameManager.bonusPoints[tracker] = 2;
                break;
        }
    }

    void DisableButtons(int button)//disable chosen button from roster
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
    public void Square()//set square attributes
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

    public void Circle()//set Circle attributes
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

    public void Triangle()//set Triangle attributes
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

    public void Diamond()//set Diamond attributes
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

    public void ConfirmPick()//confirm and disable character pick
    {
        DisableButtons(buttonIndex);
        Pressed();
        CheckForOrder(pick, playerColor);
        playerTracker++;
    }
  
}

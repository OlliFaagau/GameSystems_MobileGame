using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingController : MonoBehaviour
{
    MainCard tokenUp1 = null;
    MainCard tokenUp2 = null;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 7, 7 }; //getting the indexes of all the cards and doubling them for matches

    public static System.Random rand = new System.Random();
    public static float points;
    public static int matches;

    public Slider healthBar;
    public Slider armorBar;
    public GameObject player;

    [SerializeField]
    private GameObject token;
    private int shuffleNum = 0;

    float tokenScale = 1;
    float yStart = 3.4f;
    float yChange = -2.2f;
    int numOfTokens = 16;//number of cards on the board

    void StartGame()
    {
        int startTokenCount = numOfTokens; 
        int row = 1; //current row 
        float yPos = yStart;
        float xPos = -2.2f;
        float ortho = Camera.main.orthographicSize / 2.0f; //for consistancy in size

        for (int i = 1; i < startTokenCount + 1; i++)//go through deck and setting up 4 by 4 board
        {
            shuffleNum = rand.Next(0, numOfTokens);//for getting a random card
            var temp = Instantiate(token, new Vector3(xPos, yPos, 0), Quaternion.identity);//spawning card in row
            temp.GetComponent<MainCard>().faceIndex = faceIndexes[shuffleNum];//getting the face for the card 
            temp.transform.localScale = new Vector3(ortho / tokenScale, ortho / tokenScale, 0);//setting the proper scale for the card
            faceIndexes.Remove(faceIndexes[shuffleNum]);//remove this card from deck so it won't be repeated 
            numOfTokens--;//subtract card total
            xPos = xPos + 1.5f;

            if (i % 4 < 1)//check if there are 4 cards in this row
            {
                xPos = -2.2f;//reset x axis
                yPos = yPos + yChange;//go to down and start new row
                row++;//adding new row
            }
        }
    }

    public void TokenDown(MainCard tempToken)
    {
        if (tokenUp1 == tempToken)
        {
            tokenUp1 = null;
        }
        else if (tokenUp2 == tempToken)
        {
            tokenUp2 = null;
        }
    }
    public bool TokenUp(MainCard tempToken)
    {
        bool flipCard = true;
        if (tokenUp1 == null)
        {
            tokenUp1 = tempToken;
        }
        else if (tokenUp2 == null)
        {
            tokenUp2 = tempToken;
        }
        else
        {
            flipCard = false;
        }
        return flipCard;
    }

    public void CheckTokens()
    {
        if (tokenUp1 != null && tokenUp2 != null && tokenUp1.faceIndex == tokenUp2.faceIndex)//check for match
        {
            tokenUp1.matched = true;
            tokenUp2.matched = true;
            tokenUp1 = null;
            tokenUp2 = null;
            matches++;
        }
        else if(tokenUp1 != null && tokenUp2 != null && tokenUp1.faceIndex != tokenUp2.faceIndex)//check for mismatch
        {
            //if cards are not a match apply damage
            GameManager.damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }

    private void Start()
    {
        StartGame();
        GameManager.health = GameManager.healthPoints[GameManager.playerNum -GameManager.numOfPlays];  //Initialize health for player
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - GameManager.numOfPlays];   //Initialize armor for player
    
        player.GetComponent<SpriteRenderer>().sprite = GameManager.sprites[GameManager.playerNum - GameManager.numOfPlays];   //Set up sprite for player
        player.GetComponent<SpriteRenderer>().color = GameManager.colors[GameManager.playerNum - GameManager.numOfPlays];     //Set up color for player
    
        healthBar.maxValue = GameManager.health;//Set up health slider value
        armorBar.maxValue = GameManager.armor;  //Set up armor slider values

        points = 30f;//Game starts with 30 possible points
        matches = 0;//number of pairs matched
    }

    private void Update()
    {
        healthBar.value = GameManager.health;    //Updeate health slider
        armorBar.value = GameManager.armor;     //Updeate armor slider

        points -= Time.deltaTime; //points decreases as time runs out
    }
}

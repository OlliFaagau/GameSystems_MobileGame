using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingController : MonoBehaviour
{
    MainCard tokenUp1 = null;
    MainCard tokenUp2 = null;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

    public static System.Random rand = new System.Random();
    public static float points = 30f;
    public static int matches = 0;
    public Slider healthBar;
    public Slider armorBar;
    public Image damageImage;
    public AudioSource damageAudio;
    public GameObject player;

    [SerializeField]
    private GameObject token;
    private int shuffleNum = 0;
    private bool damaged = false;
    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    float tokenScale = 1;
    float yStart = 3.4f;
    float yChange = -2.2f;
    int numOfTokens = 16;

    void StartGame()
    {
        int startTokenCount = numOfTokens;
        int row = 1;
        float xPos = yStart;
        float yPos = -2.2f;
        float ortho = Camera.main.orthographicSize / 2.0f;

        for (int i = 1; i < startTokenCount + 1; i++)
        {
            shuffleNum = rand.Next(0, numOfTokens);
            var temp = Instantiate(token, new Vector3(yPos, xPos, 0), Quaternion.identity);
            temp.GetComponent<MainCard>().faceIndex = faceIndexes[shuffleNum];
            temp.transform.localScale = new Vector3(ortho / tokenScale, ortho / tokenScale, 0);
            faceIndexes.Remove(faceIndexes[shuffleNum]);
            numOfTokens--;
            yPos = yPos + 1.5f;
            if (i % 4 < 1)
            {
                yPos = -2.2f;
                xPos = xPos + yChange;
                row++;
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
        if (tokenUp1 != null && tokenUp2 != null && tokenUp1.faceIndex == tokenUp2.faceIndex)
        {
            tokenUp1.matched = true;
            tokenUp2.matched = true;
            tokenUp1 = null;
            tokenUp2 = null;
            matches++;
        }
        else if(tokenUp1 != null && tokenUp2 != null && tokenUp1.faceIndex != tokenUp2.faceIndex)
        {
            damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }

    private void Awake()
    {
        StartGame();
    }

    private void Start()
    {
        GameManager.health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];

        player.GetComponent<SpriteRenderer>().sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        player.GetComponent<SpriteRenderer>().color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];

        healthBar.value = GameManager.health;
        armorBar.value = GameManager.armor;

        points = 30f;
        matches = 0;
    }

    private void Update()
    {
        healthBar.value = GameManager.health;
        armorBar.value = GameManager.armor;

        points -= Time.deltaTime;

        if (damaged)
        {
            damageImage.color = flashColour;
            damageAudio.Play();
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
}

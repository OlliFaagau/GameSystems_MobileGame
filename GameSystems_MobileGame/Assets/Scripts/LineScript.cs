using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour
{
    public Slider healthBar;
    public Slider armorBar;

    [SerializeField] private ShieldScript linePrefab;
    private Camera cam;
    private ShieldScript currentLine;
    private ShieldScript previousLine;
    private float timeLeft = 3f;

    public const float RESOLUTION = 0.1f;
    void Start()
    {
        if (UI_Script.numOfPlays != GameManager.playerNum && UI_Script.clickCounter == 0)
        {
            UI_Script.numOfPlays = GameManager.playerNum;
        }

        cam = Camera.main;
        GameManager.health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];     //Initialize health for player
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];      //Initialize armor for player

        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];//Set up sprite for player
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];  //Set up color for player

        healthBar.maxValue = GameManager.health;  //Set up health slider value
        armorBar.maxValue = GameManager.armor;    //Set up armor slider values
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        healthBar.value = GameManager.health;  //Updeate health slider
        armorBar.value = GameManager.armor;   //Updeate armor slider

        if (previousLine != null)
        {
            if(timeLeft <= 0)
            {
                timeLeft = 5f;
                Destroy(previousLine.gameObject);
            }
        }

        if (Time.timeScale != 0)//only play if game is unpaused
        {
            SettingUpLines();
        }
    }

    void SettingUpLines()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);//creating the line

            if (previousLine != null)//check if you have a previous line and removes it
            {
                Destroy(previousLine.gameObject);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            previousLine = currentLine;
        }

        if (Input.GetMouseButton(0))
        {
            currentLine.SetPosition(mousePos);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))//when player collides with obstacles ->  deal damage
        {
            Destroy(collision.gameObject);
            GameManager.damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }

}

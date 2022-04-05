using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMovement : MonoBehaviour
{
    public float speed = 0.01f;
    public float mapWidth = 2.5f;
    public Slider healthBar;
    public Slider armorBar;
    public GameObject blockPrefab;
    public UI_Script UI_Reference;
    public Camera cam;

    private float startXPos;
    private bool isDragging = false;

    void Start()
    {
        if (UI_Script.numOfPlays != GameManager.playerNum && UI_Script.clickCounter == 0)
        {
            UI_Script.numOfPlays = GameManager.playerNum;
        }

        GameManager.health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];  //Initialize health for player
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];   //Initialize armor for player

        gameObject.GetComponent<SpriteRenderer>().sprite =  GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];  //Set up sprite for player
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];    //Set up color for player

        healthBar.maxValue = GameManager.health;   //Set up health slider value
        armorBar.maxValue = GameManager.armor;     //Set up armor slider values
    }

    void Update()
    {
        healthBar.value = GameManager.health;     //Updeate health slider
        armorBar.value = GameManager.armor;      //Updeate armor slider


        if (Time.timeScale != 0)//only play if game is unpaused
        {
            if (isDragging)
                Drag();
        }

        if (UI_Reference.timeLeft < 40)
        {
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        }
        else if (UI_Reference.timeLeft < 20)
        {
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }
    }

    void Drag()
    {
        Vector3 mousePos = Input.mousePosition;//get touch down position of the mouse or finger

        if (!cam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = cam.ScreenToWorldPoint(mousePos);

        if (transform.localPosition.x >= 2.5 || transform.localPosition.x <= -2.5)//check if player is heading off of the screen
        {
            transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x - startXPos, startXPos - 2.5f, startXPos + 2.5f), transform.localPosition.y, transform.localPosition.z);//keep player inbounds
        }
        else//else move as usual
        {
            transform.localPosition = new Vector3(mousePos.x - startXPos, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!cam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = cam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - transform.localPosition.x;

        isDragging = true;
        
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))//when player collides with blocks -> deal damage
        {
            GameManager.damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }
}

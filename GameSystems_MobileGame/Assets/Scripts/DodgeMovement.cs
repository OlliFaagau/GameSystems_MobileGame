using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMovement : MonoBehaviour
{
    public float speed = 15f;
    public float mapWidth = 2.5f;
    public int health;
    public int armor;
    public Slider healthBar;
    public Slider armorBar;

    private Rigidbody2D rb;
    Sprite sprite;
    Color color;

    void Start()
    {
        if (UI_Script.numOfPlays != GameManager.playerNum && UI_Script.clickCounter == 0)
        {
            UI_Script.numOfPlays = GameManager.playerNum;
        }

        health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];
        armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];
        sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void FixedUpdate()
    {
        healthBar.value = health;
        armorBar.value = armor;

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        Vector2 newPosition = rb.position + Vector2.right * x;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            armor -= 25;

            if(armor <= 0)
                health -= 25;
        }
    }
}

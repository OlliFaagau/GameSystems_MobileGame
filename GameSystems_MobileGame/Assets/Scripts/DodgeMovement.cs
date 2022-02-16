using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMovement : MonoBehaviour
{
    public float speed = 15f;
    public float mapWidth = 2.5f;
    public int health = 100;
    public Slider healthBar;

    private Rigidbody2D rb;
    Sprite sprite;
    Color color;
    void Start()
    {
        sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void FixedUpdate()
    {
        healthBar.value = health;

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        Vector2 newPosition = rb.position + Vector2.right * x;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            health -= 25;
        }
    }
}

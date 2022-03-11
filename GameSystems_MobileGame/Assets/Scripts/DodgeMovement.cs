﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMovement : MonoBehaviour
{
    public float speed = 0.01f;
    public float mapWidth = 2.5f;
    public Slider healthBar;
    public Slider armorBar;
    public Image damageImage;
    public AudioSource damageAudio;
    public GameObject blockPrefab;
    public UI_Script UI_Reference;

    private Rigidbody2D rb;
    private bool damaged;
    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private Touch touch;
    private Vector3 startPos;

    Sprite sprite;
    Color color;

    void Start()
    {
        if (UI_Script.numOfPlays != GameManager.playerNum && UI_Script.clickCounter == 0)
        {
            UI_Script.numOfPlays = GameManager.playerNum;
        }

        startPos = transform.position;
        GameManager.health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];
        sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = color;

        healthBar.maxValue = GameManager.health;
        armorBar.maxValue = GameManager.armor;
    }

    void FixedUpdate()
    {
        healthBar.value = GameManager.health;
        armorBar.value = GameManager.armor;

        Drag();

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        Vector2 newPosition = rb.position + Vector2.right * x;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);

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

        if (UI_Reference.timeLeft < 40)
        {
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
        else if (UI_Reference.timeLeft < 20)
        {
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        }
    }

    void Drag()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + touch.deltaPosition.x * speed, startPos.x - mapWidth, startPos.x + mapWidth), transform.position.y, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMovement : MonoBehaviour
{
    public float speed = 0.01f;
    public float mapWidth = 2.5f;
    public int health;
    public int armor;
    public Slider healthBar;
    public Slider armorBar;
    public Image damageImage;
    public AudioSource damageAudio;

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
        health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];
        armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];
        sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = color;

        healthBar.maxValue = health;
        armorBar.maxValue = armor;
    }

    void FixedUpdate()
    {
        healthBar.value = health;
        armorBar.value = armor;

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
            if (armor <= 0)
            {
                health -= 25;
            }

            armor -= 25;
        }
    }
}

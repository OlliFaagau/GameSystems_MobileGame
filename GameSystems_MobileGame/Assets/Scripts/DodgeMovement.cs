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
    public Image damageImage;
    public AudioSource damageAudio;
    public GameObject blockPrefab;
    public UI_Script UI_Reference;
    public Camera cam;

    private Rigidbody2D rb;
    private bool damaged;
    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private float startXPos;
    private bool isDragging = false;

    Sprite sprite;
    Color color;

    void Start()
    {
        if (UI_Script.numOfPlays != GameManager.playerNum && UI_Script.clickCounter == 0)
        {
            UI_Script.numOfPlays = GameManager.playerNum;
        }

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

        if(isDragging)
            Drag();

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
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        }
        else if (UI_Reference.timeLeft < 20)
        {
            blockPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }
    }

    void Drag()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!cam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = cam.ScreenToWorldPoint(mousePos);

        if (transform.localPosition.x >= 2.5 || transform.localPosition.x <= -2.5)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x - startXPos, startXPos - 2.5f, startXPos + 2.5f), transform.localPosition.y, transform.localPosition.z);
        }
        else
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour
{
    public Slider healthBar;
    public Slider armorBar;
    public Image damageImage;
    public AudioSource damageAudio;

    [SerializeField] private ShieldScript linePrefab;
    private Camera cam;
    private ShieldScript currentLine;
    private ShieldScript previousLine;
    private float timeLeft = 3f;
    private bool damaged;
    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public const float RESOLUTION = 0.1f;
    void Start()
    {
        cam = Camera.main;
        GameManager.health = GameManager.healthPoints[GameManager.playerNum - UI_Script.numOfPlays];
        GameManager.armor = GameManager.armorPoints[GameManager.playerNum - UI_Script.numOfPlays];
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.sprites[GameManager.playerNum - UI_Script.numOfPlays];
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.colors[GameManager.playerNum - UI_Script.numOfPlays];

        healthBar.maxValue = GameManager.health;
        armorBar.maxValue = GameManager.armor;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        healthBar.value = GameManager.health;
        armorBar.value = GameManager.armor;

        if (previousLine != null)
        {
            if(timeLeft <= 0)
            {
                timeLeft = 5f;
                Destroy(previousLine.gameObject);
            }
        }

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

        SettingUpLines();
    }

    void SettingUpLines()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);

            if (previousLine != null)
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
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
            damaged = true;
            if (GameManager.armor <= 0)
            {
                GameManager.health -= 25;
            }

            GameManager.armor -= 25;
        }
    }

}

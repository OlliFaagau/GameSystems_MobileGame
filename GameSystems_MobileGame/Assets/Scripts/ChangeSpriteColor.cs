using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteColor : MonoBehaviour
{
    public Color[] colors;
    Color currentColor;
    int index = 0;
    int nextIndex;
    public float speed = 1f;
    float startTime = 0f;
    float progress = 0f;

    public SpriteRenderer[] sprite;
    private int counter;

    void Start()
    {
        counter = 0;

        if (colors.Length > 0)
        {
            currentColor = colors[index];
            nextIndex = (index + 1) % colors.Length;
        }
    }

    void Update()
    {
        progress = (Time.time - startTime) / speed;
        if (progress >= 1)
        {
            nextIndex = (index + 2) % colors.Length;
            index = (index + 1) % colors.Length;
            startTime = Time.time;
            counter++;
            if (counter >= sprite.Length)
                counter = 0;
        }
        else
        {
            currentColor = Color.Lerp(colors[index], colors[nextIndex], progress);
        }
        
        sprite[counter].color = currentColor;
    }
}

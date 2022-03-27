using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    GameObject gameControl;
    SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;

    public void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            if (matched == false)
            {
                MatchingController controlScript = gameControl.GetComponent<MatchingController>();
                if (spriteRenderer.sprite == back)
                {
                    if (controlScript.TokenUp(this))
                    {
                        spriteRenderer.sprite = faces[faceIndex];
                        controlScript.CheckTokens();
                    }
                }
                else
                {
                    spriteRenderer.sprite = back;
                    controlScript.TokenDown(this);
                }
            }
        }
    }
    private void Awake()
    {
        gameControl = GameObject.Find("Scripts");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

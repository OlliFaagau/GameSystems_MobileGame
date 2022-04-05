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
        if (Time.timeScale != 0)//only play if game is unpaused
        {
            if (matched == false)//if this card isn't a part of a match -> make it flippable
            {
                MatchingController controlScript = gameControl.GetComponent<MatchingController>();
                if (spriteRenderer.sprite == back)//if the back of the card is facing up -> flip it over to show face
                {
                    if (controlScript.TokenUp(this))
                    {
                        spriteRenderer.sprite = faces[faceIndex];
                        controlScript.CheckTokens();
                    }
                }
                else//if the face of the card is up -> flip to the back
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

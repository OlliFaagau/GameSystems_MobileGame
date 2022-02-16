using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public Sprite[] sprites = new Sprite[4];
    static public Color[] colors = new Color[4];
    static public Dictionary<string, int> scores = new Dictionary<string, int>(); // Helps save scores from reload of minigame
    static public Dictionary<int, int> orders = new Dictionary<int, int>(); //Helps get which players move on board
    static public int pM; //Player to Move Index
    static public bool canMove = false;
    static public int playerNum = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //void Start()
    //{
    //
    //}

  // void Update()
  // {
  //
  // }

}

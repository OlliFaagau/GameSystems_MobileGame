﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public Sprite[] sprites;
    static public Color[] colors;
    static public Vector2[] movePlayers;
    static public Dictionary<int, int> orders = new Dictionary<int, int>(); //Helps save scores and get which players move on board
    static public int pM; //Player to Move Index
    static public bool canMove = false;
    static public int playerNum;
    static public int roundNum;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        playerNum = 0;
        roundNum = 1;
        movePlayers = new Vector2[4];
        colors = new Color[4];
        sprites = new Sprite[4];
    }

  //void Update()
  //{
  //    
  //}

}

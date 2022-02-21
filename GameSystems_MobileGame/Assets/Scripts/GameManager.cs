using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public Sprite[] sprites = new Sprite[4];
    static public Color[] colors = new Color[4];
    static public Vector2[] movePlayers = new Vector2[4];
    static public Dictionary<int, int> orders = new Dictionary<int, int>(); //Helps save scores and get which players move on board
    static public int pM; //Player to Move Index
    static public bool canMove = false;
    static public int playerNum = 0;
    static public int roundNum = 1;

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

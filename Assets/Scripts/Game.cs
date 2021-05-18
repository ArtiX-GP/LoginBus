using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public GameObject currentPlayer;
    public grumbleAMP gA;
    private int activeSong, fadeInTime = 3;
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Game>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("Game");
                    go.AddComponent<Game>();
                }
            }

            return _instance;
        }
    }
    void Start()
    {
        _instance = this;
    }

    private void Update()
    {
        currentPlayer = PlayerPossesser.singleTon.followController.player.gameObject;
    }
}

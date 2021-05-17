using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public GameObject currentPlayer;
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

}

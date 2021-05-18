using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, GameStateListener
{

    public string key;

    private int oldVal = 0;

    void Start() {
        oldVal = GameState.GetInt(key, 0);
        GameState.AddListener(key, this);
    }

    public void onGameStateUpdated() {
        Debug.Log(GameState.GetInt(key, 0));
        if (oldVal != GameState.GetInt(key, 0)) {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            oldVal = GameState.GetInt(key, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, GameStateListener
{

    public bool isLocked = true;

    public GameObject locked;

    public GameObject unlocked;

    public string key;

    // Start is called before the first frame update
    void Start()
    {
        Invalidate();
        GameState.AddListener(key, this);
    }

    // Update is called once per frame
    void Invalidate()
    {
        locked.SetActive(isLocked);
        unlocked.SetActive(!isLocked);
        GetComponent<BoxCollider2D>().isTrigger = !isLocked;
    }

    public void onGameStateUpdated() {
        isLocked = GameState.GetInt(key) == 0;
        Invalidate();
    }
}

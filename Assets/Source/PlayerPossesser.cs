using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossesser : MonoBehaviour
{

    public static PlayerPossesser singleTon;

    public CameraFollow2D followController;

    public GameObject[] players;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        singleTon = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPlayer();
        }
    }

    public GameObject GetCurrentPlayer() {
        return players[index];
    }

    public void SwitchPlayer() {
        GetCurrentPlayer().GetComponent<PlayerController>().enabled = false;
        index = (index + 1) % players.Length;
        GetCurrentPlayer().GetComponent<PlayerController>().enabled = true;
        followController.SetPlayer(GetCurrentPlayer().transform);
    }
}

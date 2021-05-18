using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject to;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (to != null) {
            PlayerPossesser.singleTon.GetCurrentPlayer().transform.position = to.transform.position;
        }
    }
}

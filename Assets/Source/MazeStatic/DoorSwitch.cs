using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{

    public bool activated;

    public GameObject acitve;

    public GameObject disabled;

    public string key;

    void Start() {
        Invalidate();
    }

    protected void Invalidate() {
        acitve.SetActive(!activated);
        disabled.SetActive(activated);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (activated || key == null) {
            return;
        }
        activated = true;
        GameState.SetValue(key, 1);
        Invalidate();
    }

}

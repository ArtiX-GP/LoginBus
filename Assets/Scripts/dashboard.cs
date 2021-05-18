using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dashboard : MonoBehaviour
{
    public GameObject board;

    public void dashboarExit()
    {
        board.SetActive(false);
    }

    public void dashboardEnter()
    {
        board.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        dashboardEnter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) dashboarExit();
    }
}

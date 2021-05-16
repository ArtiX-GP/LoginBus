using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoulders : MonoBehaviour
{
    public int boulders_n = 5;
    public GameObject BoulderPrefab;

    void Start()
    {
        for (int i = 0; i < boulders_n; i++)
        {
            Boulder boulder = new Boulder(i, Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f), BoulderPrefab);
            boulder.render();
            boulder.obj.transform.SetParent(gameObject.transform);
        }
    }
}


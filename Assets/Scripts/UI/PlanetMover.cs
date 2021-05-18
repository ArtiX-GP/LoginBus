using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMover : MonoBehaviour
{
    public Vector2[] positions;
    private int c = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = GetComponent<RectTransform>().anchoredPosition;
        Debug.Log( c + " " + Vector2.Distance(position, positions[c]));
        if (Vector2.Distance(position, positions[c]) <= 20)
        {
            c = Random.Range(0, positions.Length -1);
        }
        float step = Random.Range(3, 5) * Time.deltaTime;
        
        position = Vector3.MoveTowards(position, positions[c], step);
        GetComponent<RectTransform>().anchoredPosition = position;
    }
}

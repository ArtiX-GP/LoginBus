using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int width, height;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject fieldParent;
    private float _startX = 0, _startY = 0;
    
    void Start()
    {
        
    }

    public void GenerateField()
    {
        DestroyField();
        _startX = fieldParent.transform.position.x - width  * cellPrefab.transform.localScale.x / 2 + cellPrefab.transform.localScale.x;
        _startY = fieldParent.transform.position.y - height  * cellPrefab.transform.localScale.y / 2 +  cellPrefab.transform.localScale.y;
        fieldParent.transform.position = new Vector2(-0.5f, -0.5f);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var pos = new Vector2(_startX + (cellPrefab.transform.localScale.x) * j, 
                    _startY + (cellPrefab.transform.localScale.y) * i);
                var cell = Instantiate(cellPrefab, fieldParent.transform, true);
                cell.transform.position = pos;
            }
        }
    }

    private void DestroyField()
    {
        foreach (var cell in gameObject.GetComponentsInChildren<Transform>())
        {
            if (cell != transform)
                DestroyImmediate(cell.gameObject);
        }
    }
}

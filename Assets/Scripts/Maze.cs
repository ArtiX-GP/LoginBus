using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int width, height;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject fieldParent;
    private float _startX = 0, _startY = 0;
    private float minX, minY;
    private Bounds bounds;
    public MazeCell[,] cells;
    void Start()
    {
        GenerateField();
        bounds = CountBounds();
        minX = bounds.min.x;
        minY = bounds.min.y;
    }

    public void PlaceObject(int x, int y, GameObject go)
    {
        go.transform.position = new Vector2(minX + x * cellPrefab.transform.localScale.x,
            minY + y * cellPrefab.transform.localScale.y);
    }

    private Bounds CountBounds()
    {
        var renderer = GetComponent<Renderer>();
        var combinedBounds = renderer.bounds;
        var renderers = GetComponentsInChildren(typeof(Renderer));
        foreach (var render in renderers)
        {
            var _render = render as Renderer;
            if (_render != renderer) combinedBounds.Encapsulate(_render.bounds);
        }

        return combinedBounds;
    }
    
    public void GenerateField()
    {
        cells = new MazeCell[height, width];
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
                
                cells[i, j] = new MazeCell(false, j, i, pos);
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

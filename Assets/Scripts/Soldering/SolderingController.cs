using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderingController : MonoBehaviour
{
    public int width, height;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject fieldParent;
    private float _startX = 0, _startY = 0;
    public float minX, minY, stepX, stepY; 
    private float _offsetCoefficient = 1.1f;
    public Cell[,] cells;
    public Bounds bounds;
    private int movableChips, chipsPlased;

    public delegate void OnSolderingControllerInitialized();

    public event OnSolderingControllerInitialized onSolderingControllerInitialized;
    
    private static SolderingController _instance;

    public static SolderingController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SolderingController>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("Soldering parent");
                    go.AddComponent<SolderingController>();
                }
            }

            return _instance;
        }
    }
    
    void Start()
    {
        _instance = this;
        cells = new Cell[height, width];
        _startX = fieldParent.transform.position.x - width * _offsetCoefficient * cellPrefab.transform.localScale.x / 2 + _offsetCoefficient * cellPrefab.transform.localScale.x;
        _startY = fieldParent.transform.position.y - height * _offsetCoefficient * cellPrefab.transform.localScale.y / 2 + _offsetCoefficient * cellPrefab.transform.localScale.y;
        GenerateField();
        fieldParent.transform.position = new Vector2(-0.5f, -0.5f);
        bounds = CountBounds();
        minX = bounds.min.x + cellPrefab.transform.localScale.x / 2;
        minY = bounds.min.y + cellPrefab.transform.localScale.y / 2;
        stepX = (bounds.size.x + (_offsetCoefficient - 1) * cellPrefab.transform.localScale.x) / width;
        stepY = (bounds.size.y + (_offsetCoefficient - 1) * cellPrefab.transform.localScale.y) / height;
        Debug.Log(minX + " " + minY);

        var chips = GameObject.FindGameObjectsWithTag("Chip");
        foreach (var chip in chips)
        {
            if (chip.GetComponent<Chip>().isMovable)
                movableChips += 1;
        }
        onSolderingControllerInitialized?.Invoke();
    }

    public void OnChipPlaced()
    {
        chipsPlased += 1;
        if (chipsPlased == movableChips)
        {
            var win = CheckWin();
            if (win)
                Debug.Log("WIN WIN WIN");
        }
    }

    public void OnChipRemoved()
    {
        chipsPlased -= 1;
    }

    public bool CheckWin()
    {
        var chips = GameObject.FindGameObjectsWithTag("Chip");
        foreach (var chip in chips)
        {
            if (!chip.GetComponent<Chip>().FullContacted())
                return false;
        }

        return true;
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
    
    private void GenerateField()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var pos = new Vector2(_startX + (cellPrefab.transform.localScale.x) * j * _offsetCoefficient, 
                    _startY + (cellPrefab.transform.localScale.y) * i * _offsetCoefficient);
                var cell = Instantiate(cellPrefab, fieldParent.transform, true);
                cell.transform.position = pos;
                cells[i, j] = new Cell(false, j, i, pos);
            }
        }
    }
    
    void Update()
    {
        
    }
}

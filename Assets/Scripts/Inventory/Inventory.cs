using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] cellObjects;
    private Item[] items;
    private static Inventory _instance;
    public InventoryCell[] cells;
    public float minX, minY, stepX, stepY;
    public Bounds bounds;
    public int width, height;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("Inventory");
                    go.AddComponent<Inventory>();
                }
            }

            return _instance;
        }
    }

    private List<Item> itemsList = new List<Item>();
    public Item currentItem;
    void Start()
    {
        _instance = this;
        bounds = CountBounds();
        width = 1;
        height = 5;
        stepX = 1;
        stepY = 1;
        minX = bounds.min.x + 0.5f;
        minY = bounds.min.y + 0.5f;

        cells = new InventoryCell[cellObjects.Length];
        for (int i = 0; i < cellObjects.Length; i++)
        {
            cells[i] = new InventoryCell(false, 0, i, cellObjects[i].transform.position);
        }
    }

    public bool ContainsItem(Item item)
    {
        return itemsList.Contains(item);
    }

    private int findEmptyCell()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (!cells[cells.Length - 1 - i].isOccupied)
                return cells.Length - 1 - i;
        }

        return -1;
    }
    
    public int PlaceItem(Item item, int cellId=-1)
    {
        if (item == currentItem)
            currentItem = null;
        itemsList.Add(item);
        if (cellId < 0)
            cellId = findEmptyCell();
//        Debug.Log("Place on " + cellId);
        item.gameObject.transform.position = cells[cellId].position;
        cells[cellId].isOccupied = true;
        cells[cellId].item = item;
        return cellId;
    }

    public void RemoveItem(int cellId)
    {
        var cell = cells[cellId];
        itemsList.Remove(cell.item);
        cell.isOccupied = false;
        cell.item = null;
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
}

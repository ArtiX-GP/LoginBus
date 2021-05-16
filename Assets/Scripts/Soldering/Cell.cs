using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool isOccupied;
    public int x, y;
    public Vector2 position;
    public Chip chip;

    public Cell(bool isOccupied, int x, int y, Vector2 position)
    {
        this.isOccupied = isOccupied;
        this.x = x;
        this.y = y;
        this.position = position;
    }
}

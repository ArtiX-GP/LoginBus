using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell
{
    public bool isOccupied;
    public int x;
    public int y;
    public Vector2 position;
    
    public Cell(bool isOccupied, int x, int y, Vector2 position)
    {
        this.isOccupied = isOccupied;
        this.x = x;
        this.y = y;
        this.position = position;
    }
}

public class SolderingCell: Cell
{
    public Chip chip;
    public SolderingCell(bool isOccupied, int x, int y, Vector2 position) : base(isOccupied, x, y, position) { }
}

public class MazeCell: Cell
{
    public MazeCell(bool isOccupied, int x, int y, Vector2 position) : base(isOccupied, x, y, position) { }
}
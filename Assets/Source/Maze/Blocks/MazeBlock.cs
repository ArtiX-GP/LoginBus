using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MazeBlock
{
    private Vector3Int _position;

    public MazeBlockType type = MazeBlockType.OUTSIDE;

    public MazeTileBlock[] tileBases;

    public MazeBlock() { }

    public MazeBlock(Vector3Int _pos) {
        _position = _pos;
    }

    public MazeBlock(MazeBlock origin, Vector3Int _pos) {
        this.tileBases = origin.tileBases;
        this.type = origin.type;
        this.position = _pos;
    }

    public virtual void Invalidate() {

    }

    public virtual TileBase GetTileBase(MazeBlock[,] map = null, int w = 0, int h = 0) {
        return tileBases[BusRandom.GenerateRandomNumber(tileBases.Length)].tileBase;
    }

    public virtual bool HasCollision() {
        return false;
    }

    public virtual bool HasTrigger() {
        return false;
    }

    public virtual bool IsDecoration() {
        return false;
    }

    public Vector3Int position {
        get { return _position; }
        set { _position = value; }
    }

    protected TileBase GetTileByType(MazeTileBlockType _type) {
        var result = Array.Find(tileBases, el => el.type == _type);
        return result != null ? result.tileBase : GetDefaultTile();
    }

    protected TileBase GetDefaultTile() {
        MazeTileBlock[] blocks = Array.FindAll(tileBases, el => el.type == MazeTileBlockType.DEFAULT);
        return blocks[BusRandom.GenerateRandomNumber(blocks.Length)].tileBase;
    }

}

public enum MazeTileBlockType {
    DEFAULT,
    TOP_LEFT,
    TOP_CENTER,
    TOP_RIGHT,
    LEFT,
    RIGHT,
    BOTTOM_LEFT,
    BOTTOM_CENTER,
    BOTTOM_RIGHT,

    HORIZONTAL,
    VERTICAL,

    UP_CROSS,
    DOWN_CROSS,
    LEFT_CROSS,
    RIGHT_CROSS,

    ACTIVATED,
}

public struct MazeTileBlockHitStruct {
    public bool top, left, bottom, right;
    public MazeTileBlockType requiredType;

    public MazeTileBlockHitStruct(bool l, bool b, bool t, bool r, MazeTileBlockType type) {
        top = t;
        bottom = b;
        left = l;
        right = r;
        requiredType = type;
    }
}

[Serializable]
public class MazeTileBlock {
    public MazeTileBlockType type = MazeTileBlockType.DEFAULT;

    public TileBase tileBase;
}

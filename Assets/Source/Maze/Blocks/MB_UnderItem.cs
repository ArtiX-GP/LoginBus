using UnityEngine;
using UnityEngine.Tilemaps;

public class MB_UnderItem : MazeBlock
{

    public MB_UnderItem(MazeBlock origin, Vector3Int _pos) : base(origin, _pos) { }

    public override TileBase GetTileBase(MazeBlock[,] map = null, int w = 0, int h = 0) {
        return GetDefaultTile();
    }
}

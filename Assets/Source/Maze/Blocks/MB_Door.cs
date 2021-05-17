using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MB_Door : MazeBlock
{
    private bool _enabled;

    private string _key;

    public MB_Door(MazeBlock origin, Vector3Int _pos, string key) : base(origin, _pos) {
        _key = key;
    }

    public override void Invalidate() {
        _enabled = GameState.GetInt(_key) == 1;
    }

    public override TileBase GetTileBase(MazeBlock[,] map = null, int w = 0, int h = 0) {
        if (_enabled) {
            return GetTileByType(MazeTileBlockType.ACTIVATED);
        }
        return GetDefaultTile();
    }

    public override bool HasCollision() {
        return !_enabled;
    }

}

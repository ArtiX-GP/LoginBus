using UnityEngine;
using UnityEngine.Tilemaps;

public class MB_DoorSwitch : MazeBlock
{
    private bool _activated;

    private string key;

    public MB_DoorSwitch(MazeBlock origin, Vector3Int _pos, string _key) : base(origin, _pos) {
        key = _key;
        Debug.Log("Create with " + key);
    }

    public override TileBase GetTileBase(MazeBlock[,] map = null, int w = 0, int h = 0) {
        if (_activated) {
            return GetTileByType(MazeTileBlockType.ACTIVATED);
        }
        return GetDefaultTile();
    }

    public override bool HasTrigger() {
        return true;
    }

    public override bool IsDecoration() {
        return true;
    }

    public void Activate() {
        if (_activated || key == null) {
            return;
        }
        _activated = true;
        GameState.SetValue(key, 1);
        Debug.Log("Activate " + key);
    }
}

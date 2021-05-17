using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MazeMap {
    CAPTAIN,
    MACHINE,
    BASEMENT
}

public class MazeBuilder : MonoBehaviour, GameStateListener {
    //public Dictionary<MazeBlockType, TileBase> tiles;

    public MazeMap currentMap = MazeMap.CAPTAIN;

    public MazeBlock[] sourceTiles;

    public Vector3Int pivot;

    public Tilemap tilemap, colliderTilemap, decorTilemap;

    private MazeBlock[,] _map;

    private static MazeBuilder singleTon;

    private SampleMap SOURCE {
        get { 
            switch (currentMap) {
                case MazeMap.CAPTAIN:
                    return new SampleMap();
                case MazeMap.MACHINE:
                    return new MachineMap();
            }
            return null;
        }
    }

    public MazeBuilder() {
        singleTon = this;
    }

    // Start is called before the first frame update
    void Start() {
        GameState.AddListener("MazeBuilder_" + SOURCE.GetMapName(), this);

        if (tilemap == null) {
            tilemap = transform.Find("Tilemap").GetComponent<Tilemap>();
        }

        if (colliderTilemap == null) {
            colliderTilemap = transform.Find("Collider").GetComponent<Tilemap>();
        }

        if (decorTilemap == null) {
            decorTilemap = transform.Find("Decoration").GetComponent<Tilemap>();
        }

        map = new MazeBlock[SampleMap.W, SampleMap.H];

        int rowIndex = 0;
        foreach (var row in SOURCE.GetMap()) {
            int colIndex = 0;
            foreach (var col in row) {
                MazeBlock block = BuildTileByChar(col, new Vector3Int(rowIndex, colIndex, 0));
                map[rowIndex, colIndex] = block;
                colIndex++;
            }
            rowIndex++;
        }

        transform.Find("Collider").gameObject.AddComponent<TilemapCollider2D>();

        transform.Find("Decoration").gameObject.AddComponent<TilemapCollider2D>();
        transform.Find("Decoration").gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;

        Invalidate();
    }

    private void Invalidate() {
        printMap();

        tilemap.ClearAllTiles();
        colliderTilemap.ClearAllTiles();
        decorTilemap.ClearAllTiles();

        for (int row = 0; row < SampleMap.H; row++) {
            for (int col = 0; col < SampleMap.W; col++) {
                map[col, row].Invalidate();
            }
        }

        for (int row = 0; row < SampleMap.H; row++) {
            for (int col = 0; col < SampleMap.W; col++) {
                MazeBlock block = map[row, col];
                if (block != null) {
                    Vector3Int pos = new Vector3Int(col - pivot.x, SampleMap.H - row - pivot.y, 0);
                    if (block.HasCollision()) {
                        colliderTilemap.SetTile(pos, block.GetTileBase(map, SampleMap.W, SampleMap.H));
                    } else if (block.HasTrigger() && block.IsDecoration()) {
                        tilemap.SetTile(pos, BuildTileByType(MazeBlockType.UNDER_ITEM, pos).GetTileBase());
                        decorTilemap.SetTile(pos, block.GetTileBase(map, SampleMap.W, SampleMap.H));

                    } else {
                        tilemap.SetTile(pos, block.GetTileBase());
                    }
                }
            }
        }        
    }

    private MazeBlock BuildTileByChar(char type, Vector3Int pos) {
        if (Enum.IsDefined(typeof(MazeBlockType), (int)type)) {
            MazeBlockType enumVal = (MazeBlockType)type;
            return BuildTileByType(enumVal, pos);
        }
        return null;
    }

    private MazeBlock BuildTileByType(MazeBlockType type, Vector3Int pos) {
        MazeBlock origin = Array.Find(sourceTiles, el => el.type == type);
        if (origin != null) {
            switch (origin.type) {
                case MazeBlockType.OUTSIDE:
                    return new MB_Outside(origin, pos);
                case MazeBlockType.DOOR_YELLOW:
                    return new MB_Door(origin, pos, GetYellowDoorKey());
                case MazeBlockType.DOOR_RED:
                    return new MB_Door(origin, pos, GetRedDoorKey());
                case MazeBlockType.DOOR_GREEN:
                    return new MB_Door(origin, pos, GetGreenDoorKey());
                case MazeBlockType.DOOR_BUTTON_GREEN:
                    return new MB_DoorSwitch(origin, pos, GetGreenDoorKey());
                case MazeBlockType.DOOR_BUTTON_RED:
                    return new MB_DoorSwitch(origin, pos, GetRedDoorKey());
                case MazeBlockType.DOOR_BUTTON_YELLOW:
                    return new MB_DoorSwitch(origin, pos, GetYellowDoorKey());
                case MazeBlockType.UNDER_ITEM:
                    return new MB_UnderItem(origin, pos);
                default:
                    return new MazeBlock(origin, pos);
            }
        }
        return null;
    }

    private string GetGreenDoorKey() {
        switch (currentMap) {
            case MazeMap.CAPTAIN:
                return GameStateKeys.CAPTAIN_GREEN_DOOR;
            case MazeMap.BASEMENT:
                return GameStateKeys.BASEMENT_GREEN_DOOR;
            case MazeMap.MACHINE:
                return GameStateKeys.MACHINE_GREEN_DOOR;
        }
        return null;
    }

    private string GetYellowDoorKey() {
        switch (currentMap) {
            case MazeMap.CAPTAIN:
                return GameStateKeys.CAPTAIN_YELLOW_DOOR;
            case MazeMap.BASEMENT:
                return GameStateKeys.BASEMENT_YELLOW_DOOR;
            case MazeMap.MACHINE:
                return GameStateKeys.MACHINE_YELLOW_DOOR;
        }
        return null;
    }

    private string GetRedDoorKey() {
        switch (currentMap) {
            case MazeMap.CAPTAIN:
                return GameStateKeys.CAPTAIN_RED_DOOR;
            case MazeMap.BASEMENT:
                return GameStateKeys.BASEMENT_RED_DOOR;
            case MazeMap.MACHINE:
                return GameStateKeys.MACHINE_RED_DOOR;
        }
        return null;
    }

    private void printMap() {
        string s = "MAP: \n";
        s += ("__012345678912345\n");
        for (int row = 0; row < SampleMap.H; row++) {
            s += row.ToString().Substring(0, 1) + '_';
            for (int col = 0; col < SampleMap.W; col++) {
                s += SOURCE.GetMap()[row][col];
            }
            s += "\n";
        }
        s += ("__012345678912345\n");
        Debug.Log(s.Replace(' ', 'X'));
    }

    public void onGameStateUpdated() {
        Invalidate();
    }

    public MazeBlock[,] map {
        get { return _map; }
        set { _map = value; }
    }

    public static MazeBuilder GetSingleTon() {
        return singleTon;
    }

    public MazeBlock GetBlockAt(Vector3Int pos) {
        int row = (SampleMap.W - pos.y + 1) - pivot.y;
        int col = (pos.x - 1) + pivot.x;
        try {
            return map[row, col];
        } catch (Exception e) {
            return null;
        }
    }
}

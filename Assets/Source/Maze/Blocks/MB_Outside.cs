using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Hit = MazeTileBlockHitStruct;

public class MB_Outside : MazeBlock {

    public static readonly Hit[] CONVERTS = new Hit[] {
        new Hit(true, false, false, true, MazeTileBlockType.HORIZONTAL),
        new Hit(true, true, false, false, MazeTileBlockType.TOP_RIGHT),
        new Hit(false, true, false, true, MazeTileBlockType.TOP_LEFT),
        new Hit(true, false, true, false, MazeTileBlockType.BOTTOM_RIGHT),
        new Hit(false, false, true, true, MazeTileBlockType.BOTTOM_LEFT),

        new Hit(false, false, false, true, MazeTileBlockType.LEFT),
        new Hit(true, false, false, false, MazeTileBlockType.RIGHT),
        new Hit(false, true, false, false, MazeTileBlockType.TOP_CENTER),
        new Hit(false, false, true, false, MazeTileBlockType.BOTTOM_CENTER),

        new Hit(true, true, false, true, MazeTileBlockType.DOWN_CROSS),
        new Hit(true, false, true, true, MazeTileBlockType.UP_CROSS),
        new Hit(true, true, true, false, MazeTileBlockType.LEFT_CROSS),
        new Hit(false, true, true, true, MazeTileBlockType.RIGHT_CROSS),
    };

    public MB_Outside(Vector3Int _pos) : base(_pos) { }

    public MB_Outside(MazeBlock origin, Vector3Int _pos) : base(origin, _pos) { }

    public override TileBase GetTileBase(MazeBlock[,] map = null, int w = 0, int h = 0) {
        return GetDefaultTile();
        //MazeBlock top = position.x - 1 >= 0 ? map[position.x - 1, position.y] : null;
        //MazeBlock left = position.y - 1 >= 0 ? map[position.x, position.y - 1] : null;

        //MazeBlock topLeft = position.x - 1 >= 0 && position.y - 1 >= 0 ? map[position.x - 1, position.y - 1] : null;
        //MazeBlock topRight = position.x - 1 >= 0 && position.y + 1 < w ? map[position.x - 1, position.y + 1] : null;

        //MazeBlock bottom = position.x + 1 < h ? map[position.x + 1, position.y] : null;
        //MazeBlock right = position.y + 1 < w ? map[position.x, position.y + 1] : null;

        //MazeBlock bottomRight = position.x + 1 < h && position.y + 1 < w ? map[position.x + 1, position.y + 1] : null;
        //MazeBlock bottomLeft = position.x + 1 < h && position.y - 1 >= 0 ? map[position.x + 1, position.y - 1] : null;

        //if (SameNeighbours8(topLeft, top, topRight, left, right, bottomLeft, bottom, bottomRight)) {
        //    return GetDefaultTile();
        //}

        //if (!Same(bottomRight)
        //    && (Same(top) && Same(left) && !Same(right) && !Same(bottom)) || SameNeighbours4(top, left, right, bottom)
        //) {
        //    return GetTileByType(MazeTileBlockType.TOP_LEFT);
        //}

        //if (!Same(bottomLeft)
        //    && (Same(top) && Same(right) && !Same(left) && !Same(bottom)) || SameNeighbours4(top, left, right, bottom)
        //) {
        //    return GetTileByType(MazeTileBlockType.TOP_RIGHT);
        //}

        //if (!Same(topLeft)
        //    && (Same(bottom) && Same(right) && !Same(left) && !Same(top)) || SameNeighbours4(top, left, right, bottom)
        //) {
        //    return GetTileByType(MazeTileBlockType.BOTTOM_RIGHT);
        //}

        //if (!Same(topRight)
        //    && (Same(bottom) && Same(left) && !Same(right) && !Same(top)) || SameNeighbours4(top, left, right, bottom)
        //) {
        //    return GetTileByType(MazeTileBlockType.BOTTOM_LEFT);
        //}

        //foreach (Hit hit in CONVERTS) {
        //    if (hit.top == Same(top) && hit.left == Same(left) && hit.right == Same(right) && hit.bottom == Same(bottom)) {
        //        return GetTileByType(hit.requiredType);
        //    }
        //}

        ////if (SameNeighboursExceptFirst4(right, top, bottom, left) && Same(topLeft) && Same(bottomLeft) && !Same(topRight) && !Same(bottomRight)) {
        ////    return GetTileByType(MazeTileBlockType.LEFT);
        ////} else if (SameNeighboursExceptFirst4(left, top, bottom, right) && Same(topRight) && Same(bottomRight) && !Same(topLeft) && !Same(bottomLeft)) {
        ////    return GetTileByType(MazeTileBlockType.RIGHT);
        ////} else if (SameNeighboursExceptFirst4(top, left, bottom, right) && Same(bottomRight) && Same(bottomLeft) && !Same(topRight) && !Same(topLeft)) {
        ////    return GetTileByType(MazeTileBlockType.BOTTOM_CENTER);
        ////} else if (SameNeighboursExceptFirst4(bottom, left, top, right) && Same(topLeft) && Same(topRight) && !Same(bottomLeft) && !Same(bottomRight)) {
        ////    return GetTileByType(MazeTileBlockType.TOP_CENTER);
        ////}

        //return GetTileByType(MazeTileBlockType.VERTICAL);
    }

    private bool Same(MazeBlock b) {
        return b == null || b.type == type;
    }

    private bool SameNeighboursExceptFirst4(MazeBlock notEqual, MazeBlock n, MazeBlock n1, MazeBlock n2) {
        return !Same(notEqual) && Same(n) && Same(n1) && Same(n2);
    }

    private bool SameNeighbours4(MazeBlock top, MazeBlock left, MazeBlock bottom, MazeBlock right) {
        return Same(top) && Same(left) && Same(bottom) && Same(right);
    }

    private bool SameNeighbours8(MazeBlock tl, MazeBlock t, MazeBlock tr, MazeBlock l, 
        MazeBlock r, MazeBlock bl, MazeBlock b, MazeBlock br) {
        return SameNeighbours4(t, l, b, r) && SameNeighbours4(tl, tr, bl, br);
    }

    public override bool HasCollision() {
        return true;
    }
}

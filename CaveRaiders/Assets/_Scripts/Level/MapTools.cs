using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

static class MapTools
{
    public static void ValidateTile(TileData[,] tileData, Vector2Int tileID)
    {
        var tileSettings = tileData[tileID.x, tileID.y].Settings;
        if (tileSettings.MeshType == TileConfig.MeshType.Floor)
            return;
        // check if tile is at the edge of map
        if (tileID.x == 0 || tileID.x == tileData.GetLength(0) - 1 || tileID.y == 0 || tileID.y == tileData.GetLength(1) - 1)
        {
            // TODO: Check for MapEdgeTile
            return;
        }
        // check if tile is surrounded by ceilings
        int x = tileID.x;
        int y = tileID.y;
        // check direction clockwise, starting from tile on up (y+1)
        uint checkIDint = 0;
        if (tileData[x, y + 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b1000_0000;
        if (tileData[x + 1, y + 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0100_0000;
        if (tileData[x + 1, y].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0010_0000;
        if (tileData[x + 1, y - 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0001_0000;
        if (tileData[x, y - 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0000_1000;
        if (tileData[x - 1, y - 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0000_0100;
        if (tileData[x - 1, y].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0000_0010;
        if (tileData[x - 1, y + 1].Settings.MeshType == TileConfig.MeshType.Ceiling)
            checkIDint = checkIDint | 0b0000_0001;
        byte checkID = (byte)checkIDint;
        // check if two opposing walls
        if (((checkID & 0b1000_1000) == 0) || ((checkID & 0b0010_0010) == 0))
        {
            Debug.Log("Wall Destroyed with checkID: " + checkID + " at " + tileID);
            // change tile to floor
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.Floor);
            // Validate all neighbouring tiles (4 directions) for changes
            ValidateTile(tileData, new Vector2Int(x, y + 1));
            ValidateTile(tileData, new Vector2Int(x + 1, y));
            ValidateTile(tileData, new Vector2Int(x, y - 1));
            ValidateTile(tileData, new Vector2Int(x - 1, y));
        }
        // you could use loops and bitrotation, but for readability here are the hardcoded checks for 4 directiosn each
        // outward facing corners
        else if (checkID == 0b1110_0000) // bottom left corner out
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerOut, 0);
        else if (checkID == 0b0011_1000) // top left corner out
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerOut, 1);
        else if (checkID == 0b0000_1110) // top right corner out
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerOut, 2);
        else if (checkID == 0b1000_0011) // bottom right corner out
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerOut, 3);
        // inward facing corners
        else if (checkID == 0b1111_1011) // bottom left corner in
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerIn, 0);
        else if (checkID == 0b1111_1110) // top left corner in
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerIn, 1);
        else if (checkID == 0b1011_1111) // top right corner in
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerIn, 2);
        else if (checkID == 0b1110_1111) // bottom right corner in
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.CornerIn, 3);
        // wall
        else if (checkID == 0b1110_0011) //bottom wall
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.Wall, 0);
        else if (checkID == 0b1111_1000) // left wall
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.Wall, 1);
        else if (checkID == 0b0011_1110) // top wall
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.Wall, 2);
        else if (checkID == 0b1000_1111) // right wall
            tileData[x, y].Tile.SwitchToTileType(TileConfig.MeshType.Wall, 3);
    }

    public static void ValidateTileMap(TileData[,] tileData)
    {
        for (int x = 0; x < tileData.GetLength(0); x++)
        {
            for (int y = 0; y < tileData.GetLength(1); y++)
            {
                if (tileData[x, y].Settings.MeshType != TileConfig.MeshType.Floor)
                    ValidateTile(tileData, new Vector2Int(x, y));
            }
        }
    }

    public static TileData[,] GetTileData(Tilemap tilemap)
    {
        var bounds = tilemap.cellBounds;
        var tileData = new TileData[bounds.size.x, bounds.size.y];
        var tilemapData = tilemap.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                var tile = tilemapData[x + y * bounds.size.x];
                var tileScript = TileConfig.TileClasses[tile.name];
                tilemap.GetTile(new Vector3Int(x, y, 0));
                var tilePos = new Vector2Int(x, y);
                // var tileType = TileConfig.Type.Floor;
                tileData[x, y] = new TileData(tilePos, tileScript);
            }
        }
        return tileData;
    }


}
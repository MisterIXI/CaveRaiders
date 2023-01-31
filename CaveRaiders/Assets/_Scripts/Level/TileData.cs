using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TileData
{
    public TileData(Vector2Int pos, ITile script)
    {
        Pos = pos;
        Script = script;
        Settings = script.Settings;
    }
    public Vector2Int Pos;
    public ITile Script;
    public TileSettings Settings;
    public Tile Tile;
}
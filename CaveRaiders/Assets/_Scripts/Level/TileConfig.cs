using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
public static class TileConfig
{
    public enum MeshType
    {
        Floor,
        Ceiling,
        Wall,
        CornerIn,
        CornerOut
    }

    // array with 3 times a list of 1,2,3,4
    public static Dictionary<MeshType, int[]> _tileData = new Dictionary<MeshType, int[]>{
        {MeshType.Floor, new int[]{0,2,3,0,3,1}},
        {MeshType.Wall, new int[]{0,6,7,0,7,1}},
        {MeshType.CornerIn, new int[]{0,6,7,0,7,5}},
        {MeshType.CornerOut, new int[]{0,2,7,0,7,1}},
        {MeshType.Ceiling, new int[]{4,7,5,4,6,7}}
    };

    public static Dictionary<string, ITile> TileClasses = new Dictionary<string, ITile>{
        {"FloorTile", new FloorTile()},
        {"GravelTile", new GravelTile()},
        {"PathTile", new PathTile()},
        {"WallTile", new WallTile()},
        {"LooseRockTile", new LooseRockTile()},
        {"MediumRockTile", new MediumRockTile()},
        {"HardRockTile", new HardRockTile()},
        {"CrystalTile", new CrystalTile()},
        {"OreSeamTile", new OreTile()},
        {"DenseRockTile", new DenseRockTile()}
    };


}
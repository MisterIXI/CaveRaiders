using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
public static class TileData
{
    public enum Type{
        Floor,
        Ceiling,
        Wall,
        CornerIn,
        CornerOut
    }
    // array with 3 times a list of 1,2,3,4
    public static Dictionary<Type, int[]> _tileData = new Dictionary<Type, int[]>{
        {Type.Floor, new int[]{0,2,3,0,3,1}},
        {Type.Wall, new int[]{0,6,7,0,7,1}},
        {Type.CornerIn, new int[]{0,6,7,0,7,5}},
        {Type.CornerOut, new int[]{0,2,7,0,7,1}},
        {Type.Ceiling, new int[]{4,7,5,4,6,7}}
    };
}
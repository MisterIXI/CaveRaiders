using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System;
public class TileBuilder : MonoBehaviour
{

    [SerializeField] Tilemap tilemap;

    private void Start()
    {
        SpawnTiles();

    }

    private void SpawnTiles()
    {
        var tileData = MapTools.GetTileData(tilemap);
        for (int x = 0; x < tileData.GetLength(0); x++)
        {
            for (int y = 0; y < tileData.GetLength(1); y++)
            {
                TileData tile = tileData[x, y];
                GameObject tileObject = new GameObject("Tile");
                Tile tileComponent = tileObject.AddComponent<Tile>();
                tileComponent.InitTileMesh(tile);
                tileComponent.SwitchToTileType(tile.Settings.MeshType);
                tileObject.transform.parent = transform;
                tileObject.transform.localPosition = new Vector3(x, 0, y);
            }
        }
        MapTools.ValidateTileMap(tileData);
    }

}
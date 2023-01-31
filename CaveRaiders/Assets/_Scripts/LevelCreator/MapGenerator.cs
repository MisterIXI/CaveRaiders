using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    // add a debug button in inspector
    Grid grid;
    public void CreateNewMap()
    {
        if(grid != null)
        {
            DestroyImmediate(grid.gameObject);
        }
        grid = new GameObject("Grid").AddComponent<Grid>();
        grid.transform.parent = transform;

        var tilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        tilemap.gameObject.AddComponent<TilemapRenderer>();
        tilemap.transform.parent = grid.transform;
        grid.transform.localScale = Vector3.one * 5f;
    }

    [SerializeField]
    public void ValidateMap()
    {
        Debug.Log("ValidateMap");
    }
    public void GenerateMap()
    {
        Debug.Log("GenerateMap");
    }
}
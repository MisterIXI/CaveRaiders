using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileData.Type _startType;
    [SerializeField] private BoxCollider _floorCollider;
    [SerializeField] private BoxCollider _CeilingCollider;
    public TileData.Type Type { get; private set; }
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = new Mesh();
        GenerateDefaultMesh();
    }

    private void GenerateDefaultMesh()
    {
        // init mesh with 2x2x2 cube, centered at 0,0,0
        _mesh.vertices = new Vector3[]{
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, -0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(-0.5f, 1, -0.5f),
            new Vector3(0.5f, 1, -0.5f),
            new Vector3(-0.5f, 1, 0.5f),
            new Vector3(0.5f, 1, 0.5f)
        };
        _mesh.triangles = TileData._tileData[_startType];
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter.mesh = _mesh;
        Type = _startType;
    }

    private void SwitchToTileType(TileData.Type type, int rotation = 0)
    {
        _mesh.triangles = TileData._tileData[type];
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        Type = type;
        _meshFilter.mesh = _mesh;
        transform.localRotation = Quaternion.Euler(0, rotation * 90, 0);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
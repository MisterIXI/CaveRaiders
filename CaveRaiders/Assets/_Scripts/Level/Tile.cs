using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileConfig.MeshType _startType;
    [SerializeField] private BoxCollider _floorCollider;
    [SerializeField] private BoxCollider _CeilingCollider;
    public TileConfig.MeshType Type { get; private set; }
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private bool _isInitialized = false;
    private TileSettings _tileSettings;
    [SerializeField] private TileData _tileData;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void InitTileMesh(TileData tileData)
    {
        _tileData = tileData;
        if (tileData.Tile != null)
        {
            Debug.LogError("Another tile existing in TileData!");
            Destroy(gameObject);
            return;
        }
        _tileData.Tile = this;
        // _tileSettings = RefManager.settingsManager.tileSettings;
        _isInitialized = true;
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshFilter == null)
            _meshFilter = gameObject.AddComponent<MeshFilter>();
        if (_meshRenderer == null)
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        _mesh = new Mesh();
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
        _mesh.triangles = TileConfig._tileData[_startType];
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter.mesh = _mesh;
        Type = _startType;
        CheckMaterialUpdate();
    }
    private void InitializedCheck()
    {
        if (!_isInitialized)
        {
            Debug.LogError("Tile not initialized");
            Destroy(gameObject);
            return;
        }
    }
    public void SwitchToTileType(TileConfig.MeshType type, int rotation = 0)
    {
        InitializedCheck();
        _mesh.triangles = TileConfig._tileData[type];
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        Type = type;
        _meshFilter.mesh = _mesh;
        // set new settings
        if(type == TileConfig.MeshType.Floor)
        {
            _tileData.Script = TileConfig.TileClasses["FloorTile"];
            _tileData.Settings = _tileData.Script.Settings;
        }
        
        transform.localRotation = Quaternion.Euler(0, rotation * 90, 0);
        CheckMaterialUpdate();
    }

    private void CheckMaterialUpdate()
    {
        _meshRenderer.material = _tileData.Settings.Texture;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
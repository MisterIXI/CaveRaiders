using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TileSettings", menuName = "CaveRaiders/TileSettings", order = 0)]
public class TileSettings : ScriptableObject
{
    [Header("TileSettings")]
    public Material Texture;
    [Range(0f, 2f)] public float WalkSpeedMultiplier;
    public TileConfig.MeshType MeshType;
    public int DrillHealth;
    [Header("DrillTypes")]
    public bool CanBeHandDrilled;
    public bool CanBeMachineDrilled;
    public bool CanBeLaserDrilled;
    public bool CanBeExplosiveDrilled;
    // TODO: add loottable


}

[Serializable]
public struct TileSettingsPair{
    public string name;
    public TileSettings settings;
}
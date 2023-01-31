using UnityEngine;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour {

    [Header("TileSettings")]
    public TileSettings FloorSetting;
    public TileSettings GravelSetting;
    public TileSettings PathSetting;
    public TileSettings LooseRockSetting;
    public TileSettings MediumRockSetting;
    public TileSettings HardRockSetting;
    public TileSettings WallSetting;
    public TileSettings CrystalWallSetting;
    public TileSettings OreSetting;
    public TileSettings DenseRockSetting;


    private void Awake() {
        if(RefManager.settingsManager != null)
        {
            Destroy(gameObject);
            return;
        }
        RefManager.settingsManager = this;
    }
}


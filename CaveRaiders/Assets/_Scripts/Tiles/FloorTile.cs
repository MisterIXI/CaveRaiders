class FloorTile : ITile
{
    public float WalkSpeed => throw new System.NotImplementedException();
    public TileSettings Settings => _settings;
    private TileSettings _settings;

    public FloorTile()
    {
        _settings = RefManager.settingsManager.FloorSetting;
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrillTick(int drillPower)
    {
        throw new System.NotImplementedException();
    }
}
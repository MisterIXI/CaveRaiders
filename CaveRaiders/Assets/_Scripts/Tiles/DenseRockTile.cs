class DenseRockTile : ITile
{
    public float WalkSpeed => throw new System.NotImplementedException();
    public TileSettings Settings => _settings;
    private TileSettings _settings;

    public DenseRockTile()
    {
        _settings = RefManager.settingsManager.DenseRockSetting;
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
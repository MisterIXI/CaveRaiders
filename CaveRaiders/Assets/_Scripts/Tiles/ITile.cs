public interface ITile
{
    public float WalkSpeed { get; }
    public void OnDrillTick(int drillPower);
    public void OnDestroy();
    public TileSettings Settings { get; }

}
namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances
{
    public interface IRoomEntrance
    {
        IRoom ParentRoom { get; }
        IRoomEntrance ConnectedEntrance { get; }
    }
}
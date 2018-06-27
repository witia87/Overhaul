using Assets.Cognitions.Maps.MapGraphs.Rooms;

namespace Assets.Cognitions.Maps.MapGraphs
{
    public interface IMapGraph
    {
        IRoom Rooms { get; }
    }
}
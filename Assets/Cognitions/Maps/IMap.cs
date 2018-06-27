using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.Paths;

namespace Assets.Cognitions.Maps
{
    public interface IMap
    {
        IMapGrid Grid { get; }
        IMapGraph Graph { get; }

        IPathPromise RequestPathToRoom(IRoom room);
        IPathPromise RequestPathToPosition(IRoom room);
    }
}
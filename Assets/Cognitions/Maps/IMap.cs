using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.Paths;

namespace Assets.Cognitions.Maps
{
    public interface IMap
    {
        IPathPromise RequestPathToRoom(IRoom room);
        IPathPromise RequestPathToPosition(IRoom room);
    }
}
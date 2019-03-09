using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.Paths;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public interface IMap
    {
        IPathPromise RequestPathToRoom(IRoom room);
        IPathPromise RequestPathToPosition(Vector3 position);

        bool ArePositionsOnTheSameTile(Vector3 a, Vector3 b);
        bool IsRectangleClear(Vector3 a, Vector3 b);
    }
}
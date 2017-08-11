using System.Collections.Generic;
using UnityEngine;

namespace Assets.Maps.PathFinders
{
    public interface IPathFinder
    {
        List<Vector3> FindPath(Vector3 start, Vector3 end);
        List<Vector3> FindSafespot(Vector3 start);
        bool TryGetClosestAvailablePosition(Vector3 position, float distanceTolerance, out Vector3 closestPosition);
    }
}
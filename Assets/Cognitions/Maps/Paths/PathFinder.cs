using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.Paths
{
    public class PathFinder : IPathFinder
    {
        private readonly MapGrid _map;
        private readonly SafespotFinder _safespotFinder;
        private readonly SimplePathFinder _simplePathFinder;

        public PathFinder(MapGrid map, int pathFindingStepsLimit)
        {
            _map = map;
            _simplePathFinder = new SimplePathFinder(map, pathFindingStepsLimit);
            _safespotFinder = new SafespotFinder(map);
        }

        public bool TryGetClosestAvailablePosition(Vector3 position, float distanceTolerance,
            out Vector3 closestPosition)
        {
            INode node;
            closestPosition = _map.TryGetClosestAvailablePosition(position, distanceTolerance, out node)
                ? node.Position
                : Vector3.zero;
            return node != null;
        }

        public List<Vector3> FindPath(Vector3 start, Vector3 end)
        {
            return _simplePathFinder.FindPath(start, end);
        }

        public List<Vector3> FindSafespot(Vector3 start)
        {
            return _safespotFinder.FindClosestSafespot(start);
        }
    }
}
using System.Collections.Generic;
using Assets.Cognitions.Maps.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.PathFinders
{
    public class PathFinder : IPathFinder
    {
        private readonly Map _map;
        private readonly SafespotFinder _safespotFinder;
        private readonly SimplePathFinder _simplePathFinder;

        public PathFinder(Map map)
        {
            _map = map;
            _simplePathFinder = new SimplePathFinder(map);
            _safespotFinder = new SafespotFinder(map);
        }

        public bool TryGetClosestAvailablePosition(Vector3 position, float distanceTolerance,
            out Vector3 closestPosition)
        {
            INode node;
            closestPosition = _map.TryGetClosestAvailablePosition(position, 5, out node)
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
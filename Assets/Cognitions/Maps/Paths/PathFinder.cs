using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGrids;
using UnityEngine;

namespace Assets.Cognitions.Maps.Paths
{
    public class PathFinder : IPathFinder
    {
        private readonly MapGrid _grid;
        private readonly SafespotFinder _safespotFinder;
        private readonly SimplePathFinder _simplePathFinder;

        public PathFinder(MapGrid grid, int pathFindingStepsLimit)
        {
            _grid = grid;
            _simplePathFinder = new SimplePathFinder(grid, pathFindingStepsLimit);
            _safespotFinder = new SafespotFinder(grid);
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
using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.Paths;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class Map: IMap
    {
        private readonly IUnit _unit;
        private readonly IMapGrid _grid;
        private readonly IMapGraph _graph;

        public Map(IUnit unit, IMapGrid grid, IMapGraph graph)
        {
            _grid = grid;
            _graph = graph;
            _unit = unit;
        }

        public IPathPromise RequestPathToRoom(IRoom room)
        {
            throw new System.NotImplementedException();
        }

        public IPathPromise RequestPathToPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
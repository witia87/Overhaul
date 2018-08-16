using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.Paths;

namespace Assets.Cognitions.Maps
{
    public class Map: IMap
    {
        private readonly IMapGrid _grid;
        private readonly IMapGraph _graph;

        public Map(IMapGrid grid, IMapGraph graph)
        {
            _grid = grid;
            _graph = graph;
        }

        public IPathPromise RequestPathToRoom(IRoom room)
        {
            throw new System.NotImplementedException();
        }

        public IPathPromise RequestPathToPosition(IRoom room)
        {
            throw new System.NotImplementedException();
        }
    }
}
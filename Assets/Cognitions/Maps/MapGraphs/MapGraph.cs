using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms;

namespace Assets.Cognitions.Maps.MapGraphs
{
    class MapGraph: IMapGraph
    {
        private readonly IEnumerable<IRoom> _rooms;

        public MapGraph(IEnumerable<IRoom> rooms)
        {
            _rooms = rooms;
        }

        public IEnumerable<IRoom> Rooms
        {
            get { return _rooms; }
        }
    }
}
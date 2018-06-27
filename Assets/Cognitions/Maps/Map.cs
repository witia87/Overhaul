using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGrids;

namespace Assets.Cognitions.Maps
{
    public class Map: IMap
    {
        public IMapGrid Grid { get; private set; }
        public IMapGraph Graph { get; private set; }
    }
}
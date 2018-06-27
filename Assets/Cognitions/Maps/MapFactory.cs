using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Environment;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class MapFactory : MonoBehaviour
    {
        private MapGraphFactory _mapGraphFactory;
        private MapGridFactory _mapGridFactory;


        public void Initialize()
        {
            _mapGraphFactory = FindObjectOfType<MapGraphFactory>();
            _mapGridFactory = FindObjectOfType<MapGridFactory>();

            _mapGraphFactory.Initialize();
            _mapGridFactory.Initialize(_mapGraphFactory.GetMapGraph());
        }


        public Map GetMap(int scale, FractionId fractionId)
        {
            var mapGrid = _mapGridFactory.GetMapGrid(fractionId, scale);
            var mapGraph = _mapGraphFactory.GetMapGraph();

            return new Map();
        }
    }
}
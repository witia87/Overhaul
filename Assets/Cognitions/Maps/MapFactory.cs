using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Environment;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class MapFactory : MonoBehaviour
    {
        private MapGridInitializer _mapGridInitializer;
        private MapGraphInitializer _mapGraphInitializer;


        public void Initialize()
        {
            _mapGridInitializer = FindObjectOfType<MapGridInitializer>();
            _mapGraphInitializer = FindObjectOfType<MapGraphInitializer>();

            var graph = _mapGraphInitializer.Initialize();
            var baseGrid = _mapGridInitializer.Initialize(graph);
        }


        public Map GetMap(int scale, FractionId fractionId)
        {
            var mapGrid = _mapGridFactory.GetMapGrid(fractionId, scale);
            var mapGraph = _mapGraphFactory.GetMapGraph();

            return new Map(mapGrid, mapGraph);
        }
    }
}
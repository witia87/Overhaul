using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Environment;
using Assets.Environment.Units;
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


        public Map GetMap(IUnit unit)
        {
            var mapGrid = _mapGridInitializer.GetMapGrid(unit.Fraction, unit.UnitScale);
            var mapGraph = _mapGraphInitializer.GetMapGraph();

            return new Map(unit, mapGrid, mapGraph);
        }
    }
}
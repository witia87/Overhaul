using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using Assets.Environment;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    internal class MapGridInitializer : MonoBehaviour
    {
        private readonly FractionGrids[] _fractionGrids = new FractionGrids[2];
        [SerializeField] private readonly int _gridLength = 200;
        [SerializeField] private readonly int _gridWidth = 200;

        public MapGrid GetMapGrid(FractionId fractionId, int scale)
        {
            return _fractionGrids[(int) fractionId].GetMapGrid(scale);
        }

        public BaseNode[,] Initialize(IMapGraph graph)
        {
            var baseGrid = GetEmptyGrid();
            var roomWriter = new RoomGridWriter(baseGrid);

            roomWriter.Write(graph.Rooms);

            var playerDnagerGrid = GetBaseDangersGrid(baseGrid);
            var enemyDangerGrid = GetBaseDangersGrid(baseGrid);

            _fractionGrids[0] = new FractionGrids(baseGrid, playerDnagerGrid, FractionId.Player);
            _fractionGrids[1] = new FractionGrids(baseGrid, enemyDangerGrid, FractionId.Enemy);

            return baseGrid;
        }

        private BaseNode[,] GetEmptyGrid()
        {
            var grid = new BaseNode[_gridLength, _gridWidth];
            for (var z = 0; z < _gridLength; z++)
            for (var x = 0; x < _gridWidth; x++)
            {
                var position = new Vector3(x + 0.5f, 10, z + 0.5f);
                RaycastHit hit;
                if (Physics.Raycast(
                        position,
                        Vector3.down, out hit, 20, MapStore.EnvironmentLayerMask)
                    && hit.transform.tag == "Floor")
                    grid[z, x] = new BaseNode(z, x, position);
            }

            return grid;
        }

        private BaseDangerNode[,] GetBaseDangersGrid(BaseNode[,] baseGrid)
        {
            var grid = new BaseDangerNode[_gridLength, _gridWidth];
            for (var z = 0; z < _gridLength; z++)
            for (var x = 0; x < _gridWidth; x++)
                if (baseGrid[z, x] != null)
                    grid[z, x] = new BaseDangerNode(baseGrid[z, x].Position);

            return grid;
        }
    }
}
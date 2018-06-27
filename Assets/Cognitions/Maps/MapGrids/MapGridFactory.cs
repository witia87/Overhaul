using System;
using System.Collections.Generic;
using Assets.Cognitions.Maps.Covers;
using Assets.Cognitions.Maps.MapGraphs;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using Assets.Environment;
using Assets.Environment.Guns.Bullets;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    internal class MapGridFactory : MonoBehaviour
    {
        private FractionGrids[] _fractionGrids = new FractionGrids[2];
        [SerializeField] private int _gridLength = 200;
        [SerializeField] private int _gridWidth = 200;

        public MapGrid GetMapGrid(FractionId fractionId, int scale)
        {
            return _fractionGrids[(int) fractionId].GetMapGrid(scale);
        }

        public void Initialize(MapGraph mapGraph)
        {
            var bulletFactories = FindObjectsOfType<BulletsFactory>();
            var availabilityGrid = GetAvailabilityGrid();
            var roomsGrid = GetRoomsGrid(mapGraph.Rooms);
            var coversGrid = GetCoversGrid();

            _fractionGrids[0] = new FractionGrids(InitializeBaseGrid(), bulletFactories, FractionId.Player);
            _fractionGrids[1] = new FractionGrids(InitializeBaseGrid(), bulletFactories, FractionId.Enemy);
        }

        private bool[,] GetAvailabilityGrid()
        {
            var grid = new bool[_gridLength, _gridWidth];
            for (var z = 0; z < _gridLength; z++)
            {
                for (var x = 0; x < _gridWidth; x++)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(
                        new Vector3(x + 0.5f, 10, z + 0.5f),
                        Vector3.down, out hit, 20, MapStore.EnvironmentLayerMask))
                    {
                        grid[z, x] = hit.transform.tag == "Floor";
                    }
                }
            }

            return grid;
        }

        private Room[,] GetRoomsGrid(IEnumerable<Room> rooms)
        {
            var grid = new Room[_gridLength, _gridWidth];
            foreach (var room in rooms)
            {
                var startX = Mathf.RoundToInt(room.Position.x - room.Width / 2);
                var endX = Mathf.FloorToInt(room.Position.x + room.Width / 2);
                var startZ = Mathf.RoundToInt(room.Position.z - room.Length / 2);
                var endZ = Mathf.FloorToInt(room.Position.z + room.Length / 2);

                for (var z = startZ; z < endZ; z++)
                {
                    for (var x = startX; x < endX; x++)
                    {
                        if (grid[z, x] != null)
                        {
                            throw new Exception("Rooms overlaping");
                        }
                        grid[z, x] = room;
                    }
                }
            }

            return grid;
        }

        private CoverInfo[,] GetCoversGrid()
        {
            var grid = new CoverInfo[_gridLength, _gridWidth];
            var coverInitializers = FindObjectsOfType<CoverInitializer>();
            foreach (var coverInitializer in coverInitializers)
            {
                coverInitializer.AppendCovers(grid);
            }

            return grid;
        }
    }
}
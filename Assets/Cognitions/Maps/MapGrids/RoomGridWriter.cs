using System;
using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    public class RoomGridWriter
    {
        private readonly BaseNode[,] _baseGrid;

        private readonly CoverGridWriter _coverGridWriter;

        public RoomGridWriter(BaseNode[,] baseGrid)
        {
            _baseGrid = baseGrid;
            _coverGridWriter = new CoverGridWriter(baseGrid);
        }

        public void Write(IEnumerable<IRoom> rooms)
        {
            foreach (var room in rooms)
            {
                var startX = Mathf.RoundToInt(room.Position.x - room.Width / 2);
                var endX = Mathf.FloorToInt(room.Position.x + room.Width / 2);
                var startZ = Mathf.RoundToInt(room.Position.z - room.Length / 2);
                var endZ = Mathf.FloorToInt(room.Position.z + room.Length / 2);

                for (var z = startZ; z < endZ; z++)
                for (var x = startX; x < endX; x++)
                    if (_baseGrid[z, x] != null)
                    {
                        if (_baseGrid[z, x].Room != null) throw new Exception("Rooms overlaping");
                        _baseGrid[z, x].Room = room;
                    }

                _coverGridWriter.Write(room.Covers);
            }
        }
    }
}
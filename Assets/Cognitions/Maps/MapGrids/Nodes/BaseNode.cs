using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids.Nodes
{
    public class BaseNode : INode
    {
        private readonly BaseNode[,] _grid;

        private readonly bool[,] _isCovered = new bool[2, 2];

        public BaseNode(BaseNode[,] grid, int x, int z, Vector3 position)
        {
            _grid = grid;
            this.x = x;
            this.z = z;
            Scale = Scale;
            Position = position;
        }

        public int DangersCount { get; set; }


        public int x { get; private set; }
        public int z { get; private set; }
        public int Scale { get; private set; }
        public Vector3 Position { get; private set; }

        public bool IsDangerous
        {
            get { return DangersCount > 0; }
        }

        public bool IsCovered(Vector3 direction)
        {
            return _isCovered[direction.z < 0 ? 0 : 1,
                direction.z < 0 ? 0 : 1];
        }

        public IRoom Room { get; set; }

        public void SetCoverage(Vector3 direction)
        {
            _isCovered[direction.z < 0 ? 0 : 1,
                direction.z < 0 ? 0 : 1] = true;
        }
    }
}
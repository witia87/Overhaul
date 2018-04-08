using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cognitions.Maps.Nodes
{
    public class BaseNode : INode
    {
        private readonly INode[,] _grid;

        public BaseNode(INode[,] grid, int x, int z, Vector3 position)
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

        public bool IsOccupied { get; private set; }

        public IEnumerator<INode> GetDirectedNeighborsEnumerator(Vector3 direction)
        {
            return new DirectedNodesEnumerator(_grid, x, z, direction);
        }

        public IEnumerator<INode> GetSimpleNeighborsEnumerator()
        {
            return new SimpleNodesEnumerator(_grid, x, z);
        }

        public IEnumerator<INode> GetRandomizedNeighborsEnumerator()
        {
            return new RandomizedNodesEnumerator(_grid, x, z);
        }

        public bool IsDangerous
        {
            get { return DangersCount > 0; }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Map.Nodes
{
    public class Node : INode
    {
        private readonly INode[,] _grid;

        public Node(INode[,] grid, int x, int z, int scale, Vector3 position)
        {
            _grid = grid;
            this.x = x;
            this.z = z;
            this.scale = scale;
            Position = position;
        }

        public int x { get; private set; }
        public int z { get; private set; }
        public int scale { get; private set; }
        public Vector3 Position { get; private set; }

        public float OccpupyingMass { get; private set; }
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
    }
}
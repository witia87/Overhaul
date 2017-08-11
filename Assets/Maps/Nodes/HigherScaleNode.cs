using System.Collections.Generic;
using UnityEngine;

namespace Assets.Maps.Nodes
{
    public class HigherScaleNode : INode
    {
        private readonly BaseNode[,] _baseGrid;
        private readonly INode[,] _grid;

        public HigherScaleNode(BaseNode[,] baseGrid, INode[,] grid, int x, int z, int scale, Vector3 position)
        {
            _baseGrid = baseGrid;
            _grid = grid;
            this.x = x;
            this.z = z;
            Scale = scale;
            Position = position;
        }

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
            get
            {
                for (var _z = z;
                    _z <= Mathf.Min(_baseGrid.GetLength(0) - 1, z + Scale);
                    _z++)
                {
                    for (var _x = x;
                        _x <= Mathf.Min(_baseGrid.GetLength(1) - 1, x + Scale);
                        _x++)
                    {
                        if (_baseGrid[_z, _x].IsDangerous)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
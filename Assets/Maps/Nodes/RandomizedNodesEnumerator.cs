using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Maps.Nodes
{
    public class RandomizedNodesEnumerator : IEnumerator<INode>
    {
        private readonly INode[,] _grid;

        private readonly int _x;
        private readonly int _z;
        private int _currentStep;
        private List<INode> _validNeighbors;

        public RandomizedNodesEnumerator(INode[,] grid, int x, int z)
        {
            _x = x;
            _z = z;
            _grid = grid;
            ConstructValidNeighbors();
            Reset();
        }

        public bool MoveNext()
        {
            if (_currentStep == _validNeighbors.Count - 1)
            {
                return false;
            }
            _currentStep++;
            return true;
        }

        public void Reset()
        {
            _currentStep = -1;
        }

        public INode Current
        {
            get { return _validNeighbors[_currentStep]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }

        private void ConstructValidNeighbors()
        {
            _validNeighbors = new List<INode>();
            if (IsNodePresent(-1, 0))
            {
                _validNeighbors.Add(_grid[_z, _x - 1]);
            }
            if (IsNodePresent(0, 1))
            {
                _validNeighbors.Add(_grid[_z + 1, _x]);
            }
            if (IsNodePresent(1, 0))
            {
                _validNeighbors.Add(_grid[_z, _x + 1]);
            }
            if (IsNodePresent(0, -1))
            {
                _validNeighbors.Add(_grid[_z - 1, _x]);
            }

            for (var i = 0; i < _validNeighbors.Count; i++)
            {
                // perform random swap
                var index1 = Mathf.Min(_validNeighbors.Count - 1, Mathf.FloorToInt(Random.value*_validNeighbors.Count));
                var index2 = Mathf.Min(_validNeighbors.Count - 1, Mathf.FloorToInt(Random.value*_validNeighbors.Count));
                var temp = _validNeighbors[index2];
                _validNeighbors[index2] = _validNeighbors[index1];
                _validNeighbors[index1] = temp;
            }
        }

        private bool IsNodePresent(int offsetX, int offsetZ)
        {
            if (_x + offsetX >= 0 && _x + offsetX < _grid.GetLength(1)
                && _z + offsetZ >= 0 && _z + offsetZ < _grid.GetLength(0)
                && _grid[_z + offsetZ, _x + offsetX] != null)
            {
                return true;
            }
            return false;
        }
    }
}
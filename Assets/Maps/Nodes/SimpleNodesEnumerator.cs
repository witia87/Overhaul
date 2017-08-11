using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Maps.Nodes
{
    public class SimpleNodesEnumerator : IEnumerator<INode>
    {
        private readonly INode[,] _grid;

        private readonly int _x;
        private readonly int _z;
        private int _currentStep;

        public SimpleNodesEnumerator(INode[,] grid, int x, int z)
        {
            _x = x;
            _z = z;
            _grid = grid;
            Reset();
        }

        public bool MoveNext()
        {
            if (_currentStep == 0 && CheckDirection(-1, 0)
                || _currentStep == 1 && CheckDirection(0, 1)
                || _currentStep == 2 && CheckDirection(1, 0)
                || _currentStep == 3 && CheckDirection(0, -1))
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentStep = 0;
        }

        public INode Current
        {
            get
            {
                switch (_currentStep)
                {
                    case 1:
                        return _grid[_z, _x - 1];
                    case 2:
                        return _grid[_z + 1, _x];
                    case 3:
                        return _grid[_z, _x + 1];
                    case 4:
                        return _grid[_z - 1, _x];
                    default:
                        throw new ApplicationException("Enumerator in undefinded state.");
                }
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }

        private bool CheckDirection(int offsetX, int offsetZ)
        {
            _currentStep++;
            if (IsNodePresent(offsetX, offsetZ))
            {
                return true;
            }
            return false;
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
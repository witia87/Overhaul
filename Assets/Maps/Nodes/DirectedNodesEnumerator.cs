using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Maps.Nodes
{
    public class DirectedNodesEnumerator : IEnumerator<INode>
    {
        private readonly int _directionX;
        private readonly int _directionZ;
        private readonly INode[,] _grid;

        private readonly int _rotationDirection;

        private readonly int _x;
        private readonly int _z;
        private int _currentStep;

        public DirectedNodesEnumerator(INode[,] grid, int x, int z, Vector3 direction)
        {
            _x = x;
            _z = z;
            _grid = grid;

            var roundedDirection = GetRoundedDirection(direction);
            _directionX = Mathf.RoundToInt(roundedDirection.x);
            _directionZ = Mathf.RoundToInt(roundedDirection.z);
            _rotationDirection = Vector3.Cross(direction, roundedDirection).y >= 0 ? 1 : -1;
            Reset();
        }

        public bool MoveNext()
        {
            if ((_currentStep == 0 && CheckDirection(_directionX, _directionZ))
                || (_currentStep == 1 && CheckDirection(
                    _directionX != 0 ? 0 : _directionZ*_rotationDirection,
                    _directionZ != 0 ? 0 : _directionX*_rotationDirection))
                || (_currentStep == 2 && CheckDirection(
                    _directionX != 0 ? 0 : -_directionZ*_rotationDirection,
                    _directionZ != 0 ? 0 : -_directionX*_rotationDirection))
                || (_currentStep == 3 && CheckDirection(-_directionX, -_directionZ)))
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
                        return _grid[_z + _directionZ, _x + _directionX];
                    case 2:
                        return _grid[_z + (_directionZ != 0 ? 0 : _directionX*_rotationDirection),
                            _x + (_directionX != 0 ? 0 : _directionZ*_rotationDirection)];
                    case 3:
                        return _grid[_z - (_directionZ != 0 ? 0 : _directionX*_rotationDirection),
                            _x - (_directionX != 0 ? 0 : _directionZ*_rotationDirection)];
                    case 4:
                        return _grid[_z - _directionZ, _x - _directionX];
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

        private Vector3 GetRoundedDirection(Vector3 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                return new Vector3(Mathf.Sign(direction.x), 0, 0);
            }
            return new Vector3(0, 0, Mathf.Sign(direction.z));
        }

        private bool CheckDirection(int offsetX, int offsetZ)
        {
            if (IsNodePresent(offsetX, offsetZ))
            {
                _currentStep++;
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
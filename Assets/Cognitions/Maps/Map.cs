using System;
using Assets.Cognitions.Maps.Nodes;
using Assets.Cognitions.Maps.PathFinders;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class Map : IMap
    {
        private readonly int _scale;

        public Map(INode[,] grid, int scale)
        {
            Grid = grid;
            _scale = scale;
            PathFinder = new PathFinder(this);
        }

        public INode[,] Grid { get; private set; }

        public float MapWidth { get; private set; }
        public float MapLength { get; private set; }
        public float BaseGridUnitSize { get; private set; }

        public bool IsRectangleClear(Vector3 cornerA, Vector3 cornerB)
        {
            INode nodeA, nodeB;
            if (!TryGetNode(cornerA, out nodeA) || !TryGetNode(cornerB, out nodeB))
            {
                throw new ApplicationException("Given corners are not valid on the map.");
            }

            var xMin = Mathf.Min(nodeA.x, nodeB.x);
            var xMax = Mathf.Max(nodeA.x, nodeB.x);
            var zMin = Mathf.Min(nodeA.z, nodeB.z);
            var zMax = Mathf.Max(nodeA.z, nodeB.z);

            for (var z = zMin; z <= zMax; z++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    if (Grid[z, x] == null || Grid[z, x].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsPositionDangorous(Vector3 position)
        {
            INode node;
            if (!TryGetNode(position, out node))
            {
                throw new ApplicationException(
                    "Requested position does not belong to the accessible map area.");
            }

            return node.IsDangerous;
        }

        public bool ArePositionsOnTheSameTile(Vector3 a, Vector3 b)
        {
            return Mathf.FloorToInt(a.x - _scale * 0.5f) == Mathf.FloorToInt(b.x - _scale * 0.5f)
                   && Mathf.FloorToInt(a.z - _scale * 0.5f) == Mathf.FloorToInt(b.z - _scale * 0.5f);
        }

        public IPathFinder PathFinder { get; private set; }

        /// <summary>
        ///     Gets the closest Node present within the radius of tolerance.
        /// </summary>
        public bool TryGetNode(Vector3 position, out INode node)
        {
            var x = Mathf.FloorToInt(position.x - 0.5f * _scale);
            var z = Mathf.FloorToInt(position.z - 0.5f * _scale);

            if (Grid[z, x] != null) // This is a direct hit, slight optimization
            {
                node = Grid[z, x];
                return true;
            }

            x = Mathf.FloorToInt(position.x);
            z = Mathf.FloorToInt(position.z);

            var minDistance = float.MaxValue;
            node = null;
            for (var _z = Mathf.Max(0, z - _scale);
                _z <= Mathf.Min(Grid.GetLength(0) - 1, z);
                _z++)
            {
                for (var _x = Mathf.Max(0, x - _scale);
                    _x <= Mathf.Min(Grid.GetLength(1) - 1, x);
                    _x++)
                {
                    if (Grid[_z, _x] != null && (Grid[_z, _x].Position - position).magnitude < minDistance)
                    {
                        node = Grid[_z, _x];
                        minDistance = (Grid[_z, _x].Position - position).magnitude;
                    }
                }
            }

            return node != null;
        }

        public bool TryGetClosestAvailablePosition(Vector3 position, float distanceTolerance, out INode node)
        {
            var x = Mathf.FloorToInt(position.x - 0.5f * _scale);
            var z = Mathf.FloorToInt(position.z - 0.5f * _scale);

            var nodePosition = GetNodePosition(x, z);
            var sgnX = Mathf.RoundToInt(Mathf.Sign(position.x - nodePosition.x));
            var sgnZ = Mathf.RoundToInt(Mathf.Sign(position.z - nodePosition.z));
            var i = 0;
            while ((GetNodePosition(x + sgnX * i, z + sgnZ * i) - position).magnitude < distanceTolerance)
            {
                if (TrySetNode(x + sgnX * i, z, out node) ||
                    TrySetNode(x, z + sgnZ * i, out node) ||
                    TrySetNode(x - sgnX * i, z, out node) ||
                    TrySetNode(x, z - sgnZ * i, out node) ||
                    TrySetNode(x + sgnX * i, z + sgnZ * i, out node) ||
                    TrySetNode(x + sgnX * i, z - sgnZ * i, out node) ||
                    TrySetNode(x - sgnX * i, z + sgnZ * i, out node) ||
                    TrySetNode(x - sgnX * i, z - sgnZ * i, out node))
                {
                    return true;
                }

                i++;
            }

            node = null;
            return false;
        }

        private Vector3 GetNodePosition(int x, int z)
        {
            return new Vector3(x + (_scale + 1) * 0.5f, 0, z + (_scale + 1) * 0.5f);
        }

        private bool TrySetNode(int x, int z, out INode node)
        {
            if (x >= 0 && x < Grid.GetLength(1) &&
                z >= 0 && z < Grid.GetLength(0) &&
                Grid[z, x] != null
            )
            {
                node = Grid[z, x];
                return true;
            }

            node = null;
            return false;
        }
    }
}
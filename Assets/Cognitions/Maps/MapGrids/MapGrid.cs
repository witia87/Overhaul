using System;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using Assets.Cognitions.Maps.Paths;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    public class MapGrid : IMapGrid
    {
        private readonly int _scale;

        public MapGrid(INode[,] grid, int scale, int pathFindingStepsLimit)
        {
            Grid = grid;
            _scale = scale;
            PathFinder = new PathFinder(this, pathFindingStepsLimit);
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
                    if (Grid[z, x] == null)
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

        private Vector3 GetNodePosition(int x, int z)
        {
            return new Vector3(x + (_scale + 1) * 0.5f, 0, z + (_scale + 1) * 0.5f);
        }

        private bool TryGetNode(int x, int z, out INode node)
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
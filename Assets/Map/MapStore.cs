using System;
using System.Collections.Generic;
using Assets.Map.Nodes;
using UnityEngine;

namespace Assets.Map
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        public float MapWidth
        {
            get { return Grid[0].GetLength(1)*GridUnitSize; }
        }

        public float MapLength
        {
            get { return Grid[0].GetLength(0)*GridUnitSize; }
        }

        public float GridUnitSize { get; private set; }
        public List<INode[,]> Grid { get; private set; }


        public bool TryGetNode(Vector3 position, int scale, out INode node)
        {
            return TryGetNode(position, scale, scale, out node);
        }

        public bool TryGetNode(Vector3 position, int scale, int tolerance, out INode node)
        {
            var gridX = Mathf.FloorToInt(position.x);
            var gridZ = Mathf.FloorToInt(position.z);

            var minDistance = float.MaxValue;
            node = null;
            for (var _z = Mathf.Max(0, gridZ - tolerance);
                _z <= Mathf.Min(Grid[scale].GetLength(0) - 1, gridZ + tolerance);
                _z++)
            {
                for (var _x = Mathf.Max(0, gridX - tolerance);
                    _x <= Mathf.Min(Grid[scale].GetLength(1) - 1, gridX + tolerance);
                    _x++)
                {
                    if (Grid[scale][_z, _x] != null && (Grid[scale][_z, _x].Position - position).magnitude < minDistance)
                    {
                        node = Grid[scale][_z, _x];
                        minDistance = (Grid[scale][_z, _x].Position - position).magnitude;
                    }
                }
            }
            return node != null;
        }

        public bool IsRectangleClear(INode cornerA, INode cornerB)
        {
            var xMin = Math.Min(cornerA.x, cornerB.x);
            var xMax = Math.Max(cornerA.x, cornerB.x);
            var zMin = Math.Min(cornerA.z, cornerB.z);
            var zMax = Math.Max(cornerA.z, cornerB.z);

            for (var z = zMin; z <= zMax; z++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    if (Grid[cornerA.scale][z, x] == null || Grid[cornerA.scale][z, x].IsOccupied)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void InitializeMap(Vector3[,] grid, float unitSize)
        {
            Grid = new List<INode[,]>();
            Grid.Add(new INode[grid.GetLength(0), grid.GetLength(1)]);
            for (var z = 0; z < Grid[0].GetLength(0); z++)
            {
                for (var x = 0; x < Grid[0].GetLength(1); x++)
                {
                    if (grid[z, x] != Vector3.zero)
                    {
                        Grid[0][z, x] = new Node(Grid[0], x, z, 0, grid[z, x]);
                    }
                    else
                    {
                        Grid[0][z, x] = null;
                    }
                }
            }

            for (var scale = 1; scale < 6; scale++)
            {
                Grid.Add(new INode[Grid[0].GetLength(0) - scale, Grid[0].GetLength(0) - scale]);
                for (var z = 0; z < Grid[scale].GetLength(0); z++)
                {
                    for (var x = 0; x < Grid[scale].GetLength(1); x++)
                    {
                        Grid[scale][z, x] = new Node(Grid[scale], x, z, scale,
                            new Vector3(x + 0.5f + scale*0.5f, 0, z + 0.5f + scale*0.5f));
                    }
                }
            }

            for (var z = 0; z < Grid[0].GetLength(0); z++)
            {
                for (var x = 0; x < Grid[0].GetLength(1); x++)
                {
                    for (var scale = 1; scale < 6; scale++)
                    {
                        if (Grid[0][z, x] == null)
                        {
                            for (var _z = Mathf.Max(0, z - scale);
                                _z <= Mathf.Min(Grid[scale].GetLength(0) - 1, z);
                                _z++)
                            {
                                for (var _x = Mathf.Max(0, x - scale);
                                    _x <= Mathf.Min(Grid[scale].GetLength(1) - 1, x);
                                    _x++)
                                {
                                    Grid[scale][_z, _x] = null;
                                }
                            }
                        }
                    }
                }
            }

            GridUnitSize = unitSize;
        }
    }
}
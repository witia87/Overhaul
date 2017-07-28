using Assets.Maps.Nodes;
using UnityEngine;

namespace Assets.Maps
{
    public class MapInitializer
    {
        private readonly int _gridLength;

        private readonly int _gridWidth;
        private LayerMask _layerMask;


        public MapInitializer(int gridWidth, int gridLength, LayerMask layerMask)
        {
            _gridLength = gridLength;
            _gridWidth = gridWidth;
            _layerMask = layerMask;
        }

        public BaseNode[,] InitializeBaseGrid()
        {
            // Scan
            var grid = new BaseNode[_gridLength, _gridWidth];
            for (var z = 0; z < _gridLength; z++)
            {
                for (var x = 0; x < _gridWidth; x++)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(
                        new Vector3(x + 0.5f, 10, z + 0.5f),
                        Vector3.down, out hit, 20, _layerMask))
                    {
                        if (hit.transform.tag == "Floor")
                        {
                            grid[z, x] = new BaseNode(grid, x, z, hit.point);
                        }
                        else
                        {
                            grid[z, x] = null;
                        }
                    }
                }
            }
            return grid;
        }

        public HigherScaleNode[,] InitializeHigherScaleGrid(BaseNode[,] baseGrid, int scale)
        {
            var grid = new HigherScaleNode[baseGrid.GetLength(0) - scale, baseGrid.GetLength(0) - scale];
            for (var z = 0; z < grid.GetLength(0); z++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    grid[z, x] = GetHigherScaleNode(baseGrid, grid, scale, x, z);
                }
            }

            return grid;
        }

        private HigherScaleNode GetHigherScaleNode(BaseNode[,] baseGrid, INode[,] grid, int scale, int x, int z)
        {
            for (var _z = z;
                _z <= Mathf.Min(baseGrid.GetLength(0) - 1, z + scale);
                _z++)
            {
                for (var _x = x;
                    _x <= Mathf.Min(baseGrid.GetLength(1) - 1, x + scale);
                    _x++)
                {
                    if (baseGrid[_z, _x] == null)
                    {
                        return null;
                    }
                }
            }

            return new HigherScaleNode(baseGrid, grid, x, z, scale,
                new Vector3(x + 0.5f + scale * 0.5f, 0, z + 0.5f + scale * 0.5f));
        }
    }
}
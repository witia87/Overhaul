using Assets.Cognitions.Maps.Dangers;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using Assets.Environment;
using Assets.Environment.Guns.Bullets;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    public class FractionGrids
    {
        public readonly BaseNode[,] BaseGrid;
        public readonly HigherScaleNode[][,] HigherScaleGrids = new HigherScaleNode[10][,];

        private DangerStore _dangerStore;

        public FractionGrids(BaseNode[,] baseGrid, BulletsFactory[] bulletFactories, FractionId fractionId)
        {
            BaseGrid = baseGrid;
            _dangerStore = new DangerStore(BaseGrid, bulletFactories, fractionId);
        }

        public HigherScaleNode[,] InitializeHigherScaleGrid(BaseNode[,] baseGrid, int scale)
        {
            var grid = new HigherScaleNode[baseGrid.GetLength(0) - scale, baseGrid.GetLength(0) - scale];
            for (var z = 0; z < grid.GetLength(0); z++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    grid[z, x] = GetHigherScaleNode(grid, scale, x, z);
                }
            }

            return grid;
        }

        private HigherScaleNode GetHigherScaleNode(INode[,] grid, int scale, int x,
            int z)
        {
            for (var _z = z;
                _z <= Mathf.Min(BaseGrid.GetLength(0) - 1, z + scale);
                _z++)
            {
                for (var _x = x;
                    _x <= Mathf.Min(BaseGrid.GetLength(1) - 1, x + scale);
                    _x++)
                {
                    if (BaseGrid[_z, _x] == null)
                    {
                        return null;
                    }
                }
            }

            return new HigherScaleNode(BaseGrid, grid, x, z, scale,
                new Vector3(x + 0.5f + scale * 0.5f, 0, z + 0.5f + scale * 0.5f));
        }

        public MapGrid GetMapGrid(int scale)
        {
            if (scale == 0)
            {
                return new MapGrid(BaseGrid, scale, 50);
            }
            else
            {
                if (HigherScaleGrids[scale] == null)
                {
                    HigherScaleGrids[scale] = InitializeHigherScaleGrid(BaseGrid, scale);
                }
                return new MapGrid(HigherScaleGrids[scale], scale, 50);
            }
        }
    }
}
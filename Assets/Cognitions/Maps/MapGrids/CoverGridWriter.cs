using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Covers;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids
{
    public class CoverGridWriter
    {
        private readonly BaseNode[,] _baseGrid;

        public CoverGridWriter(BaseNode[,] baseGrid)
        {
            _baseGrid = baseGrid;
        }

        public void Write(IEnumerable<ICover> covers)
        {
            foreach (var cover in covers) WriteCover(cover);
        }

        public void WriteCover(ICover cover)
        {
            var length = _baseGrid.GetLength(0);
            var width = _baseGrid.GetLength(1);
            var leftX = Mathf.RoundToInt(cover.Position.x - cover.Width / 2);
            var bottomZ = Mathf.RoundToInt(cover.Position.z - cover.Width / 2);
            var rightX = Mathf.RoundToInt(cover.Position.x + cover.Width / 2);
            var topZ = Mathf.RoundToInt(cover.Position.z + cover.Width / 2);

            for (var i = Mathf.Max(0, leftX); i < Mathf.Min(rightX, length); i++)
            {
                if (bottomZ - 1 >= 0 && _baseGrid[bottomZ - 1, i] != null)
                    _baseGrid[bottomZ - 1, i].SetCoverage(Vector3.forward);

                if (bottomZ - 2 >= 0 && _baseGrid[bottomZ - 2, i] != null)
                    _baseGrid[bottomZ - 2, i].SetCoverage(Vector3.forward);

                if (topZ < length && _baseGrid[topZ, i] != null) _baseGrid[topZ, i].SetCoverage(Vector3.back);

                if (topZ + 1 < length && _baseGrid[topZ + 1, i] != null)
                    _baseGrid[topZ + 1, i].SetCoverage(Vector3.back);
            }

            for (var i = Mathf.Max(0, bottomZ); i < Mathf.Min(width, topZ); i++)
            {
                if (leftX - 1 >= 0 && _baseGrid[i, leftX - 1] != null)
                    _baseGrid[i, leftX - 1].SetCoverage(Vector3.right);

                if (leftX - 2 >= 0 && _baseGrid[i, leftX - 2] != null)
                    _baseGrid[i, leftX - 2].SetCoverage(Vector3.right);

                if (rightX < width && _baseGrid[i, rightX] != null) _baseGrid[i, rightX].SetCoverage(Vector3.left);

                if (rightX + 1 < width && _baseGrid[i, rightX + 1] != null)
                    _baseGrid[i, rightX + 1].SetCoverage(Vector3.left);
            }
        }
    }
}
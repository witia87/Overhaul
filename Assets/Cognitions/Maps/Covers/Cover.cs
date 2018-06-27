using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.Covers
{
    public class CoverInitializer : MonoBehaviour
    {
        public void AppendCovers(CoverInfo[,] coverGrid)
        {
            var length = coverGrid.GetLength(0);
            var width = coverGrid.GetLength(1);
            var leftX = Mathf.RoundToInt(transform.position.x - transform.localScale.x / 2);
            var bottomZ = Mathf.RoundToInt(transform.position.z - transform.localScale.z / 2);
            var rightX = Mathf.RoundToInt(transform.position.x + transform.localScale.x / 2);
            var topZ = Mathf.RoundToInt(transform.position.z + transform.localScale.z / 2);

            for (var i = Mathf.Max(0, leftX); i < Mathf.Min(rightX, length); i++)
            {
                if (bottomZ - 1 >= 0 && coverGrid[bottomZ - 1, i] != null)
                {
                    coverGrid[bottomZ - 1, i].SetCoverDirection(Vector3.forward);
                }

                if (bottomZ - 2 >= 0 && coverGrid[bottomZ - 2, i] != null)
                {
                    coverGrid[bottomZ - 2, i].SetCoverDirection(Vector3.forward);
                }

                if (topZ < length && coverGrid[topZ, i] != null)
                {
                    coverGrid[topZ, i].SetCoverDirection(Vector3.back);
                }

                if (topZ + 1 < length && coverGrid[topZ + 1, i] != null)
                {
                    coverGrid[topZ + 1, i].SetCoverDirection(Vector3.back);
                }
            }

            for (var i = Mathf.Max(0, bottomZ); i < Mathf.Min(width, topZ); i++)
            {
                if (leftX - 1 >= 0 && coverGrid[i, leftX - 1] != null)
                {
                    coverGrid[i, leftX - 1].SetCoverDirection(Vector3.right);
                }

                if (leftX - 2 >= 0 && coverGrid[i, leftX - 2] != null)
                {
                    coverGrid[i, leftX - 2].SetCoverDirection(Vector3.right);
                }

                if (rightX < width && coverGrid[i, rightX] != null)
                {
                    coverGrid[i, rightX].SetCoverDirection(Vector3.left);
                }

                if (rightX + 1 < width && coverGrid[i, rightX + 1] != null)
                {
                    coverGrid[i, rightX + 1].SetCoverDirection(Vector3.left);
                }
            }

            Destroy(this);
        }
    }
}
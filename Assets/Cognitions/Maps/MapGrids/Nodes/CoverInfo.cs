using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids.Nodes
{
    public class CoverInfo: ICoverInfo
    {
        bool[,] _isCovered = new bool[3,3];

        public void SetCoverDirection(Vector3 fromDirection)
        {
            var x = Mathf.RoundToInt(fromDirection.x);
            var z = Mathf.RoundToInt(fromDirection.z);
            _isCovered[1 + z, 1 + x] = true;
        }

        public bool IsCovered(Vector3 fromDirection)
        {
            var x = Mathf.RoundToInt(fromDirection.x);
            var z = Mathf.RoundToInt(fromDirection.z);
            return _isCovered[1 + z, 1 + x];
        }
    }
}
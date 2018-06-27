using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids.Nodes
{
    public interface ICoverInfo
    {
        bool IsCovered(Vector3 fromDirection);
    }
}
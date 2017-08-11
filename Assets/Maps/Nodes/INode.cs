using System.Collections.Generic;
using UnityEngine;

namespace Assets.Maps.Nodes
{
    public interface INode
    {
        int x { get; }
        int z { get; }
        int Scale { get; }
        Vector3 Position { get; }
        bool IsOccupied { get; }
        bool IsDangerous { get; }
        IEnumerator<INode> GetDirectedNeighborsEnumerator(Vector3 direction);
        IEnumerator<INode> GetSimpleNeighborsEnumerator();
        IEnumerator<INode> GetRandomizedNeighborsEnumerator();
    }
}
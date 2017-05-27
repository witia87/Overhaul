using System.Collections.Generic;
using UnityEngine;

namespace Assets.Map.Nodes
{
    public interface INode
    {
        int x { get; }
        int z { get; }
        int scale { get; }
        Vector3 Position { get; }
        float OccpupyingMass { get; }
        bool IsOccupied { get; }
        IEnumerator<INode> GetDirectedNeighborsEnumerator(Vector3 direction);
        IEnumerator<INode> GetSimpleNeighborsEnumerator();
        IEnumerator<INode> GetRandomizedNeighborsEnumerator();
    }
}
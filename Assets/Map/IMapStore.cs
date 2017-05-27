using System.Collections.Generic;
using Assets.Map.Nodes;
using UnityEngine;

namespace Assets.Map
{
    public interface IMapStore
    {
        float MapWidth { get; }
        float MapLength { get; }
        float GridUnitSize { get; }

        List<INode[,]> Grid { get; }
        bool TryGetNode(Vector3 position, int scale, out INode node);
        bool TryGetNode(Vector3 position, int scale, int tolerance, out INode node);

        bool IsRectangleClear(INode cornerA, INode cornerB);
    }
}
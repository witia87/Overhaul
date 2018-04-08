using Assets.Cognitions.Maps.PathFinders;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public interface IMap
    {
        float MapWidth { get; }
        float MapLength { get; }
        float BaseGridUnitSize { get; }

        IPathFinder PathFinder { get; }

        bool IsRectangleClear(Vector3 cornerA, Vector3 cornerB);

        bool IsPositionDangorous(Vector3 position);
        bool ArePositionsOnTheSameTile(Vector3 a, Vector3 b);
    }
}
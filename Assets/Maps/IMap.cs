using Assets.Maps.PathFinders;
using UnityEngine;

namespace Assets.Maps
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
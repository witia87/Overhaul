using Assets.Cognitions.Maps.MapGraphs.Rooms;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids.Nodes
{
    public interface INode
    {
        int x { get; }
        int z { get; }
        Vector3 Position { get; }
        bool IsDangerous { get; }
        bool IsCovered(Vector3 direction);
        IRoom Room { get; }
    }
}
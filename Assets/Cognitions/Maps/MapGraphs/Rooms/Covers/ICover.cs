using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Covers
{
    public interface ICover
    {
        Vector3 Position { get; }
        float Width { get; }
        float Length { get; }
    }
}
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public interface IRoom
    {
        Vector3 Position { get; }
        float Width { get; }
        float Length { get; }

        Vector3[] EntrancePositions { get; }
        Vector3[] CoverPositions { get; }

        IRoom GetNeighboringRoomInDirection(Vector3 direction);
    }
}
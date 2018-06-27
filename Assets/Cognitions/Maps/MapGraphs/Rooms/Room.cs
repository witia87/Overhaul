using Assets.Cognitions.Maps.Covers;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    class Room : IRoom
    {
        public Vector3 Position { get; private set; }
        public float Width { get; private set; }
        public float Length { get; private set; }
        public Vector3[] EntrancePositions { get; private set; }
        public Vector3[] CoverPositions { get; private set; }
        public IRoom GetNeighboringRoomInDirection(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
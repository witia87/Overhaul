using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Covers;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public class Room : IRoom
    {
        public Room(Vector3 position, float width, float length)
        {
            Position = position;
            Width = width;
            Length = length;
        }

        public IEnumerable<ICover> Covers { get; set; }
        public IEnumerable<IRoomEntrance> RoomEntrances { get; set; }
        public float Width { get; private set; }
        public float Length { get; private set; }

        public Vector3 Position { get; private set; }
    }
}
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances
{
    public class RoomEntrance : IRoomEntrance
    {
        public Vector3 Position { get; private set; }
        private readonly IRoom _parentRoom;
        public IRoomEntrance ConnectedEntrance { get; set; }

        public RoomEntrance(Vector3 position, IRoom parentRoom)
        {
            Position = position;
            _parentRoom = parentRoom;
        }

        public IRoom ParentRoom
        {
            get { return _parentRoom; }
        }
    }
}
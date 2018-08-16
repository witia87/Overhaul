using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances
{
    public class RoomEntranceInitializer : MonoBehaviour
    {
        [SerializeField] private RoomEntranceInitializer _connectedRoomEntrance;

        private bool _isEntranceInitialized;
        private RoomEntrance _roomEntrance;

        public RoomEntrance Initialize(IRoom room)
        {
            _roomEntrance = new RoomEntrance(transform.position, room);
            _isEntranceInitialized = true;
            if (_connectedRoomEntrance._isEntranceInitialized)
            {
                _connectedRoomEntrance._roomEntrance.ConnectedEntrance = _roomEntrance;
                _roomEntrance.ConnectedEntrance = _roomEntrance;
            }
            return _roomEntrance;
        }

        public void OnDrawGizmos()
        {
            var offset = new Vector3(0.2f, 0, 0.2f);
            var offset2 = new Vector3(-0.2f, 0, 0.2f);

            DrawArrow.ForDebug(transform.position - offset,
                2 * offset, Color.green, 0.1f, 0);
            DrawArrow.ForDebug(transform.position - offset2,
                2 * offset2, Color.green, 0.1f, 0);
        }
    }
}
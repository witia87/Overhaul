using System;
using System.Linq;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Covers;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public class RoomInitializer : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _width;

        public Room Initialize()
        {
            var room = new Room(transform.position, _width, _height);

            var entranceInitializers = GetComponentsInChildren<RoomEntranceInitializer>();
            var entrances = entranceInitializers.Select(initializer => (IRoomEntrance) initializer.Initialize(room));
            room.RoomEntrances = entrances;

            var coverInitializers = GetComponentsInChildren<CoverInitializer>();
            var covers = coverInitializers.Select(initializer => (ICover) initializer.Initialize());
            room.Covers = covers;

            return room;
        }


        public void OnDrawGizmos()
        {
            /*var offset = new Vector3(0.2f, 0, 0.2f);
            var offset2 = new Vector3(-0.2f, 0, 0.2f);
            for (var z = 0; z < _baseGrid.GetLength(0); z++)
            {
                for (var x = 0; x < _baseGrid.GetLength(1); x++)
                {
                    if (_baseGrid[z, x] != null)
                    {
                        if (_baseGrid[z, x].IsDangerous)
                        {
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.red, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.red, 0.1f, 0);
                        }
                        else if(_baseGrid[z, x].IsCovered(Vector3.right) ||
                                _baseGrid[z, x].IsCovered(Vector3.left) ||
                                _baseGrid[z, x].IsCovered(Vector3.forward)||
                                _baseGrid[z, x].IsCovered(Vector3.back))
                        {
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.green, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.green, 0.1f, 0);
                        }
                    }
                }
            }*/
        }
    }
}
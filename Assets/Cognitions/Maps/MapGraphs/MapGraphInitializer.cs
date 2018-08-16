using System.Linq;
using Assets.Cognitions.Maps.MapGraphs.Rooms;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs
{
    public class MapGraphInitializer : MonoBehaviour
    {
        public IMapGraph Initialize()
        {
            var roomInitializers = FindObjectsOfType<RoomInitializer>();
            var rooms = roomInitializers.Select(initializer => (IRoom) initializer.Initialize());

            return new MapGraph(rooms);
        }
    }
}
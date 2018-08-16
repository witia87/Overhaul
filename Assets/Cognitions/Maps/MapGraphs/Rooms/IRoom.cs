using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Covers;
using Assets.Cognitions.Maps.MapGraphs.Rooms.Entrances;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public interface IRoom
    {
        Vector3 Position { get; }

        IEnumerable<IRoomEntrance> RoomEntrances { get; }
        IEnumerable<ICover> Covers { get; }

        float Width { get; }
        float Length { get; }
    }
}
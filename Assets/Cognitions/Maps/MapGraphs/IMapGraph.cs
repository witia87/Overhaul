using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGraphs.Rooms;

namespace Assets.Cognitions.Maps.MapGraphs
{
    public interface IMapGraph
    {
        IEnumerable<IRoom> Rooms { get; }
    }
}
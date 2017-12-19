using Assets.Maps.Dangers;
using Assets.Units;

namespace Assets.Maps
{
    public interface IMapStore
    {
        IDangerStore Dangers { get; }
        IMap GetMap(int scale, FractionId fractionId);
    }
}
using Assets.Maps.Dangers;

namespace Assets.Maps
{
    public interface IMapStore
    {
        IDangerStore Dangers { get; }
        IMap GetMap(int scale, FractionId fractionId);
    }
}
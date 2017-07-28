using Assets.Maps.Dangers;

namespace Assets.Maps
{
    public interface IMapStore
    {
        IMap GetMap(int scale, FractionId fractionId);
        IDangerStore Dangers { get; }
    }
}
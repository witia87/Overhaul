using Assets.Cognitions.Maps.Dangers;
using Assets.Modules;

namespace Assets.Cognitions.Maps
{
    public interface IMapStore
    {
        IMap GetMap(int scale, FractionId fractionId);
    }
}
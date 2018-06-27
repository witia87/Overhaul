using Assets.Cognitions.Maps.MapGrids;
using Assets.Environment;
using Assets.Environment.Units;

namespace Assets.Cognitions.Maps
{
    public interface IMapStore
    {
        IMap GetMap(IUnit unit);
    }
}
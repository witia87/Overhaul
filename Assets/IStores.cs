using Assets.Map;

namespace Assets
{
    public interface IStores
    {
        IMapStore MapStore { get; }
    }
}
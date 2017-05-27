using Assets.Camera;
using Assets.Map;

namespace Assets
{
    public interface IStores
    {
        IMapStore MapStore { get; }
        ICameraStore CameraStore { get; }
    }
}
using Assets.Map;
using Assets.Presentation.Camera;

namespace Assets
{
    public interface IStores
    {
        IMapStore MapStore { get; }
        ICameraStore CameraStore { get; }
    }
}
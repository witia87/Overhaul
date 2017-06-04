using Assets.Flux;
using Assets.Map;
using Assets.Presentation.Camera;

namespace Assets
{
    public class Stores : IStores
    {
        public Stores(Dispatcher dispatcher)
        {
            MapStore = new MapStore(dispatcher);
            CameraStore = new CameraStore(dispatcher);
        }

        public IMapStore MapStore { get; private set; }
        public ICameraStore CameraStore { get; private set; }
    }
}
using Assets.Flux;
using Assets.Map;

namespace Assets
{
    public class Stores : IStores
    {
        public Stores(Dispatcher dispatcher)
        {
            MapStore = new MapStore(dispatcher);
        }

        public IMapStore MapStore { get; private set; }
    }
}
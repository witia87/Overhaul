namespace Assets.Flux.Stores
{
    public class Store
    {
        protected Dispatcher Dispatcher;

        public Store(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
    }
}
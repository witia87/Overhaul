namespace Assets.Flux
{
    public delegate void DispatchCallback(IPayload payload);

    public interface IDispatcher
    {
        void Dispatch(Commands commandId, IPayload payload);
    }
}
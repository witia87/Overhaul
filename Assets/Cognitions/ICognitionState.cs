namespace Assets.Cognitions
{
    public interface ICognitionState<TStateIds>
    {
        TStateIds Id { get; }

        bool IsDisposed { get; }

        ICognitionState<TStateIds> Update();
        void OnDrawGizmos();
    }
}
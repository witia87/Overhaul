namespace Assets.Cognitions
{
    public interface ICognitionState<TStateIds>
    {
        TStateIds Id { get; }
        ICognitionState<TStateIds> Update();

        void OnDrawGizmos();
    }
}
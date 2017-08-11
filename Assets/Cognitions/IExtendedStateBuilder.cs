namespace Assets.Cognitions
{
    public interface IExtendedStateBuilder<TStateIds> : IStateBuilder<TStateIds>
    {
        CognitionState<TStateIds> AndReturnToThePreviousState();
    }
}
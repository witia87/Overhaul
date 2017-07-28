namespace Assets.Cognitions
{
    public interface IStateBuilder<TStateIds>
    {
        CognitionState<TStateIds> AndChangeStateTo(CognitionState<TStateIds> nextState);
    }
}
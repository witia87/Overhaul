namespace Assets.Flux.Automata
{
    public interface IState<TStateIds, in TTransitionIds>
    {
        IState<TStateIds, TTransitionIds> Transition(TTransitionIds transitionId, IPayload payload);
    }
}
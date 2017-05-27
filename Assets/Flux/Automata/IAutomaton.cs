namespace Assets.Flux.Automata
{
    public interface IAutomaton<TStateIds, in TTransitionIds>
    {
        void Transition(TTransitionIds transitionId, IPayload payload);
    }
}
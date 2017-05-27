namespace Assets.Flux.Automata
{
    public interface IAutomatonBuilder<TStateIds, in TTransitionIds>
    {
        IStateBuilder<TStateIds, TTransitionIds> AddState(TStateIds stateId);
        IAutomaton<TStateIds, TTransitionIds> Deploy();
    }
}
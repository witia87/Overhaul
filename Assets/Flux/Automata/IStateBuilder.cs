namespace Assets.Flux.Automata
{
    public delegate TStateIds AutomatonCallback<out TStateIds>(IPayload payload);

    public interface IStateBuilder<TStateIds, in TTransitionIds>
    {
        IStateBuilder<TStateIds, TTransitionIds> AddTransition(TTransitionIds transitionId,
            AutomatonCallback<TStateIds> callback);

        IAutomatonBuilder<TStateIds, TTransitionIds> Deploy();
    }
}
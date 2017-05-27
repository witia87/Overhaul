using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Flux.Automata
{
    public class State<TStateIds, TTransitionIds> : IStateBuilder<TStateIds, TTransitionIds>
    {
        private readonly Dictionary<TTransitionIds, AutomatonCallback<TStateIds>> _callbacks =
            new Dictionary<TTransitionIds, AutomatonCallback<TStateIds>>();

        private readonly Automaton<TStateIds, TTransitionIds> _parrent;

        private readonly Collection<TTransitionIds> _registeredTransitions = new Collection<TTransitionIds>();

        public State(Automaton<TStateIds, TTransitionIds> parrent, Collection<TTransitionIds> registredTransitions)
        {
            _parrent = parrent;
            _registeredTransitions = registredTransitions;
        }

        public virtual IStateBuilder<TStateIds, TTransitionIds> AddTransition(TTransitionIds transitionId,
            AutomatonCallback<TStateIds> callback)
        {
            if (_callbacks.ContainsKey(transitionId))
                throw new AutomatonException("Transition " + transitionId + " has already been registered.");
            _callbacks[transitionId] = callback;
            return this;
        }

        public virtual IAutomatonBuilder<TStateIds, TTransitionIds> Deploy()
        {
            return _parrent;
        }

        public virtual TStateIds Transition(TTransitionIds transitionId, IPayload payload)
        {
            if (!_callbacks.ContainsKey(transitionId))
                throw new AutomatonException("Transition " + transitionId + " has not been registered.");
            return _callbacks[transitionId](payload);
        }
    }
}
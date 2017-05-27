using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Flux.Automata
{
    public class Automaton<TStateIds, TTransitionIds> : IAutomaton<TStateIds, TTransitionIds>,
        IAutomatonBuilder<TStateIds, TTransitionIds>
    {
        protected readonly Dictionary<TStateIds, State<TStateIds, TTransitionIds>> _states
            = new Dictionary<TStateIds, State<TStateIds, TTransitionIds>>();

        protected readonly Collection<TTransitionIds> RegisteredTransitions = new Collection<TTransitionIds>();

        protected TStateIds CurrentStateId;

        public virtual void Transition(TTransitionIds transitionId, IPayload payload)
        {
            CurrentStateId = _states[CurrentStateId].Transition(transitionId, payload);
            if (!RegisteredTransitions.Contains(transitionId))
            {
                RegisteredTransitions.Add(transitionId);
            }
        }

        public virtual IStateBuilder<TStateIds, TTransitionIds> AddState(TStateIds stateId)
        {
            if (_states.ContainsKey(stateId))
                throw new AutomatonException("State " + stateId + "has already been added.");
            _states[stateId] = new State<TStateIds, TTransitionIds>(this, RegisteredTransitions);
            return _states[stateId];
        }

        public virtual IAutomaton<TStateIds, TTransitionIds> Deploy()
        {
            return this;
        }
    }
}
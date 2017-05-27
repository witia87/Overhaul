using System.Collections.Generic;

namespace Assets.Modules
{
    public abstract class AutomatedModule<TStateIds> : Module
    {
        protected Dictionary<TStateIds, IModuleState<TStateIds>> _states
            = new Dictionary<TStateIds, IModuleState<TStateIds>>();

        public TStateIds CurrentStateId;

        protected void AddState(TStateIds stateId, IModuleState<TStateIds> state)
        {
            _states.Add(stateId, state);
        }


        protected void Update()
        {
            CurrentStateId = _states[CurrentStateId].Update();
        }

        protected void FixedUpdate()
        {
            _states[CurrentStateId].FixedUpdate();
        }
    }
}
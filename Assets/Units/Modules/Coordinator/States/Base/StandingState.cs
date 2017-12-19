using System;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States.Base
{
    public abstract class GroundedState : UnitState
    {
        protected GroundedState(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection) :
            base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
        }
        
        public Vector3 GetModuleLogicDirection(Vector3 moduleForward)
        {
            if (Mathf.Abs(moduleForward.y) > 0.999f) // TODO: Verify whether this condition might be removed.
            {
                throw new ApplicationException(
                    "Cannot calculate ModuleLogicDirection when module lays horizontaly.");
            }
            return GetLogicVector(moduleForward);
        }
    }
}
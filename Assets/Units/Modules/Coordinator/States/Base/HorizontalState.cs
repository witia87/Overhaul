using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States.Base
{
    public abstract class HorizontalState : UnitState
    {
        protected HorizontalState(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection) :
            base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
            GlobalLookDirection = initialGlobalLookDirection;
        }

        protected Vector3 GetLogicVector(Vector3 vector)
        {
            vector.y = 0;
            return vector.normalized;
        }
    }
}
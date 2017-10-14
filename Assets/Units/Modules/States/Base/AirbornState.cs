using UnityEngine;

namespace Assets.Units.Modules.States.Base
{
    public abstract class AirbornState : UnitState
    {
        protected Vector3 FlightLogicDirection;
        protected Vector3 GlobalLookDirection;

        protected AirbornState(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory,
            Vector3 globalLookDirection, Vector3 flightLogicDirection) :
            base(movement, targeting, statesFactory)
        {
            FlightLogicDirection = flightLogicDirection;
            GlobalLookDirection = globalLookDirection;
        }

        public override UnitState LookTowards(Vector3 globalDirection)
        {
            GlobalLookDirection = globalDirection;
            return this;
        }
    }
}
using Assets.Units.Modules.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.States
{
    public class Jumping : AirbornState
    {
        private float _jumpForceModifier;

        public Jumping(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection, Vector3 flightLogicDirection, float jumpForceModifier) :
            base(movement, targeting, statesFactory, initialGlobalLookDirection, flightLogicDirection)
        {
            _jumpForceModifier = jumpForceModifier;
        }

        public override UnitState VerifyPhysicConditions()
        {
            return this;
        }

        public override UnitState FixedUpdate()
        {
            var movementForceModifier = Vector3.Dot(Targeting.Rigidbody.velocity, FlightLogicDirection);
            Movement.AddJumpVelocity(FlightLogicDirection + Movement.transform.up, movementForceModifier);
            var targetingForceModifier = Vector3.Dot(Targeting.Rigidbody.velocity, FlightLogicDirection);
            Targeting.AddJumpVelocity(FlightLogicDirection + Targeting.transform.up, targetingForceModifier);
            return StatesFactory.CreateGliding(GlobalLookDirection, FlightLogicDirection);
        }
    }
}
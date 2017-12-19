using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class LayingBack : HorizontalState
    {
        private bool _wasHoldSet;

        public LayingBack(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection) : base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
            GlobalLookDirection = initialGlobalLookDirection;
        }

        public override UnitState Move(Vector3 moveLogicDirection, float speedModifier)
        {
            var torsoLogicDirection = GetLogicVector(Torso.transform.up);
            if (Vector3.Angle(torsoLogicDirection, GlobalLookDirection) < 45)
            {
                return StatesFactory.CreateCrawling(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState StopMoving()
        {
            return StatesFactory.CreateStandingUp(GlobalLookDirection);
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (Legs.IsStanding)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            if (!Legs.IsGrounded && !Torso.IsGrounded)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards((GlobalLookDirection + Vector3.up).normalized);
            Torso.FlipTowards((GlobalLookDirection + Vector3.up).normalized);
            Torso.Rigidbody.AddForce(-Physics.gravity / 2, ForceMode.Acceleration);
            Legs.TurnTowards(Vector3.up);
            Legs.FlipTowards(Vector3.up);

            var movementLogicDirection = GetLogicVector(Torso.transform.up);
            Legs.AlignWith(movementLogicDirection);

            return this;
        }
    }
}
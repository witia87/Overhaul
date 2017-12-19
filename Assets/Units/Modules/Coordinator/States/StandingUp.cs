using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class StandingUp : GroundedState
    {
        public StandingUp(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection)
            : base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (Legs.IsStanding)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState LookTowards(Vector3 globalDirection)
        {
            GlobalLookDirection = globalDirection;
            return this;
        }

        public override UnitState Move(Vector3 moveLogicDirection, float speedModifier)
        {
            return StatesFactory.CreateMovingForward(GlobalLookDirection, moveLogicDirection, speedModifier);
        }

        public override UnitState FixedUpdate()
        {
            Torso.
            Legs.Crouch();
            Legs.StraightenUp(0.2f);
            Torso.StraightenUp(0.2f);
            return this;
        }
    }
}
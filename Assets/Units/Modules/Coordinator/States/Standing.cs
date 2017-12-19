using Assets.Units.Helpers;
using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class Standing : GroundedState
    {
        public Standing(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection)
            : base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (!Legs.IsStanding)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection);
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

        protected void ManageReachingLegsTurnDirection(Vector3 logicLookDirection,
            Vector3 movementTurnLogicDirection)
        {
            var movementLogicDirection = GetModuleLogicDirection(Legs.transform.forward);
            var targetingLogicDirection = GetModuleLogicDirection(Torso.transform.forward);
            var angle = AngleCalculator.CalculateLogicAngle(movementLogicDirection,
                movementTurnLogicDirection);
            if (Mathf.Abs(angle) <= 90)
            {
                Legs.TurnTowards(movementTurnLogicDirection);
            }
            else
            {
                Legs.TurnTowards(targetingLogicDirection);
            }
        }

        public override UnitState FixedUpdate()
        {
            var logicLookDirection = GetLogicVector(GlobalLookDirection);

            ManageReachingLegsTurnDirection(logicLookDirection, GlobalLookDirection);
            Torso.TurnTowards(logicLookDirection);

            Legs.StraightenUp(1);
            Torso.StraightenUp(1);
            return this;
        }
    }
}
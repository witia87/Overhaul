using Assets.Units.Helpers;
using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class Moving : Standing
    {
        protected Vector3 MoveLogicDirection;
        protected float SpeedModifier;

        public Moving(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection, Vector3 initialMoveLogicDirection, float speedModifier) :
            base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
            MoveLogicDirection = initialMoveLogicDirection;
            SpeedModifier = speedModifier;
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (!Legs.IsStanding)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState Move(Vector3 logicDirection, float speedModifier)
        {
            MoveLogicDirection = logicDirection;
            SpeedModifier = speedModifier;
            return this;
        }

        public override UnitState StopMoving()
        {
            return StatesFactory.CreateStanding(GlobalLookDirection);
        }


        public override UnitState FixedUpdate()
        {

            var parametersAngle =
                AngleCalculator.CalculateLogicAngle(MoveLogicDirection, GlobalLookDirection);
            var logicLookDirection = GetLogicVector(GlobalLookDirection);

            if (Mathf.Abs(parametersAngle) <= 120)
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                    ((GlobalLookDirection + 2 * MoveLogicDirection) / 3).normalized);
            }
            else
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                    ((GlobalLookDirection - 2 * MoveLogicDirection) / 3).normalized);
            }

            Torso.TurnTowards(logicLookDirection);
            Legs.Move(MoveLogicDirection * SpeedModifier);
            Legs.StraightenUp(1);
            Torso.Move(MoveLogicDirection * SpeedModifier);
            Torso.StraightenUp(1);
            return this;
        }
    }
}
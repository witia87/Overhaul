using Assets.Modules.Units.Bodies.Coordinator.States.Base;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Units.Bodies.Coordinator.States
{
    public class Moving : Standing
    {
        public Moving(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters)
            : base(legs, torso, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            base.FixedUpdate();
            var globalLookDirection = Parameters.LookLogicDirection;
            var moveScaledLogicDirection = Parameters.MoveScaledLogicDirection;
            var parametersAngle =
                AngleCalculator.CalculateLogicAngle(moveScaledLogicDirection, globalLookDirection);
            var logicLookDirection = GetLogicVector(globalLookDirection);

            if (Mathf.Abs(parametersAngle) <= 120)
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                    ((globalLookDirection + 2 * moveScaledLogicDirection) / 3).normalized);
            }
            else
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                    ((globalLookDirection - 2 * moveScaledLogicDirection) / 3).normalized);
            }

            Torso.TurnTowards(logicLookDirection);
            Torso.AimAt(Parameters.AimAtDirection);
            Legs.Move(moveScaledLogicDirection);
            Legs.StraightenUp(1);
            Torso.Move(moveScaledLogicDirection);
            Torso.StraightenUp(1);
            return this;
        }
    }
}
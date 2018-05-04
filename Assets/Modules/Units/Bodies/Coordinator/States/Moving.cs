using Assets.Modules.Units.Bodies.Coordinator.States.Base;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Units.Bodies.Coordinator.States
{
    public class Moving : Standing
    {
        public Moving(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
            : base(torso, legs, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            var globalLookDirection = Parameters.LookLogicDirection;
            var moveScaledLogicDirection = Parameters.MoveScaledLogicDirection;
            var parametersAngle =
                AngleCalculator.CalculateLogicAngle(moveScaledLogicDirection, globalLookDirection);
            var logicLookDirection = GetLogicVector(globalLookDirection);

            if (Mathf.Abs(parametersAngle) <= 120)
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                     moveScaledLogicDirection.normalized);
            }
            else
            {
                ManageReachingLegsTurnDirection(logicLookDirection,
                    -moveScaledLogicDirection.normalized);
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
using Assets.Environment.Units.Bodies.Coordinator.States.Base;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Environment.Units.Bodies.Coordinator.States
{
    public class Standing : UnitState
    {
        public Standing(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
            : base(torso, legs, parameters)
        {
        }

        protected void ManageReachingLegsTurnDirection(Vector3 logicLookDirection,
            Vector3 movementTurnLogicDirection)
        {
            var legsLogicDirection = GetLogicVector(Legs.transform.forward);
            var torsoLogicDirection = GetLogicVector(Torso.transform.forward);
            var angle = AngleCalculator.CalculateLogicAngle(legsLogicDirection,
                movementTurnLogicDirection);
            Legs.TurnTowards(movementTurnLogicDirection);
        }

        public override UnitState FixedUpdate()
        {
            var logicLookDirection = GetLogicVector(Parameters.LookLogicDirection);
            Torso.TurnTowards(logicLookDirection);
            Torso.AimAt(Parameters.AimAtDirection);

            ManageReachingLegsTurnDirection(logicLookDirection, logicLookDirection);

            Legs.StraightenUp();
            Torso.StraightenUp();
            return this;
        }
    }
}
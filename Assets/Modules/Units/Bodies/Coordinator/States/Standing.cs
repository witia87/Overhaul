﻿using Assets.Modules.Units.Bodies.Coordinator.States.Base;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Units.Bodies.Coordinator.States
{
    public class Standing : GroundedState
    {
        public Standing(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters)
            : base(legs, torso, parameters)
        {
        }

        protected void ManageReachingLegsTurnDirection(Vector3 logicLookDirection,
            Vector3 movementTurnLogicDirection)
        {
            var legsLogicDirection = GetLogicDirection(Legs.transform.forward);
            var torsoLogicDirection = GetLogicDirection(Torso.transform.forward);
            var angle = AngleCalculator.CalculateLogicAngle(legsLogicDirection,
                movementTurnLogicDirection);
            Legs.TurnTowards(Mathf.Abs(angle) <= 90 ? movementTurnLogicDirection : torsoLogicDirection);
        }

        public override UnitState FixedUpdate()
        {
            var logicLookDirection = GetLogicVector(Parameters.LookLogicDirection);
            Torso.TurnTowards(logicLookDirection);
            Torso.AimAt(Parameters.AimAtDirection);

            ManageReachingLegsTurnDirection(logicLookDirection, logicLookDirection);

            Legs.StraightenUp(1);
            Torso.StraightenUp(1);
            return this;
        }
    }
}
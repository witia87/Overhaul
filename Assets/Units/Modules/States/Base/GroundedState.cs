using System;
using UnityEngine;

namespace Assets.Units.Modules.States.Base
{
    public abstract class GroundedState : UnitState
    {
        protected Vector3 Limiter;
        protected GroundedState(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory, Vector3 initialGlobalLookDirection) : 
            base(movement, targeting, statesFactory)
        {
            GlobalLookDirection = initialGlobalLookDirection;
        }

        protected Vector3 GlobalLookDirection;

        protected void ManageReachingMovementTurnDirection(Vector3 logicLookDirection, Vector3 movementTurnLogicDirection)
        {
            var angle = AngleCalculator.CalculateLogicAngle(Movement.ModuleLogicDirection,
                movementTurnLogicDirection);
            if (Mathf.Abs(angle) <= 60)
            {
                Movement.AddTurnRotation(Vector3.Cross(Movement.ModuleLogicDirection, movementTurnLogicDirection).y);
            }
            else if (AngleCalculator.CalculateLogicAngle(movementTurnLogicDirection,
                         Limiter) >= 0)
            {
                Movement.AddTurnRotation(1);
            }
            else
            {
                Movement.AddTurnRotation(-1);
            }
        }

        protected void ManageReachingTargetingTurnDirection(Vector3 logicLookDirection)
        {
            var angle = AngleCalculator.CalculateLogicAngle(Movement.ModuleLogicDirection,
                logicLookDirection);
            if (Mathf.Abs(angle) < 120)
            {
                Targeting.AddTurnRotation(Vector3.Cross(Targeting.ModuleLogicDirection, logicLookDirection).y);
            }
            else if (AngleCalculator.CalculateLogicAngle(logicLookDirection,
                         Limiter) >= 0)
            {
                var direction = AngleCalculator.RotateLogicVector(Movement.ModuleLogicDirection, 60);
                Targeting.AddTurnRotation(Vector3.Cross(Targeting.ModuleLogicDirection, direction).y);
            }
            else
            {
                var direction = AngleCalculator.RotateLogicVector(Movement.ModuleLogicDirection, -60);
                Targeting.AddTurnRotation(Vector3.Cross(Targeting.ModuleLogicDirection, direction).y);
            }
        }

        protected Vector3 GetLogicVector(Vector3 vector)
        {
            vector.y = 0;
            if (vector.magnitude <= 0.001)
            {
                throw new ApplicationException("Grounded states require Modules to be in vertical postion.");
            }
            return vector.normalized;
        }

        /// <summary>
        ///     Limiter is a vector shows wich parts of the circle should result on turning left, or right.
        ///     It's calcuated based on Momement.ModuleLogicDirection and its angular velocity
        ///     (if the rigidbody is not turning, then the limiter is 180dg from the Movement.ModuleLogicDirection;
        ///     if the movement rigidbody is currently turning right, then the further the limiter is turned to the right)
        /// </summary>
        protected void CalculateLimiter()
        {
            var angleModifier = Movement.transform.InverseTransformDirection(Movement.Rigidbody.angularVelocity).y *
                                10;
            angleModifier = Mathf.Sign(angleModifier) * Mathf.Min(120, Mathf.Abs(angleModifier));
            Limiter = AngleCalculator.RotateLogicVector(-Movement.ModuleLogicDirection, angleModifier);
        }

        public override void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(Movement.transform.position, Movement.transform.position + Limiter);
            }
        }
    }
}
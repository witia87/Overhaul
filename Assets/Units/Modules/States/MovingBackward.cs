using Assets.Units.Modules.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.States
{
    public class MovingBackward : MovingForward
    {
        public MovingBackward(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection, Vector3 initialMoveLogicDirection, float speedModifier) :
            base(movement, targeting, statesFactory, initialGlobalLookDirection, initialMoveLogicDirection,
                speedModifier)
        {
        }

        public override UnitState VerifyPhysicConditions()
        {

            if (!Movement.IsStanding)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection, (GetLogicVector(Movement.Rigidbody.velocity) + MoveLogicDirection));
            }

            var parametersAngle =
                AngleCalculator.CalculateLogicAngle(MoveLogicDirection, GlobalLookDirection);
            if (Mathf.Abs(parametersAngle) <= 120)
            {
                return StatesFactory.CreateMovingForward(GlobalLookDirection, MoveLogicDirection, SpeedModifier);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            JumpCooldownLeft = Mathf.Max(0, JumpCooldownLeft - Time.fixedDeltaTime);
            var logicLookDirection = GetLogicVector(GlobalLookDirection);
            CalculateLimiter();
            ManageReachingMovementTurnDirection(logicLookDirection,
                ((GlobalLookDirection - MoveLogicDirection) / 2).normalized);
            ManageReachingTargetingTurnDirection(logicLookDirection);
            Movement.AddAcceleration(MoveLogicDirection);
            Movement.PerformStandingStraight();
            Targeting.PerformStandingStraight();
            return this;
        }
    }
}
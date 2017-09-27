using Assets.Units.Modules.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.States
{
   public class MovingForward : Standing
    {
        public MovingForward(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory, 
            Vector3 initialGlobalLookDirection, Vector3 initialMoveLogicDirection, float speedModifier) :
            base(movement, targeting, statesFactory, initialGlobalLookDirection)
        {
            MoveLogicDirection = initialMoveLogicDirection;
            SpeedModifier = speedModifier;
        }

        protected Vector3 MoveLogicDirection;
        protected float SpeedModifier;
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
                return this;
            }
            return StatesFactory.CreateMovingBackward(GlobalLookDirection, MoveLogicDirection, SpeedModifier);
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

        public override UnitState Jump(Vector3 globalDirection, float jumpForceModifier)
        {
            if (JumpCooldownLeft <= 0)
            {
                return StatesFactory.CreateJumping(GlobalLookDirection,
                    (GetLogicVector(Movement.Rigidbody.velocity) + MoveLogicDirection) / 2, jumpForceModifier);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            JumpCooldownLeft = Mathf.Max(0, JumpCooldownLeft - Time.fixedDeltaTime);
            var logicLookDirection = GetLogicVector(GlobalLookDirection);
            CalculateLimiter();
            ManageReachingMovementTurnDirection(logicLookDirection,
                ((GlobalLookDirection + MoveLogicDirection) / 2).normalized);
            ManageReachingTargetingTurnDirection(logicLookDirection);
            Movement.AddAcceleration(MoveLogicDirection * SpeedModifier);
            ManageReachingMovementTurnDirection(logicLookDirection,
                ((GlobalLookDirection + MoveLogicDirection) / 2).normalized);
            ManageReachingTargetingTurnDirection(logicLookDirection);

            Movement.PerformStandingStraight();
            Targeting.PerformStandingStraight();
            return this;
        }
    }
}
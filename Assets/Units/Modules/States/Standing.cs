using Assets.Units.Modules.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.States
{
    public class Standing : GroundedState
    {
        protected float JumpCooldownLeft;

        public Standing(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection)
            : base(movement, targeting, statesFactory, initialGlobalLookDirection)
        {
            JumpCooldownLeft = Movement.JumpCooldown;
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (!Movement.IsGrounded)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection, GlobalLookDirection);
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

        public override UnitState Jump(Vector3 globalDirection, float jumpForceModifier)
        {
            return StatesFactory.CreateJumping(GlobalLookDirection, GlobalLookDirection, jumpForceModifier);
        }

        public override UnitState FixedUpdate()
        {
            JumpCooldownLeft = Mathf.Max(0, JumpCooldownLeft - Time.fixedDeltaTime);
            var logicLookDirection = GetLogicVector(GlobalLookDirection);
            CalculateLimiter();
            ManageReachingMovementTurnDirection(logicLookDirection, GlobalLookDirection);
            ManageReachingTargetingTurnDirection(logicLookDirection);

            Movement.PerformStandingStraight();
            Targeting.PerformStandingStraight();
            return this;
        }
    }
}
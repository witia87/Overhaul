using Assets.Cognitions.Players.Controllers;
using Assets.Maps;
using Assets.Units;

namespace Assets.Cognitions.Players.States
{
    public class ControllingHumanoid : PlayerState
    {
        public ControllingHumanoid(IUnitControl unit, IMap map,
            ITargetingController targetingController, IMovementController movementController,
            IActionsController actionsController) :
            base(PlayerStateIds.ControllingHumanoid, unit, map, targetingController, movementController,
                actionsController)
        {
        }

        public override CognitionState<PlayerStateIds> Update()
        {
            Unit.LookAt(TargetingController.TargetedPosition);
            Unit.Gun.AimAt(TargetingController.TargetedPosition);
            if (TargetingController.IsFirePressed)
            {
                Unit.Gun.Fire();
            }

            if (MovementController.IsMovementPresent)
            {
                Unit.Move(MovementController.MovementVector.normalized, MovementController.MovementVector.magnitude);
            }

            if (MovementController.IsJumpPressed)
            {
                Unit.Jump(MovementController.MovementVector.normalized, MovementController.MovementVector.magnitude);
            }

            if (MovementController.IsCrouchPressed)
            {
                Unit.Crouch();
            }

            return this;
        }
    }
}
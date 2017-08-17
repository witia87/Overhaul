using Assets.Cognitions.Players.Controllers;
using Assets.Maps;
using Assets.Modules;
using UnityEngine;

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
            Unit.Targeting.LookAt(TargetingController.TargetedPosition);

            if (ActionsController.IsDropWeaponPressed)
            {
                Unit.Targeting.DropGun();
            }
            else if (ActionsController.IsUsePressed)
            {
                Unit.Targeting.PickGun();
            }
            else
            {
                if (Unit.Targeting.IsGunMounted)
                {
                    Unit.Targeting.Gun.SetFire(TargetingController.IsFirePressed);
                }
            }

            if (MovementController.IsMovementPresent)
            {
                Unit.Movement.Move(MovementController.MovementVector);
            }
            else
            {
                Unit.Movement.StopMoving();
            }

            if (MovementController.IsJumpPressed)
            {
                Unit.Movement.Jump(Vector3.up);
            }

            Unit.Movement.SetCrouch(MovementController.IsCrouchPressed);
            Unit.Targeting.SetCrouch(MovementController.IsCrouchPressed);

            return this;
        }
    }
}
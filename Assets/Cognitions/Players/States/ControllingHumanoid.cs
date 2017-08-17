using Assets.Cognitions.Players.Controllers;
using Assets.Maps;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cognitions.Players.States
{
    public class ControllingHumanoid : PlayerState
    {
        public ControllingHumanoid(IUnitControl unit, IMap map, int scale,
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
                    if (TargetingController.IsFirePressed)
                    {
                        Unit.Targeting.Gun.Fire();
                    }
                    else
                    {
                        Unit.Targeting.Gun.StopFiring();
                    }
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

            if (MovementController.IsCrouchPressed)
            {
                Unit.Movement.Crouch();
            }
            else
            {
                Unit.Movement.StopCrouching();
            }


            return this;
        }
    }
}
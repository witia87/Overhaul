using Assets.Cognitions.PlayerControllers.Controllers;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.States
{
    public class ControllingHumanoid : PlayerControllerState
    {
        public ControllingHumanoid(Cognition<PlayerControllerStateIds> parentCognition,
            ITargetingController targetingController, IMovementController movementController,
            IActionsController actionsController)
            : base(parentCognition, targetingController, movementController, actionsController,
                PlayerControllerStateIds.ControllingHumanoid)
        {
        }

        public override ICognitionState<PlayerControllerStateIds> Update()
        {
            if (TurretControl != null)
            {
                TurretControl.LookAt(TargetingController.TargetedPosition);

                if (ActionsController.IsDropWeaponPressed)
                {
                    TurretControl.DropGun();
                }
                else if (ActionsController.IsUsePressed)
                {
                    TurretControl.PickGun();
                }
                else
                {
                    if (TargetingController.IsFirePressed)
                    {
                        foreach (var gunControl in TurretControl.GunControls)
                        {
                            gunControl.Fire();
                        }
                    }
                    else
                    {
                        foreach (var gunControl in TurretControl.GunControls)
                        {
                            gunControl.StopFiring();
                        }
                    }
                }
            }

            if (MovementControl != null)
            {
                if (MovementController.IsMovementPresent)
                {
                    MovementControl.MoveTowards(MovementController.MovementVector);
                }
                else
                {
                    MovementControl.StopMoving();
                }

                if (MovementController.IsJumpPressed)
                {
                    MovementControl.Jump(Vector3.up);
                }
            }


            return this;
        }
    }
}
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
                if (TurretControl.IsGunMounted)
                {
                    if (TargetingController.IsFirePressed)
                    {
                        TurretControl.Gun.Fire();
                    }
                    else
                    {
                        TurretControl.Gun.StopFiring();
                    }
                }
            }

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


            return this;
        }
    }
}
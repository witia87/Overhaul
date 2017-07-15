using Assets.Cognitions.Player.Controllers;
using UnityEngine;

namespace Assets.Cognitions.Player.States
{
    public class ControllingHumanoid : PlayerState
    {
        public ControllingHumanoid(Cognition<PlayerStateIds> parentCognition,
            ITargetingController targetingController, IMovementController movementController,
            IActionsController actionsController)
            : base(parentCognition, targetingController, movementController, actionsController,
                PlayerStateIds.ControllingHumanoid)
        {
        }

        public override ICognitionState<PlayerStateIds> Update()
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
                Unit.Movement.MoveTowards(MovementController.MovementVector);
            }
            else
            {
                Unit.Movement.StopMoving();
            }

            if (MovementController.IsJumpPressed)
            {
                Unit.Movement.Jump(Vector3.up);
            }


            return this;
        }
    }
}
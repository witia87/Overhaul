using Assets.Cognitions.PlayerControllers.Controllers;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.States
{
    public class ControllingHumanoid : PlayerControllerState
    {
        public ControllingHumanoid(Cognition<PlayerControllerStateIds> parrentCognition,
            ITargetingController targetingController, IMovementController movementController)
            : base(parrentCognition, targetingController, movementController,
                PlayerControllerStateIds.ControllingHumanoid)
        {
        }

        public override ICognitionState<PlayerControllerStateIds> Update()
        {
            if(TurretControl != null)
            {
                TurretControl.LookAt(TargetingController.TargetedPosition);
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

            if (TargetingController.IsFirePressed)
            {
                // TODO
            }

            return this;
        }
    }
}
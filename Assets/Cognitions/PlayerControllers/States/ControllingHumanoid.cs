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
            foreach (var turretControl in Core.MountedModules.TurretControls)
            {
                turretControl.LookAt(TargetingController.TargetedPosition);
            }

            if (MovementController.IsMovementPresent)
            {
                Core.MountedModules.HumanoidMovementControl.MoveTowards(MovementController.MovementVector);
            }
            else
            {
                Core.MountedModules.HumanoidMovementControl.StopMoving();
            }

            if (TargetingController.IsFirePressed)
            {
                Core.MountedModules.HumanoidMovementControl.Jump(Vector3.up);
            }

            if (TargetingController.IsFirePressed)
            {
                foreach (var turretControl in Core.MountedModules.TurretControls)
                {
                    turretControl.Fire();
                }
            }

            return this;
        }
    }
}
using Assets.Cognitions.PlayerControllers.Controllers;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.States
{
    public class ControllingVehicle : PlayerControllerState
    {
        private int _wasForwardLastPressed = 1;

        public ControllingVehicle(Cognition<PlayerControllerStateIds> parrentCognition,
            ITargetingController targetingController, IMovementController movementController)
            : base(parrentCognition, targetingController, movementController,
                PlayerControllerStateIds.ControllingVehicle)
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
                _wasForwardLastPressed = MovementController.MovementVector.z > 0
                    ? 1
                    : MovementController.MovementVector.z < 0 ? -1 : _wasForwardLastPressed;
                var localMovementDirection =
                    Core.gameObject.transform.InverseTransformDirection(
                        Core.MountedModules.VehicleMovementControl.MovementDirection);

                Core.MountedModules.VehicleMovementControl.MoveForward(1);
            }

            if (MovementController.IsJumpPressed)
            {
                Core.MountedModules.VehicleMovementControl.Jump(Vector3.up);
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
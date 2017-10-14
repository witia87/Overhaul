using Assets.Cognitions.Players;
using UnityEngine;

namespace Assets.Gui.UnitControl
{
    public class UnitMovementComponent : GuiComponent
    {
        [SerializeField] private PlayerCognition _playerCognition;

        private Quaternion _rotationQuaternion;
        public void Start()
        {
            _rotationQuaternion = Quaternion.AngleAxis(
                CameraStore.CameraEulerAngles.y, Vector3.up);
        }

        private void Update()
        {
            if (KeyboardStore.IsMovementPresent)
            {
                _playerCognition.SetMovement(_rotationQuaternion * KeyboardStore.MovementVector);
            }

            if (KeyboardStore.IsJumpPressed)
            {
                _playerCognition.Jump();
            }

            if (KeyboardStore.IsCrouchPressed)
            {
                _playerCognition.Crouch();
            }
        }
    }
}
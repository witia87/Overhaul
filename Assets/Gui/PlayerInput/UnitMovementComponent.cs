using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class UnitMovementComponent : GuiComponent
    {
        private Quaternion _rotationQuaternion;
        [SerializeField] private Unit _unit;

        public void Start()
        {
            _rotationQuaternion = Quaternion.AngleAxis(
                CameraStore.CameraEulerAngles.y, Vector3.up);
        }

        private void Update()
        {
            if (KeyboardStore.IsMovementPresent)
            {
                _unit.Control.Move(_rotationQuaternion * KeyboardStore.MovementVector);
            }

            if (KeyboardStore.IsCrouchPressed)
            {
                _unit.Control.Crouch();
            }
        }
    }
}
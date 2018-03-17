using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Cognitions.Players
{
    public class PlayerCognition : MonoBehaviour
    {
        private Vector3 _lookAtPosition;
        private Vector3 _movementGlobalDirection;
        [SerializeField] private UnitControl _unitControl;

        private bool _wasCrouchSet;

        private bool _wasFireSet;

        private bool _wasJumpSet;

        private bool _wasLookAtPositionSet;

        private bool _wasMovementSet;

        public void Update()
        {
            if (_wasLookAtPositionSet)
            {


                _unitControl.Gun.AimAt(_lookAtPosition);
            }

            if (_wasFireSet)
            {
                _unitControl.Gun.Fire();
            }

            if (_wasMovementSet)
            {
                _unitControl.Move(_movementGlobalDirection.normalized, _movementGlobalDirection.magnitude);
            }

            if (_wasJumpSet)
            {
                _unitControl.Jump(_movementGlobalDirection.normalized, _movementGlobalDirection.magnitude);
            }

            if (_wasCrouchSet)
            {
                _unitControl.Crouch();
            }

            _wasLookAtPositionSet = false;
            _wasFireSet = false;
            _wasMovementSet = false;
            _wasJumpSet = false;
            _wasCrouchSet = false;
        }

        public void LookAt(Vector3 position)
        {
            _wasLookAtPositionSet = true;
            _lookAtPosition = position;
        }

        public void Fire()
        {
            _wasFireSet = true;
        }

        public void SetMovement(Vector3 globalDirection)
        {
            _wasMovementSet = true;
            _movementGlobalDirection = globalDirection;
        }

        public void Jump()
        {
            _wasJumpSet = true;
        }

        public void Crouch()
        {
            _wasCrouchSet = true;
        }
    }
}
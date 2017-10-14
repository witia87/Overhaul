using Assets.Units;
using UnityEngine;

namespace Assets.Cognitions.Players
{
    public class PlayerCognition: MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        public void Update()
        {
            if (_wasLookAtPositionSet)
            {
                _unit.LookAt(_lookAtPosition);
                _unit.Gun.AimAt(_lookAtPosition);
            }

            if (_wasFireSet)
            {
                _unit.Gun.Fire();
            }

            if (_wasMovementSet)
            {
                _unit.Move(_movementGlobalDirection.normalized, _movementGlobalDirection.magnitude);
            }

            if (_wasJumpSet)
            {
                _unit.Jump(_movementGlobalDirection.normalized, _movementGlobalDirection.magnitude);
            }

            if (_wasCrouchSet)
            {
                _unit.Crouch();
            }

            _wasLookAtPositionSet = false;
            _wasFireSet = false;
            _wasMovementSet = false;
            _wasJumpSet = false;
            _wasCrouchSet = false;
        }

        private bool _wasLookAtPositionSet;
        private Vector3 _lookAtPosition;
        public void LookAt(Vector3 position)
        {
            _wasLookAtPositionSet = true;
            _lookAtPosition = position;
        }

        private bool _wasFireSet;
        public void Fire()
        {
            _wasFireSet = true;
        }

        private bool _wasMovementSet;
        private Vector3 _movementGlobalDirection;
        public void SetMovement(Vector3 globalDirection)
        {
            _wasMovementSet = true;
            _movementGlobalDirection = globalDirection;
        }

        private bool _wasJumpSet;
        public void Jump()
        {
            _wasJumpSet = true;
        }

        private bool _wasCrouchSet;
        public void Crouch()
        {
            _wasCrouchSet = true;
        }
    }
}
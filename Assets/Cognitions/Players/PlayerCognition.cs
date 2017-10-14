using Assets.Units;
using UnityEngine;

namespace Assets.Cognitions.Players
{
    public class PlayerCognition : MonoBehaviour
    {
        private Vector3 _lookAtPosition;
        private Vector3 _movementGlobalDirection;
        [SerializeField] private Unit _unit;

        private bool _wasCrouchSet;

        private bool _wasFireSet;

        private bool _wasJumpSet;

        private bool _wasLookAtPositionSet;

        private bool _wasMovementSet;

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
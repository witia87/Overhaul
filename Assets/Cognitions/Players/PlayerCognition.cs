using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Cognitions.Players
{
    public class PlayerCognition : MonoBehaviour
    {
        private Vector3 _lookAtPosition;
        private Vector3 _movementScaledLogicDirection;
        private Unit _unit;

        private bool _wasCrouchSet;

        private bool _wasFireSet;

        private bool _wasLookAtPositionSet;

        private bool _wasMovementSet;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void FixedUpdate()
        {
            if (_wasLookAtPositionSet)
            {
                var newLookDirection = _lookAtPosition - _unit.Gun.Position;
                _unit.Control.LookTowards(newLookDirection.normalized);
            }

            _unit.Control.Fire(_wasFireSet);

            if (_wasMovementSet)
            {
                _unit.Control.Move(_movementScaledLogicDirection);
            }
            else
            {
                _unit.Control.Move(Vector3.zero);
            }

            _unit.Control.Crouch(_wasCrouchSet);
        }

        private void Update()
        {
            _wasLookAtPositionSet = false;
            _wasFireSet = false;
            _wasMovementSet = false;
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

        public void SetMovement(Vector3 globalScaledLogicDirection)
        {
            _wasMovementSet = true;
            _movementScaledLogicDirection = globalScaledLogicDirection;
        }

        public void Crouch()
        {
            _wasCrouchSet = true;
        }
    }
}
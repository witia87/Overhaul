using System;
using UnityEngine;

namespace Assets.Environment.Units
{
    public class UnitControl : IUnitControl, IUnitControlParameters
    {
        private const float Epsilon = 0.000001f;
        private Vector3 _lookTowardsDirection;
        private Vector3 _movementScaledLogicDirection;
        private Unit _unit;

        private bool _wasCrouchSet;

        private bool _wasFireSet;

        private bool _wasLookTowardsDirectionSet;

        private bool _wasMovementSet;

        public UnitControl(Unit unit)
        {
            _unit = unit;
        }

        public void LookAt(Vector3 position)
        {
            _wasLookTowardsDirectionSet = true;
            _lookTowardsDirection = (position - _unit.Gun.Position).normalized;
        }

        public void LookTowards(Vector3 direction)
        {
            if (Math.Abs(direction.magnitude - 1) > Epsilon)
            {
                throw new ApplicationException("Vector not normalized.");
            }

            _wasLookTowardsDirectionSet = true;
            _lookTowardsDirection = direction;
        }

        public void Fire()
        {
            _wasFireSet = true;
        }

        public void Move(Vector3 globalScaledLogicDirection)
        {
            if (globalScaledLogicDirection.magnitude > 1 + Epsilon
                || Math.Abs(globalScaledLogicDirection.y) > Epsilon)
            {
                throw new ApplicationException("Vector not scaled logic.");
            }

            _wasMovementSet = true;
            _movementScaledLogicDirection = globalScaledLogicDirection;
        }

        public void Crouch()
        {
            _wasCrouchSet = true;
        }

        public Vector3 MoveScaledLogicDirection { get; private set; }
        public Vector3 LookLogicDirection { get; private set; }
        public Vector3 AimAtDirection { get; private set; }
        public bool IsSetToCrouch { get; private set; }
        public bool IsSetToFire { get; private set; }

        public bool IsSetToMove
        {
            get { return MoveScaledLogicDirection.magnitude > Epsilon; }
        }

        public void FixedUpdate()
        {
            if (_wasLookTowardsDirectionSet)
            {
                AimAtDirection = _lookTowardsDirection;
                var logicDirection = _lookTowardsDirection;
                logicDirection.y = 0;
                LookLogicDirection = logicDirection.normalized;
            }

            IsSetToFire = _wasFireSet;

            if (_wasMovementSet)
            {
                MoveScaledLogicDirection = _movementScaledLogicDirection;
            }
            else
            {
                MoveScaledLogicDirection = Vector3.zero;
            }

            IsSetToCrouch = _wasCrouchSet;
        }

        public void Update()
        {
            _wasLookTowardsDirectionSet = false;
            _wasFireSet = false;
            _wasMovementSet = false;
            _wasCrouchSet = false;
        }
    }
}
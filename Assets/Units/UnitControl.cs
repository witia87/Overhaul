using System;
using UnityEngine;

namespace Assets.Units
{ 
    public class UnitControl : IUnitControl, IUnitControlParameters
    {
        private const float Epsilon = 0.000001f;

        public void LookTowards(Vector3 direction)
        {
            if (Math.Abs(direction.magnitude - 1) > Epsilon)
                throw new ApplicationException("Vector not normalized.");

            AimAtDirection = direction;

            direction.y = 0;
            LookLogicDirection = direction.normalized;
        }

        public void Move(Vector3 scaledLogicDirection)
        {
            if (scaledLogicDirection.magnitude > 1 + Epsilon
                || Math.Abs(scaledLogicDirection.y) > Epsilon)
                throw new ApplicationException("Vector not scaled logic.");

            MoveScaledLogicDirection = scaledLogicDirection;
        }

        public void Crouch()
        {
            IsSetToCrouch = true;
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public Vector3 MoveScaledLogicDirection { get; private set; }
        public Vector3 LookLogicDirection { get; private set; }
        public Vector3 AimAtDirection { get; private set; }
        public bool IsSetToCrouch { get; private set; }


        public bool IsSetToMove
        {
            get { return MoveScaledLogicDirection.magnitude > Epsilon; }
        }

        public Vector3 ShouldFire
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
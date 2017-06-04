using UnityEngine;

namespace Assets.Modules.Movement
{
    public class VehicleMovementModule : MovementModule, IVehicleMovementControl
    {
        private float _speedToMove;
        public MovementType MovementType { get; private set; }

        public void MoveForward(float speed)
        {
            _speedToMove = speed;
            IsSetToMove = true;
            MovementType = MovementType.Forward;
        }

        public void MoveBackward(float speed)
        {
            _speedToMove = -speed;
            IsSetToMove = true;
            MovementType = MovementType.Forward;
        }

        public void TurnLeft()
        {
            if (MovementType == MovementType.Forward)
            {
                TurnFrontTowards(-gameObject.transform.right);
            }
            else
            {
                TurnBackTowards(-gameObject.transform.right);
            }
        }

        public void TurnRight()
        {
            if (MovementType == MovementType.Forward)
            {
                TurnFrontTowards(gameObject.transform.right);
            }
            else
            {
                TurnBackTowards(gameObject.transform.right);
            }
        }

        public void TurnFrontTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            globalDirection.Normalize();
            GlobalDirectionToTurnTowards = globalDirection;
            IsSetToTurn = true;
        }

        public void TurnBackTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            globalDirection.Normalize();
            GlobalDirectionToTurnTowards = -globalDirection;
            IsSetToTurn = true;
        }

        public void LookAtFront(Vector3 position)
        {
            TurnFrontTowards(position - gameObject.transform.position);
        }

        public void LookAtBack(Vector3 position)
        {
            TurnBackTowards(position - gameObject.transform.position);
        }

        protected void FixedUpdate()
        {
            var acceleration = Acceleration;
            if (!IsGrounded)
            {
                Rigidbody.drag = GroundedDrag*0.01f;
                acceleration = AirAcceleration;
            }
            else
            {
                Rigidbody.drag = GroundedDrag;
            }

            if (IsSetToMove)
            {
                var forwardSpeed = gameObject.transform.InverseTransformDirection(GlobalDirectionInWhichToMove).z;
                Rigidbody.AddRelativeForce(Vector3.forward*forwardSpeed*acceleration, ForceMode.Acceleration);
            }

            if (IsSetToTurn)
            {
                var value = Vector3.Dot(GlobalDirectionToTurnTowards,
                    gameObject.transform.right);

                var torque = Vector3.up*value*AngularAcceleration;
                Rigidbody.AddRelativeTorque(torque);
            }
        }
    }
}
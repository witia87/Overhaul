using Assets.Modules.Turrets;
using UnityEditor;
using UnityEngine;

namespace Assets.Modules.Movement
{
    public class HumanoidMovementModule : MovementModule, IHumanoidMovementControl
    {
        public TurretModule TurretModule;
        bool IsTurretModulePresent { get { return TurretModule != null; } }

        public void Move(Vector3 localDirection)
        {
            MoveTowards(gameObject.transform.TransformDirection(localDirection));
        }

        public void MoveTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            GlobalDirectionInWhichToMove = globalDirection;

            GlobalDirectionToTurnTowards = Vector3.Dot(GlobalDirectionInWhichToMove,
                TurretModule.gameObject.transform.forward) >= 0
                ? globalDirection
                : -globalDirection;
            GlobalDirectionToTurnTowards.Normalize();

            IsSetToMove = true;
        }

        public void GoTo(Vector3 position)
        {
            MoveTowards(position - gameObject.transform.position);
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
                // Find a best way to reach the deasired direction
                Vector3 torque;
                if (Vector3.Angle(GlobalDirectionToTurnTowards, TurretModule.SightDirection) < 90)
                {
                    // If desired direction is in front of the Torso, 
                    // then simply try to reach it the closest way.
                    torque = GetTorqueTowards(GlobalDirectionToTurnTowards);
                }
                else
                {
                    //torque = Vector3.Dot(gameObject.transform.right, TurretModule.gameObject.transform.forward);
                    torque = GetTorqueTowards(TurretModule.SightDirection);

                }

                var speedModifier = 0.75f + 0.25f * Vector3.Dot(gameObject.transform.forward, MovementDirection);
                Rigidbody.AddForce(GlobalDirectionInWhichToMove * acceleration * speedModifier, ForceMode.Acceleration);

                var torqueToApply = torque * AngularAcceleration;
                Rigidbody.AddTorque(torqueToApply);
            }
            else
            {
                var direction = ((gameObject.transform.forward + TurretModule.TargetGlobalDirection) / 2).normalized;
                //direction = gameObject.transform.InverseTransformDirection(direction);
                var torque = GetTorqueTowards(direction);
                var torqueToApply = torque * AngularAcceleration;
                Rigidbody.AddTorque(torqueToApply);
            }
        }

        private Vector3 GetTorqueTowards(Vector3 direction)
        {
            var torque = Vector3.Cross(gameObject.transform.forward, direction);
            if (Vector3.Dot(direction, gameObject.transform.forward) < 0)
            {
                torque.y = Mathf.Sign(torque.y);
            }
            return torque;
        }
        
    }
}
using System;
using Assets.Modules.Turrets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Movement
{
    public class MovementModule : Module, IMovementControl
    {
        public float Acceleration = 100;
        public float AngularAcceleration = 200;
        protected Vector3 GlobalDirectionInWhichToMove;
        protected Vector3 GlobalDirectionToTurnTowards;

        protected bool IsSetToMove;
        protected bool IsSetToTurn;

        public float JumpCooldown = 1;
        protected float JumpCooldownLeft;
        public float JumpVelocity = 100;

        public TurretModule TurretModule;

        public MovementType MovementType { get; private set; }

        public float MovementSpeed
        {
            get { return Rigidbody.velocity.magnitude; }
        }

        public Vector3 UnitDirection
        {
            get { return gameObject.transform.forward; }
            set { gameObject.transform.forward = value; }
        }

        public Vector3 MovementDirection
        {
            get { return Rigidbody.velocity.magnitude > 0 ? Rigidbody.velocity.normalized : Vector3.zero; }
        }

        public void StopTurning()
        {
            IsSetToTurn = false;
        }

        public void JumpTowards(Vector3 globalDirection)
        {
            Jump(gameObject.transform.worldToLocalMatrix*globalDirection);
        }

        public void StopMoving()
        {
            IsSetToMove = false;
        }

        public void Jump(Vector3 localDirection)
        {
            if (JumpCooldownLeft <= 0 && IsGrounded)
            {
                localDirection *= JumpVelocity;
                Rigidbody.AddRelativeForce(localDirection, ForceMode.VelocityChange);
            }
            JumpCooldownLeft = Mathf.Max(0, JumpCooldownLeft - Time.deltaTime);
        }


        public void MoveTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            GlobalDirectionInWhichToMove = globalDirection;

            if (Vector3.Dot(GlobalDirectionInWhichToMove, TurretModule.TargetGlobalDirection) >= 0)
            {
                GlobalDirectionToTurnTowards = globalDirection;
                MovementType = MovementType.Forward;
            }
            else
            {
                GlobalDirectionToTurnTowards = -globalDirection;
                MovementType = MovementType.Backward;
            }

            GlobalDirectionToTurnTowards.Normalize();

            IsSetToMove = true;
        }

        public void GoTo(Vector3 position)
        {
            MoveTowards(position - gameObject.transform.position);
        }

        public void Move(Vector3 localDirection)
        {
            throw new NotImplementedException();
        }

        public bool IsGrounded { get; private set; }

        public override void Mount(GameObject parentGameObject, Vector3 localPosition)
        {
            base.Mount(parentGameObject, localPosition);
            Unit.Rigidbody.drag = Drag;
            Unit.Rigidbody.angularDrag = AngularDrag;
        }

        protected void FixedUpdate()
        {
            var acceleration = Acceleration;
            if (!IsGrounded)
            {
                Rigidbody.drag = 0;
                acceleration = 0;
            }
            else
            {
                Rigidbody.drag = Drag;
            }

            if (IsSetToMove)
            {
                var torque = Vector3.Cross(gameObject.transform.forward, GlobalDirectionToTurnTowards);

                var speedModifier = 0.75f + 0.25f*Vector3.Dot(gameObject.transform.forward, MovementDirection);
                Rigidbody.AddForce(GlobalDirectionInWhichToMove*acceleration*speedModifier, ForceMode.Acceleration);

                var torqueToApply = torque*AngularAcceleration;
                Rigidbody.AddTorque(torqueToApply);
            }
            else
            {
                var direction = ((gameObject.transform.forward + TurretModule.TargetGlobalDirection)/2).normalized;
                var torque = Vector3.Cross(gameObject.transform.forward, direction)*AngularAcceleration;
                Rigidbody.AddTorque(torque);
            }
        }

        protected void Update()
        {
            IsGrounded = Physics.Raycast(transform.position + Vector3.up*0.01f, -Vector3.up, 0.1f);
        }

        protected void OnDrawGizmos()
        {
            DrawArrow.ForDebug(gameObject.transform.position + UnitDirection*Size.z/2, UnitDirection,
                Color.magenta, 0.1f, 20);
        }
    }
}
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Movement
{
    public abstract class MovementModule : Module
    {
        public float Acceleration = 100;
        public float AirAcceleration = 20;
        public float AngularAcceleration = 200;
        public float AngularAirAcceleration = 30;
        protected Vector3 GlobalDirectionInWhichToMove;
        protected Vector3 GlobalDirectionToTurnTowards;

        protected float GroundedDrag;

        protected bool IsSetToMove;


        protected bool IsSetToTurn;
        public float JumpCooldown = 1;

        protected float JumpCooldownLeft;
        public float JumpVelocity = 100;

        public bool IsGrounded
        {
            get { return gameObject.transform.position.y <= 0.001; }
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
            Jump(gameObject.transform.worldToLocalMatrix * globalDirection);
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

        public override void Mount(GameObject parrentGameObject, Vector3 localPosition)
        {
        }

        public override void Unmount()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            GroundedDrag = Rigidbody.drag;
        }

        protected void OnDrawGizmos()
        {
            DrawArrow.ForDebug(gameObject.transform.position + UnitDirection*Size.z/2, UnitDirection,
                Color.magenta, 0.1f, 20);
        }
    }
}
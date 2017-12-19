using Assets.Units.Helpers;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class DynamicModule : Module
    {
        [SerializeField] private readonly float _couchTime = 0.4f;
        [SerializeField] private float _minimalCrouchLevel = 0.6f;
        public CrouchHelper CrouchHelper;

        public float AngularAccelerationAlignZ = 20;
        public float AngularAccelerationLookAtX = 10;
        public float AngularAccelerationLookAtY = 20;
        public float AngularDragAlignZ = 20;
        public float AngularDragLookAtX = 20;
        public float AngularDragLookAtY = 30;

        public float CrawlingForce = 5;

        protected bool IsSetToCrouch;

        public float MovingForce = 100;

        public float StandingStraightForce = 20;

        public override Vector3 Bottom
        {
            get { return transform.TransformPoint(new Vector3(0, -CrouchHelper.CurrentHeight / 2, 0)); }
        }

        public override Vector3 Top
        {
            get { return transform.TransformPoint(new Vector3(0, CrouchHelper.CurrentHeight / 2, 0)); }
        }

        protected bool IsCrawling { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CrouchHelper = new CrouchHelper(_couchTime, _minimalCrouchLevel, CapsuleCollider.height);
        }

        private bool GetIsCrawling()
        {
            return IsGrounded &&
                   Physics.Raycast(
                       CapsuleCollider.center + transform.forward * CapsuleCollider.radius * 0.9f,
                       Vector3.down,
                       CapsuleCollider.radius * 0.3f, FloorLayerMask);
        }

        protected virtual void Update()
        {
            CrouchHelper.Update(IsSetToCrouch);
            IsSetToCrouch = false;
            CapsuleCollider.height = CrouchHelper.CurrentHeight;
            IsCrawling = GetIsCrawling();
        }

        public virtual void Crouch()
        {
            IsSetToCrouch = true;
        }

        public void StraightenUp(float modifier)
        {
            var cross = Vector3.Cross(transform.up, Vector3.up);
            if (Vector3.Angle(transform.up, Vector3.up) > 90)
            {
                cross.Normalize();
            }
            Rigidbody.AddTorque(cross * modifier * StandingStraightForce * StunModifier, ForceMode.Force);
        }

        public void TurnTowards(Vector3 globalDirection)
        {
            var localDirection = transform.InverseTransformDirection(globalDirection);
            var torque = Vector3.Cross(Vector3.forward, transform.InverseTransformDirection(globalDirection));
            Rigidbody.AddRelativeTorque(new Vector3(0, torque.y, 0) * AngularAccelerationLookAtY * StunModifier);

            if (Vector3.Dot(Vector3.forward, localDirection) >= 0)
            {
                Rigidbody.AddRelativeTorque(new Vector3(0, torque.y, 0) * AngularAccelerationLookAtY * StunModifier);
            }
            else
            {
                Rigidbody.AddRelativeTorque(new Vector3(0, Mathf.Sign(torque.y), 0) * AngularAccelerationLookAtY *
                                            StunModifier);
            }
        }

        public void FlipTowards(Vector3 globalDirection)
        {
            var torque = Vector3.Cross(Vector3.forward, transform.InverseTransformDirection(globalDirection));
            Rigidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * AngularAccelerationLookAtX * StunModifier);
        }

        public void AlignWith(Vector3 globalDirection)
        {
            var torque = Vector3.Cross(Vector3.up, transform.InverseTransformDirection(globalDirection));
            Rigidbody.AddRelativeTorque(new Vector3(0, 0, torque.z) * AngularAccelerationAlignZ, ForceMode.Force);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            var localVelocity = transform.InverseTransformDirection(Rigidbody.angularVelocity);
            localVelocity = new Vector3(
                localVelocity.x * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragLookAtX),
                localVelocity.y * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragLookAtY),
                localVelocity.z * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragAlignZ));
            Rigidbody.angularVelocity = transform.TransformDirection(localVelocity);
        }

        public void Crawl(Vector3 direction)
        {
            if (IsCrawling)
            {
                Rigidbody.AddForce(direction * CrawlingForce, ForceMode.Force);
            }
        }
    }
}
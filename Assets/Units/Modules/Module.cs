using System;
using Assets.Units.Helpers;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class Module : MonoBehaviour
    {
        private float _crouchLevel;

        [SerializeField] private LayerMask _floorLayerMask;
        public float Acceleration = 100;
        protected AngleCalculator AngleCalculator = new AngleCalculator();
        public float AngularAccelerationAlignZ = 20;

        public float AngularAccelerationLookAtX = 10;
        public float AngularAccelerationLookAtY = 20;
        public float AngularDragAlignZ = 20;
        public float AngularDragLookAtX = 20;
        public float AngularDragLookAtY = 30;
        protected CapsuleCollider CapsuleCollider;

        public float CrouchTime = 0.4f;

        public float JumpVelocity = 2;
        public float MinimalCrouchLevel = 0.6f;

        public float StandingForce = 20;

        public float StunResistanceTime = 10;
        [HideInInspector] public Unit Unit;

        [HideInInspector]
        public Rigidbody Rigidbody { get; private set; }

        protected float StunTimeLeft
        {
            get
            {
                if (Unit != null)
                {
                    return Unit.StunTimeLeft;
                }
                return StunResistanceTime;
            }
        }

        protected float StunModifier
        {
            get { return Mathf.Max(0, 1 - StunTimeLeft / StunResistanceTime); }
        }

        public bool IsStuned
        {
            get { return StunModifier <= 0; }
        }

        public Vector3 ModuleLogicDirection
        {
            get
            {
                if (Mathf.Abs(transform.forward.y) > 0.999f) // TODO: Verify whether this condition might be removed.
                {
                    throw new ApplicationException(
                        "Cannot calculate ModuleLogicDirection when module lays horizontaly.");
                }
                return NormalizeVector(transform.forward);
            }
        }

        public Vector3 ModuleLogicPosition
        {
            get
            {
                var position = transform.position;
                position.y = 0;
                return position;
            }
        }

        protected float CrouchModifier
        {
            get { return MinimalCrouchLevel + _crouchLevel * (1 - MinimalCrouchLevel); }
        }

        public Vector3 Bottom
        {
            get { return transform.TransformPoint(new Vector3(0, -0.5f, 0) * CrouchModifier); }
        }

        public Vector3 Top
        {
            get { return transform.TransformPoint(new Vector3(0, 0.5f, 0) * CrouchModifier); }
        }

        public Vector3 Center
        {
            get { return transform.TransformPoint(new Vector3(0, 0, 0)); }
        }

        public bool IsGrounded { get; protected set; }

        protected virtual void Awake()
        {
            Unit = transform.root.GetComponent<Unit>();
            Rigidbody = GetComponent<Rigidbody>();
            CapsuleCollider = GetComponent<CapsuleCollider>();
            IsGrounded = true;
        }

        protected virtual void FixedUpdate()
        {
            var localVelocity = transform.InverseTransformDirection(Rigidbody.angularVelocity);
            localVelocity = new Vector3(
                localVelocity.x * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragLookAtX),
                localVelocity.y * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragLookAtY),
                localVelocity.z * Mathf.Max(0, 1 - Time.fixedDeltaTime * AngularDragAlignZ));
            Rigidbody.angularVelocity = transform.TransformDirection(localVelocity);
        }

        protected virtual void Update()
        {
            CapsuleCollider.height = CrouchModifier;
        }

        protected Vector3 NormalizeVector(Vector3 v)
        {
            v.y = 0;
            return v.normalized;
        }

        public virtual void Stun(float time)
        {
            if (Unit != null)
            {
                Unit.Stun(time);
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            OnCollisionStay(collision);
        }

        protected virtual void OnCollisionStay(Collision collision)
        {
            if (_floorLayerMask.value == (_floorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = true;
            }
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            if (_floorLayerMask.value == (_floorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = false;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2, transform.position + transform.forward);
            }
        }

        public void PerformStandingStraight()
        {
            var cross = Vector3.Cross(transform.up, Vector3.up);
            if (Vector3.Angle(transform.up, Vector3.up) > 90)
            {
                cross.Normalize();
            }
            Rigidbody.AddTorque(cross * StandingForce * StunModifier, ForceMode.Force);
        }

        public void LookTowards(Vector3 localDirection)
        {
            var torque = Vector3.Cross(Vector3.forward,
                localDirection);
            Rigidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * AngularAccelerationLookAtX * StunModifier);
            Rigidbody.AddRelativeTorque(new Vector3(0, torque.y, 0) * AngularAccelerationLookAtY * StunModifier);
        }

        public void AlignTowards(Vector3 localDirection)
        {
            var torque = Vector3.Cross(Vector3.up,
                localDirection);
            torque.x = 0;
            torque.y = 0;
            Rigidbody.AddRelativeTorque(torque * AngularAccelerationAlignZ * StunModifier);
        }

        public void AddJumpVelocity(Vector3 globalDirection, float jumpVelocityModifier)
        {
            Rigidbody.AddForce(globalDirection * JumpVelocity * jumpVelocityModifier * StunModifier,
                ForceMode.VelocityChange);
        }

        /// <summary>
        ///     Method allows to add a torue around vertical axis of the module
        /// </summary>
        /// <param name="value">Must be from range (rotate left) [-1..1] (rotate right)</param>
        public void AddTurnRotation(float value)
        {
            if (Mathf.Abs(value) > 1.0001)
            {
                throw new ApplicationException(
                    "Value of the torque must be from [-1..1] range."); // TODO: Remove to otpimize
            }
            Rigidbody.AddRelativeTorque(Vector3.up * value * AngularAccelerationLookAtY * StunModifier,
                ForceMode.Force);
        }

        public void AddAlignRotation(float value)
        {
            if (Mathf.Abs(value) > 1.0001)
            {
                throw new ApplicationException(
                    "Value of the torque must be from [-1..1] range."); // TODO: Remove to otpimize
            }
            Rigidbody.AddRelativeTorque(Vector3.forward * value * AngularAccelerationAlignZ * StunModifier,
                ForceMode.Force);
        }

        public void AddFlipRotation(float value)
        {
            if (Mathf.Abs(value) > 1.0001)
            {
                throw new ApplicationException(
                    "Value of the torque must be from [-1..1] range."); // TODO: Remove to otpimize
            }
            Rigidbody.AddRelativeTorque(Vector3.right * value * AngularAccelerationLookAtX * StunModifier,
                ForceMode.Force);
        }

        public virtual void SetCrouch(float crouchLevel)
        {
            _crouchLevel = crouchLevel;
        }
    }
}
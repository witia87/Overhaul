using System;
using Assets.NewUnits.Helpers;
using UnityEngine;

namespace Assets.NewUnits
{
    /// <summary>
    ///     Module class responsible for maintaining internal integrity of the module.
    ///     Manages physics that does not rely on logic decisions.
    /// </summary>
    public class MovementModule : Module, INewMovementModuleParameters
    {
        private readonly AngleCalculator _angleCalculator = new AngleCalculator();

        private ConfigurableJoint _configurableJoint;
        [SerializeField] private LayerMask _floorLayerMask;

        private Vector3 _globalJumpDirection;
        private bool _isAfterJump;

        private Vector3 _lastFloorContact;
        public float BackwardDrag = 5;

        public float BaseDrag = 5;

        public float JumpCooldown = 1;
        protected float JumpCooldownLeft;
        public float JumpVelocity = 100;
        public float JumpRotationForce = 100;
        public float SidewayDrag = 10;
        public float StandingForce = 10;

        public float MovementSpeed { get { return Rigidbody.velocity.magnitude; } }
        public bool IsGrounded { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Rigidbody.centerOfMass = new Vector3(0, 0.4f, 0);
            _configurableJoint = GetComponent<ConfigurableJoint>();
            IsGrounded = true;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsGrounded)
            {
                var stunModifier = 0.0f + 1f * (1 - Mathf.Min(1, StunTimeLeft / 3));
                Rigidbody.AddTorque(Vector3.Cross(transform.up, Vector3.up) * StandingForce * stunModifier,
                    ForceMode.Impulse);
                AddDrag();
            }

            _configurableJoint.anchor = new Vector3(0, 0.5f * CrouchHelper.CrouchLevel, 0);
            _configurableJoint.connectedAnchor = new Vector3(0, -0.5f * CrouchHelper.CrouchLevel, 0);
        }

        private void AddDrag()
        {
            if (Mathf.Abs(transform.forward.y) > 0.99)
            {
                return;
            }
            var logicVelocity = Rigidbody.velocity;
            logicVelocity.y = 0;
            logicVelocity.Normalize();
            var angle = _angleCalculator.CalculateLogicAngle(ModuleLogicDirection, logicVelocity);
            float drag = 0;
            if (angle < 90)
            {
                drag = BaseDrag + SidewayDrag * (1 - Vector3.Dot(logicVelocity, ModuleLogicDirection));
            }
            else
            {
                drag = BaseDrag + BackwardDrag + (SidewayDrag - BackwardDrag) *
                       (1 + Vector3.Dot(logicVelocity, ModuleLogicDirection));
            }

            Rigidbody.velocity = Rigidbody.velocity * (1 - Time.fixedDeltaTime * drag);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2, transform.position + transform.forward);
            }
        }

        public void Jump(Vector3 globalLogicDirection, float jumpForceModifier)
        {
            if (JumpCooldownLeft <= 0 && IsGrounded)
            {
                _globalJumpDirection = globalLogicDirection;
                //Rigidbody.AddTorque(Vector3.Cross(Vector3.up, globalLogicDirection) *,+
                //    ForceMode.VelocityChange);
                globalLogicDirection *= jumpForceModifier;
                globalLogicDirection.y = 1;
                globalLogicDirection *= JumpVelocity;
                Rigidbody.AddForce(globalLogicDirection, ForceMode.VelocityChange);
                _isAfterJump = true;
            }
            JumpCooldownLeft = Mathf.Max(0, JumpCooldownLeft - Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_floorLayerMask.value == (_floorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                //_lastFloorContact = collision.contacts[0].point;
                IsGrounded = true;
                _isAfterJump = false;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (_floorLayerMask.value == (_floorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                //_lastFloorContact = collision.contacts[0].point;
                IsGrounded = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (_floorLayerMask.value == (_floorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = false;
            }
        }


        /// <summary>
        ///     Method allows to add a force (in global coordinates system) to a module
        /// </summary>
        /// <param name="acceleration">Force must be from [0..1] range</param>
        public void AddAcceleration(Vector3 acceleration)
        {
            if (acceleration.magnitude > 1.00000001)
            {
                throw new ApplicationException("Force must be from [0..1] range.");
            }
            if (IsGrounded)
            {
                Rigidbody.AddForce(acceleration * Acceleration, ForceMode.Acceleration);
            }
            else if (_isAfterJump)
            {
                Rigidbody.AddTorque(Vector3.Cross(Vector3.up, _globalJumpDirection) * JumpRotationForce,
                    ForceMode.Force);
            }
        }
    }
}
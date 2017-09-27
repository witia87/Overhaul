using System;
using Assets.Units.Helpers;
using UnityEngine;

namespace Assets.Units.Modules
{
    /// <summary>
    ///     Module class responsible for maintaining internal integrity of the module.
    ///     Manages physics that does not rely on logic decisions.
    /// </summary>
    public class MovementModule : Module
    {
        private readonly AngleCalculator _angleCalculator = new AngleCalculator();
        private ConfigurableJoint _configurableJoint;

        public float DragAirborn = 1;
        public float DragGrounded = 20;
        public float DragStandingForward = 5;
        public float DragPenaltyStandingBackward = 5;
        public float DragPenaltyStandingSideway = 10;
        public float JumpCooldown = 0.5f;

        protected override void Awake()
        {
            base.Awake();
            Rigidbody.centerOfMass = new Vector3(0, 0.4f, 0);
            _configurableJoint = GetComponent<ConfigurableJoint>();
        }

        [SerializeField] private LayerMask _standingLayerMask;

        public bool IsStanding
        {
            get { return GetIsStanding(); }
        }

        private bool GetIsStanding()
        {
            return IsGrounded && Physics.Raycast(transform.position - transform.up / 2.1f * CrouchModifier, -Vector3.up, 0.1f);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _configurableJoint.anchor = new Vector3(0, 0.5f * CrouchModifier, 0);
            _configurableJoint.connectedAnchor = new Vector3(0, -0.5f * CrouchModifier, 0);

            AddDrag();
        }

        private void AddDrag()
        {
            float drag;
            if (IsStanding)
            {
                var logicVelocity = Rigidbody.velocity;
                logicVelocity.y = 0;
                logicVelocity.Normalize();
                var angle = _angleCalculator.CalculateLogicAngle(ModuleLogicDirection, logicVelocity);
                if (angle < 90)
                {
                    drag = DragStandingForward + DragPenaltyStandingSideway * (1 - Vector3.Dot(logicVelocity, ModuleLogicDirection));
                }
                else
                {
                    drag = DragStandingForward + DragPenaltyStandingBackward + (DragPenaltyStandingSideway - DragPenaltyStandingBackward) *
                           (1 + Vector3.Dot(logicVelocity, ModuleLogicDirection));
                }

            }
            else if (IsGrounded)
            {
                drag = DragGrounded;
            }
            else
            {
                drag = DragAirborn;
            }

            Rigidbody.velocity = Rigidbody.velocity * (1 - Time.fixedDeltaTime * drag);
        }

        public TargetingModule TargetingModule;
        public float AlignWithTargetingForce = 10;
        public void MangeAligningWithTargetingModule()
        {
            Rigidbody.AddRelativeTorque(
                Vector3.Cross(Vector3.up, transform.InverseTransformDirection(TargetingModule.transform.up)) *
                AlignWithTargetingForce, ForceMode.Force);
        }

        /// <summary>
        ///     Method allows to add a force (in global coordinates system) to a module
        /// </summary>
        /// <param name="acceleration">Force must be from [0..1] range</param>
        public void AddAcceleration(Vector3 acceleration)
        {
            if (acceleration.magnitude > 1.00001)
            {
                throw new ApplicationException("Force must be from [0..1] range.");
            }
            if (IsGrounded)
            {
                Rigidbody.AddForce(acceleration * Acceleration, ForceMode.Acceleration);
            }
        }
    }
}
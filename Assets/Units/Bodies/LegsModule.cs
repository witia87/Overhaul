using UnityEngine;

namespace Assets.Units.Modules
{
    /// <summary>
    ///     Module class responsible for maintaining internal integrity of the module.
    ///     On itself, manages physics that does not rely on logic decisions.
    ///     Exposes basic control methods to the external modules;
    /// </summary>
    public class LegsModule : DynamicModule
    {
        private bool _isStanding = true;

        public float DragPenaltyStandingBackward = 5;
        public float JumpCooldown = 0.5f;

        public bool IsStanding
        {
            get { return _isStanding; }
        }

        protected override void Awake()
        {
            base.Awake();
            Rigidbody.centerOfMass = new Vector3(0, 0.3f, 0);
        }

        private bool GetIsStanding()
        {
            return IsGrounded && Physics.Raycast(
                       transform.position - transform.up * CrouchHelper.CurrentHeight / 2.1f,
                       Vector3.down, 0.1f, FloorLayerMask);
        }

        protected override void Update()
        {
            base.Update();
            _isStanding = GetIsStanding();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            AddDrag();
        }

        private void AddDrag()
        {
            if (IsStanding)
            {
                var logicVelocity = Rigidbody.velocity;
                logicVelocity.y = 0;
                var logicDirection = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
                logicVelocity.Normalize();
                var dot = Vector3.Dot(logicVelocity, logicDirection);
                var drag = (0.5f - dot / 2) * DragPenaltyStandingBackward;
                Rigidbody.velocity = Rigidbody.velocity * (1 - Time.fixedDeltaTime * drag);
            }
        }

        /// <summary>
        ///     Method allows to add a force (in global coordinates system) to a module
        /// </summary>
        /// <param name="direction">Force must be from [0..1] range</param>
        public void Move(Vector3 direction)
        {
            if (IsStanding)
            {
                Rigidbody.AddForce(direction * MovingForce, ForceMode.Force);
            }
        }
    }
}
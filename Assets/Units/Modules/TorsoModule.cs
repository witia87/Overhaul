using System;
using Assets.Units.Guns;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class TorsoModule : DynamicModule
    {
        private ConfigurableJoint _legsConfigurableJoint;

        [SerializeField] private LegsModule _legsToMount; // TODO: temporary
        protected Gun Gun;

        public Vector3 GunSlotPosition = new Vector3(0.2f, -0.2f, 0.2f);
        public Vector3 GunSlotPositionCrouched = new Vector3(0.2f, 0.1f, 0.2f);
        public LegsModule Legs { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Gun = GetComponentInChildren<Gun>();
            Rigidbody.centerOfMass = new Vector3(0, 0.5f, 0.1f);
            IsGrounded = false;
            _legsConfigurableJoint = GetComponent<ConfigurableJoint>();
            MountLegs(_legsToMount);
        }

        protected override void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (Legs != null)
            {
                SetupLegsJoint();
            }
        }

        public void MountLegs(LegsModule legs)
        {
            if (Legs != null)
            {
                throw new ApplicationException("Legs already mounted");
            }
            Legs = legs;
            _legsConfigurableJoint.connectedBody = Legs.Rigidbody;
            SetupLegsJoint();
        }

        private void SetupLegsJoint()
        {
            _legsConfigurableJoint.anchor = new Vector3(0, -CrouchHelper.CurrentHeight / 2, 0);
            _legsConfigurableJoint.connectedAnchor = new Vector3(0, Legs.CrouchHelper.CurrentHeight / 2, 0);
        }

        public void Move(Vector3 direction)
        {
            if (Legs.IsStanding)
            {
                Rigidbody.AddForce(direction * MovingForce, ForceMode.Force);
            }
        }
    }
}
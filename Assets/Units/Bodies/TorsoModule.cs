using Assets.Units.Guns;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class TorsoModule : DynamicModule
    {
        private const float AimAngleTolerance = Mathf.PI/6;
        [SerializeField] private Gun _gun;

        [SerializeField] private float _gunHandlingAcceleration = 10;
        private LegsModule _legs;
        private ConfigurableJoint _legsConfigurableJoint;

        public Vector3 GunSlotPosition = new Vector3(0.2f, -0.2f, 0.2f);
        public Vector3 GunSlotPositionCrouched = new Vector3(0.2f, 0.1f, 0.2f);

        protected override void Awake()
        {
            base.Awake();
            if(_gun != null) _gun.MountOnto(this);
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
            if (_legs != null)
            {
                SetupLegsJoint();
            }
        }

        public void SetupLegs(LegsModule legs)
        {
            _legsConfigurableJoint = GetComponent<ConfigurableJoint>();
            _legs = legs;
            _legsConfigurableJoint.connectedBody = _legs.Rigidbody;
            SetupLegsJoint();
        }

        private void SetupLegsJoint()
        {
            _legsConfigurableJoint.anchor = new Vector3(0, -CrouchHelper.CurrentHeight / 2, 0);
            _legsConfigurableJoint.connectedAnchor = new Vector3(0, _legs.CrouchHelper.CurrentHeight / 2, 0);
        }

        public void Move(Vector3 direction)
        {
            if (_legs.IsStanding)
            {
                Rigidbody.AddForce(direction * MovingForce, ForceMode.Force);
            }
        }

        public void AimAt(Vector3 globaldirection)
        {
            if (_gun == null) return;
            var localDirection =
                _gun.transform.InverseTransformDirection(
                    GetTrimmedAimDirection(globaldirection));

            var torque = Vector3.Cross(Vector3.forward, localDirection);
            _gun.Rigidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * _gunHandlingAcceleration *
                                             StunModifier);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if(_gun != null) HandleGun();
        }

        protected void HandleGun()
        {
            const float gunAngularDrag = 5;
            var localAngularVelocity =
                _gun.transform.InverseTransformDirection(_gun.Rigidbody.angularVelocity);
            localAngularVelocity.x = localAngularVelocity.x * (1 - Time.fixedDeltaTime * gunAngularDrag);
            _gun.Rigidbody.angularVelocity = _gun.transform.TransformDirection(localAngularVelocity);
        }

        private Vector3 GetTrimmedAimDirection(Vector3 globalDirection)
        {
            var localTargetingDirection = transform.InverseTransformDirection(globalDirection);
            if (Vector3.Angle(Vector3.forward, localTargetingDirection) < AimAngleTolerance)
            {
                return globalDirection;
            }

            var trimmedLocalTargetingDirection =
                Vector3.RotateTowards(Vector3.forward, localTargetingDirection, AimAngleTolerance, 0);
            return transform.TransformDirection(trimmedLocalTargetingDirection);
        }
    }
}
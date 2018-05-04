using Assets.Environment.Guns;
using Assets.Environment.Units.Vision;
using UnityEngine;

namespace Assets.Environment.Units.Bodies
{
    public class TorsoModule : BodyModule
    {
        private float _aimAngleTolerance = Mathf.PI / 3;

        [SerializeField] private float _gunHandlingAcceleration = 10;
        private LegsModule _legs;
        private ConfigurableJoint _legsConfigurableJoint;

        public Vector3 GunSlotPosition = new Vector3(0.2f, -0.2f, 0.2f);
        public Vector3 GunSlotPositionCrouched = new Vector3(0.2f, 0.1f, 0.2f);

        public VisionSensor VisionSensor;
        public Gun Gun { get; private set; }

        protected override void Awake()
        {
            _aimAngleTolerance = Mathf.Deg2Rad * GetComponent<ConfigurableJoint>().highAngularXLimit.limit;
            base.Awake();
            VisionSensor = GetComponentInChildren<VisionSensor>();
            Gun = GetComponentInChildren<Gun>();
            if (Gun != null)
            {
                Gun.MountOnto(this);
            }
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
            if (Gun == null)
            {
                return;
            }

            var localDirection =
                Gun.transform.InverseTransformDirection(
                    GetTrimmedAimDirection(globaldirection));

            var torque = Vector3.Cross(Vector3.forward, localDirection);
            Gun.Rigidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * _gunHandlingAcceleration *
                                            Unit.StunModifier);
        }

        protected virtual void FixedUpdate()
        {
            if (Gun != null)
            {
                HandleGun();
            }
        }

        protected void HandleGun()
        {
            const float gunAngularDrag = 5;
            var localAngularVelocity =
                Gun.transform.InverseTransformDirection(Gun.Rigidbody.angularVelocity);
            localAngularVelocity.x = localAngularVelocity.x * (1 - Time.fixedDeltaTime * gunAngularDrag);
            Gun.Rigidbody.angularVelocity = Gun.transform.TransformDirection(localAngularVelocity);
        }

        private Vector3 GetTrimmedAimDirection(Vector3 globalDirection)
        {
            var localTargetingDirection = transform.InverseTransformDirection(globalDirection);
            if (Vector3.Angle(Vector3.forward, localTargetingDirection) < _aimAngleTolerance)
            {
                return globalDirection;
            }

            var trimmedLocalTargetingDirection =
                Vector3.RotateTowards(Vector3.forward, localTargetingDirection, _aimAngleTolerance, 0);
            return transform.TransformDirection(trimmedLocalTargetingDirection);
        }
    }
}
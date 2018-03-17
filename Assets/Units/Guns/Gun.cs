using Assets.Units.Guns.Bullets;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Units.Guns
{
    public class Gun : Module, IGunControl
    {
        private readonly Vector2 _efectiveRange = new Vector2(10, 15);
        [SerializeField] private float _angleTolerance;
        private BulletsFactory _bulletsFactory;

        private Vector3 _globalPositionToAimAt;

        private bool _isSetToFire;

        [SerializeField] private readonly int _maxAmmo = 30;
        [SerializeField] private readonly float _refreshTime = 0.2f;

        private float _timeLeft;

        private bool _wasSetToFire;

        public float AngularAccelerationLookAtX = 1;
        public float AngularAccelerationLookAtY = 1;

        protected ConfigurableJoint ConfigurableJoint;
        protected Rigidbody Ririgidbody;

        protected TorsoModule TorsoModule;

        public void Fire()
        {
            _wasSetToFire = true;
        }

        public void AimAt(Vector3 globalPosition)
        {
            _globalPositionToAimAt = globalPosition;
        }

        public int MaxAmmo
        {
            get { return _maxAmmo; }
        }

        public int AmmoLeft { get; private set; }

        public Vector2 EfectiveRange
        {
            get { return _efectiveRange; }
        }

        public Vector3 FirePosition
        {
            get { return _bulletsFactory.transform.position; }
        }

        public Vector3 Direction
        {
            get { return transform.forward; }
        }

        private void Awake()
        {
            TorsoModule = transform.parent.GetComponent<TorsoModule>();
            Ririgidbody = GetComponent<Rigidbody>();
            _bulletsFactory = GetComponentInChildren<BulletsFactory>();
            ConfigurableJoint = GetComponent<ConfigurableJoint>();
            AmmoLeft = _maxAmmo;
        }

        private void FixedUpdate()
        {
            var localDirection =
                transform.InverseTransformDirection(
                    GetTrimmedDirection((_globalPositionToAimAt - transform.position).normalized));

            var torque = Vector3.Cross(Vector3.forward, localDirection);
            Ririgidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * AngularAccelerationLookAtX * StunModifier);
            Ririgidbody.AddRelativeTorque(new Vector3(0, torque.y, 0) * AngularAccelerationLookAtY * StunModifier);
        }

        private Vector3 GetTrimmedDirection(Vector3 globalDirection)
        {
            var localTargetingDirection = TorsoModule.transform.InverseTransformDirection(globalDirection);
            if (Vector3.Angle(Vector3.forward, localTargetingDirection) < _angleTolerance) return globalDirection;
            var trimmedLocalTargetingDirection =
                Vector3.RotateTowards(Vector3.forward, localTargetingDirection, _angleTolerance, 0);
            return TorsoModule.transform.TransformDirection(trimmedLocalTargetingDirection);
        }

        private void Update()
        {
            _timeLeft = Mathf.Max(0, _timeLeft - Time.deltaTime);
            if (_isSetToFire && _timeLeft <= 0)
            {
                _timeLeft = _refreshTime;
                _bulletsFactory.Create();
            }
        }

        private void LateUpdate()
        {
            _isSetToFire = _wasSetToFire;
            _wasSetToFire = false;
        }

        public void SetFire(bool isSetToFire)
        {
            _isSetToFire = isSetToFire;
        }

        protected void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.green;
                //Gizmos.DrawLine(transform.position + ModuleLogicDirection / 2,
                //    transform.position + ModuleLogicDirection * 15);
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
            }
        }

        public virtual void SetSlotPosition(Vector3 position)
        {
            ConfigurableJoint.connectedAnchor = position;
        }
    }
}
using Assets.Units.Guns.Bullets;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Units.Guns
{
    public class Gun : MonoBehaviour, IGunControl
    {
        [SerializeField] private float _angleTolerance;

        private float _timeLeft;
        [SerializeField] private float _refreshTime = 0.2f;
        private BulletsFactory _bulletsFactory;

        protected TargetingModule TargetingModule;
        protected Rigidbody Ririgidbody;
        protected Unit Unit;

        public float StunResistanceTime = 15;
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
            get
            {
                return Mathf.Max(0, 1 - StunTimeLeft / StunResistanceTime);
            }
        }
        public bool IsStuned { get { return StunModifier <= 0; } }

        private void Awake()
        {
            Unit = transform.root.GetComponent<Unit>();
            TargetingModule = transform.parent.GetComponent<TargetingModule>();
            Ririgidbody = GetComponent<Rigidbody>();
            _bulletsFactory = GetComponentInChildren<BulletsFactory>();
            ConfigurableJoint = GetComponent<ConfigurableJoint>();
            AmmoLeft = _maxAmmo;
        }

        public float AngularAccelerationLookAtX = 1;
        public float AngularAccelerationLookAtY = 1;
        private void FixedUpdate()
        {
            var localDirection = transform.InverseTransformDirection(GetTrimmedDirection((_globalPositionToAimAt - transform.position).normalized));

            var torque = Vector3.Cross(Vector3.forward, localDirection);
            Ririgidbody.AddRelativeTorque(new Vector3(torque.x, 0, 0) * AngularAccelerationLookAtX * StunModifier);
            Ririgidbody.AddRelativeTorque(new Vector3(0, torque.y, 0) * AngularAccelerationLookAtY * StunModifier);
        }

        private Vector3 GetTrimmedDirection(Vector3 globalDirection)
        {
            var localTargetingDirection = TargetingModule.transform.InverseTransformDirection(globalDirection);
            if (Vector3.Angle(Vector3.forward, localTargetingDirection) < _angleTolerance)
            {
                return globalDirection;
            }
            var trimmedLocalTargetingDirection = Vector3.RotateTowards(Vector3.forward, localTargetingDirection, _angleTolerance, 0);
            return TargetingModule.transform.TransformDirection(trimmedLocalTargetingDirection);
        }

        void Update()
        {
            _timeLeft = Mathf.Max(0, _timeLeft - Time.deltaTime);
            if (_isSetToFire && _timeLeft <= 0)
            {
                _timeLeft = _refreshTime;
                _bulletsFactory.Create();
            }
        }

        void LateUpdate()
        {
            _isSetToFire = _wasSetToFire;
            _wasSetToFire = false;
        }

        private bool _wasSetToFire = false;
        public void Fire()
        {
            _wasSetToFire = true;
        }



        private Vector3 _globalPositionToAimAt;
        public void AimAt(Vector3 globalPosition)
        {
            _globalPositionToAimAt = globalPosition;
        }

        private bool _isSetToFire;
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

        protected ConfigurableJoint ConfigurableJoint;

        public virtual void SetSlotPosition(Vector3 position)
        {
            ConfigurableJoint.connectedAnchor = position;
        }


        private readonly int _maxAmmo = 30;
        public int MaxAmmo
        {
            get { return _maxAmmo; }
        }

        public int AmmoLeft { get; private set; }


        private readonly Vector2 _efectiveRange = new Vector2(10, 15);
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
    }
}

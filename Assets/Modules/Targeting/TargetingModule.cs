using Assets.Modules.Movement;
using Assets.Modules.Targeting.Guns;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Targeting
{
    public class TargetingModule : Module, ITargetingControl
    {
        private bool _isTargetDirectionSet;
        private float _velocity;

        [SerializeField] private VisionSensor _visionSensor;

        public GunSensor GunSensor;

        public MovementModule MovementModule;

        public Vector3 SightLocalOffset = new Vector3(0, 0.5f, 1);
        public float SmoothTime = 0.2f;
        public Vector3 TargetGlobalDirection;

        public Vector3 SightPosition
        {
            get { return gameObject.transform.TransformPoint(SightLocalOffset); }
        }

        public IVisionSensor VisionSensor
        {
            get { return _visionSensor; }
        }

        public Vector3 TargetingDirection
        {
            get { return gameObject.transform.forward; }
        }

        public bool IsGunMounted
        {
            get { return Gun != null; }
        }

        public IGunControl Gun { get; private set; }

        public void TurnTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            globalDirection.Normalize();
            _isTargetDirectionSet = true;
            TargetGlobalDirection = globalDirection;
        }

        public void LookAt(Vector3 point)
        {
            TurnTowards(point - gameObject.transform.position);
        }

        public void DropGun()
        {
            var gunSlot = gameObject.GetComponent<GunSlot>();
            if (gunSlot && gunSlot.IsModuleMounted)
            {
                var module = gunSlot.MountedModule;
                module.gameObject.transform.position = module.gameObject.transform.position +
                                                       gameObject.transform.forward;
                gunSlot.UnmountModule();
                Gun = null;
                module.Rigidbody.AddForce(gameObject.transform.forward*15f, ForceMode.Impulse);
            }
        }

        public void PickGun()
        {
            if (GunSensor.IsGunVisible())
            {
                var gun = GunSensor.GetClosestGun();
                var gunSlot = gameObject.GetComponent<GunSlot>();
                if (!gunSlot.IsModuleMounted)
                {
                    gunSlot.MountModule(gun);
                    Gun = gun;
                    return;
                }
            }
            Gun = GetComponentInChildren<GunModule>();
        }

        protected override void Awake()
        {
            base.Awake();
            Gun = GetComponentInChildren<GunModule>();
        }

        private void FixedUpdate()
        {
            if (_isTargetDirectionSet)
            {
                var targetGlobalDirection = TargetGlobalDirection;
                if (Vector3.Angle(TargetGlobalDirection, MovementModule.UnitDirection) > 85)
                {
                    targetGlobalDirection = Vector3.RotateTowards(MovementModule.UnitDirection, TargetGlobalDirection,
                        Mathf.Deg2Rad*85, 0);
                }

                var angle = Vector3.Angle(targetGlobalDirection, TargetingDirection);
                if (Vector3.Dot(gameObject.transform.right, targetGlobalDirection) < 0)
                {
                    angle *= -1;
                }
                var angleToTurn = Mathf.SmoothDampAngle(0, angle, ref _velocity, SmoothTime);
                gameObject.transform.forward = Quaternion.Euler(0, angleToTurn, 0)*gameObject.transform.forward;
                if (Mathf.Abs(angle) < 1)
                {
                    _isTargetDirectionSet = false;
                    _velocity = 0;
                }
            }
        }

        private void OnDrawGizmos()
        {
            DrawArrow.ForGizmo(gameObject.transform.position, gameObject.transform.forward*2, Color.red);
        }
    }
}
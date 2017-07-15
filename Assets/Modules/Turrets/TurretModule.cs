using Assets.Modules.Movement;
using Assets.Modules.Turrets.Guns;
using Assets.Modules.Turrets.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public class TurretModule : Module, ITurretControl
    {
        private bool _isTargetDirectionSet;
        private float _velocity;

        [SerializeField] private VisionSensor _visionSensor;
        public GunModule Gun;

        public GunSensor GunSensor;

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

        public Vector3 TurretDirection
        {
            get { return gameObject.transform.forward; }
        }

        public bool AreGunControlsMounted
        {
            get { return GunControls.Length > 0; }
        }

        public IGunControl[] GunControls { get; private set; }

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
            var gunSlots = gameObject.GetComponents<GunSlot>();
            foreach (var gunSlot in gunSlots)
            {
                if (gunSlot.IsModuleMounted)
                {
                    var module = gunSlot.MountedModule;
                    module.gameObject.transform.position = module.gameObject.transform.position +
                                                           gameObject.transform.forward;
                    gunSlot.UnmountModule();
                    module.Rigidbody.AddForce(gameObject.transform.forward*15f, ForceMode.Impulse);
                }
            }
        }

        public void PickGun()
        {
            if (GunSensor.IsGunVisible())
            {
                var gun = GunSensor.GetClosestGun();
                var gunSlots = gameObject.GetComponents<GunSlot>();
                foreach (var gunSlot in gunSlots)
                {
                    if (!gunSlot.IsModuleMounted)
                    {
                        gunSlot.MountModule(gun);
                        return;
                    }
                }
            }
            ScanGunControls();
        }

        protected override void Awake()
        {
            base.Awake();
            ScanGunControls();
        }

        private void ScanGunControls()
        {
            var gunModules = GetComponentsInChildren<GunModule>();
            GunControls = new IGunControl[gunModules.Length];
            for (var i = 0; i < gunModules.Length; i++)
            {
                GunControls[i] = gunModules[i];
            }
        }

        public MovementModule MovementModule;
        private void FixedUpdate()
        {
            if (_isTargetDirectionSet)
            {
                var targetGlobalDirection = TargetGlobalDirection;
                if (Vector3.Angle(TargetGlobalDirection, MovementModule.UnitDirection) > 85)
                {
                    targetGlobalDirection = Vector3.RotateTowards(MovementModule.UnitDirection, TargetGlobalDirection,
                        Mathf.Deg2Rad * 85, 0);
                }

                var angle = Vector3.Angle(targetGlobalDirection, TurretDirection);
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
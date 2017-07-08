using System.Collections.Generic;
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

        public List<IGunControl> GunControls { get; private set; }

        public bool AreGunControlsMounted
        {
            get { return GunControls.Count > 0; }
        }

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
                    module.Rigidbody.AddForce(gameObject.transform.forward * 50f, ForceMode.Impulse);
                }
            }
        }

        public GunSensor GunSensor;
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
        }

        private void FixedUpdate()
        {
            if (_isTargetDirectionSet)
            {
                var angle = Vector3.Angle(TargetGlobalDirection, TurretDirection);
                if (Vector3.Dot(gameObject.transform.right, TargetGlobalDirection) < 0)
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
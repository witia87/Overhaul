using System.Collections.Generic;
using Assets.Cores;
using Assets.Modules.Vision;
using Assets.Pojectiles.Bullets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public class TurretModule : Module, ITurretControl
    {
        public float SmoothTime = 0.2f;

        private bool _isSetToFire;
        private bool _isTargetDirectionSet;

        private BulletComponentFactory _leftBulletComponentFactory;
        private BulletComponentFactory _rightBulletComponentFactory;

        private bool _shouldLeftShoot = true;
        public Vector3 TargetGlobalDirection;
        private float _velocity;

        public float Cooldown = 1;

        public Vector3 SightLocalOffset = new Vector3(0, 0.5f, 1);

        public VisionSensor VisionSensor;

        public Vector3 SightDirection
        {
            get { return gameObject.transform.forward; }
        }

        public Vector3 SightPosition
        {
            get { return gameObject.transform.TransformPoint(SightLocalOffset); }
        }


        public float CooldownTime
        {
            get { return Cooldown; }
        }

        public float CooldownTimeLeft { get; private set; }

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

        public void Fire()
        {
            _isSetToFire = true;
        }

        public List<GameObject> VisibleGameObjects
        {
            get { return VisionSensor.VisibleGameObjects; }
        }

        public List<Core> VisibleCores
        {
            get { return VisionSensor.VisibleCores; }
        }

        private void FixedUpdate()
        {
            if (_isTargetDirectionSet)
            {
                var angle = Vector3.Angle(TargetGlobalDirection, SightDirection);
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

            CooldownTimeLeft = Mathf.Max(0, CooldownTimeLeft -= Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (_isSetToFire && CooldownTimeLeft <= 0)
            {
                CooldownTimeLeft = CooldownTime;
                if (_shouldLeftShoot)
                {
                    _leftBulletComponentFactory.Create();
                }
                else
                {
                    _rightBulletComponentFactory.Create();
                }
                _shouldLeftShoot = !_shouldLeftShoot;
                _isSetToFire = false;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _leftBulletComponentFactory = new BulletComponentFactory(gameObject, new Vector3(-0.5f, 0.75f, 2),
                new Vector3(0, 0, 1));
            _rightBulletComponentFactory = new BulletComponentFactory(gameObject, new Vector3(0.5f, 0.75f, 2),
                new Vector3(0, 0, 1));
        }

        private void OnDrawGizmos()
        {
            if (_isTargetDirectionSet)
            {
               // DrawArrow.ForGizmo(gameObject.transform.position, TargetGlobalDirection * 2, Color.red);
            }
            DrawArrow.ForGizmo(gameObject.transform.position, gameObject.transform.forward * 2, Color.red);
        }
    }
}
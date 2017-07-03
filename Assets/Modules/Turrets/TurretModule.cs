﻿using System.Collections.Generic;
using Assets.Modules.Turrets.Guns;
using Assets.Modules.Turrets.Guns.Bullets;
using Assets.Modules.Turrets.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public class TurretModule : Module, ITurretControl
    {
        public GunModule Gun;

        public Vector3 SightLocalOffset = new Vector3(0, 0.5f, 1);
        public float SmoothTime = 0.2f;
        public Vector3 TargetGlobalDirection;

        [SerializeField] private VisionSensor _visionSensor;
        public IVisionSensor VisionSensor { get {  return _visionSensor;} }
        
        public Vector3 TurretDirection
        {
            get { return gameObject.transform.forward; }
        }

        public Vector3 SightPosition
        {
            get { return gameObject.transform.TransformPoint(SightLocalOffset); }
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

        bool _isTargetDirectionSet;
        float _velocity;
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
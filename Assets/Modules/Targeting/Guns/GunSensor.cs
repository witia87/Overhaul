using System;
using Assets.Modules.Targeting.Vision;
using UnityEngine;

namespace Assets.Modules.Targeting.Guns
{
    public class GunSensor : DiamondDetector
    {
        [Range(0.1f, Mathf.PI/2)] public float HorizontalAngleTolerance = Mathf.PI/3;

        [Range(0.1f, Mathf.PI/2)] public float VerticalAngleTolerance = Mathf.PI/4;

        [Range(0.1f, 100)] public float VisionLenght = 10;

        public Vector3 SightPosition
        {
            get { return transform.position; }
        }

        public bool IsGunVisible()
        {
            foreach (var collidingGameObject in CollidingGameObjects)
            {
                if (collidingGameObject.tag == "Gun")
                {
                    return true;
                }
            }
            return false;
        }

        public GunModule GetClosestGun()
        {
            foreach (var collidingGameObject in CollidingGameObjects)
            {
                if (collidingGameObject.tag == "Gun")
                {
                    return collidingGameObject.GetComponent<GunModule>();
                }
            }
            throw new ApplicationException("No guns in present.");
        }

        protected override void Awake()
        {
            Length = VisionLenght;
            Width = Mathf.Tan(HorizontalAngleTolerance)*VisionLenght;
            Height = Mathf.Tan(VerticalAngleTolerance)*VisionLenght;
            base.Awake();
        }
    }
}
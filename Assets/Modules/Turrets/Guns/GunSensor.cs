using System;
using System.Collections.Generic;
using Assets.Modules.Turrets.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    public class GunSensor : DiamondDetector
    {
        [Range(0.1f, Mathf.PI/2)] public float HorizontalAngleTolerance = Mathf.PI/3;

        [Range(0.1f, Mathf.PI/2)] public float VerticalAngleTolerance = Mathf.PI/4;

        [Range(0.1f, 100)] public float VisionLenght = 10;

        public Vector3 SightPosition
        {
            get { return gameObject.transform.position; }
        }

        public bool IsGunVisible()
        {
            foreach (var collidingGameObject in CollidingGameObjects)
            {
                if (collidingGameObject.layer == Layers.Guns)
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
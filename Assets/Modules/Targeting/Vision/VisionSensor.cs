using System.Collections.Generic;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public class VisionSensor : DiamondDetector, IVisionSensor
    {
        [SerializeField] private LayerMask _layerMask;

        [Range(0.1f, Mathf.PI/2)] public float HorizontalAngleTolerance = Mathf.PI/3;
        public string TagToDetect = "Player";

        [Range(0.1f, Mathf.PI/2)] public float VerticalAngleTolerance = Mathf.PI/4;

        [Range(0.1f, 100)] public float VisionLenght = 10;

        public Vector3 SightDirection { get { return gameObject.transform.forward; } }

        public Vector3 SightPosition
        {
            get { return gameObject.transform.position; }
        }
        
        public List<Unit> VisibleUnits { get; private set; }

        protected override void Awake()
        {
            Length = VisionLenght;
            Width = Mathf.Tan(HorizontalAngleTolerance)*VisionLenght;
            Height = Mathf.Tan(VerticalAngleTolerance)*VisionLenght;
            VisibleUnits = new List<Unit>();
            base.Awake();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagToDetect)
            {
                CollidingGameObjects.Add(other.gameObject);
            }
        }

        public void Update()
        {
            VisibleUnits = new List<Unit>();
            foreach (var collidingObject in CollidingGameObjects)
            {
                var ray = collidingObject.transform.position + Vector3.up/2 - gameObject.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, ray, out hit, VisionLenght, _layerMask)
                    && collidingObject.transform.root == hit.transform.root)
                {
                    var unit = collidingObject.transform.root.GetComponent<Unit>();
                    if (unit != null)
                    {
                        VisibleUnits.Add(unit);
                    }

                    DrawArrow.ForDebug(gameObject.transform.position,
                        ray, Color.green, 0.1f, 0);
                }
                else
                {
                    DrawArrow.ForDebug(gameObject.transform.position,
                        ray, Color.red, 0.1f, 0);
                }
            }
        }
    }
}
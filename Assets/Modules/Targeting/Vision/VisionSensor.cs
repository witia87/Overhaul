using System.Collections.Generic;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public class VisionSensor : DiamondDetector, IVisionSensor
    {
        [SerializeField] private LayerMask _layerMask;

        [Range(0.1f, Mathf.PI / 2)] public float HorizontalAngleTolerance = Mathf.PI / 3;
        public string TagToDetect = "Player";

        [Range(0.1f, Mathf.PI / 2)] public float VerticalAngleTolerance = Mathf.PI / 4;

        [Range(0.1f, 100)] public float VisionLenght = 10;

        public Vector3 SightDirection
        {
            get { return gameObject.transform.forward; }
        }

        public Vector3 SightPosition
        {
            get { return gameObject.transform.position; }
        }

        [SerializeField] private float _targetRememberingTime = 10;
        private readonly List<Target> _targetsArchive = new List<Target>();
        private readonly List<float> _targetsArchivisionTime = new List<float>();
        public List<ITarget> VisibleTargets { get; private set; }

        protected override void Awake()
        {
            Length = VisionLenght;
            Width = Mathf.Tan(HorizontalAngleTolerance) * VisionLenght;
            Height = Mathf.Tan(VerticalAngleTolerance) * VisionLenght;
            VisibleTargets = new List<ITarget>();
            base.Awake();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagToDetect)
                CollidingGameObjects.Add(other.gameObject);
        }

        public void Update()
        {
            VisibleTargets.Clear();

            foreach (var collidingObject in CollidingGameObjects)
            {
                var ray = collidingObject.transform.position + Vector3.up / 2 - gameObject.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, ray, out hit, VisionLenght, _layerMask)
                    && collidingObject.transform.root == hit.transform.root)
                {
                    var unit = collidingObject.transform.root.GetComponent<Unit>();
                    if (unit != null)
                    {
                        var target = FindInArchiveOrCreateTarget(unit);
                        target.TargetLastSeenTime = Time.time;
                        target.LastSeenPosition = unit.gameObject.transform.position;
                        target.LastSeenMovementDirection = unit.Rigidbody.velocity.magnitude > 0.01
                            ? unit.Rigidbody.velocity.normalized
                            : Vector3.zero;
                        VisibleTargets.Add(target);
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

        private Target FindInArchiveOrCreateTarget(Unit unit)
        {
            for (var i = 0; i < _targetsArchive.Count; i++)
            {
                if (_targetsArchive[i].Unit == unit)
                {
                    _targetsArchivisionTime[i] = Time.time;
                    return _targetsArchive[i];
                }
                if (_targetsArchivisionTime[i] < Time.time - _targetRememberingTime)
                {
                    _targetsArchive.RemoveAt(i);
                    _targetsArchivisionTime.RemoveAt(i);
                    i--;
                }
            }
                
            _targetsArchive.Add(new Target(unit));
            _targetsArchivisionTime.Add(Time.time);
            return _targetsArchive[_targetsArchive.Count - 1];
        }
    }
}
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

        private readonly List<Target> _visibleTargets = new List<Target>();
        public int VisibleTargetsCount { get { return _visibleTargets.Count; } }

        public ITarget GetClosestTarget()
        {
            return _visibleTargets[0];
        }


        protected override void Awake()
        {
            Length = VisionLenght;
            Width = Mathf.Tan(HorizontalAngleTolerance) * VisionLenght;
            Height = Mathf.Tan(VerticalAngleTolerance) * VisionLenght;
            base.Awake();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagToDetect)
                CollidingGameObjects.Add(other.gameObject);
        }

        public void Update()
        {
            foreach (var visibleTarget in _visibleTargets)
            {
                visibleTarget.IsVisible = false;
            }
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
                        UpdateOrAddTarget(unit);
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

            RemoveNotVisibleTargets();
        }

        private void UpdateOrAddTarget(Unit unit)
        {
            for (var i = 0; i < _visibleTargets.Count; i++)
            {
                if (_visibleTargets[i].Unit == unit)
                {
                    _visibleTargets[i].IsVisible = true;
                    return;
                }
            }

            _visibleTargets.Add(new Target(unit));
        }

        private void RemoveNotVisibleTargets()
        {
            for (var i = 0; i < _visibleTargets.Count; i++)
            {
                if (!_visibleTargets[i].IsVisible)
                {
                    _visibleTargets[i].Memorize();
                    _visibleTargets.RemoveAt(i);
                    i--;
                }
            }
        }

        readonly Vector3[] _raysToMeasureDistances = {
            new Vector3(1,0,0),
            new Vector3(1,0,1),
            new Vector3(0,0,1),
            new Vector3(-1,0,1),
            new Vector3(-1,0,0),
            new Vector3(-1,0,-1),
            new Vector3(0,0,-1),
            new Vector3(1,0,-1),
        };

        [SerializeField] private LayerMask _wallLayerMask;
        public List<Vector3> GetThreeClosestDirections()
        {
            var distances = new List<float>();
            var directions = new List<Vector3>(_raysToMeasureDistances);
            for (int j = 0; j < 8; j++)
            {
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, _raysToMeasureDistances[j], out hit, _wallLayerMask))
                {
                    distances.Add(hit.distance);
                }
            }

            var i = Mathf.FloorToInt(Random.value * 8);
            while (directions.Count > 3)
            {
                if (distances[i] < distances[(i + 1) % directions.Count])
                {
                    distances.RemoveAt(i);
                    directions.RemoveAt(i);
                }
                i = (i + 1) % directions.Count;
            }
            return directions;
        }
    }
}
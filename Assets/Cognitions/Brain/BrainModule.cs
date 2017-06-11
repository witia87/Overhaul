using System;
using System.Collections.Generic;
using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Map.Nodes;
using Assets.Modules.Movement;
using Assets.Modules.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Brain
{
    public class BrainModule
    {
        protected MovementModule _movementModule;
        private List<INode> _path;
        private IMapStore MapStore;

        protected IPathFinder _pathFinder;
        private GameObject _target;
        private Vector3 _targetLastSeenDirection;
        private Vector3 _targetLastSeenPosition;

        protected IVisionSensor
            _visionSensor;

        private bool _wasTargetSeen;

        public GameObject connectedGameObject;

        public bool IsMovementControlRegistered
        {
            get { return _movementModule != null; }
        }

        public bool IsVisionSensorRegistered
        {
            get { return _visionSensor != null; }
        }

        public void RegisterMovementControl(MovementModule movementModule)
        {
            if (IsMovementControlRegistered)
                throw new ApplicationException("MovementControl already registered within CognitionModule.");
            _movementModule = movementModule;
        }

        public void UnregisterMovementControl()
        {
            if (!IsMovementControlRegistered)
                throw new ApplicationException("No MovementControl registered within CognitionModule.");
            _movementModule = null;
        }

        public void RegisterVisionSensor(IVisionSensor visionSensor)
        {
            if (IsVisionSensorRegistered)
                throw new ApplicationException("VisionSensor already registered within CognitionModule.");
            _visionSensor = visionSensor;
        }

        public void UnregisterVisionSensor()
        {
            if (!IsVisionSensorRegistered)
                throw new ApplicationException("No VisionSensor registered within CognitionModule.");
            _visionSensor = null;
        }

        public void Update()
        {
            if (IsVisionSensorRegistered && IsMovementControlRegistered)
            {
                _target = GetHighestPriorityTarget();
                if (_target != null)
                {
                    _wasTargetSeen = true;
                    _targetLastSeenDirection = _target.GetComponent<Rigidbody>().velocity.normalized;
                    _targetLastSeenPosition = _target.transform.position;
                    _path = _pathFinder.FindPath(connectedGameObject.transform.position,
                        _target.transform.position);
                }
                else if (_wasTargetSeen)
                {
                    //_path = _pathFinder.FindPath(gameObject.transform.position,
                    //    _targetLastSeenPosition + _targetLastSeenDirection * 10);
                    _wasTargetSeen = false;
                }
            }

            if (_path != null)
            {
                for (var i = 0; i < _path.Count - 1; i++)
                {
                    DrawArrow.ForDebug(_path[i].Position + Vector3.up/100,
                        _path[i + 1].Position - _path[i].Position, Color.green, 0.1f, 0);
                }
            }
        }

        private void FixedUpdate()
        {
            INode currentNode;
            MapStore.TryGetNode(connectedGameObject.transform.position, 1, out currentNode);
            if (_path != null)
            {
                var index = _path.LastIndexOf(currentNode);
                if (index >= 0)
                {
                    _path.RemoveRange(0, index + 1);
                }
                if (_path.Count == 0)
                {
                    _path = null;
                }
            }

            if (_path != null)
            {
                var shortcutLength = 0;
                while (shortcutLength < _path.Count - 1 &&
                       MapStore.IsRectangleClear(
                           currentNode, _path[shortcutLength]))
                {
                    shortcutLength++;
                }
                var pathDirection = _path[shortcutLength].Position - connectedGameObject.transform.position;
                pathDirection.Normalize();
                //_movementModule.Walk(pathDirection);
            }

            if (_target != null)
            {
                var pathDirection = _target.transform.position - connectedGameObject.transform.position;
                var angle = Vector3.Angle(pathDirection, _movementModule.UnitDirection);
                if (angle > 15)
                {
                    //_movementModule.TurnTowards(pathDirection);
                }
            }
        }

        protected void Awake()
        {
        }

        private GameObject GetHighestPriorityTarget()
        {
            GameObject highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            foreach (var testedGameObject in _visionSensor.VisibleGameObjects)
            {
                var currentDistance =
                    (testedGameObject.transform.position - connectedGameObject.transform.position).magnitude;
                if (currentDistance < minDistance)
                {
                    highestPriorityTargetSoFar = testedGameObject;
                    minDistance = currentDistance;
                }
            }

            return highestPriorityTargetSoFar;
        }
    }
}
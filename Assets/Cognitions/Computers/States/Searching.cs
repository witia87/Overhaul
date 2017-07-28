using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Searching : ComputerState
    {
        private readonly ITarget _target;
        private List<Vector3> _path;
        private float _initialTime;
        private float _initialDistance;
        private Vector3 _targetPositionPrediction;

        public Searching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITarget target)
            : base(ComputerStateIds.Searching, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
            _initialTime = Time.time;
            _initialDistance = (_target.LastSeenPosition - Unit.gameObject.transform.position).magnitude;
            
            FindPath();
        }

        private void FindPath()
        {
            if (!Map.PathFinder.TryGetClosestAvailablePosition(_target.LastSeenPosition +
                                                               _target.MovementDirectionPrediction * _initialDistance,
                10, out _targetPositionPrediction))
            {
                _targetPositionPrediction = _target.LastSeenPosition;
            }
            _path = Map.PathFinder.FindPath(Unit.gameObject.transform.position, _targetPositionPrediction);
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            Unit.Targeting.Gun.StopFiring();
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Unit.Targeting.VisionSensor.VisibleTargets.Count > 0)
                {
                    var target = TargetingHelper.GetHighestPriorityTarget();
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
                }
            }

            ProbabilisticTriggering.PerformOnAverageOnceEvery(0.5f, FindPath);
            MovementHelper.ManageMovingAlongThePath(_path);
            return this;
        }

        public override void OnDrawGizmos()
        {
            for (var i = 0; i < _path.Count - 1; i++)
            {
                DrawArrow.ForDebug(_path[i] + Vector3.up / 100,
                    _path[i + 1] - _path[i], Color.green, 0.1f, 0);
            }
        }
    }
}
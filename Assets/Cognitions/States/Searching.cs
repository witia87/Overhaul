using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public class Searching : CognitionState
    {
        private readonly float _initialDistance;
        private List<Vector3> _path;
        private readonly ITargetMemory _targetMemory;
        private Vector3 _targetPositionPrediction;

        public Searching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnit unit, IMap map,
            IVisionObserver vision,
            ITargetMemory memory)
            : base(ComputerStateIds.Chasing, movementHelper, targetingHelper, unit, map, vision)
        {
            _targetMemory = memory;
            _initialDistance = (_targetMemory.LastSeenPosition - Unit.LogicPosition).magnitude;

            FindPath();
        }

        private void FindPath()
        {
            if (!Map.PathFinder.TryGetClosestAvailablePosition(_targetMemory.LastSeenPosition +
                                                               _targetMemory.LastSeenVelocity.normalized *
                                                               _initialDistance,
                10, out _targetPositionPrediction))
            {
                _targetPositionPrediction = _targetMemory.LastSeenPosition;
            }

            _path = Map.PathFinder.FindPath(Unit.LogicPosition, _targetPositionPrediction);
        }

        public override CognitionState Update()
        {
            if (Map.ArePositionsOnTheSameTile(Unit.LogicPosition, _path[0]))
            {
                return DisposeCurrent()
                    .AndChangeStateTo(StatesFactory.CreateMoving(_targetMemory.LastSeenPosition));
            }

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Vision.UnitsSpottedByTeam.Count > 0)
                {
                    var target = Vision.GetHighestPriorityTarget();
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
                }
            }

            ProbabilisticTriggering.PerformOnAverageOnceEvery(0.5f, FindPath);
            MovementHelper.ManageMovingAlongThePath(_path);
            Unit.Control.LookAt(_path[0]);

            return this;
        }

        public override void OnDrawGizmos()
        {
            for (var i = 0; i < _path.Count - 1; i++)
            {
                DrawArrow.ForDebug(_path[i] + Vector3.up / 100,
                    _path[i + 1] - _path[i], Color.green, 0.1f, 0);
            }

            Gizmos.DrawSphere(_path[0], 0.25f);
        }
    }
}
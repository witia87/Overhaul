using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Units;
using Assets.Units.Heads.Vision;
using Assets.Utilities;
using Assets.Vision;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Searching : ComputerState
    {
        private readonly float _initialDistance;
        private readonly ITargetMemory _targetMemory;
        private List<Vector3> _path;
        private Vector3 _targetPositionPrediction;

        public Searching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITargetMemory targetMemory)
            : base(ComputerStateIds.Searching, movementHelper, targetingHelper, unit, map)
        {
            _targetMemory = targetMemory;
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

        public override CognitionState<ComputerStateIds> Update()
        {
            if (Map.ArePositionsOnTheSameTile(Unit.LogicPosition, _path[0]))
            {
                return DisposeCurrent()
                    .AndChangeStateTo(StatesFactory.CreateMoving(_targetMemory.LastSeenPosition));
            }

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Unit.Vision.VisibleTargetsCount > 0)
                {
                    var target = Unit.Vision.GetClosestTarget();
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
                }
            }

            ProbabilisticTriggering.PerformOnAverageOnceEvery(0.5f, FindPath);
            MovementHelper.ManageMovingAlongThePath(_path);
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.4f))
            {
                if (ProbabilisticTriggering.TestProbabilisty(0.2f))
                {
                    ChangeDirection();
                }
                else
                {
                    Unit.LookAt(_path[0]);
                }
            }
            return this;
        }

        private void ChangeDirection()
        {
            var directions = Unit.Vision.GetThreeClosestDirections();
            Unit.LookTowards(directions[Mathf.FloorToInt(Random.value * 3)]);
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
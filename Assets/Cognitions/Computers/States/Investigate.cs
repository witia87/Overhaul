using System.Collections.Generic;
using Assets.Map.Nodes;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Investigate : ComputerState
    {
        private readonly Vector3 _targetLastSeenDirection;

        private readonly Vector3 _targetLastSeenPosition;
        private List<INode> _path;

        public Investigate(Cognition<ComputerStateIds> parrentCognition, Vector3 targetLastSeenPosition,
            Vector3 targetLastSeenDirection)
            : base(parrentCognition, ComputerStateIds.Investigate)
        {
            _targetLastSeenPosition = targetLastSeenPosition;
            _targetLastSeenDirection = targetLastSeenDirection;
            if (targetLastSeenDirection.magnitude > 0)
            {
                _targetLastSeenDirection.Normalize();
            }
            _path = PathFinder.FindPath(Unit.gameObject.transform.position,
                _targetLastSeenPosition);
        }

        public override ICognitionState<ComputerStateIds> Update()
        {
            ProbabilisticTriggering.PerformOnAverageOnceEvery(1, () => Unit.Targeting.LookAt(_targetLastSeenPosition));
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.5f))
            {
                if (Unit.Targeting.VisionSensor.VisibleUnits.Count > 0)
                {
                    var target = GetHighestPriorityTarget();
                    return new ChasingEnemy(ParentCognition, target);
                }
            }

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(1))
            {
                _path = PathFinder.FindPath(Unit.gameObject.transform.position,
                    _targetLastSeenPosition);
            }
            ManageMovingAlongThePath(ref _path);

            if (_path == null || _path.Count == 0)
            {
                Dispose();
                return null;
            }

            return this;
        }

        public override void OnDrawGizmos()
        {
            if (_path != null)
            {
                for (var i = 0; i < _path.Count - 1; i++)
                {
                    DrawArrow.ForDebug(_path[i].Position + Vector3.up/100,
                        _path[i + 1].Position - _path[i].Position, Color.green, 0.1f, 0);
                }
            }
        }
    }
}
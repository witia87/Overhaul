using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.States;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public class Moving : CognitionState
    {
        private readonly List<Vector3> _path;
        private readonly Vector3 _targetPositoon;

        public Moving(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnit unit, IMap map,
            IVisionObserver vision, Vector3 targetPosition)
            : base(ComputerStateIds.Chasing, movementHelper, targetingHelper, unit, map, vision)
        {
            _targetPositoon = targetPosition;
            _path = Map.PathFinder.FindPath(Unit.LogicPosition, _targetPositoon);
        }

        public override CognitionState Update()
        {
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Map.ArePositionsOnTheSameTile(Unit.LogicPosition, _path[0]))
                {
                    return DisposeCurrent()
                        .AndChangeStateTo(StatesFactory.CreateWatching(null, 3));
                }

                if (Vision.UnitsSpottedByTeam.Count > 0)
                {
                    var target = Vision.GetHighestPriorityTarget();
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
                }
            }
            
            MovementHelper.ManageMovingAlongThePath(_path);
            Unit.Control.LookAt(_path[0]);
            return this;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            for (var i = 0; i < _path.Count - 1; i++)
            {
                DrawArrow.ForDebug(_path[i] + Vector3.up / 100,
                    _path[i + 1] - _path[i], Color.green, 0.1f, 0);
            }

            Gizmos.DrawSphere(_path[0], 0.25f);
        }
    }
}
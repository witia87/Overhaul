using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Moving : ComputerState
    {
        private readonly List<Vector3> _path;
        private readonly Vector3 _targetPositoon;

        public Moving(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            Vector3 targetPosition)
            : base(ComputerStateIds.Searching, movementHelper, targetingHelper, unit, map)
        {
            _targetPositoon = targetPosition;
            _path = Map.PathFinder.FindPath(Unit.Position, _targetPositoon);
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            Unit.Targeting.Gun.SetFire(false);

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Map.ArePositionsOnTheSameTile(Unit.Position, _path[0]))
                {
                    return DisposeCurrent()
                        .AndChangeStateTo(StatesFactory.CreateWatching(null, 3));
                }

                if (Unit.Targeting.VisionSensor.VisibleTargetsCount > 0)
                {
                    var target = Unit.Targeting.VisionSensor.GetClosestTarget();
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
                }
            }

            //ProbabilisticTriggering.PerformOnAverageOnceEvery(0.5f, FindPath);
            MovementHelper.ManageMovingAlongThePath(_path);
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.5f))
            {
                Unit.Targeting.LookAt(_path[0]);
            }
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
using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public class Strafing : CognitionState
    {
        private List<Vector3> _path;
        private readonly ITarget _target;

        public Strafing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnit unit, IMapGrid map,
            IVisionObserver vision,
            ITarget target)
            : base(ComputerStateIds.Chasing, movementHelper, targetingHelper, unit, map, vision)
        {
            _target = target;
            _path = Map.PathFinder.FindSafespot(Unit.LogicPosition);
        }

        public override CognitionState Update()
        {
            if (!Map.IsPositionDangorous(Unit.LogicPosition))
            {
                return DisposeCurrent().AndReturnToThePreviousState();
            }

            ProbabilisticTriggering.PerformOnAverageOnceEvery(0.3f,
                () => _path = Map.PathFinder.FindSafespot(Unit.LogicPosition));
            MovementHelper.ManageMovingAlongThePath(_path);
            if (_target.IsVisible)
            {
                TargetingHelper.ManageAimingAtTheTarget(_target);
            }

            return this;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (_path != null)
            {
                for (var i = 0; i < _path.Count - 1; i++)
                {
                    DrawArrow.ForDebug(_path[i] + Vector3.up / 100,
                        _path[i + 1] - _path[i], Color.green, 0.1f, 0);
                }
            }
        }
    }
}
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
    public class Chasing : CognitionState
    {
        private List<Vector3> _path;
        private readonly ITarget _target;

        public Chasing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnit unit, IMapGrid map,
            IVisionObserver vision,
            ITarget target)
            : base(ComputerStateIds.Chasing, movementHelper, targetingHelper, unit, map, vision)
        {
            _target = target;
            _path = Map.PathFinder.FindPath(Unit.LogicPosition,
                _target.Position);
        }

        public override CognitionState Update()
        {
            if (Map.IsPositionDangorous(Unit.LogicPosition))
            {
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateStrafing(_target));
            }

            if (_target.IsVisible)
            {
                if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
                {
                    _path = Map.PathFinder.FindPath(Unit.LogicPosition,
                        _target.Position);
                }

                TargetingHelper.ManageAimingAtTheTarget(_target);

                var distanceToTarget = (_target.Position - Unit.LogicPosition).magnitude;
                if (distanceToTarget > 4)
                {
                    MovementHelper.ManageMovingAlongThePath(_path);
                }

                return this;
            }

            return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateSearching(_target));
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            for (var i = 0; i < _path.Count - 1; i++)
            {
                DrawArrow.ForDebug(_path[i] + Vector3.up / 100,
                    _path[i + 1] - _path[i], Color.green, 0.1f, 0);
            }
        }

        protected Vector3 ClampVector(Vector3 v)
        {
            return new Vector3(
                Mathf.Min(Map.MapWidth, Mathf.Max(0, v.x)),
                0,
                Mathf.Min(Map.MapLength, Mathf.Max(0, v.z)));
        }
    }
}
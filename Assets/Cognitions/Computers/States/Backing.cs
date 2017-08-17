using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Backing : ComputerState
    {
        private readonly ITarget _target;

        private Vector3 _backingPosition;
        private List<Vector3> _path;

        public Backing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITarget target)
            : base(ComputerStateIds.Backing, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
            FindPath();
        }

        private void FindPath()
        {
            var offset = (_target.Position - Unit.Position).normalized * 5;
            if (!Map.PathFinder.TryGetClosestAvailablePosition(Unit.Position
                                                               - offset,
                10, out _backingPosition))
            {
                _backingPosition = Unit.Position;
            }
            _path = Map.PathFinder.FindPath(Unit.Position, _backingPosition);
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            Unit.Targeting.Gun.SetFire(false);
            if (Map.IsPositionDangorous(Unit.Position))
            {
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateStrafing(_target));
            }

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (_target.IsVisible)
                {
                    var distanceToTarget = (_target.Position - Unit.Position).magnitude;
                    if (Unit.Targeting.IsGunMounted
                        && distanceToTarget > Unit.Targeting.Gun.EfectiveRange.x
                        && distanceToTarget < Unit.Targeting.Gun.EfectiveRange.y)
                    {
                        return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateFiring(_target));
                    }
                    FindPath();
                    TargetingHelper.ManageAimingAtTheTarget(_target);
                    MovementHelper.ManageMovingAlongThePath(_path);
                }
                else
                {
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateSearching(_target));
                }
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
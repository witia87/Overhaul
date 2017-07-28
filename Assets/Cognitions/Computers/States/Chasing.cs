using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Chasing : ComputerState
    {
        private readonly ITarget _target;
        private List<Vector3> _path;

        public Chasing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITarget target)
            : base(ComputerStateIds.Chasing, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
            _path = Map.PathFinder.FindPath(Unit.gameObject.transform.position,
                _target.LastSeenPosition);
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            Unit.Targeting.Gun.StopFiring();
            if (Map.IsPositionDangorous(Unit.gameObject.transform.position))
            {
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateStrafing(_target));
            }

            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                var distanceToTarget = (_target.LastSeenPosition - Unit.gameObject.transform.position).magnitude;
                if (_target.IsVisible)
                {
                    if (Unit.Targeting.IsGunMounted 
                        && distanceToTarget > Unit.Targeting.Gun.EfectiveRange.x
                        && distanceToTarget < Unit.Targeting.Gun.EfectiveRange.y)
                    {
                        return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateFiring(_target));
                    }
                    _path = Map.PathFinder.FindPath(Unit.gameObject.transform.position,
                        _target.LastSeenPosition);
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
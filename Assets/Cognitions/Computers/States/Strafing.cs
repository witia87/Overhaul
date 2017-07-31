using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Strafing : ComputerState
    {
        private readonly ITarget _target;
        private List<Vector3> _path;

        public Strafing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITarget target)
            : base(ComputerStateIds.Strafing, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
            _path = Map.PathFinder.FindSafespot(Unit.gameObject.transform.position);
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            if (!Map.IsPositionDangorous(Unit.gameObject.transform.position))
            {
                return DisposeCurrent().AndReturnToThePreviousState();
            }
            ProbabilisticTriggering.PerformOnAverageOnceEvery(0.3f,
                () => _path = Map.PathFinder.FindSafespot(Unit.gameObject.transform.position));
            MovementHelper.ManageMovingAlongThePath(_path);
            if (_target.IsVisible)
            {
                TargetingHelper.ManageAimingAtTheTarget(_target);
            }
            return this;
        }

        public override void OnDrawGizmos()
        {
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
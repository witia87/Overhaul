using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Watching : ComputerState
    {
        private readonly Vector3 _preferedLookDirection;
        private readonly bool _isLimited;
        private readonly float _timeLimit;
        private float _timeSpent;
        
        public Watching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            Vector3 preferedLookDirection, float timeLimit = 0)
            : base(ComputerStateIds.Watching, movementHelper, targetingHelper, unit, map)
        {
            _preferedLookDirection = preferedLookDirection;
            if (timeLimit > 0)
            {
                _isLimited = true;
                _timeLimit = timeLimit;
            }
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            _timeSpent += Time.deltaTime;
            if (_isLimited && _timeSpent > _timeLimit)
            {
                return DisposeCurrent().AndReturnToThePreviousState();
            }

            Unit.Movement.StopMoving();
            ProbabilisticTriggering.PerformOnAverageOnceEvery(
                Vector3.Dot(Unit.Targeting.TargetingDirection, _preferedLookDirection) < 0 ? 0.5f : 5,
                TargetingHelper.ChangeSightDirection);

            if (Unit.Targeting.VisionSensor.VisibleTargets.Count > 0)
            {
                var target = TargetingHelper.GetHighestPriorityTarget();
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
            }
            return this;
        }
    }
}
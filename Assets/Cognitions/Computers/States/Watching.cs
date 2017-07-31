using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Watching : ComputerState
    {
        private readonly bool _isLimited;
        private readonly Vector3? _preferedLookDirection;
        private readonly float _timeLimit;
        private float _timeSpent;

        public Watching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            Vector3? preferedLookDirection, float timeLimit = 0)
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

            if (Map.IsPositionDangorous(Unit.gameObject.transform.position))
            {
                ProbabilisticTriggering.PerformOnAverageOnceEvery(0.1f, ChangeDirection);
            }

            Unit.Movement.StopMoving();
            ProbabilisticTriggering.PerformOnAverageOnceEvery(2, ChangeDirection);

            if (Unit.Targeting.VisionSensor.VisibleTargetsCount > 0)
            {
                var target = Unit.Targeting.VisionSensor.GetClosestTarget();
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
            }
            return this;
        }

        private void ChangeDirection()
        {
            if (_preferedLookDirection.HasValue && ProbabilisticTriggering.TestProbabilisty(0.3f))
            {
                Unit.Targeting.TurnTowards(_preferedLookDirection.Value);
            }
            else
            {
                var directions = Unit.Targeting.VisionSensor.GetThreeClosestDirections();
                Unit.Targeting.TurnTowards(directions[Mathf.FloorToInt(Random.value * 3)]);
            }
        }
    }
}
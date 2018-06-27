using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public class Watching : CognitionState
    {
        private readonly bool _isLimited;
        private readonly Vector3? _preferedLookDirection;
        private readonly float _timeLimit;
        private float _timeSpent;

        public Watching(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnit unit, IMapGrid map,
            IVisionObserver vision,
            Vector3? preferedLookDirection, float timeLimit = 0)
            : base(ComputerStateIds.Watching, movementHelper, targetingHelper, unit, map, vision)
        {
            _preferedLookDirection = preferedLookDirection;
            if (timeLimit > 0)
            {
                _isLimited = true;
                _timeLimit = timeLimit;
            }
        }

        public override CognitionState Update()
        {
            _timeSpent += Time.deltaTime;
            if (_isLimited && _timeSpent > _timeLimit)
            {
                return DisposeCurrent().AndReturnToThePreviousState();
            }

            if (Map.IsPositionDangorous(Unit.LogicPosition))
            {
                ProbabilisticTriggering.PerformOnAverageOnceEvery(0.1f, ChangeDirection);
            }

            if (Map.IsPositionDangorous(Unit.LogicPosition))
            {
                ProbabilisticTriggering.PerformOnAverageOnceEvery(0.1f, ChangeDirection);
            }

            ProbabilisticTriggering.PerformOnAverageOnceEvery(2, ChangeDirection);
            if (Vision.UnitsSpottedByTeam.Count > 0)
            {
                var target = Vision.GetHighestPriorityTarget();
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateChasing(target));
            }

            return this;
        }

        private void ChangeDirection()
        {
            if (_preferedLookDirection.HasValue && ProbabilisticTriggering.TestProbabilisty(0.3f))
            {
                Unit.Control.LookTowards(_preferedLookDirection.Value);
            }
        }
    }
}
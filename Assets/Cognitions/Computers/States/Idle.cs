using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers.States
{
    public class Idle : ComputerState
    {
        private readonly Vector3 _preferedLookDirection;
        private bool _isLimited = false;
        private float _timeSpent = 0;
        private float _timeToSpend = 10;

        public Idle(Cognition<ComputerStateIds> parrentCognition)
            : base(parrentCognition, ComputerStateIds.Idle)
        {
        }

        public Idle(Cognition<ComputerStateIds> parrentCognition, Vector3 preferedLookDirection, float timeToSpend)
            : base(parrentCognition, ComputerStateIds.Idle)
        {
            _preferedLookDirection = preferedLookDirection;
            _isLimited = true;
            _timeToSpend = timeToSpend;
        }

        public override ICognitionState<ComputerStateIds> Update()
        {
            _timeSpent += Time.deltaTime;
            if (_isLimited && _timeSpent > _timeToSpend)
            {
                Dispose();
                return null;
            }

            Unit.Movement.StopMoving();
            ProbabilisticTriggering.PerformOnAverageOnceEvery(5, ChangeTurretDircetion);
            if (Unit.Targeting.VisionSensor.VisibleUnits.Count > 0)
            {
                var target = GetHighestPriorityTarget();
                return new ChasingEnemy(ParentCognition, target);
            }
            return this;
        }

        private void ChangeTurretDircetion()
        {
            var circle = Random.insideUnitCircle.normalized;
            var directionCandidate = new Vector3(circle.x, 0, circle.y);
            if (Vector3.Dot(directionCandidate, _preferedLookDirection) >= 0)
            {
                Unit.Targeting.TurnTowards(directionCandidate);
            }
            else
            {
                Unit.Targeting.TurnTowards(-directionCandidate);
            }
        }
    }
}
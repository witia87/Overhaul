using Assets.Modules.Turrets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Brain.States
{
    public class Idle : CognitionState<HumanoidCognitionStateIds>
    {
        private readonly Vector3 _preferedLookDirection;

        public Idle(Cognition<HumanoidCognitionStateIds> parrentCognition)
            : base(parrentCognition, HumanoidCognitionStateIds.Idle)
        {
        }

        public Idle(Cognition<HumanoidCognitionStateIds> parrentCognition, Vector3 preferedLookDirection)
            : base(parrentCognition, HumanoidCognitionStateIds.Idle)
        {
            _preferedLookDirection = preferedLookDirection;
        }

        public override ICognitionState<HumanoidCognitionStateIds> Update()
        {
            if (MountedModules.AreTurretControlsMounted)
            {
                foreach (var turret in MountedModules.TurretControls)
                {
                    ProbabilisticTriggering.PerformOnAverageOnceEvery(5, () => ChangeTurretDircetion(turret));
                    if (turret.VisibleCores.Count > 0)
                    {
                        var target = GetHighestPriorityTarget();
                        return new ChasingEnemy(ParrentCognition, target);
                    }
                }
            }
            if (MountedModules.AreTurretControlsMounted)
            {
                //    MountedModules.MovementControl.Stop();
            }
            return this;
        }

        private void ChangeTurretDircetion(ITurretControl turret)
        {
            var circle = Random.insideUnitCircle.normalized;
            var directionCandidate = new Vector3(circle.x, 0, circle.y);
            if (Vector3.Dot(directionCandidate, _preferedLookDirection) >= 0)
            {
                turret.TurnTowards(directionCandidate);
            }
            else
            {
                turret.TurnTowards(-directionCandidate);
            }
        }
    }
}
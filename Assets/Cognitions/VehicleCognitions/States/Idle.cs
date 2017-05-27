using Assets.Modules.Turrets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.VehicleCognitions.States
{
    public class Idle : VehicleCognitionState
    {
        private readonly Vector3 _preferedLookDirection;

        public Idle(Cognition<VehicleCognitionStateIds> parrentCognition)
            : base(parrentCognition, VehicleCognitionStateIds.Idle)
        {
        }

        public Idle(Cognition<VehicleCognitionStateIds> parrentCognition, Vector3 preferedLookDirection)
            : base(parrentCognition, VehicleCognitionStateIds.Idle)
        {
            _preferedLookDirection = preferedLookDirection;
        }

        public override ICognitionState<VehicleCognitionStateIds> Update()
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
            if (MountedModules.IsHumanoidMovementControlMounted)
            {
                MountedModules.VehicleMovementControl.StopMoving();
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
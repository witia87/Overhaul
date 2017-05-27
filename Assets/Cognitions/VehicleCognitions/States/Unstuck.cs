using Assets.Modules.Turrets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.VehicleCognitions.States
{
    public class Unstuck : VehicleCognitionState
    {
        private readonly Vector3 _direction;
        private readonly ICognitionState<VehicleCognitionStateIds> _followUpState;
        private readonly Vector3 _pointToLookAt;
        private float _timeToTryUnstacking;

        public Unstuck(Cognition<VehicleCognitionStateIds> parrentCognition, Vector3 direction,
            Vector3 pointToLookAt,
            float timeToTryUnstacking,
            ICognitionState<VehicleCognitionStateIds> followUpState)
            : base(parrentCognition, VehicleCognitionStateIds.Unstuck)
        {
            _direction = direction;
            _pointToLookAt = pointToLookAt;
            _timeToTryUnstacking = timeToTryUnstacking;
            _followUpState = followUpState;
        }

        public override ICognitionState<VehicleCognitionStateIds> Update()
        {
            _timeToTryUnstacking -= Time.deltaTime;
            if (_timeToTryUnstacking < 0)
            {
                return _followUpState;
            }

            if (MountedModules.AreTurretControlsMounted)
            {
                foreach (var turret in MountedModules.TurretControls)
                {
                    ProbabilisticTriggering.PerformOnAverageOnceEvery(5, () => ChangeTurretDircetion(turret));
                }
            }

            if (MountedModules.IsHumanoidMovementControlMounted)
            {
                ManageMovement();
            }

            return this;
        }

        private void ChangeTurretDircetion(ITurretControl turret)
        {
            turret.LookAt(_pointToLookAt);
        }

        protected virtual void ManageMovement()
        {
            var localPathDirection = Core.gameObject.transform.InverseTransformDirection(_direction);
            localPathDirection.Normalize();
            //MountedModules.MovementControl.Turn(localPathDirection.x*
            //                                    Vector3.Dot(MountedModules.MovementControl.MovementDirection,
            //                                        MountedModules.MovementControl.BodyDirection));

            MountedModules.VehicleMovementControl.MoveForward(1);
        }

        public override void OnDrawGizmos()
        {
        }
    }
}
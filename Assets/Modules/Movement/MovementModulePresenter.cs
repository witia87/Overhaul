using UnityEngine;

namespace Assets.Modules.Movement
{
    internal class MovementModulePresenter : ModuleSpritePresenter

    {
        private IMovementModuleParameters _movementModuleParameters;

        protected override void Start()
        {
            base.Start();
            _movementModuleParameters = Module as IMovementModuleParameters;
        }

        protected override void Update()
        {
            base.Update();
            var direction = _movementModuleParameters.UnitDirection;
            direction = Quaternion.AngleAxis(-GameMechanics.Stores.CameraStore.CameraEulerAngles.y, Vector3.up)*
                        direction;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetFloat("Speed", _movementModuleParameters.MovementSpeed);
            Animator.SetBool("IsMovingForward", _movementModuleParameters.MovementType == MovementType.Forward);
        }
    }
}
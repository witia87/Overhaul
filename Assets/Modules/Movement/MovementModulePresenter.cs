using UnityEngine;

namespace Assets.Modules.Movement
{
    internal class MovementModulePresenter : ModuleSpritePresenter

    {
        private IMovementModuleParameters _movementModuleParameters;

        protected override void Start()
        {
            base.Start();
            _movementModuleParameters = Module as MovementModule;
        }

        public override void Update()
        {
            base.Update();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                            _movementModuleParameters.UnitDirection;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetFloat("Speed", _movementModuleParameters.MovementSpeed);
            Animator.SetBool("IsMovingForward", _movementModuleParameters.MovementType == MovementType.Forward);
        }
    }
}
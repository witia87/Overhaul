using UnityEngine;

namespace Assets.Modules.Movement
{
    internal class MovementModulePresenter : ModuleSpritePresenter<IMovementModuleParameters>

    {
        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                            Module.UnitDirection;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetFloat("Speed", Module.MovementSpeed);
            Animator.SetBool("IsMovingForward", Module.MovementType == MovementType.Forward);
        }
    }
}
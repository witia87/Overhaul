using UnityEngine;

namespace Assets.Modules.Targeting
{
    internal class TargetingModulePresenter : ModuleSpritePresenter<ITargetingParameters>
    {
        public override void Update()
        {
            base.Update();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up) *
                            Module.TargetingDirection;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
        }
    }
}
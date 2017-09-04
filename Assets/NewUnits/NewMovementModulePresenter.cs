using Assets.Modules;
using Assets.Modules.Movement;
using UnityEditor;
using UnityEngine;

namespace Assets.NewUnits
{
    internal class NewMovementModulePresenter : NewModuleSpritePresenter<INewMovementModuleParameters>
    {
        public override void Update()
        {
            base.Update();
            var logicDirection = Module.ModuleForward;
            logicDirection.y = 0;
            logicDirection.Normalize();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up) *
                            logicDirection;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetFloat("Speed", Module.MovementSpeed);
            Animator.SetBool("IsMovingForward", Module.MovementSpeed >= 0);
        }
    }
}
using System;
using UnityEngine;

namespace Assets.Modules.Targeting.Guns
{
    internal class GunPresenter : ModuleSpritePresenter<IGunParameters>
    {
        public override void Update()
        {
            base.Update();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)* Module.FireDirection;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetBool("IsFiring", Module.IsFiring);

        }
    }
}
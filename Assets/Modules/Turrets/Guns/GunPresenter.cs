using System;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    internal class GunPresenter : ModuleSpritePresenter
    {
        public event Action ExampleEvent;

        public override void Update()
        {
            base.Update();
            var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                            Module.gameObject.transform.forward;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            ExampleEvent = () => { };
        }
    }
}
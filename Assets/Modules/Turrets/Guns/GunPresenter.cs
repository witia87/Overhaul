using Assets.Gui;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    internal class GunPresenter : ModuleSpritePresenter
    {
        private Animator Animator;

        private void Start()
        {
            Animator = GetComponent<Animator>();
        }

        private void Update()
        {
                base.Update();
                var direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                                Module.gameObject.transform.forward;
                Animator.SetFloat("H", direction.x);
                Animator.SetFloat("V", direction.z);
        }
    }
}
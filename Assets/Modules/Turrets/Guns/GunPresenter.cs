using Assets.Gui;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    internal class GunPresenter : Presenter

    {
        private Vector3 _baseCameraEulerAngles;
        private Animator Animator;
        public Gun Gun;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            _baseCameraEulerAngles = CameraStore.CameraEulerAngles;
        }

        private void Update()
        {
            Animator.SetBool("IsFiring", Input.GetAxis("Fire1") > 0.1f);

            gameObject.transform.eulerAngles = _baseCameraEulerAngles;
            var direction = Gun.gameObject.transform.forward;
            direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                        direction;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
        }
    }
}
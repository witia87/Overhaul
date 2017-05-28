using Assets.Modules.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    class GunPresenter : MonoBehaviour

    {
        public Gun Gun;
        private Animator Animator;

        Vector3 _baseCameraEulerAngles;
        private void Start() {
            Animator = GetComponent<Animator>();
            _baseCameraEulerAngles = GameMechanics.Stores.CameraStore.CameraEulerAngles;
        }
        
        private void Update()
        {
            Animator.SetBool("IsFiring", Input.GetAxis("Fire1") > 0.1f);

            gameObject.transform.eulerAngles = _baseCameraEulerAngles;
            var direction = Gun.gameObject.transform.forward;
            direction = Quaternion.AngleAxis(-GameMechanics.Stores.CameraStore.CameraEulerAngles.y, Vector3.up) * direction; 
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
        }        
    }
}

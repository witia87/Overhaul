using Assets.Modules.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Movement
{
    class TurretModulePresenter : MonoBehaviour

    {
        public TurretModule TurretModule;
        public Animator Animator;

        Vector3 _baseCameraEulerAngles;
        private void Start() {
            Animator = GetComponent<Animator>();
            _baseCameraEulerAngles = GameMechanics.Stores.CameraStore.CameraEulerAngles;
        }

        private void Update()
        {
            gameObject.transform.eulerAngles = _baseCameraEulerAngles;
            var direction = TurretModule.TurretDirection;
            direction = Quaternion.AngleAxis(-GameMechanics.Stores.CameraStore.CameraEulerAngles.y, Vector3.up) * direction; ;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
        }        
    }
}

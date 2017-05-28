using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Movement
{
    class MovementModulePresenter: ModuleSpritePresenter

    {
        private IMovementModuleParameters _movementModuleParameters;
        
        protected override void Start() {
            base.Start();
            _movementModuleParameters = Module as IMovementModuleParameters;
        }

        private void Update()
        {
            base.Update();
            var direction = _movementModuleParameters.UnitDirection;
            direction = Quaternion.AngleAxis(-GameMechanics.Stores.CameraStore.CameraEulerAngles.y, Vector3.up) * direction;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
            Animator.SetFloat("Speed", _movementModuleParameters.MovementSpeed);
            Animator.SetBool("IsMovingForward", _movementModuleParameters.MovementType == MovementType.Forward);
        }
    }
}

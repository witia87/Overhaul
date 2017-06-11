using UnityEngine;

namespace Assets.Modules.Turrets
{
    internal class TurretModulePresenter : ModuleSpritePresenter
    {
        private ITurretParameters _turretParameters;

        protected override void Start()
        {
            base.Start();
            _turretParameters = Module as ITurretParameters;
        }

        protected override void Update()
        {
            base.Update();
            var direction = _turretParameters.TurretDirection;
            direction = Quaternion.AngleAxis(-CameraStore.CameraEulerAngles.y, Vector3.up)*
                        direction;
            ;
            Animator.SetFloat("H", direction.x);
            Animator.SetFloat("V", direction.z);
        }
    }
}
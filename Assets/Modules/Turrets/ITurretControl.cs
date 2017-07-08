using System.Collections.Generic;
using Assets.Modules.Turrets.Guns;
using Assets.Modules.Turrets.Vision;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public interface ITurretControl : ITurretParameters
    {
        IVisionSensor VisionSensor { get; }
        List<IGunControl> GunControls { get; }
        bool AreGunControlsMounted { get; }
        void TurnTowards(Vector3 direction);
        void LookAt(Vector3 point);
        void DropGun();
        void PickGun();
    }
}
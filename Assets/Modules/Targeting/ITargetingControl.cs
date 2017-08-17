using Assets.Modules.Targeting.Guns;
using Assets.Modules.Targeting.Vision;
using UnityEngine;

namespace Assets.Modules.Targeting
{
    public interface ITargetingControl : ITargetingParameters, IModuleControl
    {
        IVisionSensor VisionSensor { get; }
        bool IsGunMounted { get; }
        IGunControl Gun { get; }
        void TurnTowards(Vector3 direction);
        void LookAt(Vector3 point);
        void DropGun();
        void PickGun();

        void SetCrouch(bool isCrouching);
    }
}
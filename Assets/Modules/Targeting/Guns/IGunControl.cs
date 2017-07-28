using UnityEngine;

namespace Assets.Modules.Targeting.Guns
{
    public interface IGunControl: IModuleControl, IGunParameters
    {
        void Fire();
        void Fire(float distance, float bulletHeightAtAGivenDisntance);
        void StopFiring();
        void Reload();
        Vector2 EfectiveRange { get; }
    }
}
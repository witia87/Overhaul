using UnityEngine;

namespace Assets.Modules.Targeting.Guns
{
    public interface IGunControl : IModuleControl, IGunParameters
    {
        Vector2 EfectiveRange { get; }
        void SetFire(bool isSetToFire);
        void SetFire(float distance, float bulletHeightAtAGivenDisntance);
        void Reload();
    }
}
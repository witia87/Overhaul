using UnityEngine;

namespace Assets.Modules.Targeting.Guns.Bullets
{
    public interface IBulletComponent
    {
        Vector3 InitialPosition { get; set; }
    }
}
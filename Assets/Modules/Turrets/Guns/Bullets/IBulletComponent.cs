using UnityEngine;

namespace Assets.Modules.Turrets.Guns.Bullets
{
    public interface IBulletComponent
    {
        Vector3 InitialPosition { get; set; }
    }
}
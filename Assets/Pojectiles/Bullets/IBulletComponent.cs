using UnityEngine;

namespace Assets.Pojectiles.Bullets
{
    public interface IBulletComponent
    {
        Vector3 InitialPosition { get; set; }
    }
}
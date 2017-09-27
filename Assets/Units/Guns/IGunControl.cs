using UnityEngine;

namespace Assets.Units.Guns
{
    public interface IGunControl
    {
        int MaxAmmo { get; }
        int AmmoLeft { get; }
        void AimAt(Vector3 globalPosition);
        void Fire();
        Vector2 EfectiveRange { get; }
        Vector3 FirePosition { get; }
        Vector3 Direction { get; }
    }
}
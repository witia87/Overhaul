using UnityEngine;

namespace Assets.Modules.Targeting.Guns
{
    public interface IGunParameters
    {
        Vector3 FirePosition { get; }
        Vector3 FireDirection { get; }

        int TotalAmmoLeft { get; }
        int ClipSize { get; }
        int AmmoLeftInTheClip { get; }

        bool IsFiring { get; }
        float CooldownTime { get; }
        float CooldownTimeLeft { get; }

        bool IsReloading { get; }
        float ReloadTime { get; }
    }
}
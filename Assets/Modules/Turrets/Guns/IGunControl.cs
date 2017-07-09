namespace Assets.Modules.Turrets.Guns
{
    public interface IGunControl
    {
        int TotalAmmoLeft { get; }
        int ClipSize { get; }
        int AmmoLeftInTheClip { get; }

        bool IsFiring { get; }
        float CooldownTime { get; }
        float CooldownTimeLeft { get; }

        bool IsReloading { get; }
        float ReloadTime { get; }
        void Fire();
        void StopFiring();
        void Reload();
    }
}
namespace Assets.Modules.Turrets.Guns
{
    public interface IGunControl
    {
        int TotalAmmoLeft { get; }
        int ClipSize { get; }
        int AmmoLeftInTheClip { get; }
        float CooldownTime { get; }
        float CooldownTimeLeft { get; }
        void Fire();
        void Reload();
    }
}
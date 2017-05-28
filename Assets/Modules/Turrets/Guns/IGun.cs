namespace Assets.Modules.Turrets.Guns
{
    public interface IGun
    {
        int TotalAmmoLeft { get; }
        int ClipSize { get; }
        int AmmoLeftInTheClip { get; }
        void Fire();
        void Reload();
    }
}
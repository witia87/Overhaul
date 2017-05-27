namespace Assets.Modules.Guns
{
    public interface IGunModule
    {
        int TotalAmmoLeft { get; }
        int ClipSize { get; }
        int AmmoLeftInTheClip { get; }
        void Fire();
        void Reload();
    }
}
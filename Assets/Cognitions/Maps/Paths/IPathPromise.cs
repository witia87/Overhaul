namespace Assets.Cognitions.Maps.Paths
{
    public interface IPathPromise
    {
        bool IsPathReady { get; }
        IPath GetPath();
    }
}
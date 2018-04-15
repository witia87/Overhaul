using Assets.Modules.Units;

namespace Assets.Cognitions.Vision
{
    public interface IVisionStore
    {
        IVisionObserver GetVisionObserver(IUnit unit);
    }
}
using Assets.Environment.Units;

namespace Assets.Cognitions.Vision
{
    public interface IVisionStore
    {
        IVisionObserver GetVisionObserver(IUnit unit);
    }
}
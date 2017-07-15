using Assets.Modules.Movement;
using Assets.Modules.Targeting;

namespace Assets.Modules
{
    public interface IUnitControl
    {
        IMovementControl Movement { get; }
        ITargetingControl Targeting { get; }
    }
}
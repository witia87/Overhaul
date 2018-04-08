using System.Collections.Generic;
using Assets.Modules.Units;

namespace Assets.Cognitions.Vision
{
    public interface IVisionObserver
    {
        List<Unit> VisibleOpposingUnits { get; }
        bool GetClosestTarget(out ITarget target);
    }
}
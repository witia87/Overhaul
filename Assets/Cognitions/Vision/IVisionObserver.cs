using System.Collections.Generic;
using Assets.Modules.Units;

namespace Assets.Cognitions.Vision
{
    public interface IVisionObserver
    {
        List<IUnit> UnitsSpottedByTeam { get; }
        ITarget GetHighestPriorityTarget();
    }
}
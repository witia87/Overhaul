using System.Collections.Generic;
using Assets.Environment.Units;

namespace Assets.Cognitions.Vision
{
    public interface IVisionObserver
    {
        List<IUnit> UnitsSpottedByTeam { get; }
        ITarget GetHighestPriorityTarget();
    }
}
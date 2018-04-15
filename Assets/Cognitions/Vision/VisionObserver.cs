using System;
using System.Collections.Generic;
using Assets.Modules.Units;

namespace Assets.Cognitions.Vision
{
    internal class VisionObserver : IVisionObserver
    {
        private readonly List<VisibleUnit> _spottedVisibleUnits;
        private IUnit _unit;

        public VisionObserver(IUnit unit, List<VisibleUnit> spottedVisibleVisibleUnits,
            List<IUnit> spottedUnits)
        {
            _unit = unit;
            _spottedVisibleUnits = spottedVisibleVisibleUnits;
            UnitsSpottedByTeam = spottedUnits;
        }

        public List<IUnit> UnitsSpottedByTeam { get; private set; }

        public ITarget GetHighestPriorityTarget()
        {
            if (_spottedVisibleUnits.Count > 0)
            {
                var closesVisibleOpposingUnit = _spottedVisibleUnits[0];
                for (var i = 1; i < _spottedVisibleUnits.Count; i++)
                {
                    if ((_unit.Position - _spottedVisibleUnits[i].Unit.Position).magnitude <
                        (_unit.Position - closesVisibleOpposingUnit.Unit.Position).magnitude)
                    {
                        closesVisibleOpposingUnit = _spottedVisibleUnits[i];
                    }
                }

                return new Target(closesVisibleOpposingUnit);
            }

            throw new ApplicationException("No unit to target");
        }
    }
}
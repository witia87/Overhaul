using System.Collections.Generic;
using Assets.Units;
using Assets.Units.Modules;

namespace Assets.Vision
{
    public class VisionObserver: IVisionObserver
    {
        private Unit _unit;

        public VisionObserver(Unit unit)
        {
            _unit = unit;
        }

        public bool GetClosestTarget(out ITarget target)
        {
            if (_closesVisibleOpposingUnitControl != null)
            {
                target = new Target(_closesVisibleOpposingUnitControl);
                return true;
            }
            target = null;
            return false;
        }

        public List<Unit> VisibleOpposingUnits;
        private Unit _closesVisibleOpposingUnitControl;
        public void UpdateVisibleUnits(List<Unit> units)
        {
            VisibleOpposingUnits = units;
            if (VisibleOpposingUnits.Count > 0)
            {
                _closesVisibleOpposingUnitControl = VisibleOpposingUnits[0];
                for (int i = 1; i < VisibleOpposingUnits.Count; i++)
                {
                    if ((_unit.Position - VisibleOpposingUnits[i].Position).magnitude <
                        (_unit.Position - _closesVisibleOpposingUnitControl.Position).magnitude)
                    {
                        _closesVisibleOpposingUnitControl = VisibleOpposingUnits[i];
                    }
                }
            }
            else
            {
                _closesVisibleOpposingUnitControl = null;
            }
        }
    }
}
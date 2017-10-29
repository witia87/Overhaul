using System.Collections.Generic;
using Assets.Units;

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
            if (_closesVisibleOpposingUnit != null)
            {
                target = new Target(_closesVisibleOpposingUnit);
                return true;
            }
            target = null;
            return false;
        }

        public List<Unit> VisibleOpposingUnits;
        private Unit _closesVisibleOpposingUnit;
        public void UpdateVisibleUnits(List<Unit> units)
        {
            VisibleOpposingUnits = units;
            if (VisibleOpposingUnits.Count > 0)
            {
                _closesVisibleOpposingUnit = VisibleOpposingUnits[0];
                for (int i = 1; i < VisibleOpposingUnits.Count; i++)
                {
                    if ((_unit.Center - VisibleOpposingUnits[i].Center).magnitude <
                        (_unit.Center - _closesVisibleOpposingUnit.Center).magnitude)
                    {
                        _closesVisibleOpposingUnit = VisibleOpposingUnits[i];
                    }
                }
            }
            else
            {
                _closesVisibleOpposingUnit = null;
            }
        }

        public void SwitchSidesOfConflict()
        {
            throw new System.NotImplementedException();
        }
    }
}
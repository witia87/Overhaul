using System.Collections.Generic;
using Assets.Units;
using Assets.Units.Modules;

namespace Assets.Vision
{
    public class VisionObserver: IVisionObserver
    {
        private UnitControl _UnitControl;

        public VisionObserver(UnitControl UnitControl)
        {
            _UnitControl = UnitControl;
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

        public List<UnitControl> VisibleOpposingUnits;
        private UnitControl _closesVisibleOpposingUnitControl;
        public void UpdateVisibleUnits(List<UnitControl> units)
        {
            VisibleOpposingUnits = units;
            if (VisibleOpposingUnits.Count > 0)
            {
                _closesVisibleOpposingUnitControl = VisibleOpposingUnits[0];
                for (int i = 1; i < VisibleOpposingUnits.Count; i++)
                {
                    if ((_UnitControl.Center - VisibleOpposingUnits[i].Center).magnitude <
                        (_UnitControl.Center - _closesVisibleOpposingUnitControl.Center).magnitude)
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

        public void SwitchSidesOfConflict()
        {
            throw new System.NotImplementedException();
        }
    }
}
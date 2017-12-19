using System.Collections.Generic;
using Assets.Units;
using Assets.Units.Modules;

namespace Assets.Vision
{
    public class VisionObserver: IVisionObserver
    {
        private HeadModule _headModule;

        public VisionObserver(HeadModule headModule)
        {
            _headModule = headModule;
        }

        public bool GetClosestTarget(out ITarget target)
        {
            if (_closesVisibleOpposingHeadModule != null)
            {
                target = new Target(_closesVisibleOpposingHeadModule);
                return true;
            }
            target = null;
            return false;
        }

        public List<HeadModule> VisibleOpposingUnits;
        private HeadModule _closesVisibleOpposingHeadModule;
        public void UpdateVisibleUnits(List<HeadModule> units)
        {
            VisibleOpposingUnits = units;
            if (VisibleOpposingUnits.Count > 0)
            {
                _closesVisibleOpposingHeadModule = VisibleOpposingUnits[0];
                for (int i = 1; i < VisibleOpposingUnits.Count; i++)
                {
                    if ((_headModule.Center - VisibleOpposingUnits[i].Center).magnitude <
                        (_headModule.Center - _closesVisibleOpposingHeadModule.Center).magnitude)
                    {
                        _closesVisibleOpposingHeadModule = VisibleOpposingUnits[i];
                    }
                }
            }
            else
            {
                _closesVisibleOpposingHeadModule = null;
            }
        }

        public void SwitchSidesOfConflict()
        {
            throw new System.NotImplementedException();
        }
    }
}
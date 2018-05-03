using System.Collections.Generic;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Vision
{
    internal class VisibleUnit
    {
        private List<VisibleUnit> _oposingUnits;
        public IUnit Unit;

        public VisibleUnit(IUnit unit, List<VisibleUnit>[] registeredUnits)
        {
            Unit = unit;
            _oposingUnits = registeredUnits[((int) unit.Fraction + 1) % 2];
        }

        public bool IsVisible { get; private set; }

        public void UpdateVisibility()
        {
            IsVisible = true;
            foreach (var enemy in _oposingUnits)
            {
                if (!Physics.Linecast(enemy.Unit.Position, Unit.Vision.Position,
                    VisionStore.VisionBlockingLayerMask))
                {
                    return;
                }
            }
            IsVisible = false;
        }
    }
}
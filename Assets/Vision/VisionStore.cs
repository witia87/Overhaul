using System;
using System.Collections.Generic;
using Assets.Units;
using UnityEngine;

namespace Assets.Vision
{
    public class VisionStore : MonoBehaviour
    {
        private int _currentlyProcessedUnitIndex;
        private readonly List<UnitControl> _enemyUnits = new List<UnitControl>();
        private readonly List<UnitControl> _neutralUnits = new List<UnitControl>();
        private readonly List<UnitControl> _playerUnits = new List<UnitControl>();

        private readonly Dictionary<UnitControl, VisionObserver> _registeredObservers =
            new Dictionary<UnitControl, VisionObserver>();

        [SerializeField] private LayerMask _visionBlockingLayerMask;

        private void Start()
        {
            do
            {
                Update();
            } while (_currentlyProcessedUnitIndex > 0);
        }

        public void Update()
        {
            _currentlyProcessedUnitIndex = (_currentlyProcessedUnitIndex + 1) %
                                           (_playerUnits.Count + _enemyUnits.Count);

            if (_currentlyProcessedUnitIndex < _playerUnits.Count)
                ProcessUnit(_playerUnits[_currentlyProcessedUnitIndex], _enemyUnits);
            else
                ProcessUnit(_enemyUnits[_currentlyProcessedUnitIndex - _playerUnits.Count], _playerUnits);
        }

        private void ProcessUnit(UnitControl unitControl, List<UnitControl> opposingUnits)
        {
            var visibleUnits = new List<UnitControl>();
            for (var i = 0; i < opposingUnits.Count; i++)
                if (!Physics.Linecast(opposingUnits[i].Center, unitControl.Vision.SightPosition,
                    _visionBlockingLayerMask))
                    visibleUnits.Add(opposingUnits[i]);
            _registeredObservers[unitControl].UpdateVisibleUnits(visibleUnits);
        }

        public IVisionObserver RegisterUnit(UnitControl unitControl)
        {
            switch (unitControl.FractionId)
            {
                case FractionId.Player:
                    _playerUnits.Add(unitControl);
                    break;
                case FractionId.Enemy:
                    _enemyUnits.Add(unitControl);
                    break;
                case FractionId.Neutral:
                    _neutralUnits.Add(unitControl);
                    break;
                default:
                    throw new ApplicationException("Not all units have proper fractions set.");
            }

            _registeredObservers.Add(unitControl, new VisionObserver(unitControl));
            return _registeredObservers[unitControl];
        }

        public List<UnitControl> GetVisibleOpposingUnits(UnitControl unitControl)
        {
            var output = new List<UnitControl>();
            foreach (var opposingUnit in _registeredObservers[unitControl].VisibleOpposingUnits)
                output.Add(opposingUnit);
            return output;
        }

        public void UnregisterUnit(UnitControl unitControl)
        {
            throw new NotImplementedException();
        }
    }
}
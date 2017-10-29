using System;
using System.Collections.Generic;
using Assets.Gui.UnitsVisibility;
using Assets.Maps;
using Assets.Units;
using UnityEngine;

namespace Assets.Vision
{
    public class VisionStore : MonoBehaviour
    {
        private int _currentlyProcessedUnitIndex;
        private List<Unit> _enemyUnits = new List<Unit>();
        private List<Unit> _neutralUnits = new List<Unit>();
        private List<Unit> _playerUnits = new List<Unit>();
        private Dictionary<Unit, VisionObserver> _registeredObservers = new Dictionary<Unit, VisionObserver>();
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
            {
                ProcessUnit(_playerUnits[_currentlyProcessedUnitIndex], _enemyUnits);
            }
            else
            {
                ProcessUnit(_enemyUnits[_currentlyProcessedUnitIndex - _playerUnits.Count], _playerUnits);
            }
        }

        private void ProcessUnit(Unit unit, List<Unit> opposingUnits)
        {
            var visibleUnits = new List<Unit>();
            for (var i = 0; i < opposingUnits.Count; i++)
            {
                if (!Physics.Linecast(opposingUnits[i].Center, unit.Head.VisionPosition,
                    _visionBlockingLayerMask))
                {
                    visibleUnits.Add(opposingUnits[i]);
                }
            }
            _registeredObservers[unit].UpdateVisibleUnits(visibleUnits);
        }

        public IVisionObserver RegisterUnit(Unit unit)
        {
            switch (unit.FractionId)
            {
                case FractionId.Player:
                    _playerUnits.Add(unit);
                    break;
                case FractionId.Enemy:
                    _enemyUnits.Add(unit);
                    break;
                case FractionId.Neutral:
                    _neutralUnits.Add(unit);
                    break;
                default:
                    throw new ApplicationException("Not all units have proper fractions set.");
            }
            _registeredObservers.Add(unit, new VisionObserver(unit));
            return _registeredObservers[unit];
        }

        public List<Unit> GetVisibleOpposingUnits(Unit unit)
        {
            var output = new List<Unit>();
            foreach (var opposingUnit in _registeredObservers[unit].VisibleOpposingUnits)
            {
                output.Add(opposingUnit);
            }
            return output;
        }

        public void UnregisterUnit(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using Assets.Units;
using UnityEngine;

namespace Assets.Vision
{
    public class VisionStore : MonoBehaviour
    {
        private int _currentlyProcessedUnitIndex;
        private readonly List<Unit> _enemyUnits = new List<Unit>();
        private readonly List<Unit> _neutralUnits = new List<Unit>();
        private readonly List<Unit> _playerUnits = new List<Unit>();

        private readonly Dictionary<Unit, VisionObserver> _registeredObservers =
            new Dictionary<Unit, VisionObserver>();

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

        private void ProcessUnit(Unit unit, List<Unit> opposingUnits)
        {
            var visibleUnits = new List<Unit>();
            foreach (var opposingUnit in opposingUnits)
                if (!Physics.Linecast(opposingUnit.Position, unit.Vision.SightPosition,
                    _visionBlockingLayerMask))
                    visibleUnits.Add(opposingUnit);

            _registeredObservers[unit].UpdateVisibleUnits(visibleUnits);
        }

        public IVisionObserver RegisterUnit(Unit unit)
        {
            switch (unit.Fraction)
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
                output.Add(opposingUnit);
            return output;
        }

        public void UnregisterUnit(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
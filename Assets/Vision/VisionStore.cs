using System;
using System.Collections.Generic;
using Assets.Gui.UnitsVisibility;
using Assets.Maps;
using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Vision
{
    public class VisionStore : MonoBehaviour
    {
        private int _currentlyProcessedUnitIndex;
        private List<HeadModule> _enemyUnits = new List<HeadModule>();
        private List<HeadModule> _neutralUnits = new List<HeadModule>();
        private List<HeadModule> _playerUnits = new List<HeadModule>();
        private Dictionary<HeadModule, VisionObserver> _registeredObservers = new Dictionary<HeadModule, VisionObserver>();
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

        private void ProcessUnit(HeadModule headModule, List<HeadModule> opposingUnits)
        {
            var visibleUnits = new List<HeadModule>();
            for (var i = 0; i < opposingUnits.Count; i++)
            {
                if (!Physics.Linecast(opposingUnits[i].Center, headModule.VisionPosition,
                    _visionBlockingLayerMask))
                {
                    visibleUnits.Add(opposingUnits[i]);
                }
            }
            _registeredObservers[headModule].UpdateVisibleUnits(visibleUnits);
        }

        public IVisionObserver RegisterUnit(HeadModule headModule)
        {
            switch (headModule.FractionId)
            {
                case FractionId.Player:
                    _playerUnits.Add(headModule);
                    break;
                case FractionId.Enemy:
                    _enemyUnits.Add(headModule);
                    break;
                case FractionId.Neutral:
                    _neutralUnits.Add(headModule);
                    break;
                default:
                    throw new ApplicationException("Not all units have proper fractions set.");
            }
            _registeredObservers.Add(headModule, new VisionObserver(headModule));
            return _registeredObservers[headModule];
        }

        public List<HeadModule> GetVisibleOpposingUnits(HeadModule headModule)
        {
            var output = new List<HeadModule>();
            foreach (var opposingUnit in _registeredObservers[headModule].VisibleOpposingUnits)
            {
                output.Add(opposingUnit);
            }
            return output;
        }

        public void UnregisterUnit(HeadModule headModule)
        {
            throw new NotImplementedException();
        }
    }
}
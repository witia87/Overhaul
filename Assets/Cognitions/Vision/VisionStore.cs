using System;
using System.Collections.Generic;
using Assets.Modules;
using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Cognitions.Vision
{
    public class VisionStore : MonoBehaviour, IVisionStore
    {
        public static LayerMask VisionBlockingLayerMask;
        private int _currentlyProcessedUnitIndex;

        private readonly List<VisibleUnit>[] _registeredUnits = new List<VisibleUnit>[2];
        private readonly List<IUnit>[] _unitsSpottedBy = new List<IUnit>[2];
        private readonly List<VisibleUnit>[] _visibleUnitsSpottedBy = new List<VisibleUnit>[2];

        [SerializeField] public LayerMask _visionBlockingLayerMask;

        public LayerMask WalLayerMask;

        private void Awake()
        {
            VisionBlockingLayerMask = _visionBlockingLayerMask;

            _registeredUnits[(int)FractionId.Player] = new List<VisibleUnit>();
            _registeredUnits[(int)FractionId.Enemy] = new List<VisibleUnit>();
            var units = FindObjectsOfType<Unit>();
            foreach (var unit in units)
            {
                _registeredUnits[(int) unit.Fraction].Add(new VisibleUnit(unit, _registeredUnits));
            }

            _visibleUnitsSpottedBy[(int) FractionId.Player] = new List<VisibleUnit>();
            _visibleUnitsSpottedBy[(int) FractionId.Enemy] = new List<VisibleUnit>();

            _unitsSpottedBy[(int) FractionId.Player] = new List<IUnit>();
            _unitsSpottedBy[(int) FractionId.Enemy] = new List<IUnit>();
        }

        public IVisionObserver GetVisionObserver(IUnit unit)
        {
            return new VisionObserver(unit,
                _visibleUnitsSpottedBy[(int)unit.Fraction],
                _unitsSpottedBy[(int)unit.Fraction]);
        }

        private void Start()
        {
            do
            {
                Update();
            } while (_currentlyProcessedUnitIndex > 0);
        }

        public void Update()
        {
            var playerUnits = _registeredUnits[(int) FractionId.Player];
            var enemyUnits = _registeredUnits[(int) FractionId.Enemy];
            _currentlyProcessedUnitIndex = (_currentlyProcessedUnitIndex + 1) %
                                           (playerUnits.Count + enemyUnits.Count);

            if (_currentlyProcessedUnitIndex < playerUnits.Count)
            {
                var unitIndex = _currentlyProcessedUnitIndex;
                playerUnits[unitIndex].UpdateVisibility();
            }
            else
            {
                var unitIndex = _currentlyProcessedUnitIndex - playerUnits.Count;
                enemyUnits[unitIndex].UpdateVisibility();
            }

            if (_currentlyProcessedUnitIndex == playerUnits.Count + enemyUnits.Count - 1)
            {
                var visibleUnitsSpottedByPlayerTeam
                    = _visibleUnitsSpottedBy[(int) FractionId.Player];
                visibleUnitsSpottedByPlayerTeam.Clear();
                var unitsSpottedByPlayerTeam
                    = _unitsSpottedBy[(int) FractionId.Player];
                unitsSpottedByPlayerTeam.Clear();
                foreach (var enemyUnit in enemyUnits)
                {
                    if (enemyUnit.IsVisible)
                    {
                        visibleUnitsSpottedByPlayerTeam.Add(enemyUnit);
                        unitsSpottedByPlayerTeam.Add(enemyUnit.Unit);
                    }
                }

                var visibleUnitsSpottedByEnemyTeam
                    = _visibleUnitsSpottedBy[(int) FractionId.Enemy];
                visibleUnitsSpottedByEnemyTeam.Clear();
                var unitsSpottedByEnemyTeam
                    = _unitsSpottedBy[(int) FractionId.Enemy];
                unitsSpottedByEnemyTeam.Clear();
                foreach (var playerUnit in playerUnits)
                {
                    if (playerUnit.IsVisible)
                    {
                        visibleUnitsSpottedByEnemyTeam.Add(playerUnit);
                        unitsSpottedByEnemyTeam.Add(playerUnit.Unit);
                    }
                }
            }
        }

        public void UnregisterUnit(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
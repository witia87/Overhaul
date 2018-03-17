using System.Collections.Generic;
using Assets.Gui.Player;
using Assets.Units;
using Assets.Units.Modules;
using Assets.Vision;
using UnityEngine;

namespace Assets.Gui.UnitsVisibility
{
    public class UnitsVisibilityStore : GuiStore
    {
        private HashSet<HeadModule> _unitsVisibility = new HashSet<HeadModule>();
        private VisionStore _visionStore;
        private IPlayerStore _playerStore;
        public float DisapearingTime = 0.5f;

        private void Awake()
        {
            _playerStore = FindObjectOfType<PlayerStore>();
            _visionStore = FindObjectOfType<VisionStore>(); // TODO: Is that the proper way to communicate with logic (not GUI) store?
        }

        private void Update()
        {
            var newVisibleUnits = _visionStore.GetVisibleOpposingUnits(_playerStore.PlayerUnitControl);
            _unitsVisibility.Clear();
            foreach (var unit in newVisibleUnits)
            {
                _unitsVisibility.Add(unit);
            }
        }

        public bool IsUnitVisible(HeadModule headModule)
        {
            return _unitsVisibility.Contains(headModule);
        }
    }
}
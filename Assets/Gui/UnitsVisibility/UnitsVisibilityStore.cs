using System.Collections.Generic;
using Assets.Cognitions.Vision;
using Assets.Gui.Player;
using Assets.Modules.Units;

namespace Assets.Gui.UnitsVisibility
{
    public class UnitsVisibilityStore : GuiStore
    {
        private IPlayerStore _playerStore;
        private HashSet<Unit> _unitsVisibility = new HashSet<Unit>();
        private VisionStore _visionStore;
        public float DisapearingTime = 0.5f;

        private void Awake()
        {
            _playerStore = FindObjectOfType<PlayerStore>();
            _visionStore =
                FindObjectOfType<VisionStore>(); // TODO: Is that the proper way to communicate with logic (not GUI) store?
        }

        private void Update()
        {
            var newVisibleUnits = _visionStore.GetVisibleOpposingUnits(_playerStore.PlayerUnit);
            _unitsVisibility.Clear();
            foreach (var unit in newVisibleUnits)
            {
                _unitsVisibility.Add(unit);
            }
        }

        public bool IsUnitVisible(Unit unit)
        {
            return _unitsVisibility.Contains(unit);
        }
    }
}
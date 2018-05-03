using System.Collections.Generic;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Gui.Player;

namespace Assets.Gui.UnitsVisibility
{
    public class UnitsVisibilityStore : GuiStore
    {
        private IPlayerStore _playerStore;
        private HashSet<IUnit> _unitsVisibility = new HashSet<IUnit>();
        private IVisionObserver _visionObserver;
        public float DisapearingTime = 0.5f;

        private void Awake()
        {
            _playerStore = FindObjectOfType<PlayerStore>();
            _visionObserver =
                FindObjectOfType<VisionStore>().GetVisionObserver(_playerStore.PlayerUnit);
        }

        private void Update()
        {
            var newVisibleUnits = _visionObserver.UnitsSpottedByTeam;
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
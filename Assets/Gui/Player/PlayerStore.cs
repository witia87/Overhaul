using Assets.Units;
using UnityEngine;

namespace Assets.Gui.Player
{
    public class PlayerStore : GuiStore, IPlayerStore
    {
        [SerializeField] private UnitControl _playerUnitControl;

        public UnitControl PlayerUnitControl
        {
            get { return _playerUnitControl; }
        }
    }
}
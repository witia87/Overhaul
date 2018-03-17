using Assets.Units;
using UnityEngine;

namespace Assets.Gui.Player
{
    public class PlayerStore : GuiStore, IPlayerStore
    {
        [SerializeField] private UnitControl _playerUnitControl;

        public IUnitControl PlayerUnitControl
        {
            get { return _playerUnitControl; }
        }
    }
}
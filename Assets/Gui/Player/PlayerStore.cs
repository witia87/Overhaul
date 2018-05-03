using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Gui.Player
{
    public class PlayerStore : GuiStore, IPlayerStore
    {
        [SerializeField] private Unit _playerUnit;

        public Unit PlayerUnit
        {
            get { return _playerUnit; }
        }
    }
}
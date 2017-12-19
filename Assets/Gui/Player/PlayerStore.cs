using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Gui.Player
{
    public class PlayerStore: GuiStore, IPlayerStore
    {
        [SerializeField] private HeadModule _playerHeadModule;
        public HeadModule PlayerHeadModule
        {
            get { return _playerHeadModule; }
        }
    }
}
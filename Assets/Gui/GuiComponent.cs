using Assets.Gui.Board;
using Assets.Gui.Cameras;
using Assets.Gui.Player;
using Assets.Gui.PlayerInput;
using Assets.Gui.Sight;
using Assets.Gui.View;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiComponent : MonoBehaviour
    {
        protected IBoardStore BoardStore;
        protected ICameraStore CameraStore;
        protected IKeyboardStore KeyboardStore;
        protected IMouseStore MouseStore;
        protected IPlayerStore PlayerStore;
        protected ISightStore SightStore;
        protected IViewStore ViewStore;

        protected virtual void Awake()
        {
            ViewStore = FindObjectOfType<ViewStore>();
            CameraStore = FindObjectOfType<CameraStore>();
            BoardStore = FindObjectOfType<BoardStore>();
            MouseStore = FindObjectOfType<MouseStore>();
            KeyboardStore = FindObjectOfType<KeyboardStore>();
        }
    }
}
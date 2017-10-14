using UnityEngine;

namespace Assets.Gui
{
    public class GuiCommander : GuiComponent
    {
        protected GuiDispatcher Dispatcher;

        protected virtual void Awake()
        {
            base.Awake();
            Dispatcher = FindObjectOfType<GuiDispatcher>();
        }
    }
}

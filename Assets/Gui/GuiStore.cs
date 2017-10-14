using UnityEngine;

namespace Assets.Gui
{
    public abstract class GuiStore : MonoBehaviour
    {
        protected GuiDispatcher Dispatcher;

        protected virtual void Awake()
        {
            Dispatcher = FindObjectOfType<GuiDispatcher>();
        }
    }
}
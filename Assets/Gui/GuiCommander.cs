namespace Assets.Gui
{
    public class GuiCommander : GuiComponent
    {
        protected GuiDispatcher Dispatcher;

        protected override void Awake()
        {
            base.Awake();
            Dispatcher = FindObjectOfType<GuiDispatcher>();
        }
    }
}
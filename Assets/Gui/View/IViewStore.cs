namespace Assets.Gui.View
{
    public interface IViewStore
    {
        int PixelizationSize { get; }
        float ScreenWidthInPixels { get; }
        float ScreenHeightInPixels { get; }
    }
}
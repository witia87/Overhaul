using UnityEngine;

namespace Assets.Gui
{
    public interface IGuiStore
    {
        int PixelationSize { get; }

        int OutlineSize { get; }

        int BoardPixelWidth { get; }

        int BoardPixelHeight { get; }

        Camera GuiCamera { get; }

        RenderTexture BoardTexture { get; }
        RenderTexture OutlineTexture { get; }
    }
}
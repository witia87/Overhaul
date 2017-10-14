using UnityEngine;

namespace Assets.Gui.View
{
    public class ViewStore : GuiStore, IViewStore
    {
        [SerializeField] public int _pixelizationSize = 4;

        public int PixelizationSize
        {
            get { return _pixelizationSize; }
        }

        public float ScreenWidthInPixels
        {
            get { return Screen.width / (float) PixelizationSize; }
        }

        public float ScreenHeightInPixels
        {
            get { return Screen.height / (float) PixelizationSize; }
        }
    }
}
using UnityEngine;

namespace Assets.Gui
{
    public class GuiStore: MonoBehaviour
    {
        public int PixelizationSize = 4;

        public float ScreenWidthInPixels { get { return Screen.width / (float) PixelizationSize; } }
        public float ScreenHeightInPixels { get { return Screen.height / (float)PixelizationSize; } }
    }
}
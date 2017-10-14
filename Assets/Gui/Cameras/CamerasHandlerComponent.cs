using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CamerasHandlerComponent : GuiComponent
    {
        private void Update()
        {
            this.transform.localPosition = CameraStore.PixelatedCameraPositionInBoardSpace;
        }
    }
}
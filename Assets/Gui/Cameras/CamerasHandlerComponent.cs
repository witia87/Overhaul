namespace Assets.Gui.Cameras
{
    public class CamerasHandlerComponent : GuiComponent
    {
        private void Update()
        {
            transform.localPosition = CameraStore.PixelatedCameraPositionInBoardSpace;
        }
    }
}
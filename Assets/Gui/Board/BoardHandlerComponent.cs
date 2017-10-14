using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui.Board
{
    public class BoardHandlerComponent: GuiComponent
    {
        private void Update()
        {
            // Commenting the code below will tur off the camera smoothing and restore the full low pixel display effect.
            var offset = CameraStore.CameraPositionInCameraPlaneSpace - CameraStore.PixelatedCameraPositionInBoardSpace;
            transform.localPosition = -offset;
        }

    }
}
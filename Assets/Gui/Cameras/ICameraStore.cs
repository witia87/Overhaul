using UnityEngine;

namespace Assets.Gui.Cameras
{
    public interface ICameraStore
    {
        float Rescale { get; }
        PixelatedPositionsCalculator Pixelation { get; }
        Vector3 CameraEulerAngles { get; }
        Vector2 CameraPositionInCameraPlaneSpace { get; }
        Vector2 PixelatedCameraPositionInBoardSpace { get; }
    }
}
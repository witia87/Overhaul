using UnityEngine;

namespace Assets.Gui.Cameras
{
    public interface ICameraStore
    {
        PixelatedPositionsCalculator Pixelation { get; }
        Vector3 CameraEulerAngles { get; }
        Vector2 CameraPositionInCameraPlaneSpace { get; }
        Vector2 PixelatedCameraPositionInBoardSpace { get; }
        Vector2 TransformWorldPositionToCameraPlane(Vector3 worldPosition);
    }
}
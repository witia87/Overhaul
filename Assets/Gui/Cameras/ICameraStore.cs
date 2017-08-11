using UnityEngine;

namespace Assets.Gui.Cameras
{
    public interface ICameraStore
    {
        Vector3 FocusPoint { get; }
        Vector3 CameraEulerAngles { get; }
        RaycastsHelper Raycasts { get; }
        PixelatedPositionsCalculator Pixelation { get; }
        Camera MainCamera { get; }
        float PixelsPerOneUnitInHeight { get; }
        Vector3 TransformVectorToCameraSpace(Vector3 vector);
    }
}
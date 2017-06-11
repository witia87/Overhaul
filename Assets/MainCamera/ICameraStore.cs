using UnityEngine;

namespace Assets.MainCamera
{
    public interface ICameraStore
    {
        Vector3 FocusPoint { get; }
        Vector3 CameraEulerAngles { get; }
        RaycastsHelper Raycasts { get; }
        PixelatedPositionsCalculator Pixelation { get; }
    }
}
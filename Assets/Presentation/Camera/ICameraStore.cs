using UnityEngine;

namespace Assets.Presentation.Camera
{
    public interface ICameraStore
    {
        Vector3 CameraEulerAngles { get; }
        Vector3 FocusPoint { get; }
    }
}
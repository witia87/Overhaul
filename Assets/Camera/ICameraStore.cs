using UnityEngine;

namespace Assets.Camera
{
    public interface ICameraStore
    {
        Vector3 CameraEulerAngles { get; }
        Vector3 FocusPoint { get; }
    }
}
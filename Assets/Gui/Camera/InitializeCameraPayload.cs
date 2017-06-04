using Assets.Flux;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class InitializeCameraPayload : IPayload
    {
        public Vector3 CameraEulerAngles;
    }
}
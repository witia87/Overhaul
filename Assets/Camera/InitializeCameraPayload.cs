using Assets.Flux;
using UnityEngine;

namespace Assets.Camera
{
    public class InitializeCameraPayload : IPayload
    {
        public Vector3 CameraEulerAngles;
    }
}
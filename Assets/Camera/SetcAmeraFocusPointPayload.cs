using Assets.Flux;
using UnityEngine;

namespace Assets.Camera
{
    public class SetCameraFocusPointPayload : IPayload
    {
        public Vector3 FocusPoint;
    }
}
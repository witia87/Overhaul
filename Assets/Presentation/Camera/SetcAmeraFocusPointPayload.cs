using Assets.Flux;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class SetCameraFocusPointPayload : IPayload
    {
        public Vector3 FocusPoint;
    }
}
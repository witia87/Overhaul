using Assets.Flux;
using Assets.Flux.Stores;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class CameraStore : Store, ICameraStore
    {
        public CameraStore(Dispatcher dispatcher) : base(dispatcher)
        {
            dispatcher.Register(Commands.InitializeCamera,
                payload => OnInitializeCammeraCommand(payload as InitializeCameraPayload));

            dispatcher.Register(Commands.SetCameraFocusPoint,
                payload => { FocusPoint = (payload as SetCameraFocusPointPayload).FocusPoint; });
        }

        public bool IsInitialized { get; private set; }

        public Vector3 CameraEulerAngles { get; private set; }

        public Vector3 FocusPoint { get; private set; }

        public void OnInitializeCammeraCommand(InitializeCameraPayload paylaod)
        {
            CameraEulerAngles = paylaod.CameraEulerAngles;
        }
    }
}
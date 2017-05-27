using Assets.Flux;
using Assets.Flux.Stores;
using UnityEngine;

namespace Assets.Camera
{
    public class CameraStore : Store, ICameraStore
    {
        public CameraStore(Dispatcher dispatcher) : base(dispatcher)
        {
            dispatcher.Register(Commands.InitializeCamera,
                payload => OnInitializeCammeraCommand(payload as InitializeCameraPayload));
        }

        public bool IsInitialized { get; private set; }

        public Vector3 CameraEulerAngles { get; private set; }

        public void OnInitializeCammeraCommand(InitializeCameraPayload paylaod)
        {
            CameraEulerAngles = paylaod.CameraEulerAngles;
        }
    }
}